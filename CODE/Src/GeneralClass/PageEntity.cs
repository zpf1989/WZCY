using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OA.GeneralClass
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    public class PageEntity
    {
        public PageEntity(int pageIndex, int pageSize)
        {
            this.PageIndex = pageIndex;
            this.PageSize = pageSize;
        }

        int _pageIndex = 0;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (value < 1)
                {
                    _pageIndex = 1;
                    return;
                }
                _pageIndex = value;
            }
        }

        int _pageSize = 10;
        const int _defaultPageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value < 1)
                {
                    _pageSize = _defaultPageSize;//默认分页大小为10
                    return;
                }
                _pageSize = value;
            }
        }

        public int TotalRecords { get; set; }

        public int PageCount
        {
            get
            {
                if (this.PageSize == 0)
                {
                    return 0;
                }
                return (int)Math.Ceiling(this.TotalRecords * 1.0 / this.PageSize);
            }
        }

        /// <summary>
        /// 分页查询起止范围
        /// </summary>
        public Range QueryRange
        {
            get
            {
                int start = 0;
                int end = 0;
                start = (this.PageIndex - 1) * this.PageSize + 1;
                end = start + this.PageSize - 1;

                return new Range { Start = start, End = end };
            }
        }
    }

    public class Range
    {
        public int Start;
        public int End;
    }
}