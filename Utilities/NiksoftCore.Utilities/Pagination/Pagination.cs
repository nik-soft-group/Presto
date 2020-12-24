using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiksoftCore.Utilities
{
    public class Pagination
    {
        public int TotalSize { get; set; }
        public int PageSize { get; set; }
        public int Part { get; set; }
        public int StartIndex { get; set; }

        public Pagination(int totalSize, int pageSize, int part)
        {
            TotalSize = totalSize;
            PageSize = pageSize;
            Part = part;
            StartIndex = GetSkip();
        }

        public int GetSkip()
        {
            if (Part == 1 || Part == 0)
            {
                return 0;
            }
            return (Part - 1) * PageSize;
        }

        public int GetTotalParts()
        {
            return Convert.ToInt32(Math.Ceiling((double)TotalSize / PageSize));
        }
    }
}
