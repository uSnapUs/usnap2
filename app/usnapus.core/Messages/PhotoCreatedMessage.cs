using TinyMessenger;
using uSnapUs.Core.Model;

namespace uSnapUs.Core.Messages
{
    public class PhotoCreatedMessage:ITinyMessage
    {
        public object Sender { get; private set; }

        public Photo Photo { get; set; }
    }
}
