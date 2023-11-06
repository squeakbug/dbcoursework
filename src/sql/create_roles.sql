use pharmacy;
go

--select * from pharmacy.sys.server_principals;

--select * from pharmacy.sys.database_role_members;

create or alter procedure drop_all_roles
as
begin
	declare @sql nvarchar(max);

	select @sql = 'ALTER ROLE ' +  QUOTENAME(rp.name)  + ' DROP MEMBER ' + QUOTENAME(mp.name)
	from sys.database_role_members drm
	  join sys.database_principals rp on (drm.role_principal_id = rp.principal_id)
	  join sys.database_principals mp on (drm.member_principal_id = mp.principal_id)
	WHERE rp.name = 'product_manager'
		or rp.name = 'pharmacy_manager'
		or rp.name = 'pharmacist'
		or rp.name = 'stockman'
	order by rp.name

	while (count(@sql) != 0)
	begin
		exec (@sql)
		select @sql = 'ALTER ROLE ' +  QUOTENAME(rp.name)  + ' DROP MEMBER ' + QUOTENAME(mp.name)
		from sys.database_role_members drm
		  join sys.database_principals rp on (drm.role_principal_id = rp.principal_id)
		  join sys.database_principals mp on (drm.member_principal_id = mp.principal_id)
		WHERE rp.name = 'product_manager'
			or rp.name = 'pharmacy_manager'
			or rp.name = 'pharmacist'
			or rp.name = 'stockman'
		order by rp.name
	end;
end;
go

exec drop_all_roles;

drop role if exists product_manager;
drop role if exists pharmacist;
drop role if exists pharmacy_manager;
drop role if exists stockman;
go

create role product_manager;
go

create role pharmacist;
go

create role pharmacy_manager;
go

create role stockman;
go

grant select, update on invoice to stockman;
grant select on vendor to stockman;
grant select on manufacturer to stockman;
grant select on person_metadata to stockman;
grant select on employee to stockman;
grant select on category to stockman;
grant select on invoice to stockman;
grant select on has_category to stockman;
grant select on product to stockman; 
grant select on marked_product to stockman;
grant select, update on mark_info to stockman;
grant select on write_off_product to stockman;
grant select, update on income_product to stockman;
grant execute to stockman;

grant select to pharmacy_manager;
grant select, update, delete, insert on person_metadata to pharmacy_manager;
grant select, update, delete, insert on employee to pharmacy_manager;
grant select, update, delete, insert on invoice to pharmacy_manager;
grant select, update, delete, insert on write_off_product to pharmacy_manager;
grant select, update, delete, insert on income_product to pharmacy_manager;
grant select, update, delete, insert on product to pharmacy_manager;
grant select, update, delete, insert on marked_product to pharmacy_manager;
grant select, update, delete, insert on mark_info to pharmacy_manager;
grant select, update, delete, insert on category to pharmacy_manager;
grant select, update, delete, insert on has_category to pharmacy_manager;
grant select, update, delete, insert on product_request to pharmacy_manager;
grant select, update, delete, insert on vendor to pharmacy_manager;
grant select, update, delete, insert on manufacturer to pharmacy_manager;
grant execute to pharmacy_manager;

grant select on invoice to pharmacist;
grant select on vendor to pharmacist;
grant select on manufacturer to pharmacist;
grant select, insert on person_metadata to pharmacist;
grant select, insert on product_request to pharmacist;
grant select on employee to pharmacist;
grant select on category to pharmacist;
grant select on has_category to pharmacist;
grant select on product to pharmacist; 
grant select, update, delete, insert on income_product to pharmacist;
grant select, update, delete, insert on marked_product to pharmacist;
grant select, update, delete, insert on mark_info to pharmacist;
grant select, update, delete, insert on receipt_marked_product to pharmacist;
grant select, update, delete, insert on receipt to pharmacist;
grant execute to pharmacist;

grant select to product_manager;
grant select, update, insert, delete on vendor to product_manager;
grant select, update on product to product_manager; 
grant select, update on mark_info to product_manager;
grant execute to product_manager;
go

if object_id(N'dbo.RegistrateUser', N'P') is not null
    drop procedure dbo.RegistrateUser;
go

create procedure dbo.RegistrateUser (@login nvarchar(max), @password nvarchar(max), @role nvarchar(max))
as
begin
	if @login <> 'stockman' 
		and @login <> 'pharmacy_manager'
		and @login <> 'product_manager'
		and @login <> 'pharmacist'
	begin
		declare @sql nvarchar(max)
		set @sql = 
		'if not exists (
			select 1 
			from sys.server_principals 
			where name = ''' +@login+ ''')
		begin
			create login '+@login+' 
				with password=''' +@password + ''',
				CHECK_POLICY  = OFF
		create user '+@login+' 
		  for login '+@login+';
		alter role '+@role+' add member '+@login+';
		end;'
		exec (@sql)
	end;	
end;
go
