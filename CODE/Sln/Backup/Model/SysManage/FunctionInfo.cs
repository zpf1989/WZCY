using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    /// <summary>
    /// 功能实体类
    /// </summary>
    public class FunctionInfo
    {
        private int _funID;
        private string _funName;
        private int _parentFunID;
        private string _funURL;

        /// <summary>
        /// 功能ID
        /// </summary>
        public int FunID
        {
            set { _funID = value; }
            get { return _funID; }
        }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunName
        {
            set { _funName = value; }
            get { return _funName; }
        }
        /// <summary>
        /// 父节点功能ID
        /// </summary>
        public int ParentFunID
        {
            get { return _parentFunID; }
            set { this._parentFunID = value; }
        }
        /// <summary>
        /// 功能URL
        /// </summary>
        public string FunURL
        {
            set { _funURL = value; }
            get { return _funURL; }
        }
    }
}
