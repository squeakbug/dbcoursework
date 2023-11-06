use pharmacy;
go

alter table product 
add constraint FK_manufacturer foreign key (manufacturer_id) references manufacturer(id) on delete cascade on update cascade,
    constraint gtin_c check (gtin >= 0),
    constraint articul_c check (articul >= 0),
    constraint threashold_count_c check (threashold_count >= 0),
    constraint dosage_c check (dosage > 0),
    constraint shelf_life_c check (maximum_shelf_life > 0),
    constraint count_c check (count_in_package > 0),
    constraint maximum_markup_c check (maximum_markup >= 0),
    constraint default_markup_c check (default_markup >= 0 and default_markup <= maximum_markup)
go

alter table category 
add constraint FK_parent_category foreign key (parent_category_id) references category(id) on delete no action on update no action
go

alter table has_category
add constraint FK_product foreign key (product_id) references product(id) on delete cascade on update cascade,
    constraint FK_category foreign key (category_id) references category(id) on delete no action on update cascade
go

alter table employee
add constraint FK_metadata foreign key (metadata_id) references person_metadata(id) on delete no action on update no action
go

alter table invoice
add constraint FK_vendor foreign key (vendor_id) references vendor(id) on delete set null on update cascade,
    constraint FK_accepted_employee foreign key (accepted_employee_id) references employee(id) on delete no action on update no action,
	constraint FK_approved_employee foreign key (approved_employee_id) references employee(id) on delete no action on update no action,
	constraint FK_created_employee foreign key (created_employee_id) references employee(id) on delete no action on update no action,
    constraint invoice_date_c check (YEAR(invoice_date) > 2000 and YEAR(invoice_date) < 2100),
    constraint receiving_date_c check (YEAR(receiving_date) > 2000 and YEAR(receiving_date) < 2100),
    constraint shipping_date_c check (YEAR(shipping_date) > 2000 and YEAR(shipping_date) < 2100),
    constraint document_gtin_c check (document_gtin > 0)
go

alter table income_product
add constraint FK_invoice foreign key (invoice_id) references invoice(id) on delete set null on update cascade,
    constraint FK_income_product foreign key (product_id) references product(id) on delete no action on update cascade,
    constraint p_count_c check (p_count > 0),
    constraint measure_unit_c check (measure_unit IN (N'g', N'u', N'ml', N'mm3')),
    constraint series_c check (series > 0),
    constraint vendor_price_c check (vendor_price > 0),
    constraint production_date check (YEAR(production_date) > 2000 and YEAR(production_date) < 2100),
    constraint vendor_vax check (vendor_vax > 0)
go

alter table mark_info
add constraint FK_mark_info_employee foreign key (approved_employee_id) references employee(id) on delete set null on update cascade,
    constraint FK_mi_income_product foreign key (income_product_id) references income_product(id) on delete no action on update no action, --
    constraint approved_date_c check (YEAR(approved_date) > 2000 and YEAR(approved_date) < 2100),
    constraint markup_c check (markup > 0)
go

alter table marked_product
add constraint FK_mark_info foreign key (mark_info_id) references mark_info(id) on delete no action on update cascade
go

alter table receipt
add constraint FK_receipt_created_employee foreign key (employee_id) references employee(id) on delete set null on update cascade,
	constraint leave_date_c check (YEAR(leave_date) > 2000 and YEAR(leave_date) < 2100),
	constraint discount_c check (discount >= 0),
	constraint cash_size_c check (cash_size >= 0),
	constraint card_size_c check (card_size >= 0)
go

alter table receipt_marked_product
add constraint FK_receipt_marked_product_product foreign key (marked_product_id) references marked_product(id) on delete cascade on update cascade,
    constraint FK_receipt_marked_product_receipt foreign key (receipt_id) references receipt(id) on delete no action on update no action
go

alter table product_request
add constraint FK_product_request_product foreign key (product_id) references product(id) on delete no action on update cascade,
    constraint FK_product_request_request_employee foreign key (request_employee_id) references employee(id) on delete no action on update cascade,
    constraint FK_product_request_approved_employee foreign key (approved_employee_id) references employee(id) on delete no action on update no action,
    constraint FK_product_request_customer_metadata foreign key (customer_metadata_id) references person_metadata(id) on delete no action on update cascade,
    constraint pr_count_c check (pr_count > 0),
    constraint request_date_c check (YEAR(request_date) > 2000 and YEAR(request_date) < 2100),
    constraint product_request_approved_date_c check (YEAR(approved_date) > 2000 and YEAR(approved_date) < 2100)
go

alter table write_off_product
add constraint FK_write_off_product_product_data foreign key (mark_info_id) references mark_info(id) on delete no action on update cascade,
    constraint FK_write_off_product_employee foreign key (write_off_employee_id) references employee(id) on delete no action on update no action,
    constraint write_off_date_c check (YEAR(write_off_date) > 2000 and YEAR(write_off_date) < 2100)
go
