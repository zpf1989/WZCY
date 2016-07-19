using OA.GeneralClass;
using OA.IDAL;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.BLL
{
    public class GoodsMovementItemBLL : BaseBLL<GoodsMovementItem>
    {
        private readonly IGoodsMovementItemDAL idal = DALFactory.Helper.GetIGoodsMovementItemDAL();

        public IList<GoodsMovementItem> GetGMItems(PageEntity pageEntity, string gmId)
        {
            if (ValidateUtil.isBlank(gmId))
            {
                return null;
            }
            return idal.GetGMItems(pageEntity, gmId);
        }

        public bool Save(params GoodsMovementItem[] entites)
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

        public override bool RepeatCheck(GoodsMovementItem[] entities)
        {
            return true;
        }
    }
}
