using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class Materials
    {
        private string _materialid;
        private string _materialcode;
        private string _materialname;
        private string _specs;
        private string _materialclassid;
        private string _materialtypeid;
        private string _primaryunitid;
        private decimal _price;
        private string _remark;
        private string _creator;
        private DateTime _createtime;
        private decimal _wasterrate;

        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialID
        {
            set { _materialid = value; }
            get { return _materialid; }
        }
        /// <summary>
        /// 物料编号
        /// </summary>
        public string MaterialCode
        {
            set { _materialcode = value; }
            get { return _materialcode; }
        }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName
        {
            set { _materialname = value; }
            get { return _materialname; }
        }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Specs
        {
            set { _specs = value; }
            get { return _specs; }
        }
        /// <summary>
        /// 物料分类ID
        /// </summary>
        public string MaterialClassID
        {
            set { _materialclassid = value; }
            get { return _materialclassid; }
        }
        /// <summary>
        /// 物料类型ID
        /// </summary>
        public string MaterialTypeID
        {
            set { _materialtypeid = value; }
            get { return _materialtypeid; }
        }
        /// <summary>
        /// 基本计量单位ID
        /// </summary>
        public string PrimaryUnitID
        {
            set { _primaryunitid = value; }
            get { return _primaryunitid; }
        }
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 废品率
        /// </summary>
        public decimal WasterRate
        {
            set { _wasterrate = value; }
            get { return _wasterrate; }
        }

    }
}
