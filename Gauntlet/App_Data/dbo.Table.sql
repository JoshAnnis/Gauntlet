CREATE TABLE [dbo].[Table] (
    [Id]        INT             NOT NULL,
    [ImageID]   INT             NOT NULL,
    [FileName]  VARCHAR (200)   NOT NULL,
    [ImageData] VARBINARY (MAX) NOT NULL,
    [ImageSize] INT NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

