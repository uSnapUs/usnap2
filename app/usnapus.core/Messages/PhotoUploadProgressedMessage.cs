using TinyMessenger;
using uSnapUs.Core.Model;

namespace uSnapUs.Core.Messages
{
    internal class PhotoUploadProgressedMessage : ITinyMessage
    {
        public object Sender { get; set; }

        public Photo Photo { get; set; }

        public int Uploaded { get; set; }
    }
}