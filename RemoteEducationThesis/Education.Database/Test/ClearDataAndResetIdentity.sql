/****** Script for SelectTopNRows command from SSMS  ******/
SELECT * FROM [EEducation].EEducation.Answers
SELECT * FROM [EEducation].EEducation.Logs
SELECT * FROM [EEducation].EEducation.Questions
SELECT * FROM [EEducation].EEducation.ERole
SELECT * FROM [EEducation].EEducation.ELogType
SELECT * FROM [EEducation].EEducation.RoleUsers
SELECT * FROM [EEducation].EEducation.ScoreLogs
SELECT * FROM [EEducation].EEducation.ServerInfo
SELECT * FROM [EEducation].EEducation.UserDetails
SELECT * FROM [EEducation].EEducation.Users
SELECT * FROM [EEducation].EEducation.Subjects
SELECT * FROM [EEducation].EEducation.EAttachmentType
SELECT * FROM [EEducation].EEducation.SubjectAttachments

/*

DELETE FROM [EEducation].EEducation.Answers
DELETE FROM [EEducation].EEducation.Logs
DELETE FROM [EEducation].EEducation.Questions
DELETE FROM [EEducation].EEducation.ERole
DELETE FROM [EEducation].EEducation.ELogType
DELETE FROM [EEducation].EEducation.RoleUsers
DELETE FROM [EEducation].EEducation.ScoreLogs
DELETE FROM [EEducation].EEducation.ServerInfo
DELETE FROM [EEducation].EEducation.Users
DELETE FROM [EEducation].EEducation.UserDetails
DELETE FROM [EEducation].EEducation.Subjects
DELETE FROM [EEducation].EEducation.EAttachmentType
DELETE FROM [EEducation].EEducation.SubjectAttachments

DBCC CHECKIDENT('EEducation.Answers', RESEED, 0)
DBCC CHECKIDENT('EEducation.Logs', RESEED, 0)
DBCC CHECKIDENT('EEducation.Questions', RESEED, 0)
DBCC CHECKIDENT('EEducation.ERole', RESEED, 0)
DBCC CHECKIDENT('EEducation.ELogType', RESEED, 0)
DBCC CHECKIDENT('EEducation.ScoreLogs', RESEED, 0)
DBCC CHECKIDENT('EEducation.ServerInfo', RESEED, 0)
DBCC CHECKIDENT('EEducation.UserDetails', RESEED, 0)
DBCC CHECKIDENT('EEducation.Users', RESEED, 0)
DBCC CHECKIDENT('EEducation.Subjects', RESEED, 0)
DBCC CHECKIDENT('EEducation.EAttachmentType', RESEED, 0)
DBCC CHECKIDENT('[EEducation].EEducation.SubjectAttachments', RESEED, 0)


RemoteDesktopThesisServer
*/
