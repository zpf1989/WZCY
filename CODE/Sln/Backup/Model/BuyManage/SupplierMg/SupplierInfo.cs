using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class SupplierInfo
    {
        private string _supplierid;
        private string _suppliercode;
        private string _suppliername;
        private string _contactor;
        private string _tel;
        private string _fax;
        private string _remark;

        /// <summary>
        /// 供应商ID
        /// </summary>
        public string SupplierID
        {
            set { _supplierid = value; }
            get { return _supplierid; }
        }
        /// <summary>
        /// 供应商编号
        /// </summary>
        public string SupplierCode
        {
            set { _suppliercode = value; }
            get { return _suppliercode; }
        }
        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName
        {
            set { _suppliername = value; }
            get { return _suppliername; }
        }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contactor
        {
            set { _contactor = value; }
            get { return _contactor; }
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
        /// 传真
        /// </summary>
        public string Fax
        {
            set { _fax = value; }
            get { return _fax; }
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
