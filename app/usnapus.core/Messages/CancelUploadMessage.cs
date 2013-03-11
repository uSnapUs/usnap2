using TinyMessenger;
using uSnapUs.Core.Model;

namespace uSnapUs.Core.Messages
{
    public class CancelUploadMessage : ITinyMessage
    {
        public object Sender { get; set; }

        public Photo Photo { get; set; }
    }
}