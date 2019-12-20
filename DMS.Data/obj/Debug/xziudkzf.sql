IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    CREATE TABLE [Users] (
        [UserId] int NOT NULL IDENTITY,
        [UserEmail] nvarchar(max) NOT NULL,
        [UserName] nvarchar(max) NOT NULL,
        [password] nvarchar(max) NOT NULL,
        [UserRole] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    CREATE TABLE [Category] (
        [CategoryId] int NOT NULL IDENTITY,
        [CategoryName] nvarchar(max) NOT NULL,
        [UsersUserId] int NOT NULL,
        CONSTRAINT [PK_Category] PRIMARY KEY ([CategoryId]),
        CONSTRAINT [FK_Category_Users_UsersUserId] FOREIGN KEY ([UsersUserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    CREATE TABLE [Document] (
        [DocumentId] int NOT NULL IDENTITY,
        [DocumentPath] nvarchar(max) NOT NULL,
        [DocumentName] nvarchar(max) NOT NULL,
        [DocumentTags] nvarchar(max) NOT NULL,
        [CategoryId] int NOT NULL,
        [UsersUserId] int NOT NULL,
        CONSTRAINT [PK_Document] PRIMARY KEY ([DocumentId]),
        CONSTRAINT [FK_Document_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Document_Users_UsersUserId] FOREIGN KEY ([UsersUserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    CREATE INDEX [IX_Category_UsersUserId] ON [Category] ([UsersUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    CREATE INDEX [IX_Document_CategoryId] ON [Document] ([CategoryId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    CREATE INDEX [IX_Document_UsersUserId] ON [Document] ([UsersUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721064633_initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180721064633_initial', N'2.1.1-rtm-30846');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065023_initial2')
BEGIN
    ALTER TABLE [Document] DROP CONSTRAINT [FK_Document_Users_UsersUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065023_initial2')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Document]') AND [c].[name] = N'UsersUserId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Document] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Document] ALTER COLUMN [UsersUserId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065023_initial2')
BEGIN
    ALTER TABLE [Document] ADD CONSTRAINT [FK_Document_Users_UsersUserId] FOREIGN KEY ([UsersUserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065023_initial2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180721065023_initial2', N'2.1.1-rtm-30846');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065900_initial3')
BEGIN
    ALTER TABLE [Document] DROP CONSTRAINT [FK_Document_Category_CategoryId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065900_initial3')
BEGIN
    ALTER TABLE [Document] ADD CONSTRAINT [FK_Document_Category_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Category] ([CategoryId]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721065900_initial3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180721065900_initial3', N'2.1.1-rtm-30846');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721070541_in')
BEGIN
    ALTER TABLE [Category] DROP CONSTRAINT [FK_Category_Users_UsersUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721070541_in')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Category]') AND [c].[name] = N'UsersUserId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Category] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Category] ALTER COLUMN [UsersUserId] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721070541_in')
BEGIN
    ALTER TABLE [Category] ADD CONSTRAINT [FK_Category_Users_UsersUserId] FOREIGN KEY ([UsersUserId]) REFERENCES [Users] ([UserId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20180721070541_in')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20180721070541_in', N'2.1.1-rtm-30846');
END;

GO

