create Procedure Proc_GetDataByPage 
 @tableName varchar(2000),        --����
 @pk varchar(50),				 --��������
 @fields varchar(2000) = '*',    --�ֶ���(ȫ���ֶ�Ϊ*)
 @orderBySql varchar(1000),        --�����ֶ�(����!֧�ֶ��ֶΣ����Ϊ�գ���ȡ@pk)
 @whereSql varchar(1000),--�������(���ü�where)
 @pageSize int,                    --ÿҳ��������¼
 @pageIndex int = 1 output,            --ָ����ǰΪ�ڼ�ҳ
 @totalRecord int output		--�ܼ�¼��
as
begin
    declare @_sql nvarchar(4000);
    declare @_totalRecord int;    
	declare @_totalPage int;
    --�����ܼ�¼��
    if (@whereSql is NULL or @whereSql='')
        set @_sql = 'select @_totalRecord = count(*) from ' + @tableName
    else
        set @_sql = 'select @_totalRecord = count(*) from ' + @tableName + ' where 1=1 ' + @whereSql

    exec sp_executesql @_sql,N'@_totalRecord int output',@_totalRecord output--�����ܼ�¼��       
    set @totalRecord=@_totalRecord
    --������ҳ��
    set @_totalPage=ceiling((@_totalRecord+0.0)/@pageSize)
	if (@orderBySql is null or @orderBySql = '')
		set @orderBySql=@pk
    if (@whereSql is NULL or @whereSql='')
        set @_sql = 'Select t.* FROM (select ROW_NUMBER() Over(order by ' + @orderBySql + ') as rowId,' + @fields + ' from ' + @tableName  
    else
        set @_sql = 'Select t.* FROM (select ROW_NUMBER() Over(order by ' + @orderBySql + ') as rowId,' + @fields + ' from ' + @tableName + ' where 1=1 ' + @whereSql  
        
    
    --����ҳ����������Χ���
    if @pageIndex<=0 
        set @pageIndex = 1
    
    if @pageIndex>@_totalPage
        set @pageIndex = @_totalPage

     --����ʼ��ͽ�����
    declare @start int
    declare @end int
    
    set @start = (@pageIndex-1)*@pageSize + 1
	if @start <=0
		set @start=1
    set @end = @start + @pageSize - 1
	if @end <=0
		set @end=1

    --�����ϳ�sql���
    set @_sql = @_sql + ') as t where rowId between ' + Convert(varchar(50),@start) + ' and ' +  Convert(varchar(50),@end)
    --print @_sql
    exec(@_sql)

--    print @totalRecord 
    if @@Error <> 0
        return -1
     else
        --print @totalRecord 
        return @totalRecord ---���ؼ�¼���� 
end


