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
        public string SuppliersType { get; set; }
        public int PageIndex { get; set; }//通用
        public int StoType { get; set; }
        public string StoOrderId { get; set; }
        public DateTime? Start { get; set; }//通用
        public DateTime? End { get; set; }//通用
        //报损
        public int BGLid;
        public int BreakageType;
        public string PreparedMan;
        public int PreparedCount;
        public int BreakageGLAduitState;
        public string CreationMan;
        public DateTime? CreationTime;

        //退货管理
        public string RefundId { get; set; }


        //盘点
        public string CheckId { get; set; }

        //出库
        public string DeliveryId { get; set; }
        public int DeliveryTYpe { get; set; }

    }
}