using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class ClientClassificationBLL : BaseBLL<ClientClassification>
    {
        private readonly IClientClassificationDAL idal = DALFactory.Helper.GetIClientClassificationDAL();

        public List<ClientClassification> GetClientClassificationByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {

            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params ClientClassification[] entites)
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
        public override bool RepeatCheck(ClientClassification[] entities)
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
            var newNames = (from e in entities
                            where ValidateUtil.isBlank(e.Id)
                            select e.ClientName).ToArray();
            if (newNames == null || newNames.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newNames);
        }

        public bool Classify()
        {
            return idal.Classify();
        }
    }
}
