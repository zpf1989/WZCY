using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 员工实体类
    /// </summary>
    public class EmpleeInfo
    {
        private string _empID;
        private string _empCode;
        private string _empName;
        private string _empSex;
        private int _empAge;
        private string _empBrithday;
        private string _empNative;
        private string _empNation;
        private string _empPolitics;
        private string _empPosition;
        private string _empEducation;
        private string _empSpecialty;
        private string _empSchool;
        private string _empState;
        private string _empTel;
        private string _empMobile;
        private string _empEmail;
        private string _remart;
        private DateTime _createTime;
        private string _createUserID;
        private string _empcard;
        private string _empaddress;
        private string _emptype;
        private string _empdate;
        private string _empcontractstart;
        private string _empcontractend;

        /// <summary>
        /// 员工ID
        /// </summary>
        public string EmpID
        {
            set { _empID = value; }
            get { return _empID; }
        }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmpCode
        {
            set { _empCode = value; }
            get { return _empCode; }
        }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmpName
        {
            get { return _empName; }
            set { this._empName = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string EmpSex
        {
            set { _empSex = value; }
            get { return _empSex; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int EmpAge
        {
            set { _empAge = value; }
            get { return _empAge; }
        }
        /// <summary>
        /// 生日
        /// </summary>
        public string EmpBrithday
        {
            set { _empBrithday = value; }
            get { return _empBrithday; }
        }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string EmpNative
        {
            set { _empNative = value; }
            get { return _empNative; }
        }
        /// <summary>
        /// 民族
        /// </summary>
        public string EmpNation
        {
            set { _empNation = value; }
            get { return _empNation; }
        }
        /// <summary>
        /// 政治面貌
        /// </summary>
        public string EmpPolitics
        {
            get { return _empPolitics; }
            set { this._empPolitics = value; }
        }
        /// <summary>
        /// 岗位
        /// </summary>
        public string EmpPosition
        {
            set { _empPosition = value; }
            get { return _empPosition; }
        }
        /// <summary>
        /// 学历
        /// </summary>
        public string EmpEducation
        {
            set { _empEducation = value; }
            get { return _empEducation; }
        }
        /// <summary>
        /// 专业
        /// </summary>
        public string EmpSpecialty
        {
            set { _empSpecialty = value; }
            get { return _empSpecialty; }
        }
        /// <summary>
        /// 毕业院校
        /// </summary>
        public string EmpSchool
        {
            set { _empSchool = value; }
            get { return _empSchool; }
        }
        /// <summary>
        /// 员工状态
        /// </summary>
        public string EmpState
        {
            set { _empState = value; }
            get { return _empState; }
        }
        /// <summary>
        /// 办公电话
        /// </summary>
        public string EmpTel
        {
            get { return _empTel; }
            set { this._empTel = value; }
        }
        /// <summary>
        /// 手机
        /// </summary>
        public string EmpMobile
        {
            set { _empMobile = value; }
            get { return _empMobile; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string EmpEmail
        {
            set { _empEmail = value; }
            get { return _empEmail; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remart
        {
            set { _remart = value; }
            get { return _remart; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createTime = value; }
            get { return _createTime; }
        }
        /// <summary>
        /// 创建人ID
        /// </summary>
        public string CreateUserID
        {
            set { _createUserID = value; }
            get { return _createUserID; }
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string EmpCard
        {
            set { _empcard = value; }
            get { return _empcard; }
        }
        /// <summary>
        /// 现居地
        /// </summary>
        public string EmpAddress
        {
            set { _empaddress = value; }
            get { return _empaddress; }
        }
        /// <summary>
        /// 用工形式
        /// </summary>
        public string EmpType
        {
            set { _emptype = value; }
            get { return _emptype; }
        }
        /// <summary>
        /// 用工起始时间
        /// </summary>
        public string EmpDate
        {
            set { _empdate = value; }
            get { return _empdate; }
        }
        /// <summary>
        /// 合同起始时间
        /// </summary>
        public string EmpContractStart
        {
            set { _empcontractstart = value; }
            get { return _empcontractstart; }
        }
        /// <summary>
        /// 合同结束时间
        /// </summary>
        public string EmpContractEnd
        {
            set { _empcontractend = value; }
            get { return _empcontractend; }
        }
    }
}
