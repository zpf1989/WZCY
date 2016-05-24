using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.DAL
{
    public class DB
    {
        public static string ConnectionString
        {
            get
            {
                if (System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"] == null)
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                }
                else
                {
                    return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                }
            }
        }
        public static GentleUtil.DB.DBAccess.DBType Type
        {
            get
            {
                string type = string.Empty;
                type = System.Configuration.ConfigurationManager.AppSettings["MainDBType"].ToString();
                return (GentleUtil.DB.DBAccess.DBType)System.Enum.Parse(typeof(GentleUtil.DB.DBAccess.DBType), type);
            }
        }
    }
}
