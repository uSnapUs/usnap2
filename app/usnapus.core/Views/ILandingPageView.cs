using System;
using MonoTouch.FacebookConnect;

namespace uSnapUs.Core.Views
{
    public interface ILandingPageView
    {
        event EventHandler<EventArgs> FindButtonPressed;
        event EventHandler<EventArgs> CreateButtonPressed;
        event EventHandler<EventArgs> MyEventsButtonPressed;
        event EventHandler<EventArgs> BackButtonPressed;
        event EventHandler<EventArgs> FindNearbyButtonPressed;
        event EventHandler<EventArgs> JoinButtonPressed;

        FBLoginView LoginButton { get; }
    }
}