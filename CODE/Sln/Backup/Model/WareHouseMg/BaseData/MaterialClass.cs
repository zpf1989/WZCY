using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class MaterialClass
    {
        private string _materialclassid;
        private string _materialclasscode;
        private string _materialclassname;
        private string _remark;

        /// <summary>
        /// 物料分类ID
        /// </summary>
        public string MaterialClassID
        {
            set { _materialclassid = value; }
            get { return _materialclassid; }
        }
        /// <summary>
        /// 物料分类编号
        /// </summary>
        public string MaterialClassCode
        {
            set { _materialclasscode = value; }
            get { return _materialclasscode; }
        }
        /// <summary>
        /// 物料分类名称
        /// </summary>
        public string MaterialClassName
        {
            set { _materialclassname = value; }
            get { return _materialclassname; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
    }
}
