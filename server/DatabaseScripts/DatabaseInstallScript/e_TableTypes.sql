-- Create the data type
CREATE TYPE SearchIDs AS TABLE 
(
	ID BIGINT NOT NULL,
	seq_nr BIGINT,
	PRIMARY KEY (ID)
)
GO

CREATE TYPE [dbo].[Integ_RAExterno] AS TABLE(
	[_ID] [bigint] NOT NULL,
	[IDExterno] [nvarchar](200) COLLATE Latin1_General_CS_AS NOT NULL,
	[Titulo] [nvarchar](200) COLLATE Latin1_General_CS_AS,
	[IDSistema] [int] NOT NULL,
	[IDTipoEntidade] [int] NOT NULL
)
GO

CREATE TYPE [dbo].[Integ_RAExternoOnomasticos] AS TABLE(
	[Titulo] [nvarchar](768) NULL,
	[NIF] [nvarchar](256) NULL
)
GO

CREATE TYPE [dbo].[Integ_DocExterno] AS TABLE(
	[IDExterno] [nvarchar](200) COLLATE Latin1_General_CS_AS NOT NULL,
	[IDSistema] [int] NULL
)
GO

CREATE TYPE [dbo].[DocInPortoRecord] AS TABLE(
	[IDSistema] INT NOT NULL,
	[IDTipoEntidade] INT NOT NULL,
	[IDExterno] [nvarchar](200) COLLATE Latin1_General_CS_AS NOT NULL,
	[DataArquivo] [datetime2](7) NOT NULL
)
GO

CREATE TYPE [dbo].[SearchDesignacoes] AS TABLE(
	[Designacao] [nvarchar](768) COLLATE Latin1_General_CS_AS NOT NULL
)
GO