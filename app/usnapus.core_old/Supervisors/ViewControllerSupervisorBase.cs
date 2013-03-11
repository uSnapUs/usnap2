using System;
using usnapus.core.Controllers;
using usnapus.core.Views;

namespace usnapus.core.Supervisors
{
    public abstract class ViewControllerSupervisorBase<T>:IDisposable
    {
        readonly T _controller;

        protected ViewControllerSupervisorBase(T controller)
        {
            _controller = controller;
        }


        public abstract void Dispose();
    }
}