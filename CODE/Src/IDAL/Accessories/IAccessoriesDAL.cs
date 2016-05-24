using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;

namespace OA.IDAL
{
    public interface IAccessoriesDAL
    {
        /// <summary>
        /// 获取一个附件对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Accessories Read(string id);
        /// <summary>
        /// 获取一条信息对应的所有附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        Accessories[] Read(string funCode, string infoID);
        /// <summary>
        /// 增加一条附件信息
        /// </summary>
        /// <param name="accessory"></param>
        /// <returns></returns>
        int Add(Accessories accessory);
        /// <summary>
        /// 批量增加附件信息
        /// </summary>
        /// <param name="accessories"></param>
        /// <returns></returns>
        int Add(Accessories[] accessories);
        /// <summary>
        /// 修改附件信息
        /// </summary>
        /// <param name="accessories"></param>
        /// <returns></returns>
        int Update(string funCode, string infoID, Accessories[] accessories);
        /// <summary>
        /// 删除一条附件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int Del(string id);
        /// <summary>
        /// 删除一条信息对应的所有附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        int Del(string funCode, string infoID);
        int Del(string funCode, string infoID, List<string> accessID);
        /// <summary>
        /// 将相同栏目一条信息的附件拷贝给令一条记录，用于邮件转发
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="oldInfoID"></param>
        /// <param name="newInfoID"></param>
        /// <returns></returns>
        bool Copy(string funCode, string oldInfoID, string newInfoID);

        /// <summary>
        /// 附件转子流程
        /// </summary>
        /// <param name="parentInstance">父流程ID</param>
        /// <param name="instanceId">子流程Id</param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        bool GoToChild(string parentInstance, string instanceId, string funCode);

        /// <summary>
        /// 附件转父流程
        /// </summary>
        /// <param name="parentInstance">父流程ID</param>
        /// <param name="instanceId">子流程Id</param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        bool GoToParent(string parentInstance, string instanceId, string oldCode, string newFunCode);
    }
}
