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
        public string Standby3 { get; set; }//单据编号
        public string PreparedMan { get; set; }
        public int PreparedCount { get; set; }
        public int BreakageGLAduitState { get; set; }
        public string CreationMan { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Standby4 { get; set; }//记录产品
        //退货管理
        public string RefundId { get; set; }
        //出库产品、入库产品、报损产品表
        public int CRBTPid { get; set; }
        public string BorrowedId { get; set; }
        public string BorrowedName { get; set; }
        public string BorrowedBarcode { get; set; }
        public string Specifications { get; set; }
        public string Batch { get; set; }
        public decimal SinglePrice { get; set; }
        public string StorageCount { get; set; }
        public string DeliveryCount { get; set; }
        public string RefundCount { get; set; }
        public string BreakageCount { get; set; }
        public string StorageNumber { get; set; }
        public string DistinctionState { get; set; }
        public int Standby1 { get; set; }//库位
        public int State { get; set; }


    }
}