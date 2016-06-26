-- �ͻ��ּ����ñ�
create table ClientLevel (
   LevelId              nvarchar(36)         not null,
   LevelName            nvarchar(50)         not null,
   LevelType            nvarchar(50)         not null,
   LevelMax             decimal(20,8)        not null,
   LevelMin             decimal(20,8)        not null,
   constraint PK_CLIENTLEVEL primary key (LevelId)
);

-- �ͻ��ּ���¼��
create table ClientClassification (
   Id                   nvarchar(36)         not null,
   Amount               decimal(20,8)        null,
   ClientName           nvarchar(255)        not null,
   LevelName            nvarchar(50)         not null,
   LevelTypeName        nvarchar(50)         null,
   constraint PK_CLIENTCLASSIFICATION primary key (Id)
);