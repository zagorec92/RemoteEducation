USE [EEducation]
GO

/****** Object:  View [EEducation].[vUser]    Script Date: 26.9.2015. 16:18:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [EEducation].[vUser]
AS
SELECT
	EU.Identifier, 
	EU.SecurityCode, 
	EUD.FirstName, 
	EUD.LastName, 
	EUD.Email, 
	EUD.DateOfBirth, 
	EUD.Gender
FROM 
	EEducation.Users AS EU
INNER JOIN	EEducation.UserDetails AS EUD
	ON EUD.ID = EU.UserDetailsID


GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'EEducation', @level1type=N'VIEW',@level1name=N'vUser'
GO


