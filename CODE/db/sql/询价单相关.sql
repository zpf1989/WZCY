/*============================1、询价单主表AskPrice============================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('AskPrice')
            and   type = 'U')
   drop table AskPrice
go

create table AskPrice (
   APID                 varchar(36)          not null,
   APCode               varchar(30)          not null,
   APType               varchar(20)          null,
   AskDate              datetime             not null,
   ClientID             varchar(36)          not null,
   PayTypeID            varchar(36)          null,
   TrackDescription     varchar(1024)        null,
   ClientSurvey         varchar(1024)        null,
   APRemark             varchar(1024)        null,
   Creator              varchar(36)          null,
   CreateTime           datetime             null,
   Editor               varchar(36)          null,
   EditTime             datetime             null,
   FirstChecker         varchar(36)          null,
   FirstCheckTime       datetime             null,
   FirstCheckView       varchar(255)         null,
   State                char(1)              not null default '1',
   SecondCheckerName    varchar(255)         null,
   ReaderName           varchar(255)         null,
   constraint PK_ASKPRICE primary key (APID)
)
go

sp_addextendedproperty N'MS_Description', N'询价单类型。OffsetPrint：胶印新产品；SilkScreen：丝印新产品', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'APType'
GO

sp_addextendedproperty N'MS_Description', N'跟踪情况', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'TrackDescription'
GO

sp_addextendedproperty N'MS_Description', N'客户调查', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'ClientSurvey'
GO

sp_addextendedproperty N'MS_Description', N'1:编制
   2:提交初审
   3:初审通过
   4:初审不通过
   5:提交复审  
   6:复审通过
   7:复审不通过
   8:关闭', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'State'
GO


/*=========================2、询价单从表AskPriceItem========================*/
if exists (select 1
            from  sysobjects
           where  id = object_id('AskPriceItem')
            and   type = 'U')
   drop table AskPriceItem
go

create table AskPriceItem (
   APItemID             varchar(36)          not null,
   APID                 varchar(36)          not null,
   MaterialID           varchar(36)          not null,
   Routing              varchar(1024)        null,
   PlanPrice            decimal(18,2)        null,
   Qty                  decimal(18,2)        null,
   UnitID               varchar(36)          null,
   ActualPrice          decimal(18,2)        null,
   IsTax                char(1)              null,
   IsShipping           char(1)              null,
   constraint PK_ASKPRICEITEM primary key (APItemID)
)
go

sp_addextendedproperty N'MS_Description', N'价格', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'PlanPrice'
GO

sp_addextendedproperty N'MS_Description', N'数量', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'Qty'
GO

sp_addextendedproperty N'MS_Description', N'金额', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'ActualPrice'
GO

sp_addextendedproperty N'MS_Description', N'是否含税', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'IsTax'
GO

sp_addextendedproperty N'MS_Description', N'是否含运费', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'IsShipping'
GO
