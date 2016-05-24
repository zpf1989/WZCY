using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 货物移动行
    /// </summary>
    public class GoodsMovementItem
    {
        private string _goodsmovementitemid;
        private string _goodsmovementid;
        private string _materialid;
        private decimal _targinpqty;
        private decimal _actinpqty;
        private string _recunitid;
        private decimal _targoutqty;
        private decimal _actoutqty;
        private string _issunitid;
        private string _remark;
        private decimal _inpplaprice = 0M;
        private decimal _inpplavalue = 0M;
        private decimal _inpactprice = 0M;
        private decimal _inpactvalue = 0M;
        private decimal _outplaprice = 0M;
        private decimal _outplavalue = 0M;
        private decimal _outactprice = 0M;
        private decimal _outactvalue = 0M;
        private decimal _returnquantity = 0M;

        /// <summary>
        /// 货物移动行ID
        /// </summary>
        public string GoodsMovementItemID
        {
            set { _goodsmovementitemid = value; }
            get { return _goodsmovementitemid; }
        }
        /// <summary>
        /// 货物移动ID
        /// </summary>
        public string GoodsMovementID
        {
            set { _goodsmovementid = value; }
            get { return _goodsmovementid; }
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
        /// 应收数量
        /// </summary>
        public decimal TargInpQty
        {
            set { _targinpqty = value; }
            get { return _targinpqty; }
        }
        /// <summary>
        /// 实收数量
        /// </summary>
        public decimal ActInpQty
        {
            set { _actinpqty = value; }
            get { return _actinpqty; }
        }
        /// <summary>
        /// 接收计量单位
        /// </summary>
        public string RecUnitID
        {
            set { _recunitid = value; }
            get { return _recunitid; }
        }
        /// <summary>
        /// 应发数量
        /// </summary>
        public decimal TargOutQty
        {
            set { _targoutqty = value; }
            get { return _targoutqty; }
        }
        /// <summary>
        /// 实发数量
        /// </summary>
        public decimal ActOutQty
        {
            set { _actoutqty = value; }
            get { return _actoutqty; }
        }
        /// <summary>
        /// 发出计量单位
        /// </summary>
        public string IssUnitID
        {
            set { _issunitid = value; }
            get { return _issunitid; }
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
        /// 接收计划单价
        /// </summary>
        public decimal InpPlaPrice
        {
            set { _inpplaprice = value; }
            get { return _inpplaprice; }
        }
        /// <summary>
        /// 接收计划金额
        /// </summary>
        public decimal InpPlaValue
        {
            set { _inpplavalue = value; }
            get { return _inpplavalue; }
        }
        /// <summary>
        /// 接收实际单价
        /// </summary>
        public decimal InpActPrice
        {
            set { _inpactprice = value; }
            get { return _inpactprice; }
        }
        /// <summary>
        /// 接收实际金额
        /// </summary>
        public decimal InpActValue
        {
            set { _inpactvalue = value; }
            get { return _inpactvalue; }
        }
        /// <summary>
        /// 发出计划单价
        /// </summary>
        public decimal OutPlaPrice
        {
            set { _outplaprice = value; }
            get { return _outplaprice; }
        }
        /// <summary>
        /// 发出计划金额
        /// </summary>
        public decimal OutPlaValue
        {
            set { _outplavalue = value; }
            get { return _outplavalue; }
        }
        /// <summary>
        /// 发出实际单价
        /// </summary>
        public decimal OutActPrice
        {
            set { _outactprice = value; }
            get { return _outactprice; }
        }
        /// <summary>
        /// 发出实际金额
        /// </summary>
        public decimal OutActValue
        {
            set { _outactvalue = value; }
            get { return _outactvalue; }
        }
        /// <summary>
        /// 退回数量
        /// </summary>
        public decimal ReturnQuantity
        {
            set { _returnquantity = value; }
            get { return _returnquantity; }
        }
    }
}
