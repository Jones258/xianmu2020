using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace xianmu2020.Model
{
    public class BreakDto
    {
        public int BGLid;
        public int BreakageType;
        public string PreparedMan;
        public int PreparedCount;
        public int BreakageGLAduitState;
        public string CreationMan;
        public DateTime? CreationTime;
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}