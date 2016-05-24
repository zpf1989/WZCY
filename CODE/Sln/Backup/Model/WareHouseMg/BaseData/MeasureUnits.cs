using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class MeasureUnits
    {
        private string _unitid;
        private string _unitcode;
        private string _unitname;

        /// <summary>
        /// 计量单位ID
        /// </summary>
        public string UnitID
        {
            set { _unitid = value; }
            get { return _unitid; }
        }
        /// <summary>
        /// 计量单位编号
        /// </summary>
        public string UnitCode
        {
            set { _unitcode = value; }
            get { return _unitcode; }
        }
        /// <summary>
        /// 计量单位名称
        /// </summary>
        public string UnitName
        {
            set { _unitname = value; }
            get { return _unitname; }
        }
    }
}
