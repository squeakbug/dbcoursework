use pharmacy;
go

-- ======================== Product ========================

if object_id(N'dbo.getProductByName', N'IF') is not null
    drop function dbo.getProductByName;
go

if object_id(N'dbo.getProductByGTIN', N'IF') is not null
    drop function dbo.getProductByGTIN;
go

if object_id(N'dbo.getIncomeProductDefaultMarkup', N'FN') is not null
    drop function dbo.getIncomeProductDefaultMarkup;
go

if object_id(N'dbo.getMaxMarkInfoId', N'FN') is not null
    drop function dbo.getMaxMarkInfoId;
go

create function dbo.getProductByName(@namePart nvarchar(max))
returns table
as
return
    select *
    from product
    where p_name like ('%' + @namePart + '%')
        or international_name like ('%' + @namePart + '%')
go

create function dbo.getMaxMarkInfoId()
returns int
as
begin
    declare @result int

    select @result = MAX(id)
    from mark_info

    return @result
end;
go

create function dbo.getProductByGTIN(@gtin int)
returns table
as
return
    select *
    from product
    where gtin = @gtin
go

create function dbo.getIncomeProductDefaultMarkup(@IncomeProductId int)
returns float
as
begin
    declare @result decimal

    select @result = default_markup
    from product p join income_product i on p.id = i.product_id
    where i.id = @IncomeProductId

    return @result
end;
go

if object_id(N'dbo.SoldProducts', N'V') is not null
    drop view dbo.SoldProducts;
go

if object_id(N'dbo.SellingProducts', N'V') is not null
    drop view dbo.SellingProducts;
go

if object_id(N'dbo.OverdueProducts', N'V') is not null
    drop view dbo.OverdueProducts;
go

-- TODO overdue products imp
create view OverdueProducts as
    select *
    from marked_product
    where sold = 0
go

create view SoldProducts as
    select *
    from marked_product
    where sold = 1
go

create view SellingProducts as
    select *
    from marked_product
    where sold = 0
go

-- ======================== Product moving ========================

if object_id(N'dbo.approveProductsByInvoice', N'P') is not null
    drop procedure dbo.approveProductsByInvoice;
go

-- TODO ApproveInvoice
create procedure dbo.approveProductsByInvoice(@invoiceId int, @employeeId int, @storageLocation nvarchar(max) = null)
as
begin
    begin try
    begin transaction
        declare income_product_cursor cursor for  
            select id, p_count
            from income_product
            where invoice_id = @invoiceId

        declare @incomeProductId int, @cnt int

        open income_product_cursor  
        fetch next from income_product_cursor
            into @incomeProductId, @cnt

		update invoice
		set i_state = 'approved'
		where id = @invoiceId
        
		declare @markup decimal;

        while @@FETCH_STATUS = 0  
        begin 
		    select @markup = dbo.getIncomeProductDefaultMarkup(@incomeProductId)

            insert into mark_info
            (approved_employee_id, income_product_id, approved_date, markup, p_count, storage_location)
            values 
            (@employeeId, @incomeProductId, CURRENT_TIMESTAMP, @markup, @cnt, @storageLocation)

            insert into marked_product
            (mark_info_id, sold)
            values
            (dbo.getMaxMarkInfoId(), 0)

            fetch next from income_product_cursor
                into @incomeProductId, @cnt
        end

        close income_product_cursor
        deallocate income_product_cursor
		commit
    end try
    begin catch
        rollback transaction;
    end catch
end;
go

if object_id(N'dbo.acceptInvoice', N'P') is not null
    drop procedure dbo.acceptInvoice;
go

create procedure dbo.acceptInvoice(@invoiceId int, @employeeId int, @shippingDate date = null)
as
begin
    update invoice
    set i_state = 'accepted',
        accepted_employee_id = @employeeId,
        receiving_date = CURRENT_TIMESTAMP,
        shipping_date = @shippingDate
    where id = @invoiceId
end;
go

if object_id(N'dbo.sellProduct', N'P') is not null
    drop procedure dbo.sellProduct;
go

-- TODO check correctness after inserting (need to create special type)
create procedure dbo.sellProduct(
    @markedProductId int, 
    @cnt int,
    @receiptId int)
as
begin
    begin transaction
        declare @remain int

        select @remain = p_count
        from SellingProducts mp join mark_info mi on mp.mark_info_id = mi.id
        where @markedProductId = mp.id

        if @remain < @cnt
            throw 51000, 'Remain less then sold product', 1
        set @remain = @remain - @cnt

        update mark_info
        set p_count = @remain
        where id = (select mark_info_id
                    from SellingProducts
                    where SellingProducts.id = @markedProductId)
        
        insert into mark_info (approved_employee_id, income_product_id, approved_date, markup, p_count, storage_location)
        select approved_employee_id, income_product_id, approved_date, markup, @cnt, storage_location
        from mark_info
        where id = (select mark_info_id
                    from SellingProducts
                    where SellingProducts.id = @markedProductId)

        insert into marked_product (mark_info_id, sold)
        select dbo.getMaxMarkInfoId(), 1
        from marked_product
        where id = @markedProductId

        insert into receipt_marked_product
            (marked_product_id, receipt_id)
        values
            (@markedProductId, @receiptId)
    commit
end;
go

if object_id(N'dbo.writeOffProduct', N'P') is not null
    drop procedure dbo.writeOffProduct;
go

-- TODO testing
create procedure dbo.writeOffProduct(
    @markedProductId int, 
    @cnt int,
    @employeeId int,
    @reason nvarchar(max))
as
begin
    begin transaction
        declare @remain int

        select @remain = p_count
        from marked_product mp join mark_info mi on mp.mark_info_id = mi.id
        where @markedProductId = mp.id

        if @remain < @cnt
            throw 51000, 'Remain less then sold product', 1
        set @remain = @remain - @cnt

        update mark_info
        set p_count = @remain
        where id = (select mark_info_id
                    from SellingProducts
                    where SellingProducts.id = @markedProductId)
        
        insert into mark_info (approved_employee_id, income_product_id, approved_date, markup, p_count, storage_location)
        select approved_employee_id, income_product_id, approved_date, markup, @cnt, storage_location
        from mark_info
        where id = (select mark_info_id
                    from SellingProducts
                    where SellingProducts.id = @markedProductId)

        insert into write_off_product (mark_info_id, write_off_employee_id, write_off_date, reason)
        select dbo.getMaxMarkInfoId(), @employeeId, CURRENT_TIMESTAMP, @reason
        from marked_product
        where id = @markedProductId
    commit
end;
go

if object_id(N'dbo.approveProductRequest', N'P') is not null
    drop procedure dbo.approveProductRequest;
go

create procedure dbo.approveProductRequest(
    @productRequestId int, 
    @employeeId int)
as
begin
    update product_request
    set approved_employee_id = @employeeId,
        approved = 1,
        approved_date = CURRENT_TIMESTAMP
    where id = @productRequestId
end;
go
