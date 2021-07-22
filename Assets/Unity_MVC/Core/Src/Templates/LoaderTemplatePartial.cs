using UnityMVC;
using UnityMVC.Model;

/* Autogenerated code. DO NOT CHANGE unless it is really needed and you know what you are doing. */
public partial class LoaderTemplate : Loader
{
    private SolverTemplate Solver => _solver;
    public /*new*/ void Initialize()
    {
        if (_solver != null)
        {
            return;
        }
        _solver = MVCApplication.Solvers.Get<SolverTemplate>();
    }
}
