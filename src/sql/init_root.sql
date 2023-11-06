use pharmacy;
go

select * from sys.database_principals
select * from employee

insert into person_metadata
(first_name, middle_name, email, phone, last_name)
values
('', '', '', 0, '');

insert into employee
(metadata_id, e_login, p_hash, appointment)
values
(@@IDENTITY, 'SA', 'P@ssword', 'root');
