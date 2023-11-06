drop table if exists write_off_product;
go

drop table if exists has_category;
go

drop table if exists category;
go

drop table if exists receipt_marked_product;
go

drop table if exists marked_product;
go

drop table if exists mark_info;
go

drop table if exists receipt;
go

drop table if exists product_request;
go

drop table if exists income_product;
go

drop table if exists invoice;
go

drop table if exists vendor;
go

drop table if exists employee;
go

drop table if exists person_metadata;
go

drop table if exists product;
go

drop table if exists manufacturer;
go

use master;
go

--drop database if exists pharmacy;
--go

create database pharmacy;
go

use pharmacy;
go

create table product
(
    id int identity(1, 1) primary key,
    manufacturer_id int not null,
    p_name nvarchar(max) not null,
    international_name nvarchar(max) not null,
    gtin varchar(64) not null,
    articul varchar(64) not null,
    primacy_packaging nvarchar(max) null,
    threashold_count int null,
    trademark nvarchar(max) null,
    dosage float null,                      -- in g.
    storage_temperature float not null,     -- in celsius
    maximum_shelf_life int not null,        -- in days
    leave_condition nvarchar(max) not null,
    p_description text,
    instruction text,
    photo varbinary(max),
    count_in_package int,
    dosage_form nvarchar(max),
    maximum_markup decimal,
    default_markup decimal not null,
);
go

create table category
(
    id int identity(1, 1) primary key,
    title nvarchar(max) not null,
    parent_category_id int,
    c_description text
);
go

create table has_category
(
    category_id int not null,
    product_id int not null
);
go

create table manufacturer
(
    id int identity(1, 1) primary key,
    m_name nvarchar(max) not null,
    country nvarchar(max) not null,
    concern nvarchar(max)
);
go

create table employee
(
    id int identity(1, 1) primary key,
    metadata_id int not null,
    e_login varchar(max) not null,
    p_hash varchar(max) not null,
    appointment nvarchar(max) not null
);
go

create table person_metadata
(
    id int identity(1, 1) primary key,
    first_name nvarchar(max) not null,
    middle_name nvarchar(max) not null,
    email varchar(max),
    phone varchar(max),
    last_name nvarchar(max)
);
go

create table vendor
(
    id int identity(1, 1) primary key,
    short_name nvarchar(max) not null,
    full_name nvarchar(max) not null,
    phone varchar(max),
    inn int,
    kpp int
);
go

create table invoice
(
    id int identity(1, 1) primary key,
    vendor_id int,
    created_employee_id int,
    invoice_date date not null,
    invoice_number int not null,
    accepted_employee_id int,
    approved_employee_id int,
    receiving_date date,
    shipping_date date,
    document_gtin varchar(max),
    i_state nvarchar(max) not null
);
go

create table income_product
(
    id int identity(1, 1) primary key,
    invoice_id int,
    product_id int not null,
    p_count int not null,
    measure_unit nvarchar(max) not null,            -- move to product table
    series int not null,
    vendor_price money not null,
    production_date date not null,
    vendor_vax int not null,
    storage_location nvarchar(max)
);
go

create table mark_info
(
    id int identity(1, 1) primary key,
    approved_employee_id int,
    income_product_id int not null,
    approved_date date not null,
    markup decimal not null,           -- need to be less then max_markup (create trigger)
    p_count int not null,
    storage_location nvarchar(max)
);
go

create table marked_product
(
    id int identity(1, 1) primary key,
    mark_info_id int not null,
    sold int not null                   -- sold/stored = 1/0
);
go

create table receipt
(
    id int identity(1, 1) primary key,
    employee_id int,
    cash_register_number int not null,
    leave_date date not null,
    receipt_number int not null,
    payment_type nvarchar(max) not null,
    discount decimal(3, 2) not null,        -- Возможно имеет смысл создать отдельную таблицу для скидок
    cash_size money not null,
    card_size money not null,
    work_shift int not null
);
go

create table receipt_marked_product
(
    marked_product_id int not null,
    receipt_id int not null
);
go

create table product_request
(
    id int identity(1, 1) primary key,
    request_employee_id int,
    customer_metadata_id int not null,
    request_date date not null,
    product_id int,
    approved_employee_id int,
    pr_count int not null,
    approved int not null,
    approximate_name nvarchar(max),
    approved_date date
);
go

create table write_off_product
(
    id int identity(1, 1) primary key,
    mark_info_id int not null,
    write_off_employee_id int,
    write_off_date date not null,
    reason nvarchar(max),
);
go

drop index if exists product_category_index on has_category;
go

drop index if exists invoice_index on invoice;
go

drop index if exists income_product_index on income_product;
go

drop index if exists receipt_index on receipt;
go

drop index if exists product_request_index on product_request;
go

create index product_category_index
on has_category(category_id, product_id);
go

create index invoice_index
on invoice(invoice_date);
go

create index income_product_index
on income_product(production_date);
go

create index receipt_index
on receipt(leave_date);
go

create index product_request_index
on product_request(customer_metadata_id);
go
