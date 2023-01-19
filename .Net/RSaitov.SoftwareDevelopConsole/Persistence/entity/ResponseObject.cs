using System;
using System.Collections.Generic;
using System.Text;

namespace RSaitov.SoftwareDevelop.Data
{
    public class ResponseObject
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Value { get; set; }

        public ResponseObject(bool success)
        {
            Success = success;
        }

        public ResponseObject(string failMessage)
        {
            Success = false;
            Message = failMessage;
        }

        public ResponseObject(bool success, dynamic value)
        {
            Success = success;
            Value = value;
        }
    }
}
