using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace ProjectBoostLadder.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
