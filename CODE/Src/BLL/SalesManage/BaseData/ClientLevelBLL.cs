using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class ClientLevelBLL : BaseBLL<ClientLevel>
    {
        private readonly IClientLevelDAL idal = DALFactory.Helper.GetIClientLevelDAL();

        public List<ClientLevel> GetClientLevelsByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public List<ClientLevel> GetClientLevelsByPageForHelp(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {
            return idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params ClientLevel[] entites)
        {
            if (!RepeatCheck(entites))
            {
                return false;
            }
            return idal.Save(entites);
        }

        public bool Delete(params string[] entityIds)
        {
            return idal.Delete(entityIds);
        }
        /// <summary>
        /// 名称重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(ClientLevel[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameName = entities.ToLookup(e => e.LevelName).Count;
            if (sameName != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newNames = (from m in entities
                            where ValidateUtil.isBlank(m.LevelId)
                            select m.LevelName).ToArray();
            if (newNames == null || newNames.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newNames);
        }
    }
}
