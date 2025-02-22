IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'DBHotel')
BEGIN
    CREATE DATABASE [DBHotel];
END
GO
USE [DBHotel];
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[IdCategoria] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[IdServicio] [int] NOT NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK__Categori__A3C02A10EE752289] PRIMARY KEY CLUSTERED 
(
	[IdCategoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[TipoDocumento] [varchar](15) NULL,
	[Documento] [varchar](15) NULL,
	[NombreCompleto] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoHabitacion]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoHabitacion](
	[IdEstadoHabitacion] [int] NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstadoHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Habitacion]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Habitacion](
	[IdHabitacion] [int] IDENTITY(1,1) NOT NULL,
	[Numero] [varchar](50) NULL,
	[Detalle] [varchar](100) NULL,
	[Precio] [decimal](10, 2) NULL,
	[IdEstadoHabitacion] [int] NULL,
	[IdPiso] [int] NULL,
	[IdCategoria] [int] NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK__Habitaci__8BBBF901944003AF] PRIMARY KEY CLUSTERED 
(
	[IdHabitacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Piso]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Piso](
	[IdPiso] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recepcion]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recepcion](
	[IdRecepcion] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NULL,
	[IdHabitacion] [int] NULL,
	[FechaEntrada] [datetime] NULL,
	[FechaSalida] [datetime] NULL,
	[FechaSalidaConfirmacion] [datetime] NULL,
	[PrecioInicial] [decimal](10, 2) NULL,
	[Adelanto] [decimal](10, 2) NULL,
	[PrecioRestante] [decimal](10, 2) NULL,
	[TotalPagado] [decimal](10, 2) NULL,
	[CostoPenalidad] [decimal](10, 2) NULL,
	[Observacion] [varchar](500) NULL,
	[Estado] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRecepcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RolUsuario]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolUsuario](
	[IdRolUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRolUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Servicios]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Servicios](
	[IdServicio] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
 CONSTRAINT [PK_Servicios] PRIMARY KEY CLUSTERED 
(
	[IdServicio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tarifas]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarifas](
	[IdTarifa] [int] IDENTITY(1,1) NOT NULL,
	[IdHabitacion] [int] NOT NULL,
	[FechaInicio] [date] NOT NULL,
	[FechaFin] [date] NOT NULL,
	[PrecioPorNoche] [money] NOT NULL,
	[Descuento] [numeric](5, 2) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK__Tarifas__747D03893AFBF449] PRIMARY KEY CLUSTERED 
(
	[IdTarifa] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 1/14/2025 8:18:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[NombreCompleto] [varchar](50) NULL,
	[Correo] [varchar](50) NULL,
	[IdRolUsuario] [int] NULL,
	[Clave] [varchar](50) NULL,
	[Estado] [bit] NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categoria] ADD  CONSTRAINT [DF__Categoria__Estad__44FF419A]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Categoria] ADD  CONSTRAINT [DF__Categoria__Fecha__45F365D3]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Cliente] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[EstadoHabitacion] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[EstadoHabitacion] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Habitacion] ADD  CONSTRAINT [DF__Habitacio__Estad__4AB81AF0]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Habitacion] ADD  CONSTRAINT [DF__Habitacio__Fecha__4BAC3F29]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Piso] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Piso] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Recepcion] ADD  DEFAULT (getdate()) FOR [FechaEntrada]
GO
ALTER TABLE [dbo].[Recepcion] ADD  DEFAULT ((0)) FOR [TotalPagado]
GO
ALTER TABLE [dbo].[Recepcion] ADD  DEFAULT ((0)) FOR [CostoPenalidad]
GO
ALTER TABLE [dbo].[RolUsuario] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[RolUsuario] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Categoria]  WITH CHECK ADD  CONSTRAINT [FK_Categoria_Servicio] FOREIGN KEY([IdServicio])
REFERENCES [dbo].[Servicios] ([IdServicio])
GO
ALTER TABLE [dbo].[Categoria] CHECK CONSTRAINT [FK_Categoria_Servicio]
GO
ALTER TABLE [dbo].[Habitacion]  WITH CHECK ADD  CONSTRAINT [FK__Habitacio__IdCat__5535A963] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categoria] ([IdCategoria])
GO
ALTER TABLE [dbo].[Habitacion] CHECK CONSTRAINT [FK__Habitacio__IdCat__5535A963]
GO
ALTER TABLE [dbo].[Habitacion]  WITH CHECK ADD  CONSTRAINT [FK__Habitacio__IdEst__5629CD9C] FOREIGN KEY([IdEstadoHabitacion])
REFERENCES [dbo].[EstadoHabitacion] ([IdEstadoHabitacion])
GO
ALTER TABLE [dbo].[Habitacion] CHECK CONSTRAINT [FK__Habitacio__IdEst__5629CD9C]
GO
ALTER TABLE [dbo].[Habitacion]  WITH CHECK ADD  CONSTRAINT [FK__Habitacio__IdPis__571DF1D5] FOREIGN KEY([IdPiso])
REFERENCES [dbo].[Piso] ([IdPiso])
GO
ALTER TABLE [dbo].[Habitacion] CHECK CONSTRAINT [FK__Habitacio__IdPis__571DF1D5]
GO
ALTER TABLE [dbo].[Recepcion]  WITH CHECK ADD FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Cliente] ([IdCliente])
GO
ALTER TABLE [dbo].[Recepcion]  WITH CHECK ADD  CONSTRAINT [FK__RECEPCION__IdHab__59063A47] FOREIGN KEY([IdHabitacion])
REFERENCES [dbo].[Habitacion] ([IdHabitacion])
GO
ALTER TABLE [dbo].[Recepcion] CHECK CONSTRAINT [FK__RECEPCION__IdHab__59063A47]
GO
ALTER TABLE [dbo].[Tarifas]  WITH CHECK ADD  CONSTRAINT [FK_Tarifas_Habitacion] FOREIGN KEY([IdHabitacion])
REFERENCES [dbo].[Habitacion] ([IdHabitacion])
GO
ALTER TABLE [dbo].[Tarifas] CHECK CONSTRAINT [FK_Tarifas_Habitacion]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdRolUsuario])
REFERENCES [dbo].[RolUsuario] ([IdRolUsuario])
GO
ALTER TABLE [dbo].[Tarifas]  WITH CHECK ADD  CONSTRAINT [CK__Tarifas__estado__74AE54BC] CHECK  (([estado]='inactivo' OR [estado]='activo'))
GO
ALTER TABLE [dbo].[Tarifas] CHECK CONSTRAINT [CK__Tarifas__estado__74AE54BC]
GO