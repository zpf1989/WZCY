﻿using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IAPReaderDAL : IBaseDAL<APReader>
    {
        /// <summary>
        /// 根据删除指定询价单下的所有分阅记录
        /// </summary>
        /// <param name="apIds"></param>
        /// <returns></returns>
        bool DeleteByAPIds(params string[] apIds);
    }
}
