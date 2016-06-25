-- 客户分级设置表
create table ClientLevel (
   LevelId              nvarchar(36)         not null,
   LevelName            nvarchar(50)         not null,
   LevelType            nvarchar(50)         not null,
   LevelMax             decimal(20,8)        not null,
   LevelMin             decimal(20,8)        not null,
   constraint PK_CLIENTLEVEL primary key (LevelId)
);