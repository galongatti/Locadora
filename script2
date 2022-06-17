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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE TABLE [Cliente] (
        [Id] int NOT NULL IDENTITY,
        [Nome] VARCHAR(255) NULL,
        [Documento] VARCHAR(20) NULL,
        [Ativo] BIT NOT NULL,
        CONSTRAINT [PK_Cliente] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE TABLE [Filme] (
        [Id] int NOT NULL IDENTITY,
        [Nome] VARCHAR(255) NULL,
        [Ativo] BIT NOT NULL,
        [Disponivel] BIT NOT NULL,
        CONSTRAINT [PK_Filme] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE TABLE [Locacao] (
        [Id] int NOT NULL IDENTITY,
        [IDCliente] int NOT NULL,
        [Situacao] VARCHAR(20) NOT NULL,
        [DataAlocacao] DATETIME NOT NULL,
        [DataParaDevolucao] DATETIME NOT NULL,
        [DiasAlocacao] INT NOT NULL,
        [ObservacaoSituacao] nvarchar(max) NULL,
        CONSTRAINT [PK_Locacao] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Locacao_Cliente_IDCliente] FOREIGN KEY ([IDCliente]) REFERENCES [Cliente] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE TABLE [LocacaoItem] (
        [Id] int NOT NULL IDENTITY,
        [IDFilme] int NOT NULL,
        [IDLocacao] int NOT NULL,
        [Ativo] bit NOT NULL,
        CONSTRAINT [PK_LocacaoItem] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_LocacaoItem_Filme_IDFilme] FOREIGN KEY ([IDFilme]) REFERENCES [Filme] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_LocacaoItem_Locacao_IDLocacao] FOREIGN KEY ([IDLocacao]) REFERENCES [Locacao] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE INDEX [IX_Locacao_IDCliente] ON [Locacao] ([IDCliente]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE INDEX [IX_LocacaoItem_IDFilme] ON [LocacaoItem] ([IDFilme]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    CREATE INDEX [IX_LocacaoItem_IDLocacao] ON [LocacaoItem] ([IDLocacao]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617022918_primeira_migration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617022918_primeira_migration', N'5.0.11');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [LocacaoItem] ADD [DataAlteracao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [LocacaoItem] ADD [DataInclusao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Locacao]') AND [c].[name] = N'Situacao');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Locacao] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Locacao] ALTER COLUMN [Situacao] VARCHAR(20) NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [Locacao] ADD [DataAlteracao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [Locacao] ADD [DataInclusao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [Filme] ADD [DataAlteracao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [Filme] ADD [DataInclusao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [Cliente] ADD [DataAlteracao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    ALTER TABLE [Cliente] ADD [DataInclusao] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220617172001_novo_campo_data')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220617172001_novo_campo_data', N'5.0.11');
END;
GO

COMMIT;
GO

