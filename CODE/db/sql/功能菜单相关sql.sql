-- ���²˵�·��
-- ������λ
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MeasureUnits/MeasureUnits.aspx' where FunID='1230';
DELETE from OA_Function where FunID in ('12300','12301','12310','12311','12320','12321','12330','12331');
-- ��������
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialType/MaterialType.aspx' where FunID='1231';
-- ���Ϸ���
UPDATE OA_Function set FunURL = 'OA/InventoryManage/MaterialClass/MaterialClass.aspx' where FunID='1232';
-- ����
UPDATE OA_Function set FunURL = 'OA/InventoryManage/Materials/Materials.aspx' where FunID='1233';
-- �ֿ�
update OA_Function set FunURL='OA/InventoryManage/WareHouse/WareHouse.aspx' where FunID='1234';
delete from OA_Function where FunID in ('12340','12341');
-- ���ʽ
update OA_Function set FunURL='OA/SalesManage/PayType/PayType.aspx' where FunID='1301';
delete from OA_Function where FunID in ('13011','13012');
-- �ͻ�
update OA_Function set FunURL='OA/SalesManage/Client/Client.aspx' where FunID='1300';
delete from OA_Function where FunID in ('13000','13001');
-- �ͻ��ּ�����
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1302','�ͻ��ּ�����','130','OA/SalesManage/ClientLevel/ClientLevel.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1302');
--���۶���
insert into OA_Function(FunID,FunName,ParentFunID)values('134','���۶���','13');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','134');
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1340','���۶�������','134','OA/SalesManage/SaleOrder/SaleOrderAdd.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1340');
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1341','���۶�������','134','OA/SalesManage/SaleOrder/SaleOrder.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1341');
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1342','���۶�������','134','OA/SalesManage/SaleOrder/SaleOrderApproval.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1342');
--�ͻ��ּ�����
insert into OA_Function(FunID,FunName,ParentFunID,FunURL)values('1303','�ͻ��ּ�����','130','OA/SalesManage/ClientClassification/ClientClassification.aspx');
insert into OA_RFRelation(ID,RoleID,FunID)values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1303');
--ѯ�۵����
update OA_Function set FunURL='OA/SalesManage/AskPrice/AskPriceAdd.aspx' where FunID=1330;
update OA_Function set FunName='ѯ�۵�����',FunURL='OA/SalesManage/AskPrice/AskPrice.aspx' where FunID=1331;
update OA_Function set FunName='ѯ�۵�����',FunURL='OA/SalesManage/AskPrice/AskPriceApproval.aspx' where FunID=1332;
delete from OA_Function where FunID in (1333,1334,1335);
--���Ź���
update OA_Function
set FunURL='OA/SysManage/Department/DeptList.aspx',FunName='���Ź���'
where FunID='86'
--�����ƶ�
insert into OA_Function(FunID,FunName,ParentFunID) values('124','�����ƶ�','12');
insert into OA_RFRelation(ID,RoleID,FunID) values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','124');
insert into OA_Function(FunID,FunName,ParentFunID,FunURL) values('1241','��浥�ݹ���','124','OA/InventoryManage/GoodsMovement/GoodsMovement.aspx');
insert into OA_RFRelation(ID,RoleID,FunID) values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1241');
insert into OA_Function(FunID,FunName,ParentFunID,FunURL) values('1242','��浥������','124','OA/InventoryManage/GoodsMovement/GoodsMovementApproval.aspx');
insert into OA_RFRelation(ID,RoleID,FunID) values(NEWID(),'5FE1155A-5920-47EC-99D9-CA7ACA8DC4BB','1242');




