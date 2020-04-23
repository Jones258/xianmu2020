using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xianmu2020.Model
{
    public class BreakDto
    {
        public int BGLid { get; set; }
        public int BreakageType { get; set; }
        public string PreparedMan { get; set; }
        public int PreparedCount { get; set; }
        public int BreakageGLAduitState { get; set; }
        public string CreationMan { get; set; }
        public string Standby3 { get; set; }
        public DateTime? CreationTime { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int PageIndex { get; set; }
    }
}