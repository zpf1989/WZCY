using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Data;

namespace OA.BLL
{
    public class ClientBLL
    {
        private readonly OA.IDAL.IClientDAL iClientDAL = DALFactory.Helper.GetIClientDAL();

        /// <summary>
        /// 新增客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddClient(Client model)
        {
            return iClientDAL.AddClient(model);
        }
        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateClient(Client model)
        {
            return iClientDAL.UpdateClient(model);
        }
        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        public bool DelClient(string ClientID)
        {
            return iClientDAL.DelClient(ClientID);
        }
        /// <summary>
        /// 根据客户编号和名称判断是否已经存在
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        public bool Exists(string ClientCode, string ClientName)
        {
            return iClientDAL.Exists(ClientCode, ClientName);
        }
        /// <summary>
        /// 根据客户名称判断是否已经存在
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        public bool Exists4Update(string ClientCode, string ClientName)
        {
            return iClientDAL.Exists4Update(ClientCode, ClientName);
        }
        /// <summary>
        /// 返回分页列表集合
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <param name="pageSize">一页要显示的行数</param>
        /// <param name="startRowIndex">页码索引</param>
        /// <returns></returns>
        public DataSet GetPageList(string ClientCode, string ClientName, int pageSize, int startRowIndex)
        {
            return iClientDAL.GetPageList(ClientCode, ClientName, pageSize, startRowIndex);
        }
        /// <summary>
        /// 返回数据的所有行数
        /// </summary>
        /// <param name="ClientCode">客户编号</param>
        /// <param name="ClientName">客户名称</param>
        /// <returns></returns>
        public int GetRowCounts(string ClientCode, string ClientName)
        {
            return iClientDAL.GetRowCounts(ClientCode, ClientName);
        }
        /// <summary>
        /// 获取客户模型
        /// </summary>
        /// <param name="ClientID"></param>
        /// <returns></returns>
        public Client GetModel(string ClientID)
        {
            return iClientDAL.GetModel(ClientID);
        }
        /// <summary>
        /// 获取客户List
        /// </summary>
        /// <returns></returns>
        public IList<Client> GetClientList()
        {
            return iClientDAL.GetClientList();
        }
    }
}
