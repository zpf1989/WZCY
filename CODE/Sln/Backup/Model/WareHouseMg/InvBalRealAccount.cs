using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 库存余额账
    /// </summary>
    public class InvBalRealAccount
    {
        private string _id;
        private string _warehouseid;
        private string _materialid;
        private decimal _curqtybalance;

        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 仓库ID
        /// </summary>
        public string WareHouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialID
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        /// <summary>
        /// 当前库存余额
        /// </summary>
        public decimal CurQtyBalance
        {
            set { _curqtybalance = value; }
            get { return _curqtybalance; }
        }
    }
}
