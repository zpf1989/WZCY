using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 销售订单
    /// </summary>
    public class SaleOrder
    {
        public string SaleOrderID { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string SaleOrderCode { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string BillTypeID { get; set; }
        public string BillType_Name { get; set; }
        /// <summary>
        /// 物料ID
        /// </summary>
        public string MaterialID { get; set; }
        public string Material_Name { get; set; }
        /// <summary>
        /// 销售计量单位ID
        /// </summary>
        public string SaleUnitID { get; set; }
        public string SaleUnit_Name { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public string ClientID { get; set; }
        public string Client_Name { get; set; }
        /// <summary>
        /// 销售日期
        /// </summary>
        public string SaleDate { get; set; }
        /// <summary>
        /// 销售数量
        /// </summary>
        public Decimal SaleQty { get; set; }
        /// <summary>
        /// 销售单价
        /// </summary>
        public Decimal SalePrice { get; set; }
        /// <summary>
        /// 销售金额
        /// </summary>
        public Decimal SaleCost { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        public string FinishDate { get; set; }
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
        /// 工艺路线id：暂不用
        /// </summary>
        public string RoutingID { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string SaleState { get; set; }
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
        /// 生产工艺
        /// </summary>
        public string Routing { get; set; }

        /// <summary>
        /// 销售订单行
        /// </summary>
        public IList<SaleOrderItem> Items { get; set; }
    }
}