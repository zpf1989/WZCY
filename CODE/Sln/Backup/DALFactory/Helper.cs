using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.DALFactory
{
    public class Helper
    {
        public static string dbType = System.Configuration.ConfigurationManager.AppSettings["MainDBType"].ToString();

        /// <summary>
        /// 获取实现IUserInfoDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IUserInfoDAL GetIUserInfoDAL()
        {
            OA.IDAL.IUserInfoDAL iUserInfoDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iUserInfoDAL = new OA.DAL.UserInfoDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iUserInfoDAL = new OA.DAL.UserInfoDAL();
                    break;
            }
            return iUserInfoDAL;
        }
        /// <summary>
        /// 获取实现IFunctionDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IFunctionDAL GetIFunctionDAL()
        {
            OA.IDAL.IFunctionDAL iFunctionDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iFunctionDAL = new OA.DAL.FunctionDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iFunctionDAL = new OA.DAL.FunctionDAL();
                    break;
            }
            return iFunctionDAL;
        }
        /// <summary>
        /// 获取实现IURrelationDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IURrelationDAL GetIURrelationDAL()
        {
            OA.IDAL.IURrelationDAL iURrelationDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iURrelationDAL = new OA.DAL.URrelationDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iURrelationDAL = new OA.DAL.URrelationDAL();
                    break;
            }
            return iURrelationDAL;
        }
        /// <summary>
        /// 获取实现IRFRelationDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IRFRelationDAL GetIRFRelationDAL()
        {
            OA.IDAL.IRFRelationDAL iRFRelationDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iRFRelationDAL = new OA.DAL.RFRelationDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iRFRelationDAL = new OA.DAL.RFRelationDAL();
                    break;
            }
            return iRFRelationDAL;
        }
        /// <summary>
        /// 获取实现IRoleManageDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IRoleManageDAL GetIRoleManageDAL()
        {
            OA.IDAL.IRoleManageDAL iRoleManageDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iRoleManageDAL = new OA.DAL.RoleManageDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iRoleManageDAL = new OA.DAL.RoleManageDAL();
                    break;
            }
            return iRoleManageDAL;
        }
        /// <summary>
        /// 获取实现ISupplierDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.ISupplierDAL GetISupplierDAL()
        {
            OA.IDAL.ISupplierDAL iSupplierDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iSupplierDAL = new OA.DAL.SupplierDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iSupplierDAL = new OA.DAL.SupplierDAL();
                    break;
            }
            return iSupplierDAL;
        }
        /// <summary>
        /// 获取实现IBuyBillItemDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBuyBillItemDAL GetIBuyBillItemDAL()
        {
            OA.IDAL.IBuyBillItemDAL iBuyBillItemDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBuyBillItemDAL = new OA.DAL.BuyBillItemDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBuyBillItemDAL = new OA.DAL.BuyBillItemDAL();
                    break;
            }
            return iBuyBillItemDAL;
        }
        /// <summary>
        /// 获取实现IDeptDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IDeptDAL GetIDeptDAL()
        {
            OA.IDAL.IDeptDAL iDeptDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iDeptDAL = new OA.DAL.DeptDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iDeptDAL = new OA.DAL.DeptDAL();
                    break;
            }
            return iDeptDAL;
        }
        /// <summary>
        /// 获取实现IBuyBillDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBuyBillDAL GetIBuyBillDAL()
        {
            OA.IDAL.IBuyBillDAL iBuyBillDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBuyBillDAL = new OA.DAL.BuyBillDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBuyBillDAL = new OA.DAL.BuyBillDAL();
                    break;
            }
            return iBuyBillDAL;
        }
        /// <summary>
        /// 获取实现IReachGoodsBillDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IReachGoodsBillDAL GetIReachGoodsBillDAL()
        {
            OA.IDAL.IReachGoodsBillDAL iReachGoodsBillDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iReachGoodsBillDAL = new OA.DAL.ReachGoodsBillDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iReachGoodsBillDAL = new OA.DAL.ReachGoodsBillDAL();
                    break;
            }
            return iReachGoodsBillDAL;
        }
        /// <summary>
        /// 获取实现IEmpleeDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IEmpleeDAL GetIEmpleeDAL()
        {
            OA.IDAL.IEmpleeDAL iEmpleeDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iEmpleeDAL = new OA.DAL.EmpleeDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iEmpleeDAL = new OA.DAL.EmpleeDAL();
                    break;
            }
            return iEmpleeDAL;
        }
        /// <summary>
        /// 获取实现IPostDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IPostDAL GetIPostDAL()
        {
            OA.IDAL.IPostDAL iPostDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iPostDAL = new OA.DAL.PostDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iPostDAL = new OA.DAL.PostDAL();
                    break;
            }
            return iPostDAL;
        }
        /// <summary>
        /// 获取实现IEmpChangeDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IEmpChangeDAL GetIEmpChangeDAL()
        {
            OA.IDAL.IEmpChangeDAL iEmpChangeDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iEmpChangeDAL = new OA.DAL.EmpChangeDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iEmpChangeDAL = new OA.DAL.EmpChangeDAL();
                    break;
            }
            return iEmpChangeDAL;
        }
        /// <summary>
        /// 获取实现INewsDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.INewsDAL GetINewsDAL()
        {
            OA.IDAL.INewsDAL iNewsDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iNewsDAL = new OA.DAL.NewsDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iNewsDAL = new OA.DAL.NewsDAL();
                    break;
            }
            return iNewsDAL;
        }
        /// <summary>
        /// 获取实现IOfficeDocDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IOfficeDocDAL GetIOfficeDocDAL()
        {
            OA.IDAL.IOfficeDocDAL iOfficeDocDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iOfficeDocDAL = new OA.DAL.OfficeDocDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iOfficeDocDAL = new OA.DAL.OfficeDocDAL();
                    break;
            }
            return iOfficeDocDAL;
        }
        /// <summary>
        /// 获取实现IOfficeDocItemDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IOfficeDocItemDAL GetIOfficeDocItemDAL()
        {
            OA.IDAL.IOfficeDocItemDAL ifficeDocItemDAL;
            switch (dbType)
            {
                case "SQLServer":
                    ifficeDocItemDAL = new OA.DAL.OfficeDocItemDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    ifficeDocItemDAL = new OA.DAL.OfficeDocItemDAL();
                    break;
            }
            return ifficeDocItemDAL;
        }
        /// <summary>
        /// 获取实现IAccessoriesDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IAccessoriesDAL GetIAccessoriesDAL()
        {
            OA.IDAL.IAccessoriesDAL iAccessoriesDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iAccessoriesDAL = new OA.DAL.AccessoriesDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iAccessoriesDAL = new OA.DAL.AccessoriesDAL();
                    break;
            }
            return iAccessoriesDAL;
        }
        /// <summary>
        /// 获取实现IReachGoodsBillItemDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IReachGoodsBillItemDAL GetIReachGoodsBillItemDAL()
        {
            OA.IDAL.IReachGoodsBillItemDAL iReachGoodsBillItemDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iReachGoodsBillItemDAL = new OA.DAL.ReachGoodsBillItemDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iReachGoodsBillItemDAL = new OA.DAL.ReachGoodsBillItemDAL();
                    break;
            }
            return iReachGoodsBillItemDAL;
        }
        /// <summary>
        /// 获取实现IWareHouseDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IWareHouseDAL GetIWareHouseDAL()
        {
            OA.IDAL.IWareHouseDAL iWareHouseDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iWareHouseDAL = new OA.DAL.WareHouseDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iWareHouseDAL = new OA.DAL.WareHouseDAL();
                    break;
            }
            return iWareHouseDAL;
        }
        /// <summary>
        /// 获取实现IGoodsMovementItemDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IGoodsMovementItemDAL GetIGoodsMovementItemDAL()
        {
            OA.IDAL.IGoodsMovementItemDAL iGoodsMovementItemDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iGoodsMovementItemDAL = new OA.DAL.GoodsMovementItemDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iGoodsMovementItemDAL = new OA.DAL.GoodsMovementItemDAL();
                    break;
            }
            return iGoodsMovementItemDAL;
        }
        /// <summary>
        /// 获取实现IGoodsMovementDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IGoodsMovementDAL GetIGoodsMovementDAL()
        {
            OA.IDAL.IGoodsMovementDAL iGoodsMovementDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iGoodsMovementDAL = new OA.DAL.GoodsMovementDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iGoodsMovementDAL = new OA.DAL.GoodsMovementDAL();
                    break;
            }
            return iGoodsMovementDAL;
        }
        /// <summary>
        /// 获取实现IInvBalRealAccountDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IInvBalRealAccountDAL GetIInvBalRealAccountDAL()
        {
            OA.IDAL.IInvBalRealAccountDAL iInvBalRealAccountDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iInvBalRealAccountDAL = new OA.DAL.InvBalRealAccountDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iInvBalRealAccountDAL = new OA.DAL.InvBalRealAccountDAL();
                    break;
            }
            return iInvBalRealAccountDAL;
        }
        /// <summary>
        /// 获取实现IMeasureUnitsDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IMeasureUnitsDAL GetIMeasureUnitsDAL()
        {
            OA.IDAL.IMeasureUnitsDAL iMeasureUnitsDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iMeasureUnitsDAL = new OA.DAL.MeasureUnitsDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iMeasureUnitsDAL = new OA.DAL.MeasureUnitsDAL();
                    break;
            }
            return iMeasureUnitsDAL;
        }
        /// <summary>
        /// 获取实现IMaterialTypeDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IMaterialTypeDAL GetIMaterialTypeDAL()
        {
            OA.IDAL.IMaterialTypeDAL iMaterialTypeDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iMaterialTypeDAL = new OA.DAL.MaterialTypeDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iMaterialTypeDAL = new OA.DAL.MaterialTypeDAL();
                    break;
            }
            return iMaterialTypeDAL;
        }
        /// <summary>
        /// 获取实现IMaterialClassDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IMaterialClassDAL GetIMaterialClassDAL()
        {
            OA.IDAL.IMaterialClassDAL iMaterialClassDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iMaterialClassDAL = new OA.DAL.MaterialClassDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iMaterialClassDAL = new OA.DAL.MaterialClassDAL();
                    break;
            }
            return iMaterialClassDAL;
        }
        /// <summary>
        /// 获取实现IMaterialsDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IMaterialsDAL GetIMaterialsDAL()
        {
            OA.IDAL.IMaterialsDAL iMaterialsDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iMaterialsDAL = new OA.DAL.MaterialsDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iMaterialsDAL = new OA.DAL.MaterialsDAL();
                    break;
            }
            return iMaterialsDAL;
        }
        /// <summary>
        /// 获取实现IClientDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IClientDAL GetIClientDAL()
        {
            OA.IDAL.IClientDAL iClientDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iClientDAL = new OA.DAL.ClientDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iClientDAL = new OA.DAL.ClientDAL();
                    break;
            }
            return iClientDAL;
        }
        /// <summary>
        /// 获取实现ISaleOrderItemDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.ISaleOrderItemDAL GetISaleOrderItemDAL()
        {
            OA.IDAL.ISaleOrderItemDAL iSaleOrderItemDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iSaleOrderItemDAL = new OA.DAL.SaleOrderItemDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iSaleOrderItemDAL = new OA.DAL.SaleOrderItemDAL();
                    break;
            }
            return iSaleOrderItemDAL;
        }
        /// <summary>
        /// 获取实现ISaleOrderDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.ISaleOrderDAL GetISaleOrderDAL()
        {
            OA.IDAL.ISaleOrderDAL iSaleOrderDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iSaleOrderDAL = new OA.DAL.SaleOrderDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iSaleOrderDAL = new OA.DAL.SaleOrderDAL();
                    break;
            }
            return iSaleOrderDAL;
        }
        /// <summary>
        /// 获取实现IBillTypeDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBillTypeDAL GetIBillTypeDAL()
        {
            OA.IDAL.IBillTypeDAL iBillTypeDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBillTypeDAL = new OA.DAL.BillTypeDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBillTypeDAL = new OA.DAL.BillTypeDAL();
                    break;
            }
            return iBillTypeDAL;
        }
        /// <summary>
        /// 获取实现IApplyPublicPowerDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IApplyPublicPowerDAL GetIApplyPublicPowerDAL()
        {
            OA.IDAL.IApplyPublicPowerDAL iApplyPublicPowerDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iApplyPublicPowerDAL = new OA.DAL.ApplyPublicPowerDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iApplyPublicPowerDAL = new OA.DAL.ApplyPublicPowerDAL();
                    break;
            }
            return iApplyPublicPowerDAL;
        }
        /// <summary>
        /// 获取实现ISOSecondCheckDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.ISOSecondCheckDAL GetISOSecondCheckDAL()
        {
            OA.IDAL.ISOSecondCheckDAL iSOSecondCheckDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iSOSecondCheckDAL = new OA.DAL.SOSecondCheckDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iSOSecondCheckDAL = new OA.DAL.SOSecondCheckDAL();
                    break;
            }
            return iSOSecondCheckDAL;
        }
        /// <summary>
        /// 获取实现ISOReaderDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.ISOReaderDAL GetISOReaderDAL()
        {
            OA.IDAL.ISOReaderDAL iSOReaderDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iSOReaderDAL = new OA.DAL.SOReaderDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iSOReaderDAL = new OA.DAL.SOReaderDAL();
                    break;
            }
            return iSOReaderDAL;
        }
        /// <summary>
        /// 获取实现IBOReaderDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBOReaderDAL GetIBOReaderDAL()
        {
            OA.IDAL.IBOReaderDAL iBOReaderDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBOReaderDAL = new OA.DAL.BOReaderDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBOReaderDAL = new OA.DAL.BOReaderDAL();
                    break;
            }
            return iBOReaderDAL;
        }
        /// <summary>
        /// 获取实现IBOSecondCheckDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBOSecondCheckDAL GetIBOSecondCheckDAL()
        {
            OA.IDAL.IBOSecondCheckDAL iBOSecondCheckDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBOSecondCheckDAL = new OA.DAL.BOSecondCheckDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBOSecondCheckDAL = new OA.DAL.BOSecondCheckDAL();
                    break;
            }
            return iBOSecondCheckDAL;
        }
        /// <summary>
        /// 获取实现IGMSecondCheckDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IGMSecondCheckDAL GetIGMSecondCheckDAL()
        {
            OA.IDAL.IGMSecondCheckDAL iGMSecondCheckDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iGMSecondCheckDAL = new OA.DAL.GMSecondCheckDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iGMSecondCheckDAL = new OA.DAL.GMSecondCheckDAL();
                    break;
            }
            return iGMSecondCheckDAL;
        }
        /// <summary>
        /// 获取实现IGMReaderDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IGMReaderDAL GetIGMReaderDAL()
        {
            OA.IDAL.IGMReaderDAL iGMReaderDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iGMReaderDAL = new OA.DAL.GMReaderDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iGMReaderDAL = new OA.DAL.GMReaderDAL();
                    break;
            }
            return iGMReaderDAL;
        }
        /// <summary>
        /// 获取实现IBOAReaderDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBOAReaderDAL GetIBOAReaderDAL()
        {
            OA.IDAL.IBOAReaderDAL iBOAReaderDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBOAReaderDAL = new OA.DAL.BOAReaderDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBOAReaderDAL = new OA.DAL.BOAReaderDAL();
                    break;
            }
            return iBOAReaderDAL;
        }
        /// <summary>
        /// 获取实现IBOASecondCheckDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBOASecondCheckDAL GetIBOASecondCheckDAL()
        {
            OA.IDAL.IBOASecondCheckDAL iBOASecondCheckDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBOASecondCheckDAL = new OA.DAL.BOASecondCheckDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBOASecondCheckDAL = new OA.DAL.BOASecondCheckDAL();
                    break;
            }
            return iBOASecondCheckDAL;
        }
        /// <summary>
        /// 获取实现IBuyApplyBillItemDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBuyApplyBillItemDAL GetIBuyApplyBillItemDAL()
        {
            OA.IDAL.IBuyApplyBillItemDAL iBuyApplyBillItemDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBuyApplyBillItemDAL = new OA.DAL.BuyApplyBillItemDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBuyApplyBillItemDAL = new OA.DAL.BuyApplyBillItemDAL();
                    break;
            }
            return iBuyApplyBillItemDAL;
        }
        /// <summary>
        /// 获取实现IBuyApplyBillDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IBuyApplyBillDAL GetIBuyApplyBillDAL()
        {
            OA.IDAL.IBuyApplyBillDAL iBuyApplyBillDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iBuyApplyBillDAL = new OA.DAL.BuyApplyBillDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iBuyApplyBillDAL = new OA.DAL.BuyApplyBillDAL();
                    break;
            }
            return iBuyApplyBillDAL;
        }
        /// <summary>
        /// 获取实现IPayTypeDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IPayTypeDAL GetIPayTypeDAL()
        {
            OA.IDAL.IPayTypeDAL iPayTypeDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iPayTypeDAL = new OA.DAL.PayTypeDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iPayTypeDAL = new OA.DAL.PayTypeDAL();
                    break;
            }
            return iPayTypeDAL;
        }
        /// <summary>
        /// 获取实现IAskPriceDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IAskPriceDAL GetIAskPriceDAL()
        {
            OA.IDAL.IAskPriceDAL iAskPriceDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iAskPriceDAL = new OA.DAL.AskPriceDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iAskPriceDAL = new OA.DAL.AskPriceDAL();
                    break;
            }
            return iAskPriceDAL;
        }
        /// <summary>
        /// 获取实现IAPSecondCheckDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IAPSecondCheckDAL GetIAPSecondCheckDAL()
        {
            OA.IDAL.IAPSecondCheckDAL iAPSecondCheckDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iAPSecondCheckDAL = new OA.DAL.APSecondCheckDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iAPSecondCheckDAL = new OA.DAL.APSecondCheckDAL();
                    break;
            }
            return iAPSecondCheckDAL;
        }
        /// <summary>
        /// 获取实现IAPReaderDAL的对象
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IAPReaderDAL GetIAPReaderDAL()
        {
            OA.IDAL.IAPReaderDAL iAPReaderDAL;
            switch (dbType)
            {
                case "SQLServer":
                    iAPReaderDAL = new OA.DAL.APReaderDAL();
                    break;
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                default:
                    iAPReaderDAL = new OA.DAL.APReaderDAL();
                    break;
            }
            return iAPReaderDAL;
        }

    }
}
