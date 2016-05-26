create Procedure Proc_GetDataByPage 
 @tableName varchar(250),        --表名
 @pk varchar(50),				 --主键名称
 @fields varchar(1000) = '*',    --字段名(全部字段为*)
 @orderBySql varchar(1000),        --排序字段(必须!支持多字段，如果为空，则取@pk)
 @whereSql varchar(1000),--条件语句(不用加where)
 @pageSize int,                    --每页多少条记录
 @pageIndex int = 1 output,            --指定当前为第几页
 @totalRecord int output		--总记录数
as
begin
    declare @_sql nvarchar(4000);
    declare @_totalRecord int;    
	declare @_totalPage int;
    --计算总记录数
    if (@whereSql is NULL or @whereSql='')
        set @_sql = 'select @_totalRecord = count(*) from ' + @tableName
    else
        set @_sql = 'select @_totalRecord = count(*) from ' + @tableName + ' where 1=1 ' + @whereSql

    exec sp_executesql @_sql,N'@_totalRecord int output',@_totalRecord output--计算总记录数       
    set @totalRecord=@_totalRecord
    --计算总页数
    set @_totalPage=ceiling((@_totalRecord+0.0)/@pageSize)
	if (@orderBySql is null or @orderBySql = '')
		set @orderBySql=@pk
    if (@whereSql is NULL or @whereSql='')
        set @_sql = 'Select t.* FROM (select ROW_NUMBER() Over(order by ' + @orderBySql + ') as rowId,' + @fields + ' from ' + @tableName  
    else
        set @_sql = 'Select t.* FROM (select ROW_NUMBER() Over(order by ' + @orderBySql + ') as rowId,' + @fields + ' from ' + @tableName + ' where 1=1 ' + @whereSql  
        
    
    --处理页索引超出范围情况
    if @pageIndex<=0 
        set @pageIndex = 1
    
    if @pageIndex>@_totalPage
        set @pageIndex = @_totalPage

     --处理开始点和结束点
    declare @start int
    declare @end int
    
    set @start = (@pageIndex-1)*@pageSize + 1
	if @start <=0
		set @start=1
    set @end = @start + @pageSize - 1
	if @end <=0
		set @end=1

    --继续合成sql语句
    set @_sql = @_sql + ') as t where rowId between ' + Convert(varchar(50),@start) + ' and ' +  Convert(varchar(50),@end)
    --print @_sql
    exec(@_sql)

--    print @totalRecord 
    if @@Error <> 0
        return -1
     else
        --print @totalRecord 
        return @totalRecord ---返回记录总数 
end


