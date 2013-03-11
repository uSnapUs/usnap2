using System;
using TinyMessenger;
using uSnapUs.Core.Model;

namespace uSnapUs.Core.Messages
{
    internal class PhotoUploadFinishedMessage : ITinyMessage
    {
        public object Sender { get; set; }

        public Photo Photo { get; set; }

        public Exception Error { get; set; }

        public bool Cancelled { get; set; }

        public string Response { get; set; }
    }
}
