--����
--����
CREATE TABLE [dbo].[DemoDepartment](
	[DeptId] [varchar](36) NOT NULL,
	[DeptCode] [varchar](50) NOT NULL,
	[DeptName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DemoDepartment] PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

--ְԱ
CREATE TABLE [dbo].[DemoEmployee](
	[EmpId] [varchar](36) NOT NULL,
	[EmpCode] [varchar](50) NOT NULL,
	[EmpName] [varchar](100) NOT NULL,
	[EmpGender] [char](1) NULL,
	[EmpAge] [int] NULL,
	[EmpBirthDay] [date] NULL,
	[EmpSalary] [decimal](8, 2) NULL,
	[DeptId] [varchar](36) NULL,
	[DeptName] [varchar](50) NULL,
 CONSTRAINT [PK_DemoEmployee] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

--���벿������
insert into dbo.DemoDepartment(DeptId,DeptCode,DeptName) values('27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','HR','���²�')
insert into dbo.DemoDepartment(DeptId,DeptCode,DeptName) values('65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','Dev','�з���')
insert into dbo.DemoDepartment(DeptId,DeptCode,DeptName) values('60779B4C-C2A2-46D6-B959-00F01DF65B5A','Purchase','�ɹ���')

--����ְԱ����
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs01','����01','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls01','����01','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww01','����01','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs02','����02','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls02','����02','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww02','����02','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs03','����03','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls03','����03','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww03','����03','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs04','����04','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls04','����04','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww04','����04','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')


INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs05','����05','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls05','����05','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww05','����05','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')


INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs06','����06','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls06','����06','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww06','����06','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs07','����07','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','���²�')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls07','����07','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','�з���')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww07','����07','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','�ɹ���')
