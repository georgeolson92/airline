USE [airline_test]
GO
/****** Object:  Table [dbo].[cities]    Script Date: 7/18/2016 4:36:13 PM ******/
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
/****** Object:  Table [dbo].[flights]    Script Date: 7/18/2016 4:36:13 PM ******/
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
/****** Object:  Table [dbo].[flights_cities]    Script Date: 7/18/2016 4:36:13 PM ******/
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
SET IDENTITY_INSERT [dbo].[flights_cities] ON 

INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (1, 60, 21, 22)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (2, 65, 27, 28)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (3, 70, 33, 34)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (4, 75, 38, 39)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (5, 80, 45, 46)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (6, 85, 50, 51)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (7, 90, 54, 55)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (8, 95, 64, 65)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (9, 100, 66, 67)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (10, 105, 72, 73)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (11, 110, 82, 83)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (12, 115, 84, 85)
INSERT [dbo].[flights_cities] ([id], [flight_id], [departure_city], [arriving_city]) VALUES (13, 120, 94, 95)
SET IDENTITY_INSERT [dbo].[flights_cities] OFF
