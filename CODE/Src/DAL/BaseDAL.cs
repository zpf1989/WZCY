using GentleUtil.DB;
using OA.GeneralClass;
using OA.GeneralClass.Logger;
using OA.IDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace OA.DAL
{
    public abstract class BaseDAL<T> : IBaseDAL<T> where T : class
    {
        public ILogHelper<T> Logger
        {
            get
            {
                return LoggerFactory.GetLogger<T>();
            }
        }
        public abstract List<T> GetEntitiesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null);
        public abstract List<T> GetEntitiesByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null);
        public abstract bool Save(params T[] entities);

        public abstract bool Delete(params string[] ids);

        protected abstract string GetTableName();

        public bool Exists(string where)
        {
            if (ValidateUtil.isBlank(where))
            {
                return false;
            }

            string sql = string.Format("select count(1) from {0} where 1=1 {1}", GetTableName(), where);

            try
            {
                var data = DBAccess.ExecuteScalar(DB.Type, DB.ConnectionString, CommandType.Text, sql);
                return Convert.ToInt32(data) > 0;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return false;
            }
        }



    }
}
