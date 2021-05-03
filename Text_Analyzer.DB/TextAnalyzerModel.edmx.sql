
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/01/2021 15:03:57
-- Generated from EDMX file: C:\Users\Павел\source\repos\Text_Analyzer\Text_Analyzer.DB\TextAnalyzerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [textanalyzer_db];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Entity1FilesToDownload]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FilesToDownloadSet] DROP CONSTRAINT [FK_Entity1FilesToDownload];
GO
IF OBJECT_ID(N'[dbo].[FK_Entity1UploadedFiles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UploadedFilesSet] DROP CONSTRAINT [FK_Entity1UploadedFiles];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FileLinksSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileLinksSet];
GO
IF OBJECT_ID(N'[dbo].[FilesToDownloadSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FilesToDownloadSet];
GO
IF OBJECT_ID(N'[dbo].[UploadedFilesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UploadedFilesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UploadedFilesSet'
CREATE TABLE [dbo].[UploadedFilesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Filename] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FilesToDownloadSet'
CREATE TABLE [dbo].[FilesToDownloadSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Filename] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FileLinksSet'
CREATE TABLE [dbo].[FileLinksSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UploadedFiles_Id] int  NOT NULL,
    [FilesToDownload_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UploadedFilesSet'
ALTER TABLE [dbo].[UploadedFilesSet]
ADD CONSTRAINT [PK_UploadedFilesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FilesToDownloadSet'
ALTER TABLE [dbo].[FilesToDownloadSet]
ADD CONSTRAINT [PK_FilesToDownloadSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileLinksSet'
ALTER TABLE [dbo].[FileLinksSet]
ADD CONSTRAINT [PK_FileLinksSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UploadedFiles_Id] in table 'FileLinksSet'
ALTER TABLE [dbo].[FileLinksSet]
ADD CONSTRAINT [FK_UploadedFilesFileLinks]
    FOREIGN KEY ([UploadedFiles_Id])
    REFERENCES [dbo].[UploadedFilesSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UploadedFilesFileLinks'
CREATE INDEX [IX_FK_UploadedFilesFileLinks]
ON [dbo].[FileLinksSet]
    ([UploadedFiles_Id]);
GO

-- Creating foreign key on [FilesToDownload_Id] in table 'FileLinksSet'
ALTER TABLE [dbo].[FileLinksSet]
ADD CONSTRAINT [FK_FilesToDownloadFileLinks]
    FOREIGN KEY ([FilesToDownload_Id])
    REFERENCES [dbo].[FilesToDownloadSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FilesToDownloadFileLinks'
CREATE INDEX [IX_FK_FilesToDownloadFileLinks]
ON [dbo].[FileLinksSet]
    ([FilesToDownload_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------