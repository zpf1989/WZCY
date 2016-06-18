-- 更新菜单路径
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MeasureUnits/MeasureUnits.aspx' where FunID='1230';
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialType/MaterialType.aspx' where FunID='1231';
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialClass/MaterialClass.aspx' where FunID='1232';
UPDATE OA_Function set FunURL = 'OA/InventoryManage/Materials/Materials.aspx' where FunID='1233';
update OA_Function set FunURL='OA/InventoryManage/WareHouse/WareHouse.aspx' where FunID='1234';
update OA_Function set FunURL='OA/SalesManage/PayType/PayType.aspx' where FunID='1301';
update OA_Function set FunURL='OA/SalesManage/Client/Client.aspx' where FunID='1300';

-- 删除废弃菜单
DELETE from OA_Function where FunID in ('12300','12301','12310','12311','12320','12321','12330','12331');
delete from OA_Function where FunID in ('12340','12341');
delete from OA_Function where FunID in ('13011','13012');
delete from OA_Function where FunID in ('13000','13001');

