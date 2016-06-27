using OA.GeneralClass;
using OA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.IDAL
{
    public interface IClientClassificationDAL : IBaseDAL<ClientClassification>
    {
        /// <summary>
        /// 进行客户分级操作
        /// </summary>
        /// <returns></returns>
        bool Classify();

        /// <summary>
        /// 指定客户是否存在
        /// </summary>
        /// <param name="clientNames"></param>
        /// <returns></returns>
        bool Exists(string[] clientNames);
        /// <summary>
        /// 清空表
        /// </summary>
        /// <returns></returns>
        bool Clear();
    }
}
