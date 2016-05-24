using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class EmpChange
    {
        private string _empchangeid;
        private string _empid;
        private string _changedate;
        private string _changetype;
        private string _oldpostid;
        private string _newpostid;
        private string _changereason;
        private string _deptmanagerid;
        private string _deptview;
        private string _hrmanagerid;
        private string _hrview;
        private string _managerid;
        private string _mview;
        private string _state;
        private string _creatorid;
        private DateTime _createtime;

        /// <summary>
        /// 主编ID
        /// </summary>
        public string EmpChangeID
        {
            set { _empchangeid = value; }
            get { return _empchangeid; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmpID
        {
            set { _empid = value; }
            get { return _empid; }
        }
        /// <summary>
        /// 变动时间
        /// </summary>
        public string ChangeDate
        {
            set { _changedate = value; }
            get { return _changedate; }
        }
        /// <summary>
        /// 变动类型（1：晋升，2：平调，3：离职）
        /// </summary>
        public string ChangeType
        {
            set { _changetype = value; }
            get { return _changetype; }
        }
        /// <summary>
        /// 原岗位ID
        /// </summary>
        public string OldPostID
        {
            set { _oldpostid = value; }
            get { return _oldpostid; }
        }
        /// <summary>
        /// 新岗位ID
        /// </summary>
        public string NewPostID
        {
            set { _newpostid = value; }
            get { return _newpostid; }
        }
        /// <summary>
        /// 变动原因
        /// </summary>
        public string ChangeReason
        {
            set { _changereason = value; }
            get { return _changereason; }
        }
        /// <summary>
        /// 部门经理ID
        /// </summary>
        public string DeptManagerID
        {
            set { _deptmanagerid = value; }
            get { return _deptmanagerid; }
        }
        /// <summary>
        /// 部门经理意见
        /// </summary>
        public string DeptView
        {
            set { _deptview = value; }
            get { return _deptview; }
        }
        /// <summary>
        /// 人事经理ID
        /// </summary>
        public string HRManagerID
        {
            set { _hrmanagerid = value; }
            get { return _hrmanagerid; }
        }
        /// <summary>
        /// 人事经理意见
        /// </summary>
        public string HRView
        {
            set { _hrview = value; }
            get { return _hrview; }
        }
        /// <summary>
        /// 管理者代表ID
        /// </summary>
        public string ManagerID
        {
            set { _managerid = value; }
            get { return _managerid; }
        }
        /// <summary>
        /// 管理者代表意见
        /// </summary>
        public string MView
        {
            set { _mview = value; }
            get { return _mview; }
        }
        /// <summary>
        /// 单据状态（0：编制，1：提交，2：部门经理审核通过，3：部门经理审核不通过，4：人事经理审核通过，5：人事经理审核不通过，6：管理代表审核通过，7：管理代表不通过，8：关闭）
        /// </summary>
        public string State
        {
            set { _state = value; }
            get { return _state; }
        }
        /// <summary>
        /// 制单人ID
        /// </summary>
        public string CreatorID
        {
            set { _creatorid = value; }
            get { return _creatorid; }
        }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
    }
}
