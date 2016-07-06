/*============================1��ѯ�۵�����AskPrice============================*/
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

sp_addextendedproperty N'MS_Description', N'ѯ�۵����͡�OffsetPrint����ӡ�²�Ʒ��SilkScreen��˿ӡ�²�Ʒ', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'APType'
GO

sp_addextendedproperty N'MS_Description', N'�������', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'TrackDescription'
GO

sp_addextendedproperty N'MS_Description', N'�ͻ�����', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'ClientSurvey'
GO

sp_addextendedproperty N'MS_Description', N'1:����
   2:�ύ����
   3:����ͨ��
   4:����ͨ��
   5:�ύ����  
   6:����ͨ��
   7:����ͨ��
   8:�ر�', 'User', N'dbo', 'TABLE', N'AskPrice', 'COLUMN', N'State'
GO


/*=========================2��ѯ�۵��ӱ�AskPriceItem========================*/
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
   Routing              varchar(100)        null,
   PlanPrice            decimal(18,2)        null,
   Qty                  decimal(18,2)        null,
   UnitID               varchar(36)          null,
   ActualPrice          decimal(18,2)        null,
   IsTax                char(1)              null,
   IsShipping           char(1)              null,
   constraint PK_ASKPRICEITEM primary key (APItemID)
)
go

sp_addextendedproperty N'MS_Description', N'�۸�', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'PlanPrice'
GO

sp_addextendedproperty N'MS_Description', N'����', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'Qty'
GO

sp_addextendedproperty N'MS_Description', N'���', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'ActualPrice'
GO

sp_addextendedproperty N'MS_Description', N'�Ƿ�˰', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'IsTax'
GO

sp_addextendedproperty N'MS_Description', N'�Ƿ��˷�', 'User', N'dbo', 'TABLE', N'AskPriceItem', 'COLUMN', N'IsShipping'
GO
