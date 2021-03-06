USE [airline]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 7/18/2016 4:35:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[flights]    Script Date: 7/18/2016 4:35:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[departure] [datetime] NULL,
	[arrival] [datetime] NULL,
	[status] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[flights_cities]    Script Date: 7/18/2016 4:35:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights_cities](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[flight_id] [int] NULL,
	[departure_city] [int] NULL,
	[arriving_city] [int] NULL
) ON [PRIMARY]

GO
