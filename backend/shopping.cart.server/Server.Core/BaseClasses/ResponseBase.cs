using Server.Core.Interfaces.Base;
using Server.Model.Dto;
using System;
using System.Collections.Generic;

namespace Server.Core.BaseClasses
{
    public class ResponseBase<T>:IResponseBase
    {
        public T Result { get; set; }
        public DateTime ResponseTime { get; set; } = DateTime.Now;
        public int RoecordCount { get; set; } = 0;
        public string Token { get; set; }
        public List<ValidationError> Errors { get; set; }
    }
}
