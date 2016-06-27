using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IClientLevelDAL : IBaseDAL<ClientLevel>
    {
        /// <summary>
        /// 名称是否已存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] name);
    }
}
