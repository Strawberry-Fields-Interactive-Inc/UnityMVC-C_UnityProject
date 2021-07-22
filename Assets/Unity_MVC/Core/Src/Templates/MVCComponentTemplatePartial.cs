using UnityMVC.Component;
using UnityMVC.View;

/* Autogenerated code. DO NOT CHANGE unless it is really needed and you know what you are doing. */
public partial class MVCComponentTemplate : MVCComponent
{
    public override void SetView(View view)
    {
        _view = view as ViewTemplate;
        OnViewWasSet(_view);
    }

    protected override void InternalStart()
    {
        RegisterEvents();
        _events.onCreated?.Invoke();
    }
    protected override void InternalOnDestroy()
    {
        _view.UnregisterComponentFromView(this);
        UnregisterEvents();
        _events.onDestroyed?.Invoke();
    }
}