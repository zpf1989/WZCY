﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.DALFactory
{
    public class Helper
    {
        public static string dbType = System.Configuration.ConfigurationManager.AppSettings["MainDBType"].ToString();

        #region 系统管理
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
        /// 获取IDepartmentDAL的实例
        /// </summary>
        /// <returns></returns>
        public static OA.IDAL.IDepartmentDAL GetIDepartmentDAL()
        {
            OA.IDAL.IDepartmentDAL iDepartmentDAL;
            switch (dbType)
            {
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                case "SQLServer":
                default:
                    iDepartmentDAL = new OA.DAL.DepartmentDAL();
                    break;
            }
            return iDepartmentDAL;
        }

        public static OA.IDAL.IUserManageDAL GetIUserManageDAL()
        {
            OA.IDAL.IUserManageDAL iUserManageDAL;
            switch (dbType)
            {
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                case "SQLServer":
                default:
                    iUserManageDAL = new OA.DAL.UserManageDAL();
                    break;
            }
            return iUserManageDAL;
        }
        #endregion

        #region 基础数据
        public static OA.IDAL.IMaterialClassDAL GetIMaterialClassDAL()
        {
            OA.IDAL.IMaterialClassDAL idal;
            switch (dbType)
            {
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                case "SQLServer":
                default:
                    idal = new OA.DAL.MaterialClassDAL();
                    break;
            }
            return idal;
        }

        public static OA.IDAL.IMaterialTypeDAL GetIMaterialTypeDAL()
        {
            OA.IDAL.IMaterialTypeDAL idal;
            switch (dbType)
            {
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                case "SQLServer":
                default:
                    idal = new OA.DAL.MaterialTypeDAL();
                    break;
            }
            return idal;
        }

        public static OA.IDAL.IMeasureUnitsDAL GetIMeasureUnitsDAL()
        {
            OA.IDAL.IMeasureUnitsDAL idal;
            switch (dbType)
            {
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                case "SQLServer":
                default:
                    idal = new OA.DAL.MeasureUnitsDAL();
                    break;
            }
            return idal;
        }

        public static OA.IDAL.IMaterialsDAL GetIMaterialsDAL()
        {
            OA.IDAL.IMaterialsDAL idal;
            switch (dbType)
            {
                //case "Oracle":
                //    iAffix = new Upload.DALORACLE.Affix();
                //    break;
                //case "GBase":
                //    iAffix = new Upload.DALGBase.Affix();
                //    break;
                case "SQLServer":
                default:
                    idal = new OA.DAL.MaterialsDAL();
                    break;
            }
            return idal;
        }
        #endregion
    }
}
