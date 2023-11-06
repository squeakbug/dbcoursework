use pharmacy;
go

BULK INSERT dbo.product
    FROM '/var/opt/mssql/data/product.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.category
    FROM '/var/opt/mssql/data/category.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.has_category
    FROM '/var/opt/mssql/data/has_category.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.manufacturer
    FROM '/var/opt/mssql/data/manufacturer.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.employee
    FROM '/var/opt/mssql/data/employee.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.person_metadata
    FROM '/var/opt/mssql/data/person_metadata.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.vendor
    FROM '/var/opt/mssql/data/vendor.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.invoice
    FROM '/var/opt/mssql/data/invoice.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.income_product
    FROM '/var/opt/mssql/data/income_product.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.mark_info
    FROM '/var/opt/mssql/data/mark_info.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.marked_product
    FROM '/var/opt/mssql/data/marked_product.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.receipt
    FROM '/var/opt/mssql/data/receipt.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.receipt_marked_product
    FROM '/var/opt/mssql/data/receipt_marked_product.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.product_request
    FROM '/var/opt/mssql/data/product_request.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO

BULK INSERT dbo.write_off_product
    FROM '/var/opt/mssql/data/write_off_product.csv'
    WITH
    (
        FIELDTERMINATOR = '|',
        ROWTERMINATOR = '\n'
    );
GO
