using Server.Model.Dto;
using System;
using System.Collections.Generic;

namespace Server.Core.Interfaces.Base
{
    public interface IResponseBase 
    {
        public DateTime ResponseTime { get; set; }
        public int RoecordCount { get; set; }
        public string Token { get; set; }
        public List<ValidationError> Errors { get; set; }
    }
}
