using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.DTOs.Paging
{
    public class PagingRequest
    {
        //private int rowCount = 5;
        //public int RowCount { get => rowCount; set => rowCount = Math.Min(10, value); }
        //public int PageNumber { get; set; } = 1;

        public int page { get; set; }
        public int pageSize { get; set; }
}
}
