namespace usnapus.core.Controllers
{
    public interface IViewController<TView>
    {
        TView CreateView();
    }
}