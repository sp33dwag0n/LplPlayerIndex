CREATE TABLE [dbo].[Player] (
    [id]   INT            IDENTITY (1, 1) NOT NULL,
    [Ign]  NVARCHAR (MAX) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    [Age]  INT            NULL,
    [Team] NVARCHAR (MAX) NULL,
    [Position] NVARCHAR(MAX) NULL, 
    CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED ([id] ASC)
);

