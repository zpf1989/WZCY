using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 货物移动主表
    /// </summary>
    public class GoodsMovement
    {
        public string GoodsMovementID { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BusinessType { get; set; }
        /// <summary>
        /// 移动类型
        /// </summary>
        public string MoveTypeCode { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string GoodsMovementCode { get; set; }
        /// <summary>
        /// 单据日期
        /// </summary>
        public string CreateDate { get; set; }
        /// <summary>
        /// 接收日期
        /// </summary>
        public string ReceiptDate { get; set; }
        /// <summary>
        /// 接收部门id
        /// </summary>
        public string RecDeptID { get; set; }
        /// <summary>
        /// 接收部门
        /// </summary>
        public string RecDept_Name { get; set; }
        /// <summary>
        /// 接收经办人id
        /// </summary>
        public string RecHandler { get; set; }
        /// <summary>
        /// 接收经办人
        /// </summary>
        public string RecHandler_Name { get; set; }
        /// <summary>
        /// 接收仓库id
        /// </summary>
        public string RecWHID { get; set; }
        /// <summary>
        /// 接收仓库
        /// </summary>
        public string RecWH_Name { get; set; }
        /// <summary>
        /// 接收仓库保管员id
        /// </summary>
        public string RecWHEmpID { get; set; }
        /// <summary>
        /// 接收仓库保管员
        /// </summary>
        public string RecWHEmp_Name { get; set; }
        /// <summary>
        /// 发出日期
        /// </summary>
        public string IssDate { get; set; }
        /// <summary>
        /// 发出部门id
        /// </summary>
        public string IssDeptID { get; set; }
        /// <summary>
        /// 发出部门
        /// </summary>
        public string IssDept_Name { get; set; }
        /// <summary>
        /// 发出经办人id
        /// </summary>
        public string IssHandler { get; set; }
        /// <summary>
        /// 发出经办人
        /// </summary>
        public string IssHandler_Name { get; set; }
        /// <summary>
        /// 发出仓库id
        /// </summary>
        public string IssWHID { get; set; }
        /// <summary>
        /// 发出仓库
        /// </summary>
        public string IssWH_Name { get; set; }
        /// <summary>
        /// 发出仓库保管员id
        /// </summary>
        public string IssWHEmpID { get; set; }
        /// <summary>
        /// 发出仓库保管员
        /// </summary>
        public string IssWHEmp_Name { get; set; }
        /// <summary>
        /// 采购部门id
        /// </summary>
        public string PurDeptID { get; set; }
        /// <summary>
        /// 采购部门
        /// </summary>
        public string PurDept_Name { get; set; }
        /// <summary>
        /// 采购人员id
        /// </summary>
        public string PurEmpID { get; set; }
        /// <summary>
        /// 采购人员
        /// </summary>
        public string PurEmp_Name { get; set; }
        /// <summary>
        /// 供应商id
        /// </summary>
        public string SupplierID { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string Supplier_Name { get; set; }
        /// <summary>
        /// 销售部门id
        /// </summary>
        public string SalesDepID { get; set; }
        /// <summary>
        /// 销售部门
        /// </summary>
        public string SalesDep_Name { get; set; }
        /// <summary>
        /// 销售人员id
        /// </summary>
        public string SalesEmpID { get; set; }
        /// <summary>
        /// 销售人员
        /// </summary>
        public string SalesEmp_Name { get; set; }
        /// <summary>
        /// 销售客户id
        /// </summary>
        public string CustomerID { get; set; }
        /// <summary>
        /// 销售客户
        /// </summary>
        public string Customer_Name { get; set; }
        /// <summary>
        /// 生产部门id
        /// </summary>
        public string ProDepID { get; set; }
        /// <summary>
        /// 生产部门
        /// </summary>
        public string ProDep_Name { get; set; }
        /// <summary>
        /// 生产人id
        /// </summary>
        public string ProEmpID { get; set; }
        /// <summary>
        /// 生产人
        /// </summary>
        public string ProEmp_Name { get; set; }
        /// <summary>
        /// 领用部门id
        /// </summary>
        public string ConDepID { get; set; }
        /// <summary>
        /// 领用部门
        /// </summary>
        public string ConDep_Name { get; set; }
        /// <summary>
        /// 领用人id
        /// </summary>
        public string ConEmpID { get; set; }
        /// <summary>
        /// 领用人
        /// </summary>
        public string ConEmp_Name { get; set; }
        /// <summary>
        /// 制单人id
        /// </summary>
        public string Creator { get; set; }
        /// <summary>
        /// 制单人
        /// </summary>
        public string Creator_Name { get; set; }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 修改人id
        /// </summary>
        public string Editor { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string Editor_Name { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? EditTime { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public string BillState { get; set; }
        /// <summary>
        /// 初审人id
        /// </summary>
        public string FirstChecker { get; set; }
        /// <summary>
        /// 初审人
        /// </summary>
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
        /// 是否红单
        /// </summary>
        public string IsRed { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 复审人
        /// </summary>
        public string SecondCheckerName { get; set; }
        /// <summary>
        /// 分阅人
        /// </summary>
        public string ReaderName { get; set; }

        /// <summary>
        /// 货物移动行
        /// </summary>
        public IList<GoodsMovementItem> Items { get; set; }
    }
}