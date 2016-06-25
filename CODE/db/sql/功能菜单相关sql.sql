-- 更新菜单路径
-- 计量单位
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MeasureUnits/MeasureUnits.aspx' where FunID='1230';
DELETE from OA_Function where FunID in ('12300','12301','12310','12311','12320','12321','12330','12331');
-- 物料类型
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialType/MaterialType.aspx' where FunID='1231';
-- 物料分类
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialClass/MaterialClass.aspx' where FunID='1232';
-- 物料
UPDATE OA_Function set FunURL = 'OA/InventoryManage/Materials/Materials.aspx' where FunID='1233';
-- 仓库
update OA_Function set FunURL='OA/InventoryManage/WareHouse/WareHouse.aspx' where FunID='1234';
delete from OA_Function where FunID in ('12340','12341');
-- 付款方式
update OA_Function set FunURL='OA/SalesManage/PayType/PayType.aspx' where FunID='1301';
delete from OA_Function where FunID in ('13011','13012');
-- 客户
update OA_Function set FunURL='OA/SalesManage/Client/Client.aspx' where FunID='1300';
delete from OA_Function where FunID in ('13000','13001');
-- 客户分级设置
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1302','客户分级设置','130','OA/SalesManage/ClientLevel/ClientLevel.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1302');
--销售订单
insert into OA_Function(FunID,FunName,ParentFunID)values('134','销售订单','13');
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1341','销售订单管理','134','OA/SalesManage/SaleOrder/SaleOrder.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','134');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1341');






