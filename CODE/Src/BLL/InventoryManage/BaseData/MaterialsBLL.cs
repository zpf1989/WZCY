using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class MaterialsBLL : BaseBLL<Materials>
    {
        private readonly OA.IDAL.IMaterialsDAL idal = DALFactory.Helper.GetIMaterialsDAL();

        public List<Materials> GetMaterialsByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null, bool isForHelp = false)
        {
            return isForHelp ? idal.GetEntitiesByPageForHelp(pageEntity, whereSql, orderBySql) : idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params Materials[] materials)
        {
            if (!RepeatCheck(materials))
            {
                return false;
            }
            return idal.Save(materials);
        }

        public bool Delete(params string[] mIds)
        {
            return idal.Delete(mIds);
        }

        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(Materials[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.MaterialCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from m in entities
                            where ValidateUtil.isBlank(m.MaterialID)
                            select m.MaterialCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newCodes);
        }
    }
}
