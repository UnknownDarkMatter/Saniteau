DECLARE @var nvarchar(128)
SELECT @var = MigrationId
FROM [dbo].[__EFMigrationsHistory]
WHERE MigrationId = N'20210914112627_Add-Payment-Table'

IF @var IS NULL
BEGIN
    CREATE TABLE [Payments] (
        [IdPayment] int NOT NULL IDENTITY,
        [IdFacturation] int NOT NULL,
        [PaypalOrderId] nvarchar(max) NULL,
        [PaymentStatut] int NOT NULL,
        [PaymentDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Payments] PRIMARY KEY ([IdPayment]),
        CONSTRAINT [FK_Payments_Facturations_IdFacturation] FOREIGN KEY ([IdFacturation]) REFERENCES [Facturations] ([IdFacturation]) ON DELETE CASCADE
    );
END
GO

DECLARE @var nvarchar(128)
SELECT @var = MigrationId
FROM [dbo].[__EFMigrationsHistory]
WHERE MigrationId = N'20210914112627_Add-Payment-Table'
    IF @var IS NULL
    BEGIN
    CREATE INDEX [IX_Payments_IdFacturation] ON [Payments] ([IdFacturation]);


    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210914112627_Add-Payment-Table', N'5.0.2');
END

