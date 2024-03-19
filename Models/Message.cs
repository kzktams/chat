using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Message
    {
        public string Sender { get; set; }

        public string SendDate { get; set; } = DateTime.Now.ToString("HH:mm");

        public string SentMessage { get; set; }

    }
}
