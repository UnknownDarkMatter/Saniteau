IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [dbo].[__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO


CREATE TABLE [Abonnés] (
    [IdAbonné] int NOT NULL IDENTITY,
    [IdAdresse] int NOT NULL,
    [Nom] nvarchar(max) NULL,
    [Prénom] nvarchar(max) NULL,
    [Tarification] int NOT NULL,
    CONSTRAINT [PK_Abonnés] PRIMARY KEY ([IdAbonné])
);
GO

CREATE TABLE [Adresses] (
    [IdAdresse] int NOT NULL IDENTITY,
    [NuméroEtRue] nvarchar(max) NULL,
    [Ville] nvarchar(max) NULL,
    [CodePostal] nvarchar(max) NULL,
    CONSTRAINT [PK_Adresses] PRIMARY KEY ([IdAdresse])
);
GO

CREATE TABLE [AdressesPDL] (
    [IdAdressePDL] int NOT NULL IDENTITY,
    [IdAdresse] int NOT NULL,
    [IdPDL] int NOT NULL,
    CONSTRAINT [PK_AdressesPDL] PRIMARY KEY ([IdAdressePDL])
);
GO

CREATE TABLE [AppairagesCompteurs] (
    [IdAppairageCompteur] int NOT NULL IDENTITY,
    [IdPDL] int NOT NULL,
    [IdCompteur] int NULL,
    [DateAppairage] datetime2 NULL,
    [DateDésappairage] datetime2 NULL,
    CONSTRAINT [PK_AppairagesCompteurs] PRIMARY KEY ([IdAppairageCompteur])
);
GO

CREATE TABLE [Compteurs] (
    [IdCompteur] int NOT NULL IDENTITY,
    [NuméroCompteur] nvarchar(max) NULL,
    [CompteurPosé] bit NOT NULL,
    CONSTRAINT [PK_Compteurs] PRIMARY KEY ([IdCompteur])
);
GO

CREATE TABLE [Delegants] (
    [IdDelegant] int NOT NULL IDENTITY,
    [Nom] nvarchar(max) NULL,
    [Adresse] nvarchar(max) NULL,
    [DateContrat] datetime2 NOT NULL,
    CONSTRAINT [PK_Delegants] PRIMARY KEY ([IdDelegant])
);
GO

CREATE TABLE [FacturesPayeesAuDelegant] (
    [IdFacturePayeeAuDelegant] int NOT NULL IDENTITY,
    [IdPayeDelegant] int NOT NULL,
    [IdFacturation] int NOT NULL,
    [IdAbonné] int NOT NULL,
    CONSTRAINT [PK_FacturesPayeesAuDelegant] PRIMARY KEY ([IdFacturePayeeAuDelegant])
);
GO

CREATE TABLE [IndexesCompteur] (
    [IdIndex] int NOT NULL IDENTITY,
    [IdCompteur] int NOT NULL,
    [IdCampagneReleve] int NOT NULL,
    [IndexM3] int NOT NULL,
    [DateIndex] datetime2 NOT NULL,
    CONSTRAINT [PK_IndexesCompteur] PRIMARY KEY ([IdIndex])
);
GO

CREATE TABLE [IndexesPayésParDelegant] (
    [IdIndexPayéParDelegant] int NOT NULL IDENTITY,
    [IdPayeDelegant] int NOT NULL,
    [IdCompteur] int NOT NULL,
    [IdIndex] int NOT NULL,
    CONSTRAINT [PK_IndexesPayésParDelegant] PRIMARY KEY ([IdIndexPayéParDelegant])
);
GO

CREATE TABLE [PayesDelegant] (
    [IdPayeDelegant] int NOT NULL IDENTITY,
    [IdDelegant] int NOT NULL,
    [DatePaye] datetime2 NOT NULL,
    CONSTRAINT [PK_PayesDelegant] PRIMARY KEY ([IdPayeDelegant])
);
GO

CREATE TABLE [PDL] (
    [IdPDL] int NOT NULL IDENTITY,
    [NuméroPDL] nvarchar(max) NULL,
    CONSTRAINT [PK_PDL] PRIMARY KEY ([IdPDL])
);
GO

CREATE TABLE [Pompes] (
    [IdPompe] int NOT NULL IDENTITY,
    [IdCompteur] int NOT NULL,
    [NuméroPompe] nvarchar(max) NULL,
    CONSTRAINT [PK_Pompes] PRIMARY KEY ([IdPompe])
);
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [Prenom] nvarchar(max) NULL,
    [Nom] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Facturations] (
    [IdFacturation] int NOT NULL IDENTITY,
    [IdCampagneFacturation] int NOT NULL,
    [IdAbonné] int NOT NULL,
    [DateFacturation] datetime2 NOT NULL,
    [IdDernierIndex] int NOT NULL,
    [Payée] bit NOT NULL,
    CONSTRAINT [PK_Facturations] PRIMARY KEY ([IdFacturation]),
    CONSTRAINT [FK_Facturations_Abonnés_IdAbonné] FOREIGN KEY ([IdAbonné]) REFERENCES [Abonnés] ([IdAbonné]) ON DELETE CASCADE
);
GO

CREATE TABLE [PayesDelegantLignes] (
    [IdPayeDelegantLigne] int NOT NULL IDENTITY,
    [Classe] int NOT NULL,
    [MontantEuros] decimal(18,2) NOT NULL,
    [IdPayeDelegant] int NOT NULL,
    CONSTRAINT [PK_PayesDelegantLignes] PRIMARY KEY ([IdPayeDelegantLigne]),
    CONSTRAINT [FK_PayesDelegantLignes_PayesDelegant_IdPayeDelegant] FOREIGN KEY ([IdPayeDelegant]) REFERENCES [PayesDelegant] ([IdPayeDelegant]) ON DELETE CASCADE
);
GO

CREATE TABLE [RoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RoleClaims_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [RefreshToken] (
    [Id] int NOT NULL IDENTITY,
    [Expires] datetime2 NOT NULL,
    [Revoked] datetime2 NULL,
    [Token] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_RefreshToken] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RefreshToken_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UserClaims_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_UserLogins_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [UserTokens] (
    [UserId] int NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_UserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_UserTokens_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [FacturationLignes] (
    [IdFacturationLigne] int NOT NULL IDENTITY,
    [IdFacturation] int NOT NULL,
    [ClasseLigneFacturation] int NOT NULL,
    [MontantEuros] decimal(18,2) NOT NULL,
    [ConsommationM3] int NOT NULL,
    [PrixM3] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_FacturationLignes] PRIMARY KEY ([IdFacturationLigne]),
    CONSTRAINT [FK_FacturationLignes_Facturations_IdFacturation] FOREIGN KEY ([IdFacturation]) REFERENCES [Facturations] ([IdFacturation]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_FacturationLignes_IdFacturation] ON [FacturationLignes] ([IdFacturation]);
GO

CREATE INDEX [IX_Facturations_IdAbonné] ON [Facturations] ([IdAbonné]);
GO

CREATE INDEX [IX_PayesDelegantLignes_IdPayeDelegant] ON [PayesDelegantLignes] ([IdPayeDelegant]);
GO

CREATE INDEX [IX_RefreshToken_UserId] ON [RefreshToken] ([UserId]);
GO

CREATE INDEX [IX_RoleClaims_RoleId] ON [RoleClaims] ([RoleId]);
GO

CREATE UNIQUE INDEX [RoleNameIndex] ON [Roles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
GO

CREATE INDEX [IX_UserClaims_UserId] ON [UserClaims] ([UserId]);
GO

CREATE INDEX [IX_UserLogins_UserId] ON [UserLogins] ([UserId]);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

CREATE INDEX [EmailIndex] ON [Users] ([NormalizedEmail]);
GO

CREATE UNIQUE INDEX [UserNameIndex] ON [Users] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
GO

INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210217125532_CreateDb', N'5.0.2');
GO


