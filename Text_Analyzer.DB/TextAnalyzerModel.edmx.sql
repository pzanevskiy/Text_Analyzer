
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/01/2021 12:07:46
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


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UploadedFilesSet'
CREATE TABLE [dbo].[UploadedFilesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [Entity1UploadedFiles_UploadedFiles_Id] int  NOT NULL
);
GO

-- Creating table 'FilesToDownloadSet'
CREATE TABLE [dbo].[FilesToDownloadSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [Entity1FilesToDownload_FilesToDownload_Id] int  NOT NULL
);
GO

-- Creating table 'FileLinksSet'
CREATE TABLE [dbo].[FileLinksSet] (
    [Id] int IDENTITY(1,1) NOT NULL
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

-- Creating foreign key on [Entity1UploadedFiles_UploadedFiles_Id] in table 'UploadedFilesSet'
ALTER TABLE [dbo].[UploadedFilesSet]
ADD CONSTRAINT [FK_Entity1UploadedFiles]
    FOREIGN KEY ([Entity1UploadedFiles_UploadedFiles_Id])
    REFERENCES [dbo].[FileLinksSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Entity1UploadedFiles'
CREATE INDEX [IX_FK_Entity1UploadedFiles]
ON [dbo].[UploadedFilesSet]
    ([Entity1UploadedFiles_UploadedFiles_Id]);
GO

-- Creating foreign key on [Entity1FilesToDownload_FilesToDownload_Id] in table 'FilesToDownloadSet'
ALTER TABLE [dbo].[FilesToDownloadSet]
ADD CONSTRAINT [FK_Entity1FilesToDownload]
    FOREIGN KEY ([Entity1FilesToDownload_FilesToDownload_Id])
    REFERENCES [dbo].[FileLinksSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Entity1FilesToDownload'
CREATE INDEX [IX_FK_Entity1FilesToDownload]
ON [dbo].[FilesToDownloadSet]
    ([Entity1FilesToDownload_FilesToDownload_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------