using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OA.Model
{
    public class Accessories
    {
        private string _id;
        private string _funcode;
        private string _infoid;
        private string _oldname;
        private string _oldfullname;
        private string _newname;
        private string _newfullname;
        private string _filetype;
        private int _filelength;
        private DateTime _addtime;
        private DateTime _edittime;

        /// <summary>
        /// 附件序列号
        /// </summary>
        public string Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 功能（模块）编码
        /// </summary>
        public string FunCode
        {
            set { _funcode = value; }
            get { return _funcode; }
        }
        /// <summary>
        /// 信息id
        /// </summary>
        public string InfoId
        {
            set { _infoid = value; }
            get { return _infoid; }
        }
        /// <summary>
        /// 用户上传的文件名称
        /// </summary>
        public string OldName
        {
            set { _oldname = value; }
            get { return _oldname; }
        }
        /// <summary>
        /// 用户上传的文件全名称
        /// </summary>
        public string OldFullName
        {
            set { _oldfullname = value; }
            get { return _oldfullname; }
        }
        /// <summary>
        /// 系统重命名后的附件名称
        /// </summary>
        public string NewName
        {
            set { _newname = value; }
            get { return _newname; }
        }
        /// <summary>
        /// 系统重命名后的文件全名
        /// </summary>
        public string NewFullName
        {
            set { _newfullname = value; }
            get { return _newfullname; }
        }
        /// <summary>
        /// 附件文件类型
        /// </summary>
        public string FileType
        {
            set { _filetype = value; }
            get { return _filetype; }
        }
        /// <summary>
        /// 附件文件大小
        /// </summary>
        public int FileLength
        {
            set { _filelength = value; }
            get { return _filelength; }
        }
        /// <summary>
        /// 增加时间
        /// </summary>
        public DateTime AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime
        {
            set { _edittime = value; }
            get { return _edittime; }
        }
    }
}
