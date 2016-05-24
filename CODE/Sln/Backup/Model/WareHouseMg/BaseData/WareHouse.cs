using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class WareHouse
    {
        private string _warehouseid;
        private string _warehousecode;
        private string _warehousename;
        private string _warehouseman;
        private string _address;
        private string _tel;
        private string _remark;

        /// <summary>
        /// 仓库ID
        /// </summary>
        public string WareHouseID
        {
            set { _warehouseid = value; }
            get { return _warehouseid; }
        }
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WareHouseCode
        {
            set { _warehousecode = value; }
            get { return _warehousecode; }
        }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WareHouseName
        {
            set { _warehousename = value; }
            get { return _warehousename; }
        }
        /// <summary>
        /// 仓库主管
        /// </summary>
        public string WareHouseMan
        {
            set { _warehouseman = value; }
            get { return _warehouseman; }
        }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
