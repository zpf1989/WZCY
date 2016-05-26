--建表
--部门
CREATE TABLE [dbo].[DemoDepartment](
	[DeptId] [varchar](36) NOT NULL,
	[DeptCode] [varchar](50) NOT NULL,
	[DeptName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DemoDepartment] PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

--职员
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

--插入部门数据
insert into dbo.DemoDepartment(DeptId,DeptCode,DeptName) values('27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','HR','人事部')
insert into dbo.DemoDepartment(DeptId,DeptCode,DeptName) values('65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','Dev','研发部')
insert into dbo.DemoDepartment(DeptId,DeptCode,DeptName) values('60779B4C-C2A2-46D6-B959-00F01DF65B5A','Purchase','采购部')

--插入职员数据
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs01','张三01','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls01','李四01','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww01','王五01','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs02','张三02','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls02','李四02','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww02','王五02','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs03','张三03','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls03','李四03','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww03','王五03','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs04','张三04','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls04','李四04','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww04','王五04','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')


INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs05','张三05','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls05','李四05','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww05','王五05','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')


INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs06','张三06','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls06','李四06','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww06','王五06','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')

INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'zs07','张三07','1',20,CONVERT(varchar(100), DATEADD(YEAR,-20,GETDATE()), 23),3334.34,'27900AC3-D6E1-4D16-BC35-D0A8E4D4F1EB','人事部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ls07','李四07','1',30,CONVERT(varchar(100), DATEADD(YEAR,-30,GETDATE()), 23),4334.34,'65088E37-2CB5-4FA3-9DE1-41A75AD13BC7','研发部')
INSERT INTO DemoEmployee([EmpId],[EmpCode],[EmpName],[EmpGender],[EmpAge],[EmpBirthDay],[EmpSalary],[DeptId],[DeptName])
VALUES(NEWID(),'ww07','王五07','1',40,CONVERT(varchar(100), DATEADD(YEAR,-40,GETDATE()), 23),5000,'60779B4C-C2A2-46D6-B959-00F01DF65B5A','采购部')
