using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OA.Model;
using System.Web;

namespace OA.BLL
{
    public class AccessoriesBLL
    {
        private readonly OA.IDAL.IAccessoriesDAL iAccessoriesDAL = DALFactory.Helper.GetIAccessoriesDAL();
        public static string accessoryPath = System.Configuration.ConfigurationManager.AppSettings["AccessoryPath"].ToString();

        public AccessoriesBLL()
        {
        }
        public Accessories Read(string id)
        {
            return iAccessoriesDAL.Read(id);
        }
        /// <summary>
        /// 增加附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public int AddAccessories(string funCode, string infoID, HttpPostedFile[] files)
        {
            Accessories[] accessories = new Accessories[files.Length];
            for (int i = 0; i < files.Length;i++ )
            {
                if (files[i]!=null&&files[i].FileName != "")
                {
                    Accessories accessory = new Accessories();
                    accessory.FunCode = funCode;
                    accessory.InfoId = infoID;
                    string fileType = files[i].FileName.Substring(files[i].FileName.LastIndexOf("."),files[i].FileName.Length - files[i].FileName.LastIndexOf("."));
                    accessory.OldFullName = files[i].FileName.Substring(files[i].FileName.LastIndexOf('\\')+1,files[i].FileName.Length-1-files[i].FileName.LastIndexOf('\\'));
                    accessory.OldName = accessory.OldFullName.Substring(0, accessory.OldFullName.IndexOf("."));
                    string newName = GetNewName();
                    accessory.NewName = newName;
                    accessory.NewFullName = newName + fileType;
                    accessory.FileType = files[i].ContentType.ToString();
                    accessory.FileLength = files[i].ContentLength;
                    accessory.AddTime = System.DateTime.Now;
                    accessory.EditTime = System.DateTime.Now;
                    accessories[i] = accessory;
                    //保存文件
                    files[i].SaveAs(accessoryPath+"\\"+accessory.NewName);
                    //保存pdf文件
                    if(fileType.ToLower()==".pdf")
                    {
                        string path = @"~/Source/modules/workflow/PdfFolder";
                         path=System.Web.HttpContext.Current.Server.MapPath(path);
                         files[i].SaveAs(path + "\\" + accessory.NewFullName);
                    }
                   

                }
            }
            return iAccessoriesDAL.Add(accessories);
            
        }
        /// <summary>
        /// 修改附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public int UpdateAccessories(string funCode, string infoID, HttpPostedFile[] files, List<string> accessID)
        {
            iAccessoriesDAL.Del(funCode, infoID, accessID);
            return AddAccessories(funCode,infoID,files);
        }
        /// <summary>
        /// 取得一条信息对应的附件信息
        /// </summary>
        /// <param name="funCode"></param>
        /// <param name="infoID"></param>
        /// <returns></returns>
        public Accessories[] GetAccessories(string funCode, string infoID)
        {
            return iAccessoriesDAL.Read(funCode, infoID);
        }
        /// <summary>
        /// 获取附件重命名后的名称
        /// </summary>
        /// <returns></returns>
        private string GetNewName()
        {
            System.Random ran = new Random();

            return System.DateTime.Now.ToString().Replace(":", "").Replace("/", "") + Guid.NewGuid().ToString().Replace("-","");
        }
        /// <summary>
        /// 删除一条附件信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (iAccessoriesDAL.Del(id) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 将指定已有信息的附件信息拷贝给现有信息
        /// </summary>
        /// <returns></returns>
        public bool Copy(string funCode, string oldInfoID, string newInfoID)
        {
            return iAccessoriesDAL.Copy(funCode, oldInfoID, newInfoID);
        }

        /// <summary>
        /// 附件转子流程(只对数据库进行了操作，父流程与子流程的附件指向同一文件)
        /// </summary>
        /// <param name="parentInstance">父流程ID</param>
        /// <param name="instanceId">子流程Id</param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        public bool GoToChildFlow(string parentInstance, string instanceId, string funCode)
        {
            return iAccessoriesDAL.GoToChild(parentInstance, instanceId, funCode);
        }

        /// <summary>
        /// 附件转父流程(只对数据库进行了操作，父流程与子流程的附件指向同一文件)
        /// </summary>
        /// <param name="parentInstance">父流程ID</param>
        /// <param name="instanceId">子流程Id</param>
        /// <param name="funCode"></param>
        /// <returns></returns>
        public bool GoToParentFlow(string oldInstance, string newInstance, string oldCode, string newFunCode)
        {
            return iAccessoriesDAL.GoToParent(oldInstance, newInstance, oldCode, newFunCode);
        }

        /// <summary>
        /// 增加一条附件信息
        /// </summary>
        /// <param name="_accessory"></param>
        /// <returns></returns>
        public int Add(Accessories _accessory)
        {
            return iAccessoriesDAL.Add(_accessory);
        }
    }
}
