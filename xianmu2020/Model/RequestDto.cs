using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xianmu2020.Model
{
    public class RequestDto
    {
        public int zt { get; set; }
        //入库
        public int PageIndex { get; set; }
        public int StoType { get; set; }
        public string StoOrderId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        //报损
        public int BGLid;
        public int BreakageType;
        public string PreparedMan;
        public int PreparedCount;
        public int BreakageGLAduitState;
        public string CreationMan;
        public DateTime? CreationTime;

    }
}