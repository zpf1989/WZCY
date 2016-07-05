using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 询价单主表
    /// </summary>
    public class AskPrice
    {
        public string APID { get; set; }
        /// <summary>
        /// 询价单编号
        /// </summary>
        public string APCode { get; set; }
        /// <summary>
        /// 询价单类型
        /// </summary>
        public string APType { get; set; }

        /// <summary>
        /// 询价日期
        /// </summary>
        public DateTime AskDate { get; set; }
        /// <summary>
        /// 客户id
        /// </summary>
        public string ClientID { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string Client_Name { get; set; }
        /// <summary>
        /// 客户联系人
        /// </summary>
        public string Client_Contact { get; set; }
        /// <summary>
        /// 客户电话
        /// </summary>
        public string Client_Tel { get; set; }
        /// <summary>
        /// 客户地址
        /// </summary>
        public string Client_Address { get; set; }
        public string PayTypeID { get; set; }
        /// <summary>
        /// 付款方式
        /// </summary>
        public string PayType_Name { get; set; }
        /// <summary>
        /// 跟踪情况
        /// </summary>
        public string TrackDescription { get; set; }
        /// <summary>
        /// 客户调查
        /// </summary>
        public string ClientSurvey { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string APRemark { get; set; }
        public string Creator { get; set; }
        public string Creator_Name { get; set; }
        public DateTime CreateTime { get; set; }
        public string Editor { get; set; }
        public string Editor_Name { get; set; }
        public DateTime EditTime { get; set; }
        public string FirstChecker { get; set; }
        public string FirstChecker_Name { get; set; }
        public DateTime FirstCheckTime { get; set; }
        /// <summary>
        /// 初审意见
        /// </summary>
        public string FirstCheckView { get; set; }
        /// <summary>
        /// 单据状态：1:编制   2:提交   3:审核通过   4:审核不通过   5:批准通过   6:批准不通过   7:关闭
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// 复审人姓名
        /// </summary>
        public string SecondCheckerName { get; set; }
        /// <summary>
        /// 分阅人姓名
        /// </summary>
        public string ReaderName { get; set; }
        /// <summary>
        /// 询价单行
        /// </summary>
        public IList<AskPriceItem> Items { get; set; }
    }
}