using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 采购订单
    /// </summary>
    public class BuyOrder
    {
        /// <summary>
        /// 采购订单ID
        /// </summary>
        public string BuyOrderID { get; set; }
        /// <summary>
        /// 采购订单号
        /// </summary>
        public string BuyOrderCode { get; set; }
        /// <summary>
        /// 供应商ID
        /// </summary>
        public string SupplierID { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier_Name { get; set; }
        /// <summary>
        /// 采购日期
        /// </summary>
        public string BuyOrderDate { get; set; }
        /// <summary>
        /// 到货日期
        /// </summary>
        public string DeliveryDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        public string Creator_Name { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Editor { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        public string Editor_Name { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? EditTime { get; set; }
        /// <summary>
        /// 初审人
        /// </summary>
        public string FirstChecker { get; set; }
        public string FirstChecker_Name { get; set; }
        /// <summary>
        /// 初审时间
        /// </summary>
        public DateTime? FirstCheckTime { get; set; }
        /// <summary>
        /// 初审意见
        /// </summary>
        public string FirstCheckView { get; set; }
        /// <summary>
        /// 接收单位
        /// </summary>
        public string RecCompany { get; set; }
        /// <summary>
        /// 接收电话
        /// </summary>
        public string RecTel { get; set; }
        /// <summary>
        /// 接收传真
        /// </summary>
        public string RecFax { get; set; }
        /// <summary>
        /// 单据状态
        /// 1:编制
        /// 2:提交
        /// 3:审核通过
        /// 4:审核不通过
        /// 5:批准通过
        /// 6:批准不通过
        /// 7:关闭
        /// </summary>
        public string OrderState { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 复审人姓名
        /// </summary>
        public string SecondCheckerName { get; set; }
        /// <summary>
        /// 分阅人姓名
        /// </summary>
        public string ReaderName { get; set; }
        /// <summary>
        /// 采购订单分录
        /// </summary>
        public IList<BuyOrderItem> Items { get; set; }
    }
}
