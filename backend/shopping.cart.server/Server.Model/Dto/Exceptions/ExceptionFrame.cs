using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model.Dto.Exceptions
{
    public class ExceptionFrame
    {
        public int LineNumber { get; set; }
        public string FileName { get; set; }
       // public string MethodName {get; set; }
    }
}
