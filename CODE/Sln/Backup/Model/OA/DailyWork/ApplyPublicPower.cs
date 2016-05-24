using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class ApplyPublicPower
    {
        private string _appid;
        private string _deptid;
        private string _applydate;
        private string _creator;
        private DateTime _createtime;
        private string _firstchecker;
        private DateTime _firstoperatetime;
        private string _secondchecker;
        private DateTime _secondoperatetime;
        private string _opinion;

        /// <summary>
        /// 主键
        /// </summary>
        public string AppID
        {
            set { _appid = value; }
            get { return _appid; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyDate
        {
            set { _applydate = value; }
            get { return _applydate; }
        }
        /// <summary>
        /// 制单人
        /// </summary>
        public string Creator
        {
            set { _creator = value; }
            get { return _creator; }
        }
        /// <summary>
        /// 制单时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 初审人
        /// </summary>
        public string FirstChecker
        {
            set { _firstchecker = value; }
            get { return _firstchecker; }
        }
        /// <summary>
        /// 初审时间
        /// </summary>
        public DateTime FirstOperateTime
        {
            set { _firstoperatetime = value; }
            get { return _firstoperatetime; }
        }
        /// <summary>
        /// 复审人
        /// </summary>
        public string SecondChecker
        {
            set { _secondchecker = value; }
            get { return _secondchecker; }
        }
        /// <summary>
        /// 复审时间
        /// </summary>
        public DateTime SecondOperateTime
        {
            set { _secondoperatetime = value; }
            get { return _secondoperatetime; }
        }
        /// <summary>
        /// 意见
        /// </summary>
        public string Opinion
        {
            set { _opinion = value; }
            get { return _opinion; }
        }
    }
}
