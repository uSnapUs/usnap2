// ReSharper disable InconsistentNaming

using System;
using System.Linq;
using Machine.Fakes;
using Machine.Fakes.Adapters.Moq;
using Machine.Specifications;
using Moq;
using uSnapUs.Core.Presenters;
using uSnapUs.Core.Supervisors;
using uSnapUs.Core.Views;
using usnapus.core.specs.Helpers;
using It = Machine.Specifications.It;

namespace usnapus.core.specs.Supervisors
{
    namespace LandingViewControllerSupervisorSpecifications
    {
        [Subject(typeof (LandingViewControllerSupervisor))]
        public abstract class LandingViewControllerSupervisorSpecification:WithFakes<MoqFakeEngine>
        {
            protected static LandingViewControllerSupervisor _sut;
            protected static Mock<ILandingViewController> _viewController;
                Establish context = () =>
                    {
                        _viewController = new Mock<ILandingViewController>();
                        _sut = new LandingViewControllerSupervisor(_viewController.Object);
                    };

            static Mock<ILandingPageView> _view;
        }

        public class on_controller_unload:LandingViewControllerSupervisorSpecification
        {
            Establish context = () => _sut = new LandingViewControllerSupervisor(_stubView);
            Because of = () => _stubView.RaiseUnloadEvent();
            It should_no_longer_have_any_views_wired_up = () => EventHelpers.GetAllEventHandlers(_stubView).Count().ShouldEqual(0);
            static StubLandingViewController _stubView = new StubLandingViewController();
        }
        public class when_a_user_presses_find_nearby_button:LandingViewControllerSupervisorSpecification
        {
            Because of =
                () =>
                _viewController.Raise(viewController => viewController.FindButtonPressed += null, (EventArgs) null);
            It should_set_the_view_state_to_find_nearby = () => _viewController.VerifySet(viewController => viewController.State = LandingPageViewState.FindNearby);
        }
        
    }
}

namespace usnapus.core.specs.Supervisors.LandingViewControllerSupervisorSpecifications
{
    internal class StubLandingViewController : ILandingViewController
    {
        
        public event EventHandler<EventArgs> Load;
        public event EventHandler<EventArgs> Unload;
        public event EventHandler<EventArgs> Appear;
        public event EventHandler<EventArgs> BeforeAppear;
        public ILandingPageView LandingPageView { get; private set; }

        public void RaiseUnloadEvent()
        {
            if (Unload != null)
            {
                Unload.Invoke(this,null);
            }
        }

        public event EventHandler<EventArgs> FindButtonPressed;
        public LandingPageViewState State { get; set; }
        
    }
}