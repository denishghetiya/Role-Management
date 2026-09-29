--ALTER TABLE [RolePermission] DROP CONSTRAINT fk_RolePermission_UserId_Users_UserId;
--ALTER TABLE [RolePermission] DROP CONSTRAINT fk_RolePermission_PageId_PageNames_PageId;
--truncate table RolePermission
--truncate table Users

--ALTER TABLE [RolePermission]
--ADD CONSTRAINT fk_RolePermission_UserId_Users_UserId FOREIGN KEY (UserId)
--REFERENCES Users(UserId);

--ALTER TABLE [RolePermission]
--ADD CONSTRAINT fk_RolePermission_PageId_PageNames_PageId FOREIGN KEY (PageId)
--REFERENCES PageNames(PageId);


select * from [dbo].[Users]
select * from [dbo].[RolePermission]

select * from [dbo].[PageNames]

select * from [dbo].[UserRoles]
select * from [dbo].[StudentList]
select * from [dbo].[EmployeeList]
select * from [dbo].[CustomerList]