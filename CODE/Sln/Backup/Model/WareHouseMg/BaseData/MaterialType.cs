using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class MaterialType
    {
        private string _materialtypeid;
        private string _materialtypecode;
        private string _materialtypename;
        private string _remark;

        /// <summary>
        /// 物料类型ID
        /// </summary>
        public string MaterialTypeID
        {
            set { _materialtypeid = value; }
            get { return _materialtypeid; }
        }
        /// <summary>
        /// 物料类型编号
        /// </summary>
        public string MaterialTypeCode
        {
            set { _materialtypecode = value; }
            get { return _materialtypecode; }
        }
        /// <summary>
        /// 物料类型名称
        /// </summary>
        public string MaterialTypeName
        {
            set { _materialtypename = value; }
            get { return _materialtypename; }
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
