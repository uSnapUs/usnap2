using System;
using uSnapUs.Core.Views;

namespace uSnapUs.Core.Presenters
{
    public interface ILandingViewController:IBaseViewController
    {
        event EventHandler<EventArgs> FindButtonPressed;

        LandingPageViewState State { get; set; }
    }

    public enum LandingPageViewState
    {
        Default,
        FindNearby,
        
    }
}