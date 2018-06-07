using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace boonservice.api.Models
{
    public class Message
    {
        public string message_type { get; set; }
        public string message_code { get; set; }
        public string message_desc { get; set; }
    }
}