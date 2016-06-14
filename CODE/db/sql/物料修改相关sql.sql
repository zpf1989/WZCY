-- 更新菜单路径
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MeasureUnits/MeasureUnits.aspx' where FunID='1230';
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialType/MaterialType.aspx' where FunID='1231';
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialClass/MaterialClass.aspx' where FunID='1232';
UPDATE OA_Function set FunURL = 'OA/InventoryManage/Materials/Materials.aspx' where FunID='1233';
-- 删除废弃菜单
DELETE from OA_Function where FunID in ('12300','12301','12310','12311','12320','12321','12330','12331');