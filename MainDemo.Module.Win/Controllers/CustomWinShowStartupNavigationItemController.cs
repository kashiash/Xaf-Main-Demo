using DevExpress.ExpressApp;
using MainDemo.Module.BusinessObjects;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.SystemModule;
using System.Windows.Forms;
using DevExpress.ExpressApp.Win;
namespace MainDemo.Module.Win.Controllers
{
    public class CustomWinShowStartupNavigationItemController : WinShowStartupNavigationItemController
    {
        protected override void ShowStartupNavigationItem(ShowNavigationItemController controller)
        {
            base.ShowStartupNavigationItem(controller);
            ((WinWindow)Application.MainWindow).Form.BeginInvoke(new MethodInvoker(() =>
            {
                IObjectSpace os = Application.CreateObjectSpace();
                DevExpress.ExpressApp.View view = Application.CreateListView(os, typeof(Contact), true);
                var sp = new ShowViewParameters();
                sp.CreatedView = view;
                sp.NewWindowTarget = NewWindowTarget.MdiChild;

                sp.TargetWindow = TargetWindow.Default;
                var ss = new ShowViewSource(this.Frame, null);


                SingleViewParameters.createdView = view;
                Application.ShowViewStrategy.ShowView(sp, ss);
            }));
        }
    }
}
