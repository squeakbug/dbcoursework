import csv
import random
import pathlib
import random
import datetime

from faker import Faker
faker = Faker(['it_IT', 'en_US'])

DELIMITER = '|'

def create_product(counts):
    filename = "./product.csv"

    recordsCnt = counts['product']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                              # id
                faker.random_int(1, counts['manufacturer']),    # manufaturer_id
                faker.company(),                                # p_name
                faker.company(),                                # international_name
                faker.random_int(1, 9999),                      # gtin
                faker.random_int(1, 9999),                      # articul
                faker.company(),                                # primacy_packaging
                faker.random_int(1, 9999),                      # threashold_count
                faker.company(),                                # trademark
                random.random(),                                # dosage
                faker.random_int(10, 25),                       # storage_temperature
                faker.random_int(10, 9999),                     # maximum_shelf_life
                faker.company(),                                # leave_condition
                faker.company(),                                # p_description
                faker.company(),                                # instruction
                None,                                           # photo varbinary
                faker.random_int(1, 9999),                      # count_in_package
                faker.company(),                                # dosage_form nvarchar
                20,                                             # maximum_markup
                18,                                             # default_markup
            ])

def create_category(counts):
    filename = "./category.csv"

    recordsCnt = counts['category']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.company(),                            # title
                faker.random_int(1, i),                     # parent_category_id
                faker.company(),                            # c_description
            ])

def create_has_category(counts):
    filename = "./has_category.csv"

    recordsCnt = counts['has_category']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                faker.random_int(1, counts['category']),    # category_id
                faker.random_int(1, counts['product']),     # product_id
            ])

def create_manufacturer(counts):
    filename = "./manufacturer.csv"

    recordsCnt = counts['manufacturer']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.company(),                            # m_name
                faker.company(),                            # country
                faker.company(),                            # concern
            ])

def create_employee(counts):
    filename = "./employee.csv"

    recordsCnt = counts['employee']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                              # id
                faker.random_int(1, counts['person_metadata']), # metadata_id
                faker.company(),                                # e_login
                faker.random_int(1, 1024),                      # p_hash
                faker.company(),                                # appointment
            ])

def create_person_metadata(counts):
    filename = "./person_metadata.csv"

    recordsCnt = counts['person_metadata']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.company(),                            # first_name
                faker.company(),                            # middle_name
                faker.company(),                            # email
                faker.random_int(1, 100),                   # phone
                faker.company(),                            # last_name
            ])

def create_vendor(counts):
    filename = "./vendor.csv"

    recordsCnt = counts['vendor']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.company(),                            # short_name
                faker.company(),                            # full_name
                faker.random_int(1, 100),                   # phone
                faker.random_int(1, 100),                   # inn
                faker.random_int(1, 100),                   # kpp
            ])

def create_invoice(counts):
    filename = "./invoice.csv"

    recordsCnt = counts['invoice']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.random_int(1, counts['vendor']),      # vendor_id
                faker.random_int(1, counts['employee']),    # created_employee_id
                faker.date_between(
                    datetime.date(2018, 12, 14),
                    datetime.date(2019, 12, 14)),           # invoice_date
                faker.random_int(1, 100),                   # invoice_number
                faker.random_int(1, counts['employee']),    # accepted_employee_id
                faker.random_int(1, counts['employee']),    # approved_employee_id
                faker.date_between(
                    datetime.date(2020, 12, 14),
                    datetime.date(2021, 12, 14)),           # receiving_date
                faker.date_between(
                    datetime.date(2019, 12, 14),
                    datetime.date(2020, 12, 14)),           # shipping_date
                faker.random_int(1, 100),                   # document_gtin
                "approved",                                 # i_state
            ])

def create_income_product(counts):
    filename = "./income_product.csv"

    recordsCnt = counts['income_product']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                      # id
                faker.random_int(1, counts['invoice']), # invoice_id
                faker.random_int(1, counts['product']), # product_id
                faker.random_int(1, 100),               # p_count
                faker.company(),                        # measure_unit
                faker.random_int(1, 100),               # series
                faker.random_int(1, 100),               # vendor_price
                faker.date_between(
                    datetime.date(2017, 12, 14),
                    datetime.date(2018, 12, 14)),       # production_date
                faker.random_int(1, 100),               # vendor_vax
                faker.company(),                        # storage_location
            ])

def create_mark_info(counts):
    filename = "./mark_info.csv"

    recordsCnt = counts['mark_info']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                              # id
                faker.random_int(1, counts['employee']),        # approved_employee_id
                faker.random_int(1, counts['income_product']),  # incoming_product_data_id
                faker.date_between(
                    datetime.date(2019, 12, 14),
                    datetime.date(2020, 12, 14)),               # approved_date
                random.randint(1, 16),                          # markup
                faker.random_int(1, 100),                       # p_count
                faker.company(),                                # storage_location
            ])

def create_marked_product(counts):
    filename = "./marked_product.csv"

    recordsCnt = counts['marked_product']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.random_int(1, counts['mark_info']),   # mark_info_id
                0                                           #  sold
            ])

def create_receipt(counts):
    filename = "./receipt.csv"

    recordsCnt = counts['receipt']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                          # id
                faker.random_int(1, counts['employee']),    # employee_id
                faker.random_int(1, 100),                   # cash_register_number
                faker.date_between(
                    datetime.date(2021, 12, 14),
                    datetime.date(2022, 12, 14)),           # leave_date
                faker.random_int(1, 100),                   # receipt_number
                faker.company(),                            # payment_type
                0,                                          # discount
                faker.random_int(1, 100),                   # cash_size
                faker.random_int(1, 100),                   # card_size
                faker.random_int(1, 100),                   # work_shift
            ])

def create_receipt_marked_product(counts):
    filename = "./receipt_marked_product.csv"

    recordsCnt = counts['receipt_marked_product']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                faker.random_int(1, counts['marked_product']),  # marked_product_id
                faker.random_int(1, counts['receipt']),         # receipt_id
            ])

def create_product_request(counts):
    filename = "./product_request.csv"

    recordsCnt = counts['product_request']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                              # id
                faker.random_int(1, counts['employee']),        # request_employee_id
                faker.random_int(1, counts['person_metadata']), # customer_metadata_id
                faker.date_between(
                    datetime.date(2020, 12, 14),
                    datetime.date(2021, 12, 14)),               # request_date
                faker.random_int(1, counts['product']),         # product_id
                faker.random_int(1, counts['employee']),        # approved_employee_id
                faker.random_int(1, 100),                       # pr_count
                faker.random_int(0, 1),                         # approved
                faker.company(),                                # approximate_name
                faker.date_between(
                    datetime.date(2021, 12, 14),
                    datetime.date(2022, 12, 14)),               # approved_date
            ])

def create_write_off_product(counts):
    filename = "./write_off_product.csv"

    recordsCnt = counts['write_off_product']

    with open(filename, 'w', newline='', encoding='utf-8') as csvfile:
        writer = csv.writer(csvfile, delimiter=DELIMITER)

        for i in range(1, recordsCnt + 1):
            writer.writerow([
                i,                                              # id
                faker.random_int(1, counts['income_product']),  # incoming_product_data_id
                faker.random_int(1, counts['employee']),        # write_off_employee_id
                faker.date_between(
                    datetime.date(2021, 12, 14),
                    datetime.date(2022, 12, 14)),               # write_off_date
                faker.company(),                                # reason
            ])

def main():

    counts = {
        'product' : 2048,
        'category' : 2048,
        'has_category' : 8192,
        'manufacturer' : 256,
        'employee' : 16,
        'person_metadata' : 32,
        'vendor' : 256,
        'invoice' : 256,
        'income_product' : 2048,
        'mark_info' : 2048,
        'marked_product' : 2048,
        'receipt' : 1024,
        'receipt_marked_product' : 4096,
        'product_request' : 128,
        'write_off_product' : 128
    }

    for key in counts.keys():
        counts[key] *= 2

    create_product(counts)
    create_category(counts)
    create_has_category(counts)
    create_manufacturer(counts)
    create_employee(counts)
    create_person_metadata(counts)
    create_vendor(counts)
    create_invoice(counts)
    create_income_product(counts)
    create_mark_info(counts)
    create_marked_product(counts)
    create_receipt(counts)
    create_receipt_marked_product(counts)
    create_product_request(counts)
    create_write_off_product(counts)

main()
