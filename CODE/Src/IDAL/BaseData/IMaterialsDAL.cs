using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IMaterialsDAL : IBaseDAL<Materials>
    {
        /// <summary>
        /// 指定条件的物料是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        bool Exists(string where);
    }
}
