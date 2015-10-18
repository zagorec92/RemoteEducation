USE [EEducation]
GO

/****** Object:  View [EEducation].[vLogUser]    Script Date: 26.9.2015. 16:25:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [EEducation].[vLogUser]
AS
SELECT
	EL.[Message],
	EL.DateCreated,
	ELT.Name,
	EUD.FirstName, 
	EUD.LastName
FROM 
	[EEducation].[EEducation].[Logs] AS EL
INNER JOIN EEducation.EEducation.ELogType AS ELT
	ON ELT.ID = EL.LogTypeID
INNER JOIN EEducation.EEducation.Users AS EU
	ON EU.Identifier = EL.UserIdentifier
INNER JOIN EEducation.EEducation.UserDetails AS EUD
	ON EUD.ID = EU.UserDetailsID



GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'EEducation', @level1type=N'VIEW',@level1name=N'vLogUser'
GO


