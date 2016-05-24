using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class PostInfo
    {
        private string _postid;
        private string _postcode;
        private string _postname;
        private string _parentpostid;
        private string _deptid;

        /// <summary>
        /// 岗位ID
        /// </summary>
        public string PostID
        {
            set { _postid = value; }
            get { return _postid; }
        }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string PostCode
        {
            set { _postcode = value; }
            get { return _postcode; }
        }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostName
        {
            set { _postname = value; }
            get { return _postname; }
        }
        /// <summary>
        /// 上级岗位ID
        /// </summary>
        public string ParentPostID
        {
            set { _parentpostid = value; }
            get { return _parentpostid; }
        }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptID
        {
            set { _deptid = value; }
            get { return _deptid; }
        }
    }
}
