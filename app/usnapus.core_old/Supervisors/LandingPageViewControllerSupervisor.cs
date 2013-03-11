using usnapus.core.Controllers;
using usnapus.core.Views;

namespace usnapus.core.Supervisors
{
    public class LandingPageViewControllerSupervisor : ViewControllerSupervisorBase<ILandingPageViewController>
    {
        public LandingPageViewControllerSupervisor(ILandingPageViewController viewController) : base(viewController)
        {
         
        }


        public override void Dispose()
        {
            
        }
    }
}