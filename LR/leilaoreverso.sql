USE [master]
GO
/****** Object:  Database [LeilaoReverso]    Script Date: 27/09/2021 19:55:33 ******/
CREATE DATABASE [LeilaoReverso]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LeilaoReverso', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\LeilaoReverso.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'LeilaoReverso_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\LeilaoReverso_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [LeilaoReverso] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LeilaoReverso].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LeilaoReverso] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LeilaoReverso] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LeilaoReverso] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LeilaoReverso] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LeilaoReverso] SET ARITHABORT OFF 
GO
ALTER DATABASE [LeilaoReverso] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LeilaoReverso] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LeilaoReverso] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LeilaoReverso] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LeilaoReverso] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LeilaoReverso] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LeilaoReverso] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LeilaoReverso] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LeilaoReverso] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LeilaoReverso] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LeilaoReverso] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LeilaoReverso] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LeilaoReverso] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LeilaoReverso] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LeilaoReverso] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LeilaoReverso] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LeilaoReverso] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LeilaoReverso] SET RECOVERY FULL 
GO
ALTER DATABASE [LeilaoReverso] SET  MULTI_USER 
GO
ALTER DATABASE [LeilaoReverso] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LeilaoReverso] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LeilaoReverso] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LeilaoReverso] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [LeilaoReverso] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LeilaoReverso', N'ON'
GO
ALTER DATABASE [LeilaoReverso] SET QUERY_STORE = OFF
GO
USE [LeilaoReverso]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[DataCadastro] [datetime] NULL,
	[IdInstituicao] [int] NULL,
	[IdCurso] [int] NULL,
	[IdNivelEnsino] [int] NULL,
	[Nome] [varchar](40) NULL,
	[IdStatusGame] [smallint] NULL,
	[QtdComoComprador] [int] NOT NULL,
	[QtdComoFornecedor] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Avaliacao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avaliacao](
	[IdAvalidacao] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[Pergunta] [int] NULL,
 CONSTRAINT [PK_Avaliacao] PRIMARY KEY CLUSTERED 
(
	[IdAvalidacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CompradorFornecedor]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompradorFornecedor](
	[IdCompradorFornecedor] [int] IDENTITY(1,1) NOT NULL,
	[IdComprador] [nvarchar](450) NOT NULL,
	[IdFornecedor] [nvarchar](450) NULL,
 CONSTRAINT [PK_CompradorFornecedor] PRIMARY KEY CLUSTERED 
(
	[IdCompradorFornecedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Curso]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Curso](
	[IdCurso] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](40) NOT NULL,
 CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED 
(
	[IdCurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeviceCodes]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeviceCodes](
	[UserCode] [nvarchar](200) NOT NULL,
	[DeviceCode] [nvarchar](200) NOT NULL,
	[SubjectId] [nvarchar](200) NULL,
	[ClientId] [nvarchar](200) NOT NULL,
	[CreationTime] [datetime2](7) NOT NULL,
	[Expiration] [datetime2](7) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DeviceCodes] PRIMARY KEY CLUSTERED 
(
	[UserCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FornecedorLeilao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FornecedorLeilao](
	[IdFornecedorLeilao] [int] IDENTITY(1,1) NOT NULL,
	[IdFornecedor] [nvarchar](450) NOT NULL,
	[IdLeilao] [int] NOT NULL,
	[Lance] [decimal](8, 2) NULL,
	[Data] [datetime] NULL,
	[Classificacao] [int] NULL,
	[CustoServicoFrete] [decimal](8, 2) NULL,
	[NivelRisco] [smallint] NULL,
	[AtributoPrecoPrazo] [int] NULL,
	[IndicadorAtivo] [bit] NULL,
	[PontuacaoPeso] [decimal](10, 2) NULL,
	[Vendedor] [bit] NULL,
	[IdJustificativa] [smallint] NULL,
 CONSTRAINT [PK_FornecedorLeilao] PRIMARY KEY CLUSTERED 
(
	[IdFornecedorLeilao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FornecedorLeilaoLance]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FornecedorLeilaoLance](
	[IdFornecedorLeilaoLance] [int] IDENTITY(1,1) NOT NULL,
	[Sequencia] [int] NULL,
	[IdFornecedorLeilao] [int] NOT NULL,
	[Data] [datetime] NULL,
	[Rodada] [smallint] NULL,
	[AtributoPrecoPrazo] [int] NULL,
 CONSTRAINT [PK_FornecedorLeilaoLance] PRIMARY KEY CLUSTERED 
(
	[IdFornecedorLeilaoLance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Instituicao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Instituicao](
	[IdInstituicao] [int] IDENTITY(1,1) NOT NULL,
	[Descricao] [varchar](40) NOT NULL,
 CONSTRAINT [PK_Instituicao] PRIMARY KEY CLUSTERED 
(
	[IdInstituicao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Lance]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lance](
	[IdLance] [int] NOT NULL,
	[Lance] [varchar](30) NULL,
 CONSTRAINT [PK_Lance] PRIMARY KEY CLUSTERED 
(
	[IdLance] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leilao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leilao](
	[IdLeilao] [int] IDENTITY(1,1) NOT NULL,
	[DataCadastro] [datetime] NULL,
	[DataAberturaLance] [datetime] NULL,
	[DataFechamento] [datetime] NULL,
	[Requisicao] [text] NOT NULL,
	[IdTipoLeilao] [int] NOT NULL,
	[ValorInicial] [decimal](8, 2) NULL,
	[IndicadorAtivo] [bit] NOT NULL,
	[IdComprador] [nvarchar](450) NULL,
	[JustificativaCancelamento] [varchar](200) NULL,
	[ValorMaximo] [decimal](8, 2) NULL,
	[CustoCompraEsperado] [decimal](8, 2) NULL,
	[QuantidadeVencedores] [smallint] NULL,
	[NivelRisco] [smallint] NULL,
	[IdAtributoPrecoPrazo1] [smallint] NULL,
	[IdAtributoPrecoPrazo2] [smallint] NULL,
	[IdAtributoPrecoPrazo3] [smallint] NULL,
	[IdAtributoPrecoPrazo4] [smallint] NULL,
	[IdAtributoPrecoPrazo5] [smallint] NULL,
	[IdAtributoPrecoPrazo6] [smallint] NULL,
	[DataApuracao] [datetime] NULL,
	[ValorIncremento] [decimal](8, 2) NULL,
	[Rodada] [smallint] NULL,
	[IdCurso] [int] NULL,
	[IdNivelEnsino] [int] NULL,
	[SelecaoRealizada] [bit] NULL,
	[DataFechamentoRodada1] [datetime] NULL,
	[DataFechamentoRodada2] [datetime] NULL,
	[DataFechamentoRodada3] [datetime] NULL,
	[IdJustificativa] [smallint] NULL,
 CONSTRAINT [PK_Leilao] PRIMARY KEY CLUSTERED 
(
	[IdLeilao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NivelEnsino]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NivelEnsino](
	[IdNivelEnsino] [int] NOT NULL,
	[Descricao] [varchar](30) NOT NULL,
 CONSTRAINT [PK_NivelEnsino] PRIMARY KEY CLUSTERED 
(
	[IdNivelEnsino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersistedGrants]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersistedGrants](
	[Key] [nvarchar](200) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[SubjectId] [nvarchar](200) NULL,
	[ClientId] [nvarchar](200) NOT NULL,
	[CreationTime] [datetime2](7) NOT NULL,
	[Expiration] [datetime2](7) NULL,
	[Data] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PersistedGrants] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PesoPrioridade]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PesoPrioridade](
	[IdPesoPrioridade] [int] NOT NULL,
	[IdOpcaoComprador] [int] NULL,
	[IdOpcaoFornecedor] [int] NULL,
	[Preco] [decimal](6, 2) NULL,
	[PesoPreco] [int] NULL,
	[Prazo] [int] NULL,
	[PesoPrazo] [int] NULL,
	[Classificacao] [int] NULL,
 CONSTRAINT [PK_PesoPrioridade] PRIMARY KEY CLUSTERED 
(
	[IdPesoPrioridade] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PesoValorLeilao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PesoValorLeilao](
	[IdPesoValorLeilao] [int] IDENTITY(1,1) NOT NULL,
	[Valor] [decimal](8, 2) NULL,
	[Peso] [int] NULL,
	[Tipo] [char](1) NULL,
	[IdLeilao] [int] NULL,
 CONSTRAINT [PK_PesoValorLeilao] PRIMARY KEY CLUSTERED 
(
	[IdPesoValorLeilao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RespostaAvaliacao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RespostaAvaliacao](
	[IdRespostaAvaliacao] [int] IDENTITY(1,1) NOT NULL,
	[IdAvaliacao] [int] NULL,
	[Resposta] [int] NULL,
 CONSTRAINT [PK_RespostaAvaliacao] PRIMARY KEY CLUSTERED 
(
	[IdRespostaAvaliacao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusGame]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusGame](
	[IdStatusGame] [smallint] NOT NULL,
	[Descricao] [varchar](20) NULL,
 CONSTRAINT [PK_IdStatusGame] PRIMARY KEY CLUSTERED 
(
	[IdStatusGame] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoLeilao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoLeilao](
	[IdTipoLeilao] [int] NOT NULL,
	[Descricao] [varchar](40) NOT NULL,
	[Tempo] [int] NULL,
 CONSTRAINT [PK_TipoLeilao] PRIMARY KEY CLUSTERED 
(
	[IdTipoLeilao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsuarioInstituicao]    Script Date: 27/09/2021 19:55:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsuarioInstituicao](
	[IdUsuarioInstituicao] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [nvarchar](450) NULL,
	[IdInstituicao] [int] NOT NULL,
 CONSTRAINT [PK_UsuarioInstituicao] PRIMARY KEY CLUSTERED 
(
	[IdUsuarioInstituicao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 27/09/2021 19:55:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 27/09/2021 19:55:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_DeviceCodes_DeviceCode]    Script Date: 27/09/2021 19:55:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_DeviceCodes_DeviceCode] ON [dbo].[DeviceCodes]
(
	[DeviceCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeviceCodes_Expiration]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_DeviceCodes_Expiration] ON [dbo].[DeviceCodes]
(
	[Expiration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_PersistedGrants_Expiration]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_PersistedGrants_Expiration] ON [dbo].[PersistedGrants]
(
	[Expiration] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_PersistedGrants_SubjectId_ClientId_Type]    Script Date: 27/09/2021 19:55:34 ******/
CREATE NONCLUSTERED INDEX [IX_PersistedGrants_SubjectId_ClientId_Type] ON [dbo].[PersistedGrants]
(
	[SubjectId] ASC,
	[ClientId] ASC,
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_TipoLeilao]    Script Date: 27/09/2021 19:55:34 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_TipoLeilao] ON [dbo].[TipoLeilao]
(
	[Descricao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Curso] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Curso] ([IdCurso])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Curso]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_IdStatusGame] FOREIGN KEY([IdStatusGame])
REFERENCES [dbo].[StatusGame] ([IdStatusGame])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_IdStatusGame]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_Instituicao] FOREIGN KEY([IdInstituicao])
REFERENCES [dbo].[Instituicao] ([IdInstituicao])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_Instituicao]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUsers_NivelEnsino] FOREIGN KEY([IdNivelEnsino])
REFERENCES [dbo].[NivelEnsino] ([IdNivelEnsino])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_AspNetUsers_NivelEnsino]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Avaliacao]  WITH CHECK ADD  CONSTRAINT [FK_Avaliacao_AspNetUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Avaliacao] CHECK CONSTRAINT [FK_Avaliacao_AspNetUsers]
GO
ALTER TABLE [dbo].[CompradorFornecedor]  WITH CHECK ADD  CONSTRAINT [FK_CompradorFornecedor_AspNetUsers] FOREIGN KEY([IdComprador])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CompradorFornecedor] CHECK CONSTRAINT [FK_CompradorFornecedor_AspNetUsers]
GO
ALTER TABLE [dbo].[CompradorFornecedor]  WITH CHECK ADD  CONSTRAINT [FK_CompradorFornecedor_AspNetUsers1] FOREIGN KEY([IdFornecedor])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[CompradorFornecedor] CHECK CONSTRAINT [FK_CompradorFornecedor_AspNetUsers1]
GO
ALTER TABLE [dbo].[FornecedorLeilao]  WITH CHECK ADD  CONSTRAINT [FK_FornecedorLeilao_AspNetUsers] FOREIGN KEY([IdFornecedor])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[FornecedorLeilao] CHECK CONSTRAINT [FK_FornecedorLeilao_AspNetUsers]
GO
ALTER TABLE [dbo].[FornecedorLeilao]  WITH CHECK ADD  CONSTRAINT [FK_FornecedorLeilao_Leilao] FOREIGN KEY([IdLeilao])
REFERENCES [dbo].[Leilao] ([IdLeilao])
GO
ALTER TABLE [dbo].[FornecedorLeilao] CHECK CONSTRAINT [FK_FornecedorLeilao_Leilao]
GO
ALTER TABLE [dbo].[FornecedorLeilaoLance]  WITH CHECK ADD  CONSTRAINT [FK_FornecedorLeilaoLance_FornecedorLeilao1] FOREIGN KEY([IdFornecedorLeilao])
REFERENCES [dbo].[FornecedorLeilao] ([IdFornecedorLeilao])
GO
ALTER TABLE [dbo].[FornecedorLeilaoLance] CHECK CONSTRAINT [FK_FornecedorLeilaoLance_FornecedorLeilao1]
GO
ALTER TABLE [dbo].[Leilao]  WITH CHECK ADD  CONSTRAINT [FK_Leilao_AspNetUsers] FOREIGN KEY([IdComprador])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Leilao] CHECK CONSTRAINT [FK_Leilao_AspNetUsers]
GO
ALTER TABLE [dbo].[Leilao]  WITH CHECK ADD  CONSTRAINT [FK_Leilao_TipoLeilao] FOREIGN KEY([IdTipoLeilao])
REFERENCES [dbo].[TipoLeilao] ([IdTipoLeilao])
GO
ALTER TABLE [dbo].[Leilao] CHECK CONSTRAINT [FK_Leilao_TipoLeilao]
GO
ALTER TABLE [dbo].[PesoValorLeilao]  WITH CHECK ADD  CONSTRAINT [FK_PesoValorLeilao_Leilao] FOREIGN KEY([IdLeilao])
REFERENCES [dbo].[Leilao] ([IdLeilao])
GO
ALTER TABLE [dbo].[PesoValorLeilao] CHECK CONSTRAINT [FK_PesoValorLeilao_Leilao]
GO
ALTER TABLE [dbo].[RespostaAvaliacao]  WITH CHECK ADD  CONSTRAINT [FK_RespostaAvaliacao_Avaliacao] FOREIGN KEY([IdAvaliacao])
REFERENCES [dbo].[Avaliacao] ([IdAvalidacao])
GO
ALTER TABLE [dbo].[RespostaAvaliacao] CHECK CONSTRAINT [FK_RespostaAvaliacao_Avaliacao]
GO
ALTER TABLE [dbo].[UsuarioInstituicao]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioInstituicao_AspNetUsers] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[UsuarioInstituicao] CHECK CONSTRAINT [FK_UsuarioInstituicao_AspNetUsers]
GO
ALTER TABLE [dbo].[UsuarioInstituicao]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioInstituicao_Instituicao] FOREIGN KEY([IdInstituicao])
REFERENCES [dbo].[Instituicao] ([IdInstituicao])
GO
ALTER TABLE [dbo].[UsuarioInstituicao] CHECK CONSTRAINT [FK_UsuarioInstituicao_Instituicao]
GO
USE [master]
GO
ALTER DATABASE [LeilaoReverso] SET  READ_WRITE 
GO
