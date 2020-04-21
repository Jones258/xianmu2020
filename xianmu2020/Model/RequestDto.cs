using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xianmu2020.Model
{
    public class RequestDto
    {
        public int zt { get; set; }//通用
        //入库
        public int PageIndex { get; set; }//通用
        public int StoType { get; set; }
        public string StoOrderId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        //报损
        public int BGLid { get; set; }
        public int BreakageType { get; set; }
        public string Standby3 { get; set; }
        public string PreparedMan { get; set; }
        public int PreparedCount { get; set; }
        public int BreakageGLAduitState { get; set; }
        public string CreationMan { get; set; }
        public DateTime? CreationTime { get; set; }

        //退货管理
        public string RefundId { get; set; }

    }
}