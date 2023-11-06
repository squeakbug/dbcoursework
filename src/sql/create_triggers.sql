use pharmacy;
go

create or alter trigger dbo.empl_delete
on employee
instead of delete
as begin
	set nocount on;

	update write_off_product
	set write_off_employee_id = NULL
	where write_off_employee_id in (select id from deleted)

	update product_request
	set request_employee_id = NULL
	where request_employee_id in (select id from deleted)

	update product_request
	set approved_employee_id = NULL
	where approved_employee_id in (select id from deleted)

	update invoice
	set created_employee_id = NULL
	where created_employee_id in (select id from deleted)

	update invoice
	set accepted_employee_id = NULL
	where accepted_employee_id in (select id from deleted)

	update invoice
	set approved_employee_id = NULL
	where approved_employee_id in (select id from deleted)

	delete employee
	where id in (select id from deleted)
end;
go

create or alter trigger dbo.category_delete
on category
instead of delete
as begin
	set nocount on;

	update category
	set parent_category_id = NULL
	where parent_category_id in (select id from deleted)

	delete category
	where id in (select id from deleted)
end;
go
