using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xianmu2020.Model
{
    public class RequestDto
    {
        public int StoType { get; set; }
        public string StoOrderId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int PageIndex { get; set; }
    }
}