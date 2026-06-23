IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE TABLE [Clientes] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(150) NOT NULL,
        [Identificacion] nvarchar(20) NOT NULL,
        [Correo] nvarchar(150) NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE TABLE [Coberturas] (
        [Id] int NOT NULL IDENTITY,
        [Nombre] nvarchar(100) NOT NULL,
        [MontoCobertura] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Coberturas] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE TABLE [Vehiculos] (
        [Id] int NOT NULL IDENTITY,
        [Placa] nvarchar(10) NOT NULL,
        [Marca] nvarchar(50) NOT NULL,
        [Modelo] nvarchar(50) NOT NULL,
        [Anio] int NOT NULL,
        [ValorComercial] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Vehiculos] PRIMARY KEY ([Id])
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE TABLE [Polizas] (
        [Id] int NOT NULL IDENTITY,
        [NumeroPoliza] nvarchar(30) NOT NULL,
        [ClienteId] int NOT NULL,
        [VehiculoId] int NOT NULL,
        [FechaEmision] datetime2 NOT NULL,
        [SumaAsegurada] decimal(18,2) NOT NULL,
        [PrimaTotal] decimal(18,2) NOT NULL,
        CONSTRAINT [PK_Polizas] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Polizas_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Polizas_Vehiculos_VehiculoId] FOREIGN KEY ([VehiculoId]) REFERENCES [Vehiculos] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE TABLE [PolizaCoberturas] (
        [PolizaId] int NOT NULL,
        [CoberturaId] int NOT NULL,
        CONSTRAINT [PK_PolizaCoberturas] PRIMARY KEY ([PolizaId], [CoberturaId]),
        CONSTRAINT [FK_PolizaCoberturas_Coberturas_CoberturaId] FOREIGN KEY ([CoberturaId]) REFERENCES [Coberturas] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_PolizaCoberturas_Polizas_PolizaId] FOREIGN KEY ([PolizaId]) REFERENCES [Polizas] ([Id]) ON DELETE CASCADE
    );
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Correo', N'Identificacion', N'Nombre') AND [object_id] = OBJECT_ID(N'[Clientes]'))
        SET IDENTITY_INSERT [Clientes] ON;
    EXEC(N'INSERT INTO [Clientes] ([Id], [Correo], [Identificacion], [Nombre])
    VALUES (1, N''juan.perez@correo.com'', N''001-200390-1001X'', N''Juan Perez''),
    (2, N''maria.lopez@correo.com'', N''001-150585-1002Y'', N''Maria Lopez''),
    (3, N''carlos.ramirez@correo.com'', N''001-100777-1003Z'', N''Carlos Ramirez'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Correo', N'Identificacion', N'Nombre') AND [object_id] = OBJECT_ID(N'[Clientes]'))
        SET IDENTITY_INSERT [Clientes] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MontoCobertura', N'Nombre') AND [object_id] = OBJECT_ID(N'[Coberturas]'))
        SET IDENTITY_INSERT [Coberturas] ON;
    EXEC(N'INSERT INTO [Coberturas] ([Id], [MontoCobertura], [Nombre])
    VALUES (1, 15000.0, N''Robo Total''),
    (2, 8000.0, N''Choque y Colision''),
    (3, 5000.0, N''Responsabilidad Civil''),
    (4, 6000.0, N''Danos a Terceros''),
    (5, 1000.0, N''Asistencia en Carretera'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'MontoCobertura', N'Nombre') AND [object_id] = OBJECT_ID(N'[Coberturas]'))
        SET IDENTITY_INSERT [Coberturas] OFF;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_PolizaCoberturas_CoberturaId] ON [PolizaCoberturas] ([CoberturaId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Polizas_ClienteId] ON [Polizas] ([ClienteId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE UNIQUE INDEX [IX_Polizas_NumeroPoliza] ON [Polizas] ([NumeroPoliza]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    CREATE INDEX [IX_Polizas_VehiculoId] ON [Polizas] ([VehiculoId]);
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20260623065725_InitialCreate'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20260623065725_InitialCreate', N'9.0.17');
END;

COMMIT;
GO

