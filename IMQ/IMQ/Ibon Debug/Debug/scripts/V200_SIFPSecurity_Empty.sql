USE [master]
GO
/****** Object:  Database [SIFPSecurity]    Script Date: 20/12/2012 10:59:52 ******/
CREATE DATABASE [SIFPSecurity] ON  PRIMARY 
( NAME = N'HCDSecurity', FILENAME = N'SIFPSecurity_Data.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'HCDSecurity_log', FILENAME = N'SIFPSecurity_Log.ldf' , SIZE = 20096KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'SIFPSecurity', @new_cmptlevel=90
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SIFPSecurity].[dbo].[sp_fulltext_database] @action = 'disable'
end
GO
ALTER DATABASE [SIFPSecurity] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SIFPSecurity] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SIFPSecurity] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SIFPSecurity] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SIFPSecurity] SET ARITHABORT OFF 
GO
ALTER DATABASE [SIFPSecurity] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SIFPSecurity] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SIFPSecurity] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SIFPSecurity] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SIFPSecurity] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SIFPSecurity] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SIFPSecurity] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SIFPSecurity] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SIFPSecurity] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SIFPSecurity] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SIFPSecurity] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SIFPSecurity] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SIFPSecurity] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SIFPSecurity] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SIFPSecurity] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SIFPSecurity] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SIFPSecurity] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SIFPSecurity] SET RECOVERY FULL 
GO
ALTER DATABASE [SIFPSecurity] SET  MULTI_USER 
GO
ALTER DATABASE [SIFPSecurity] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SIFPSecurity] SET DB_CHAINING OFF 
GO
USE [SIFPSecurity]
GO
/****** Object:  User [sii]    Script Date: 20/12/2012 10:59:52 ******/
CREATE USER [sii] FOR LOGIN [sii] WITH DEFAULT_SCHEMA=[dbo]
GO
sys.sp_addrolemember @rolename = N'db_owner', @membername = N'sii'
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.AddRestriction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.AddRestriction]
(	@RoleID AS INT,
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256),
	@ComponentName AS NVARCHAR(256),
	@Status AS INT
)
AS
	SET NOCOUNT ON

	DECLARE @ItemID AS INT

	SELECT	@ItemID = [ID]	
	FROM	[RoleComponents]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID AND
			[ViewName] = @ViewName AND [ComponentName] = @ComponentName

	IF (@ItemID IS NULL)
	BEGIN
		SET NOCOUNT OFF

		BEGIN TRANSACTION

		EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID

		INSERT INTO [RoleComponents]
			([RoleID], [ModuleID], [ViewName], [ComponentName], [Status])
		VALUES
			(@RoleID, @ModuleID, @ViewName, @ComponentName, @Status)

		SELECT @@identity AS [ID]

		COMMIT TRANSACTION
	END
	ELSE
		SELECT -1 AS [ID]

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.AddRoleFunction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.AddRoleFunction]
(	@RoleID AS INT,
	@Function AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON
	DECLARE @ItemID AS INT	

	SELECT	@ItemID = [ID]
	FROM	[RoleFunctions]
	WHERE	[RoleID] = @RoleID AND [Function] = @Function

	IF (@ItemID IS NULL)
	BEGIN
		SET NOCOUNT OFF
		
		BEGIN TRANSACTION
		
		EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID
		
		INSERT INTO [RoleFunctions]
			([RoleID], [Function])
		VALUES
			(@RoleID, @Function)

		COMMIT TRANSACTION
	END
	ELSE
		SELECT -1 AS [ID]

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.AddUserRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.AddUserRole]
(	
	@UserID AS INT,
	@RoleID AS INT
)
AS
	SET NOCOUNT ON
	DECLARE @ItemID AS INT	

	SELECT	@ItemID = [ID]
	FROM	UserRoles
	WHERE	UserID = @UserID AND [RoleID] = @RoleID 

	IF (@ItemID IS NULL)
	BEGIN
		SET NOCOUNT OFF
	
		BEGIN TRANSACTION

		EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID

		INSERT INTO [UserRoles]
			([UserID], [RoleID])
		VALUES
			(@UserID, @RoleID)

		SELECT @@identity AS [ID]

		COMMIT TRANSACTION
	END
	ELSE
		SELECT -1 AS [ID]



GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearApplicationRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearApplicationRestrictions]
(	
	@ApplicationID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetApplicationUpdated] @ApplicationID

	DELETE	[RoleComponents]
	WHERE	[RoleID] IN
	(
		SELECT	[ID]
		FROM	[Roles]
		WHERE	[ApplicationID] = @ApplicationID
	)

	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearModuleRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearModuleRestrictions]
(	
	@RoleID AS INT,
	@ModuleID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID

	DELETE	[RoleComponents]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID

	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearRoleFunctions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearRoleFunctions]
(	@RoleID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID

	DELETE [RoleFunctions]
	WHERE	[RoleID] = @RoleID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearRoleRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearRoleRestrictions]
(	
	@RoleID AS INT
)
AS
	BEGIN TRANSACTION
	
	EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID
	
	DELETE	[RoleComponents]
	WHERE	[RoleID] = @RoleID
	
	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearUserRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearUserRestrictions]
(	
	@UserID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetUserUpdated] @UserID

	DELETE	[RoleComponents]
	WHERE	[RoleID] IN
	(
		SELECT	[RoleID]
		FROM	[UserRoles]
		WHERE	[UserRoles].[UserID] = @UserID
	)

	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearUserRoles]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearUserRoles]
(	
	@UserID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetUserUpdated] @UserID

	DELETE	[UserRoles]
	WHERE	[UserID] = @UserID

	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearUsersInRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearUsersInRole]
(	
	@RoleID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID

	DELETE	[UserRoles]
	WHERE	[RoleID] = @RoleID

	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ClearViewRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ClearViewRestrictions]
(	
	@RoleID AS INT,
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256)
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID
	EXEC [SII.SIFP.Security.SetModuleUpdated] @ModuleID

	DELETE	[RoleComponents]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID AND 
			[ViewName] = @ViewName

	COMMIT TRANSACTION

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.CreateApplication]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.CreateApplication]
(	@ApplicationName NVARCHAR(256),
	@Description NVARCHAR(256),
	@EnablePasswordRetrieval BIT,
	@EnablePasswordReset BIT,
	@RequiresQuestionAndAnswer BIT,
	@RequiresUniqueEmail BIT,
	@MaxInvalidPasswordAttempts INT,
	@PasswordAttemptWindow INT,
	@PasswordFormat INT,
	@WriteExceptionsToEventLog BIT,
	@MinRequiredNonAlphanumericCharacters INT,
	@MinRequiredPasswordLength INT,
	@PasswordStrengthRegularExpression NVARCHAR(1024),
	@ValidationKey NVARCHAR(1024)
)
AS
	SET NOCOUNT ON;

	INSERT INTO Applications
	(	ApplicationName, LoweredApplicationName, [Description], EnablePasswordRetrieval,
		EnablePasswordReset, RequiresQuestionAndAnswer, RequiresUniqueEmail,
		MaxInvalidPasswordAttempts, PasswordAttemptWindow, PasswordFormat,
		WriteExceptionsToEventLog, MinRequiredNonAlphanumericCharacters,
		MinRequiredPasswordLength, PasswordStrengthRegularExpression,
		ValidationKey, LastUpdated
	)
	VALUES
	(	@ApplicationName, LOWER(@ApplicationName), @Description, @EnablePasswordRetrieval,
		@EnablePasswordReset, @RequiresQuestionAndAnswer, @RequiresUniqueEmail,
		@MaxInvalidPasswordAttempts, @PasswordAttemptWindow, @PasswordFormat,
		@WriteExceptionsToEventLog, @MinRequiredNonAlphanumericCharacters,
		@MinRequiredPasswordLength, @PasswordStrengthRegularExpression,
		@ValidationKey, GetDate()
	)

	SELECT @@Identity AS [ID]

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.CreateModule]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.CreateModule]
	@ApplicationID AS INT,
	@ModuleName AS NVARCHAR(256),
	@Title AS NVARCHAR(256),
	@Description AS NVARCHAR(1024),
	@AssemblyName AS NVARCHAR(1024),
	@URL AS NVARCHAR(1024),
	@AccessQuery AS NVARCHAR(1024),
	@AdministrationQuery AS NVARCHAR(1024)
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [Modules]
		([ApplicationID], [ModuleName], [LoweredModuleName], [Title], 
		 [Description], [AssemblyName], [URL], [AccessQuery], [AdministrationQuery], 
		 [LastUpdated])
	VALUES 
		(@ApplicationID, @ModuleName, LOWER(@ModuleName), @Title, @Description, 
		 @AssemblyName, @URL, @AccessQuery, @AdministrationQuery, GetDate())

	SELECT @@IDENTITY AS ID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.CreateRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.CreateRole]
(	
	@ApplicationID AS INT,
	@RoleName AS NVARCHAR(256),
	@Description AS NVARCHAR(1024)
)
AS
	INSERT INTO [Roles]
		([ApplicationID], [RoleName], [LoweredRoleName], [Description], [LastUpdated])
	VALUES
		(@ApplicationID, @RoleName, LOWER(@RoleName), @Description, GetDate())

	SELECT @@Identity AS ID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.CreateUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.CreateUser]
(	@ApplicationID INT,
	@UserName NVARCHAR(256),
	@PasswordFormat INT,
	@PasswordSalt NVARCHAR(128),
	@Password NVARCHAR(128),
	@Email NVARCHAR(256),
	@PasswordQuestion NVARCHAR(256),
	@PasswordAnswer NVARCHAR(256),
	@IsApproved BIT,
	@IsAnonymous BIT,
	@IsAdministrator BIT,
	@Culture NVARCHAR(16),
	@Comment NVARCHAR(1024)
)
AS
	DECLARE @CreationDate AS DATETIME
	DECLARE @MinDate AS DATETIME

	SET @CreationDate = GETDATE()
	SET @MinDate = N'19000101'

	INSERT INTO Users
	(	ApplicationID, Username, LoweredUserName, PasswordFormat, PasswordSalt, 
		[Password], Email, LoweredEmail, PasswordQuestion, PasswordAnswer, IsApproved,
		IsAnonymous, IsAdministrator, Culture, Comment, CreationDate, 
		LastPasswordChangedDate, LastActivityDate, LastLoginDate,
		IsLockedOut, LastLockedOutDate,
		FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart,
		FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart,
		LastUpdated)
	VALUES(
		@ApplicationID, @UserName, LOWER(@UserName), @PasswordFormat, @PasswordSalt, 
		@Password, @Email, LOWER(@Email), @PasswordQuestion, @PasswordAnswer, @IsApproved, 
		@IsAnonymous, @IsAdministrator, @Culture, @Comment, @CreationDate, 
		@CreationDate, @CreationDate, @MinDate, 0, @MinDate, 0, @MinDate, 0, @MinDate, GetDate())

	SELECT @@Identity AS [ID]

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteApplication]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteApplication]
(	@ID INT
)
AS
	SET NOCOUNT ON
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetApplicationUpdated] @ID

	DELETE	[TemplateComponents]
	WHERE	[TemplateID] IN
	(	SELECT	[ID]
		FROM	[Templates]
		WHERE	[ApplicationID] = @ID
	)
	
	DELETE	[TemplateFunctions]
	WHERE	[TemplateID] IN
	(	SELECT	[ID]
		FROM	[Templates]
		WHERE	[ApplicationID] = @ID
	)
	
	DELETE	[Templates]
	WHERE	[ApplicationID] = @ID
	
	DELETE	[RoleComponents]
	WHERE	[RoleID] IN
	(	SELECT	[ID]
		FROM	[Roles]
		WHERE	[ApplicationID] = @ID
	)
	
	DELETE	[RoleFunctions]
	WHERE	[RoleID] IN
	(	SELECT	[ID]
		FROM	[Roles]
		WHERE	[ApplicationID] = @ID
	)

	DELETE	[UserRoles]
	WHERE	[RoleID] IN
	(	SELECT	[ID]
		FROM	[Roles]
		WHERE	[ApplicationID] = @ID
	)

	DELETE	[Roles]
	WHERE	[ApplicationID] = @ID

	DELETE	[Modules]
	WHERE	[ApplicationID] = @ID

	DELETE	[Users]
	WHERE	[ApplicationID] = @ID

	SET NOCOUNT OFF
	DELETE FROM [Applications]
	WHERE [ID] = @ID

	COMMIT TRANSACTION



GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteModule]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteModule]
	@ID AS INT
AS
BEGIN
	DELETE FROM	[Modules]
	WHERE	[ID] = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteRestriction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteRestriction]
(	@ID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRestrictionUpdated] @ID

	DELETE	[RoleComponents]
	WHERE	[ID] = @ID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteRole]
(	
	@ID AS INT
)
AS
	SET NOCOUNT ON
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRoleUpdated] @ID

	DELETE	[RoleComponents]
	WHERE	[RoleID] = @ID

	DELETE	[RoleFunctions]
	WHERE	[RoleID] = @ID

	DELETE	[UserRoles]
	WHERE	[RoleID] = @ID

	SET NOCOUNT OFF
	DELETE	[Roles]
	WHERE	[ID] = @ID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteRoleFunction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteRoleFunction]
(	@RoleID AS INT,
	@Function AS NVARCHAR(256)
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRoleUpdated] @RoleID

	DELETE [RoleFunctions]
	WHERE  [RoleID] = @RoleID AND [Function] = @Function

	COMMIT TRANSACTION

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteUser]
(	@UserID INT
)
AS
	SET NOCOUNT ON

	BEGIN TRANSACTION

	DELETE FROM UserRoles
	WHERE [UserID] = @userID

	SET NOCOUNT OFF
	DELETE FROM Users
	WHERE [ID] = @UserID

	COMMIT TRANSACTION

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.DeleteUserRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.DeleteUserRole]
(	
	@UserID AS INT,
	@RoleID AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetUserUpdated] @userID

	DELETE	[UserRoles]
	WHERE	[UserID] = @UserID AND [RoleID] = @RoleID

	COMMIT TRANSACTION
	

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetAllModuleRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetAllModuleRestrictions]
(	
	@ModuleID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	CR.[ID], CR.RoleID, R.RoleName, CR.ModuleID, M.ModuleName, 
			CR.ViewName, CR.ComponentName, CR.[Status]
	FROM	[RoleComponents] CR
	JOIN	[Roles] R ON R.[ID] = CR.RoleID
	JOIN	[Modules] M ON M.[ID] = CR.ModuleID
	WHERE	[ModuleID] = @ModuleID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetAllViewRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetAllViewRestrictions]
(	
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	CR.[ID], CR.RoleID, R.RoleName, CR.ModuleID, M.ModuleName, 
			CR.ViewName, CR.ComponentName, CR.[Status]
	FROM	[RoleComponents] CR
	JOIN	[Roles] R ON R.[ID] = CR.RoleID
	JOIN	[Modules] M ON M.[ID] = CR.ModuleID
	WHERE	[ModuleID] = @ModuleID AND [ViewName] = @ViewName

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplicationByID]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplicationByID]
(	@ApplicationID INT
)
AS
	SET NOCOUNT ON

	SELECT *
	FROM Applications
	WHERE [ID] = @ApplicationID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplicationByName]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplicationByName]
(	
	@ApplicationName NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT *
	FROM Applications
	WHERE [ApplicationName] = @ApplicationName


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplicationFunctions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplicationFunctions]
(	@ApplicationID AS INT
)
AS
	SET NOCOUNT ON

	SELECT DISTINCT [Function]
	FROM	[RoleFunctions]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleFunctions].[RoleID]
	INNER JOIN [UserRoles] ON [Roles].[ID] = [UserRoles].[RoleID]
	INNER JOIN [Users] ON [Users].[ID] = [UserRoles].[UserID]
	WHERE	[Users].[ApplicationID] = @ApplicationID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplicationID]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplicationID]
(	@ApplicationName NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT [ID]
	FROM Applications
	WHERE ApplicationName = @ApplicationName

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplicationRoleNames]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplicationRoleNames]
(	
	@ApplicationID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	[RoleName]
	FROM	[Roles]
	WHERE	[Roles].[ApplicationID] = @ApplicationID



GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplicationRoles]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplicationRoles]
(	
	@ApplicationID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	*
	FROM	[Roles]
	WHERE	[Roles].[ApplicationID] = @ApplicationID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetApplications]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetApplications]
AS
	SET NOCOUNT ON

	SELECT *
	FROM Applications

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetModule]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetModule]
	@ID AS INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[ID], [ModuleName], [AssemblyName], [AccessQuery], [AdministrationQuery], [Title],
			[Description], [URL] 
	FROM	[Modules]
	WHERE	[ID] = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetModuleID]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetModuleID]
	@ApplicationID AS INT,
	@ModuleName AS NVARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[ID]
	FROM	[Modules]
	WHERE	[ApplicationID] = @ApplicationID AND [ModuleName] = @ModuleName
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetModuleIDByAssemblyName]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetModuleIDByAssemblyName]
	@AssemblyName AS NVARCHAR(256)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[ID]
	FROM	[Modules]
	WHERE	[AssemblyName] = @AssemblyName
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetModuleListInfo]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetModuleListInfo]
	@ApplicationID AS INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[ID], [ModuleName], [AssemblyName], [AccessQuery], [AdministrationQuery], [Title],
			[Description], [URL] 
	FROM	[Modules]
	WHERE	[ApplicationID] = @ApplicationID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetModuleRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetModuleRestrictions]
(	
	@RoleID AS INT,
	@ModuleID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	*
	FROM	[RoleComponents]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetModules]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetModules]
	@ApplicationID AS INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	[ID], [ModuleName], [AssemblyName], [AccessQuery], [AdministrationQuery]
	FROM	[Modules]
	WHERE	[ApplicationID] = @ApplicationID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetNumberOfUsersWithActivity]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetNumberOfUsersWithActivity]
(	@ApplicationID INT,
	@LastActivityDateTime DATETIME
)
AS
	SET NOCOUNT ON

	SELECT Count(*) as TotalUsers 
	FROM Users
    WHERE ApplicationID = @ApplicationID AND LastActivityDate > @LastActivityDateTime
    

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetPasswordAnswer]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetPasswordAnswer]
(	@UserID INT
)
AS
	SET NOCOUNT ON

	SELECT PasswordAnswer, IsLockedOut 
	FROM Users
    WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetPasswordAttemptsInfo]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetPasswordAttemptsInfo]
(	@UserID INT
)
AS
	SET NOCOUNT ON

	SELECT	FailedPasswordAttemptCount,
			FailedPasswordAttemptWindowStart,
			FailedPasswordAnswerAttemptCount,
            FailedPasswordAnswerAttemptWindowStart
    FROM Users
    WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetPasswordInfo]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetPasswordInfo]
(	@UserID INT
)
AS
	SET NOCOUNT ON

	SELECT	[Password], PasswordAnswer, IsLockedOut, IsApproved 
	FROM	Users
	WHERE	[ID] = @UserID
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetPasswordProperties]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetPasswordProperties]
(	@UserID INT
)
AS
	SET NOCOUNT ON

	SELECT	PasswordFormat, PasswordSalt
	FROM	Users
	WHERE	[ID] = @UserID
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRestriction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRestriction]
(	
	@RoleID AS INT,
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256),
	@ComponentName AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[RoleComponents].*, [Roles].[RoleName], [Modules].[ModuleName]
	FROM	[RoleComponents]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleComponents].[RoleID]
	INNER JOIN [Modules] ON [Modules].[ID] = [RoleComponents].[ModuleID]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID AND
			[ViewName] = @ViewName AND [ComponentName] = @ComponentName


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRestrictionID]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRestrictionID]
(	
	@RoleID AS INT,
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256),
	@ComponentName AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[ID]
	FROM	[RoleComponents]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID AND
			[ViewName] = @ViewName AND [ComponentName] = @ComponentName


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRole]
	@ID AS INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT	*
	FROM	[Roles]
	WHERE	[ID] = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRoleFunctions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRoleFunctions]
(	@RoleID AS INT
)
AS
	SET NOCOUNT ON

	SELECT DISTINCT [Function]
	FROM	[RoleFunctions]
	WHERE  [RoleID] = @RoleID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRoleID]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRoleID]
(	@ApplicationID INT,
	@RoleName NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT [ID]
	FROM Roles
	WHERE ApplicationID = @ApplicationID AND RoleName = @RoleName

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRoleRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRoleRestrictions]
(	
	@RoleID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	[RoleComponents].*, [Roles].[RoleName], [Modules].[ModuleName]
	FROM	[RoleComponents]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleComponents].[RoleID]
	INNER JOIN [Modules] ON [Modules].[ID] = [RoleComponents].[ModuleID]
	WHERE	[RoleID] = @RoleID


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRolesInFunction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRolesInFunction]
(	@Function AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[Roles].*
	FROM	[Roles] JOIN [RoleFunctions] ON [Roles].[ID]=[RoleFunctions].[RoleID]
	WHERE  [RoleFunctions].[Function] = @Function

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetRoleTimestamp]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetRoleTimestamp]
(	
	@ID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	CAST([DBTimeStamp] AS BIGINT) AS DBTimeStamp
	FROM	[Roles]
	WHERE	[ID] = @ID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUser]
(	@UserID INT
)
AS
	SET NOCOUNT ON

	SELECT	[ID], Username, [Password], FullName, Culture, Email, PasswordQuestion, 
			PasswordAnswer, Comment, IsApproved, IsLockedOut, isAdministrator,
			CreationDate, LastLoginDate, LastActivityDate, 
			LastPasswordChangedDate, LastLockedOutDate, Active
	FROM Users 
	WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserFunctions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserFunctions]
(	@UserID AS INT
)
AS
	SET NOCOUNT ON

	SELECT DISTINCT [Function]
	FROM	[RoleFunctions]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleFunctions].[RoleID]
	INNER JOIN [UserRoles] ON [Roles].[ID] = [UserRoles].[RoleID]
	WHERE	[UserRoles].[UserID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserID]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserID]
(	@ApplicationID INT,
	@UserName NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT [ID]
	FROM Users
	WHERE ApplicationID = @ApplicationID AND UserName = @UserName

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserNameByEmail]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserNameByEmail]
(	@ApplicationID INT,
	@Email NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT UserName
    FROM Users 
	WHERE ApplicationID = @ApplicationID AND Email = @Email

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserNamesInRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserNamesInRole]
(	@RoleID INT,
	@UserFilter NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[UserName]
	FROM	[Users]
	INNER JOIN [UserRoles] ON [UserRoles].[UserID] = [Users].[ID]
	WHERE	[UserRoles].[RoleID] = @RoleID
		AND	[Username] LIKE @UserFilter
	ORDER BY [UserName] ASC


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserRestrictions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserRestrictions]
(	
	@UserID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	[RoleComponents].*, [Roles].[RoleName], [Modules].[ModuleName]
	FROM	[RoleComponents]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleComponents].[RoleID]
	INNER JOIN [Modules] ON [Modules].[ID] = [RoleComponents].[ModuleID]
	WHERE	[RoleID] IN
	(
		SELECT	[RoleID]
		FROM	[UserRoles]
		WHERE	[UserID] = @UserID
	)


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserRoleNames]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserRoleNames]
(	
	@UserID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	[RoleName]
	FROM	[Roles]
	INNER JOIN [UserRoles] ON [UserRoles].[RoleID] = [Roles].[ID]
	WHERE	[UserRoles].[UserID] = @UserID



GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserRoles]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserRoles]
(	
	@UserID AS INT
)
AS
	SET NOCOUNT ON

	SELECT *
	FROM [Roles]
	INNER JOIN [UserRoles] ON [UserRoles].[RoleID] = [Roles].[ID]
	WHERE [UserRoles].[UserID] = @UserID



GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUsers]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUsers]
(	@ApplicationID INT
)
AS
	SET NOCOUNT ON

	SELECT	[ID], Username, FullName, Culture, Email, PasswordQuestion, 
			PasswordAnswer, Comment, IsApproved, IsLockedOut, isAdministrator,
			CreationDate, LastLoginDate, LastActivityDate, 
			LastPasswordChangedDate, LastLockedOutDate, Active
	FROM Users
	WHERE ApplicationID = @ApplicationID
	ORDER BY UserName ASC


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUsersByEmail]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUsersByEmail]
(	@ApplicationID INT,
	@EmailToMatch NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[ID], Username, FullName, Culture, Email, PasswordQuestion, 
			PasswordAnswer, Comment, IsApproved, IsLockedOut, isAdministrator,
			CreationDate, LastLoginDate, LastActivityDate, 
			LastPasswordChangedDate, LastLockedOutDate, Active
	FROM Users
	WHERE ApplicationID = @ApplicationID AND Email LIKE @EmailToMatch
	ORDER BY UserName ASC


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUsersByName]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUsersByName]
(	@ApplicationID INT,
	@UserNameToMatch NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[ID], Username, FullName, Culture, Email, PasswordQuestion, 
			PasswordAnswer, Comment, IsApproved, IsLockedOut, isAdministrator,
			CreationDate, LastLoginDate, LastActivityDate, 
			LastPasswordChangedDate, LastLockedOutDate, Active
	FROM Users
	WHERE ApplicationID = @ApplicationID AND UserName LIKE @UserNameToMatch
	ORDER BY UserName ASC


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUsersInRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUsersInRole]
(	@RoleID INT,
	@UserFilter NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[Users].[ID], Username, FullName, Culture, Email, PasswordQuestion, 
			PasswordAnswer, Comment, IsApproved, IsLockedOut, isAdministrator,
			CreationDate, LastLoginDate, LastActivityDate, 
			LastPasswordChangedDate, LastLockedOutDate, Active
	FROM	[Users]
	INNER JOIN [UserRoles] ON [UserRoles].[UserID] = [Users].[ID]
	WHERE	[UserRoles].[RoleID] = @RoleID
		AND	[Username] LIKE @UserFilter
	ORDER BY [UserName] ASC


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetUserTimestamp]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetUserTimestamp]
(	
	@ID AS INT
)
AS
	SET NOCOUNT ON

	SELECT	CAST([DBTimeStamp] AS BIGINT) AS DBTimeStamp
	FROM	[Users]
	WHERE	[ID] = @ID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetViewRestrictionsByRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetViewRestrictionsByRole]
(	
	@RoleID AS INT,
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[RoleComponents].*, [Roles].[RoleName], [Modules].[ModuleName]
	FROM	[RoleComponents]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleComponents].[RoleID]
	INNER JOIN [Modules] ON [Modules].[ID] = [RoleComponents].[ModuleID]
	WHERE	[RoleID] = @RoleID AND [ModuleID] = @ModuleID AND
			[ViewName] = @ViewName


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.GetViewRestrictionsByUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.GetViewRestrictionsByUser]
(	
	@UserID AS INT,
	@ModuleID AS INT,
	@ViewName AS NVARCHAR(256)
)
AS
	SET NOCOUNT ON

	SELECT	[RoleComponents].*, [Roles].[RoleName], [Modules].[ModuleName]
	FROM	[RoleComponents]
	INNER JOIN [Roles] ON [Roles].[ID] = [RoleComponents].[RoleID]
	INNER JOIN [Modules] ON [Modules].[ID] = [RoleComponents].[ModuleID]
	INNER JOIN [UserRoles] ON [UserRoles].[RoleID] = [RoleComponents].[RoleID]
	WHERE	[UserID] = @UserID AND [ModuleID] = @ModuleID AND
			[ViewName] = @ViewName


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.LockOutUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.LockOutUser]
(	@UserID INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetUserUpdated] @UserID

	UPDATE Users
	SET		IsLockedOut = 1, 
			LastLockedOutDate = GETDATE()
	WHERE [ID] = @UserID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetActive]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetActive]
(	
	@Active bit,
	@UserID INT
)
AS
	UPDATE Users
	SET Active = @Active WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetApplicationUpdated]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetApplicationUpdated]
(	@ApplicationID AS INT
)
AS
	SET NOCOUNT ON

	UPDATE	[Users]
	SET		[LastUpdated] = GetDate()
	WHERE	[ApplicationID] = @ApplicationID

	UPDATE	[Roles]
	SET		[LastUpdated] = GetDate()
	WHERE	[ApplicationID] = @ApplicationID

	UPDATE	[Applications]
	SET		[LastUpdated] = GetDate()
	WHERE	[ID] = @ApplicationID




GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetModuleUpdated]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetModuleUpdated]
(	@ModuleID AS INT
)
AS
	SET NOCOUNT ON

		UPDATE	[Modules]
		SET		[LastUpdated] = GetDate()
		WHERE	[ID] = @ModuleID


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetPassword]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetPassword]
(	@UserID INT,
	@NewPassword NVARCHAR(128)
)
AS
	UPDATE Users
    SET Password = @NewPassword, 
		LastPasswordChangedDate = GetDate()
	FROM Users
    WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetQuestionAndAnswer]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetQuestionAndAnswer]
(	@UserID INT,
	@NewPasswordQuestion NVARCHAR(256),
	@NewPasswordAnswer NVARCHAR(256)
)
AS
	UPDATE Users
    SET PasswordQuestion = @NewPasswordQuestion, 
		PasswordAnswer = @NewPasswordAnswer
	FROM Users
    WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetRestrictionUpdated]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetRestrictionUpdated]
(	@RestrictionID AS INT
)
AS
	SET NOCOUNT ON

	UPDATE	[Roles]
	SET		[LastUpdated] = GetDate()
	FROM	[Roles]
	INNER JOIN [RoleComponents] ON [RoleComponents].[RoleID] = [Roles].[ID]
	WHERE	[RoleCOmponents].[ID] = @RestrictionID

	UPDATE	[Users]
	SET		[LastUpdated] = GetDate()
	FROM	[Users]
	INNER JOIN	[UserRoles] ON [UserRoles].[userID] = [Users].[ID]
	INNER JOIN [RoleComponents] ON [RoleComponents].[RoleID] = [UserRoles].[RoleID]
	WHERE	[RoleComponents].[ID] = @RestrictionID


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetRoleUpdated]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetRoleUpdated]
(	@RoleID AS INT
)
AS
	SET NOCOUNT ON

	UPDATE	[Roles]
	SET		[LastUpdated] = GetDate()
	WHERE	[ID] = @RoleID

	UPDATE	[Users]
	SET		[LastUpdated] = GetDate()
	FROM	[Users]
	INNER JOIN [UserRoles] ON [UserRoles].[UserID] = [Users].[ID]
	WHERE	[UserID] = [UserRoles].[UserID] AND [UserRoles].[RoleID] = @RoleID


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.SetUserUpdated]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.SetUserUpdated]
(	@UserID AS INT
)
AS
	SET NOCOUNT ON

		UPDATE	[Users]
		SET		[LastUpdated] = GetDate()
		WHERE	[ID] = @UserID


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UnlockUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UnlockUser]
(	@UserID INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetUserUpdated] @UserID

	UPDATE Users
	SET IsLockedOut = 0, 
		LastLockedOutDate = GETDATE()
	WHERE [ID] = @UserID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateApplication]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateApplication]
(	@ID INT,
	@Description NVARCHAR(256),
	@EnablePasswordRetrieval BIT,
	@EnablePasswordReset BIT,
	@RequiresQuestionAndAnswer BIT,
	@RequiresUniqueEmail BIT,
	@MaxInvalidPasswordAttempts INT,
	@PasswordAttemptWindow INT,
	@PasswordFormat INT,
	@WriteExceptionsToEventLog BIT,
	@MinRequiredNonAlphanumericCharacters INT,
	@MinRequiredPasswordLength INT,
	@PasswordStrengthRegularExpression NVARCHAR(1024),
	@ValidationKey NVARCHAR(1024)
)
AS
	UPDATE Applications
	SET	[Description] = @Description, 
		EnablePasswordRetrieval = @EnablePasswordRetrieval,
		EnablePasswordReset = @EnablePasswordReset, 
		RequiresQuestionAndAnswer = @RequiresQuestionAndAnswer, 
		RequiresUniqueEmail = @RequiresUniqueEmail,
		MaxInvalidPasswordAttempts = @MaxInvalidPasswordAttempts, 
		PasswordAttemptWindow = @PasswordAttemptWindow, 
		PasswordFormat = @PasswordFormat,
		WriteExceptionsToEventLog = @WriteExceptionsToEventLog, 
		MinRequiredNonAlphanumericCharacters = @MinRequiredNonAlphanumericCharacters,
		MinRequiredPasswordLength = @MinRequiredPasswordLength, 
		PasswordStrengthRegularExpression = @PasswordStrengthRegularExpression,
		ValidationKey = @ValidationKey,
		LastUpdated = GetDate()
	WHERE [ID] = @ID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateFailedPasswordAnswerCount]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateFailedPasswordAnswerCount]
(	@UserID INT,
	@Value INT
)
AS
	UPDATE Users
    SET		FailedPasswordAnswerAttemptCount = @Value,
			FailedPasswordAnswerAttemptWindowStart = GETDATE()
	WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateFailedPasswordCount]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateFailedPasswordCount]
(	@UserID INT,
	@Value INT
)
AS
	UPDATE Users
    SET		FailedPasswordAttemptCount = @Value,
			FailedPasswordAttemptWindowStart = GETDATE()
	WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateLastLoginDateTime]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateLastLoginDateTime]
(	@UserID INT
)
AS
	UPDATE Users 
	SET LastLoginDate = GETDATE()
    WHERE [ID] = @UserID
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateModule]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateModule]
	@ID AS INT,
	@Title AS NVARCHAR(256),
	@Description AS NVARCHAR(1024),
	@URL AS NVARCHAR(1024),
	@AccessQuery AS NVARCHAR(1024),
	@AdministrationQuery AS NVARCHAR(1024)
AS
BEGIN
	UPDATE	[Modules]
	SET	 [Title] = @Title,	
		 [Description] = @Description, 
		 [URL] = @URL, 
		 [AccessQuery] = @AccessQuery,
		 [AdministrationQuery] = @AdministrationQuery,
		 [LastUpdated] = GetDate()
	WHERE	[ID] = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateModuleAccess]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Roberto Muñoz
-- Create date: 01/09/2008
-- Description:	Register a new module
-- =============================================
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateModuleAccess]
	@ID AS INT,
	@AccessQuery AS NVARCHAR(1024),
	@AdministrationQuery AS NVARCHAR(1024)
AS
BEGIN
	UPDATE	[Modules]
	SET		[AccessQuery] = @AccessQuery,
			[AdministrationQuery] = @AdministrationQuery,
			[LastUpdated] = GetDate()
	WHERE	[ID] = @ID
END

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdatePassword]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdatePassword]
(	@UserID INT,
	@Password NVARCHAR(128)
)
AS
	UPDATE Users
    SET		[Password] = @Password, 
			LastPasswordChangedDate = GETDATE()
    WHERE [ID] = @UserID AND IsLockedOut = 0

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateRestriction]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateRestriction]
(	@ID AS INT,
	@Status AS INT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetRestrictionUpdated] @ID

	UPDATE	[RoleComponents]
	SET		[Status] = @Status
	WHERE	[ID] = @ID

	COMMIT TRANSACTION


GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateRole]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateRole]
(	
	@ID AS INT,
	@Description AS NVARCHAR(1024)
)
AS
	UPDATE	[Roles]
	SET		[Description] = @Description,
			[LastUpdated] = GetDate()
	WHERE	[ID] = @ID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateUser]
(	@UserID INT,
	@Email NVARCHAR(256),
	@Comment NVARCHAR(1024),
	@IsApproved BIT
)
AS
	BEGIN TRANSACTION

	EXEC [SII.SIFP.Security.SetUserUpdated] @UserID

	UPDATE Users
    SET Email = @Email,
		Comment = @Comment,
		IsApproved = @IsApproved,
		LastUpdated = GetDate()
    WHERE [ID] = @UserID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateUserActivity]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateUserActivity]
(	@UserID INT
)
AS
	UPDATE Users
	SET LastActivityDate = GETDATE()
	WHERE [ID] = @UserID

GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.UpdateUserEntity]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.UpdateUserEntity]
(
	@UserID INT,
	@Email NVARCHAR(256),
	@Comment NVARCHAR(1024),
	@IsApproved BIT,
	@FullName NVARCHAR(256),
	@Culture NVARCHAR(16),
	@IsAdministrator BIT
)
AS
	BEGIN TRANSACTION
	UPDATE Users SET Email=@Email, Comment=@Comment, IsApproved=@IsApproved,
		FullName=@FullName, Culture=@Culture, IsAdministrator=@IsAdministrator,
		LastUpdated = GetDate()
    WHERE [ID] = @UserID

	COMMIT TRANSACTION
GO
/****** Object:  StoredProcedure [dbo].[SII.SIFP.Security.ValidateUser]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SII.SIFP.Security.ValidateUser]
(	@UserID INT,
	@Password NVARCHAR(128)
)
AS
SELECT [ID]
FROM Users
WHERE [ID] = @UserID AND [Password] = @Password AND IsLockedOut = 0 AND IsApproved = 1 AND Active = 1

GO
/****** Object:  Table [dbo].[Applications]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationName] [nvarchar](256) NOT NULL,
	[LoweredApplicationName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](256) NULL,
	[EnablePasswordRetrieval] [bit] NOT NULL,
	[EnablePasswordReset] [bit] NOT NULL,
	[RequiresQuestionAndAnswer] [bit] NOT NULL,
	[RequiresUniqueEmail] [bit] NOT NULL,
	[MaxInvalidPasswordAttempts] [int] NOT NULL,
	[PasswordAttemptWindow] [int] NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[WriteExceptionsToEventLog] [bit] NOT NULL,
	[MinRequiredNonAlphanumericCharacters] [int] NOT NULL,
	[MinRequiredPasswordLength] [int] NOT NULL,
	[PasswordStrengthRegularExpression] [nvarchar](1024) NULL,
	[ValidationKey] [nvarchar](1024) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[DBTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Command]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Command](
	[ID] [int] NOT NULL,
	[CommandName] [nvarchar](256) NOT NULL,
	[EnableQuery] [nvarchar](2048) NOT NULL,
	[VisibleQuery] [nvarchar](2048) NOT NULL,
	[LastUpdated] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_Command] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleDependencies]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleDependencies](
	[ID] [int] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[DependentGroupID] [int] NOT NULL,
 CONSTRAINT [PK_ModuleDependencies] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleGroupDetails]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleGroupDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ModuleGroupID] [int] NOT NULL,
	[ModuleID] [int] NOT NULL,
 CONSTRAINT [PK_ModuleGroupDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ModuleGroups]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[LastUpdated] [datetime] NULL,
	[DBTimeStamp] [timestamp] NOT NULL,
 CONSTRAINT [PK_ModuleGroup] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Modules]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modules](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[ModuleName] [nvarchar](256) NOT NULL,
	[LoweredModuleName] [nvarchar](256) NOT NULL,
	[Title] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[AssemblyName] [nvarchar](1024) NOT NULL,
	[URL] [nvarchar](1024) NULL,
	[AccessQuery] [nvarchar](1024) NULL,
	[AdministrationQuery] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[DBTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Modules] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleComponents]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleComponents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[ModuleID] [int] NOT NULL,
	[ViewName] [nvarchar](256) NULL,
	[ComponentName] [nvarchar](256) NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_RoleComponents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleFunctions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleFunctions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[Function] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_RoleFunctions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
	[LoweredRoleName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[DBTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TemplateComponents]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateComponents](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateID] [int] NOT NULL,
	[ModuleName] [nvarchar](256) NOT NULL,
	[ViewName] [nvarchar](256) NOT NULL,
	[ComponentName] [nvarchar](256) NOT NULL,
	[Status] [smallint] NOT NULL,
 CONSTRAINT [PK_TemplateComponents] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TemplateFunctions]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TemplateFunctions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TemplateID] [int] NOT NULL,
	[Function] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_TemplateFunctions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Templates]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[TemplateName] [nvarchar](256) NOT NULL,
	[LoweredTemplateName] [nvarchar](256) NOT NULL,
	[Description] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[DBTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_SecurityTemplate] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRoles]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ApplicationID] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[LoweredUserName] [nvarchar](256) NOT NULL,
	[FullName] [nvarchar](256) NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[MobilePIN] [nvarchar](16) NULL,
	[Email] [nvarchar](256) NULL,
	[LoweredEmail] [nvarchar](256) NULL,
	[Culture] [nvarchar](16) NULL,
	[Catalog] [nvarchar](256) NULL,
	[PasswordQuestion] [nvarchar](256) NULL,
	[PasswordAnswer] [nvarchar](256) NULL,
	[IsAdministrator] [bit] NOT NULL,
	[IsAnonymous] [bit] NOT NULL,
	[IsApproved] [bit] NOT NULL,
	[IsLockedOut] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[DeletionDate] [datetime] NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockedOutDate] [datetime] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[Comment] [nvarchar](1024) NULL,
	[LastUpdated] [datetime] NULL,
	[DBTimeStamp] [timestamp] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Version]    Script Date: 20/12/2012 10:59:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MajorVersion] [smallint] NOT NULL,
	[MinorVersion] [smallint] NOT NULL,
	[Release] [smallint] NOT NULL,
	[Status] [smallint] NOT NULL,
	[UpgradeDateTime] [datetime] NOT NULL,
	[Output] [nvarchar](max) NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Applications_ApplicationName]    Script Date: 20/12/2012 10:59:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Applications_ApplicationName] ON [dbo].[Applications]
(
	[ApplicationName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_CommandName]    Script Date: 20/12/2012 10:59:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_CommandName] ON [dbo].[Command]
(
	[CommandName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_ModuleName]    Script Date: 20/12/2012 10:59:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_ModuleName] ON [dbo].[Modules]
(
	[ModuleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Roles_RoleName]    Script Date: 20/12/2012 10:59:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Roles_RoleName] ON [dbo].[Roles]
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 20/12/2012 10:59:52 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_UserName] ON [dbo].[Users]
(
	[ApplicationID] ASC,
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_EnablePasswordRetrieval]  DEFAULT ((0)) FOR [EnablePasswordRetrieval]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_EnablePasswordReset]  DEFAULT ((0)) FOR [EnablePasswordReset]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_RequiresQuestionAndAnswer]  DEFAULT ((0)) FOR [RequiresQuestionAndAnswer]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_RequiresUniqueEmail]  DEFAULT ((0)) FOR [RequiresUniqueEmail]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_MaxInvalidPasswordAttempts]  DEFAULT ((0)) FOR [MaxInvalidPasswordAttempts]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_PasswordAttemptWindow]  DEFAULT ((0)) FOR [PasswordAttemptWindow]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_WriteExceptionsToEventLog]  DEFAULT ((0)) FOR [WriteExceptionsToEventLog]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_MinRequiredNonAlphanumericalCharacters]  DEFAULT ((0)) FOR [MinRequiredNonAlphanumericCharacters]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_MinRequiredPasswordLength]  DEFAULT ((0)) FOR [MinRequiredPasswordLength]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_PasswordStrengthRegularExpression]  DEFAULT ('') FOR [PasswordStrengthRegularExpression]
GO
ALTER TABLE [dbo].[Applications] ADD  CONSTRAINT [DF_Applications_ValidationKey]  DEFAULT ('') FOR [ValidationKey]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsAdministrator]  DEFAULT ((0)) FOR [IsAdministrator]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsApproved]  DEFAULT ((0)) FOR [IsApproved]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsLockedOut]  DEFAULT ((0)) FOR [IsLockedOut]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_FailedPasswordAttemptCount]  DEFAULT ((0)) FOR [FailedPasswordAttemptCount]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_FailedPasswordAnswerAttemptCount]  DEFAULT ((0)) FOR [FailedPasswordAnswerAttemptCount]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Modules]  WITH CHECK ADD  CONSTRAINT [FK_Modules_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Modules] CHECK CONSTRAINT [FK_Modules_Applications]
GO
ALTER TABLE [dbo].[RoleComponents]  WITH CHECK ADD  CONSTRAINT [FK_RoleComponents_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[RoleComponents] CHECK CONSTRAINT [FK_RoleComponents_Roles]
GO
ALTER TABLE [dbo].[RoleFunctions]  WITH CHECK ADD  CONSTRAINT [FK_RoleFunctions_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[RoleFunctions] CHECK CONSTRAINT [FK_RoleFunctions_Roles]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Applications]
GO
ALTER TABLE [dbo].[TemplateComponents]  WITH CHECK ADD  CONSTRAINT [FK_TemplateComponents_Templates] FOREIGN KEY([TemplateID])
REFERENCES [dbo].[Templates] ([ID])
GO
ALTER TABLE [dbo].[TemplateComponents] CHECK CONSTRAINT [FK_TemplateComponents_Templates]
GO
ALTER TABLE [dbo].[TemplateFunctions]  WITH CHECK ADD  CONSTRAINT [FK_TemplateFunctions_Templates] FOREIGN KEY([TemplateID])
REFERENCES [dbo].[Templates] ([ID])
GO
ALTER TABLE [dbo].[TemplateFunctions] CHECK CONSTRAINT [FK_TemplateFunctions_Templates]
GO
ALTER TABLE [dbo].[Templates]  WITH CHECK ADD  CONSTRAINT [FK_Templates_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Templates] CHECK CONSTRAINT [FK_Templates_Applications]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Roles]
GO
ALTER TABLE [dbo].[UserRoles]  WITH CHECK ADD  CONSTRAINT [FK_UserRoles_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[UserRoles] CHECK CONSTRAINT [FK_UserRoles_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Applications] FOREIGN KEY([ApplicationID])
REFERENCES [dbo].[Applications] ([ID])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Applications]
GO
USE [master]
GO
ALTER DATABASE [SIFPSecurity] SET  READ_WRITE 
GO

/* ------------------- DATA -------------------- */
SET IDENTITY_INSERT [dbo].[Applications] ON 
INSERT [dbo].[Applications] ([ID], [ApplicationName], [LoweredApplicationName], [Description], [EnablePasswordRetrieval], [EnablePasswordReset], [RequiresQuestionAndAnswer], [RequiresUniqueEmail], [MaxInvalidPasswordAttempts], [PasswordAttemptWindow], [PasswordFormat], [WriteExceptionsToEventLog], [MinRequiredNonAlphanumericCharacters], [MinRequiredPasswordLength], [PasswordStrengthRegularExpression], [ValidationKey], [LastUpdated]) VALUES (1, N'HCD', N'hcd', N'Health Care Dynamics', 1, 1, 1, 1, 5, 30, 1, 1, 0, 5, N' ', N'HCD', CAST(0x00009F6E00D561EF AS DateTime))
SET IDENTITY_INSERT [dbo].[Applications] OFF

SET IDENTITY_INSERT [dbo].[Users] ON 
INSERT [dbo].[Users] ([ID], [ApplicationID], [UserName], [LoweredUserName], [FullName], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [Culture], [Catalog], [PasswordQuestion], [PasswordAnswer], [IsAdministrator], [IsAnonymous], [IsApproved], [IsLockedOut], [CreationDate], [DeletionDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockedOutDate], [LastActivityDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Active], [Comment], [LastUpdated]) VALUES (28, 1, N'Administrador', N'administrador', N'', N'0qPfBoHla7aKsHVFE5zR/EVq6m0=', 1, N'6XzZ/cKXVMT8qEh/PUYg/w==', NULL, N'soporte@sistemasinfo.com', N'soporte@sistemasinfo.com', N'', NULL, N'XXMYgon/iGBitIrj3o+vNQmLvlA=', N'OJaJHzgksLaMQ8kx4Q0UVksr/Yg=', 1, 0, 1, 0, CAST(0x00009A8600F4017E AS DateTime), NULL, CAST(0x0000A0C101257925 AS DateTime), CAST(0x00009F6E00B7588C AS DateTime), CAST(0x0000000000000000 AS DateTime), CAST(0x00009A8600F4017E AS DateTime), 1, CAST(0x00009D8300B501C7 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime), 1, N'', CAST(0x00009FA500C9B952 AS DateTime))
INSERT [dbo].[Users] ([ID], [ApplicationID], [UserName], [LoweredUserName], [FullName], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [Culture], [Catalog], [PasswordQuestion], [PasswordAnswer], [IsAdministrator], [IsAnonymous], [IsApproved], [IsLockedOut], [CreationDate], [DeletionDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockedOutDate], [LastActivityDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Active], [Comment], [LastUpdated]) VALUES (28, 2, N'Designer', N'designer', N'', N'BdXErQ2kurH1qKaqHlJAWZ/conA=', 1, N'rPE1fx0GV91YfbJtPptP0w==', NULL, N'soporte@sistemasinfo.com', N'soporte@sistemasinfo.com', N'', NULL, N'4Kde3Q9OrseL1s3B6hEtg6xlg5k=', N'C2tVZIhAL+auvSDlx9B11Gebjm4=', 1, 0, 1, 0, CAST(0x00009A8600F4017E AS DateTime), NULL, CAST(0x0000A0C101257925 AS DateTime), CAST(0x00009F6E00B7588C AS DateTime), CAST(0x0000000000000000 AS DateTime), CAST(0x00009A8600F4017E AS DateTime), 1, CAST(0x00009D8300B501C7 AS DateTime), 0, CAST(0x0000000000000000 AS DateTime), 1, N'', CAST(0x00009FA500C9B952 AS DateTime))
SET IDENTITY_INSERT [dbo].[Users] OFF

SET IDENTITY_INSERT [dbo].[Modules] ON 
INSERT [dbo].[Modules] ([ID], [ApplicationID], [ModuleName], [LoweredModuleName], [Title], [Description], [AssemblyName], [URL], [AccessQuery], [AdministrationQuery], [LastUpdated]) VALUES (3, 1, N'SecurityModule', N'securitymodule', N'Módulo de seguridad', N'Módulo de seguridad', N'SII.SecurityModule.dll', NULL, N'F:SecurityModuleAccess', N'F:SecurityModuleAdministration', CAST(0x00009C2A011BF0A7 AS DateTime))
INSERT [dbo].[Modules] ([ID], [ApplicationID], [ModuleName], [LoweredModuleName], [Title], [Description], [AssemblyName], [URL], [AccessQuery], [AdministrationQuery], [LastUpdated]) VALUES (12, 1, N'SII.HCD.BackOfficeModule', N'backofficemodule', N'Módulo de Procesos de BackOffice', N'Módulo de Procesos de BackOffice', N'SII.HCD.BackOfficeModule.dll', NULL, N'F:BackOfficeModuleAccess', N'F:BackOfficeModuleAccess', CAST(0x00009CF100000000 AS DateTime))
INSERT [dbo].[Modules] ([ID], [ApplicationID], [ModuleName], [LoweredModuleName], [Title], [Description], [AssemblyName], [URL], [AccessQuery], [AdministrationQuery], [LastUpdated]) VALUES (14, 1, N'SII.HCD.AdministrativeModule', N'administrativemodule', N'Módulo de Procesos Administrativos', N'Módulo de Procesos Administrativos', N'SII.HCD.AdministrativeModule.dll', NULL, N'F:AdministrativeModuleAccess', N'F:AdministrativeModuleAccess', CAST(0x00009F6E00CD81ED AS DateTime))
INSERT [dbo].[Modules] ([ID], [ApplicationID], [ModuleName], [LoweredModuleName], [Title], [Description], [AssemblyName], [URL], [AccessQuery], [AdministrationQuery], [LastUpdated]) VALUES (15, 1, N'SII.HCD.AssistanceModule', N'assistancemodule', N'Módulo de Procesos Asistenciales', N'Módulo de Procesos Asistenciales', N'SII.HCD.AssistanceModule.dll', NULL, N'F:AssistanceModuleAccess', N'F:AssistanceModuleAccess', CAST(0x00009CF100000000 AS DateTime))
INSERT [dbo].[Modules] ([ID], [ApplicationID], [ModuleName], [LoweredModuleName], [Title], [Description], [AssemblyName], [URL], [AccessQuery], [AdministrationQuery], [LastUpdated]) VALUES (16, 1, N'SII.HCD.ProtocolsModule', N'protocolsmodule', N'Módulo de Procesos Protocolares', N'Módulo de Procesos Protocolares', N'SII.HCD.ProtocolsModule.dll', NULL, N'F:ProtocolsModuleAccess', N'F:ProtocolsModuleAccess', CAST(0x00009CF100000000 AS DateTime))
SET IDENTITY_INSERT [dbo].[Modules] OFF

SET IDENTITY_INSERT [dbo].[Version] ON 
INSERT [dbo].[Version] ([ID], [MajorVersion], [MinorVersion], [Release], [Status], [UpgradeDateTime], [Output]) VALUES (2, 0, 0, 0, 0, CAST(0x00009F7F012DE4CE AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[Version] OFF
