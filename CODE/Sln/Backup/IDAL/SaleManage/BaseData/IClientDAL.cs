using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.IDAL
{
    public interface IClientDAL
    {
        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        int AddClient(Client model);
        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool UpdateClient(Client model);
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        bool DelClient(string ClientID);
        /// <summary>
        /// 根据客户编号和名称判断是否已经存在
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        bool Exists(string ClientCode, string ClientName);
        /// <summary>
        /// 根据客户名称判断是否已经存在
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        bool Exists4Update(string ClientCode, string ClientName);
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        DataSet GetPageList(string ClientCode, string ClientName, int pageSize, int startRowIndex);
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        int GetRowCounts(string ClientCode, string ClientName);
        /// <summary>
        /// 获取客户模型
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        Client GetModel(string ClientID);
        /// <summary>
        /// 获取客户List
        /// </summary>
        /// <returns></returns>
        IList<Client> GetClientList();
    }
}
