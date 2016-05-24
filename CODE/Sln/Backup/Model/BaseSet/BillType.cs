using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class BillTypeInfo
    {
        private string _billid;
        private string _billcode;
        private string _billname;
        private string _billtype;
        private string _remark;

        /// <summary>
        /// 主键
        /// </summary>
        public string BillID
        {
            set { _billid = value; }
            get { return _billid; }
        }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillCode
        {
            set { _billcode = value; }
            get { return _billcode; }
        }
        /// <summary>
        /// 单据名称
        /// </summary>
        public string BillName
        {
            set { _billname = value; }
            get { return _billname; }
        }
        /// <summary>
        /// 单据类型
        /// sale  销售订单
        /// </summary>
        public string BillType
        {
            set { _billtype = value; }
            get { return _billtype; }
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
