﻿using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    /// <summary>
    /// 仓库数据层接口
    /// </summary>
    public interface IWareHouseDAL : IBaseDAL<WareHouse>
    {
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="codes"></param>
        /// <returns>true:存在，false：不存在</returns>
        bool Exists(params string[] codes);
    }
}
