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