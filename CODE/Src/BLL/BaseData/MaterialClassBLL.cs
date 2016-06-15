using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class MaterialClassBLL : BaseBLL<MaterialClass>
    {
        private readonly OA.IDAL.IMaterialClassDAL idal = DALFactory.Helper.GetIMaterialClassDAL();

        public List<MaterialClass> GetMaterialClassesByPage(PageEntity pageEntity, string whereSql = null, string orderBySql = null)
        {

            return idal.GetEntitiesByPage(pageEntity, whereSql, orderBySql);
        }

        public bool Save(params MaterialClass[] classes)
        {
            if (!RepeatCheck(classes))
            {
                return false;
            }
            return idal.Save(classes);
        }

        public bool Delete(params string[] classIds)
        {
            return idal.Delete(classIds);
        }
        /// <summary>
        /// 编号重复检查
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>true:无重复；false：有重复</returns>
        public override bool RepeatCheck(MaterialClass[] entities)
        {
            if (entities == null || entities.Length < 1)
            {
                return true;
            }

            //"客户端"校验
            var sameCode = entities.ToLookup(m => m.MaterialClassCode).Count;
            if (sameCode != entities.Length)
            {
                return false;
            }
            //服务端校验，只校验id为空的（即新增数据）
            var newCodes = (from m in entities
                            where ValidateUtil.isBlank(m.MaterialClassID)
                            select m.MaterialClassCode).ToArray();
            if (newCodes == null || newCodes.Length < 1)
            {
                return true;
            }
            return !idal.Exists(newCodes);
        }
    }
}
