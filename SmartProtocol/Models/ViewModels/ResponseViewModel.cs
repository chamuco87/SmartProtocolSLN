using System;
using System.Collections.Generic;

namespace SmartProtocol.ViewModels
{
    public class ResponseViewModel
    {
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }
        public IEnumerable<Error> Errors { get; set; }
    }

    public class Error
    { 
        public string ErrorCode { get; set; }
        public string ErrorDetail { get; set; }
    }
}
