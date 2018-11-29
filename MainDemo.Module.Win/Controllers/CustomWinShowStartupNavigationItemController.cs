using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.SystemModule;
using MainDemo.Module.BusinessObjects;
using System.Windows.Forms;

namespace MyXafModule
{
    public class CustomWinShowStartupNavigationItemController : WinShowStartupNavigationItemController
    {
        protected override void ShowStartupNavigationItem(ShowNavigationItemController controller)
        {
            base.ShowStartupNavigationItem(controller);
            ((WinWindow)Application.MainWindow).Form.BeginInvoke(new MethodInvoker(() => {
                IObjectSpace os = Application.CreateObjectSpace();
                object theObjectToBeShown = os.FindObject<Contact>(null);
                DetailView dv = Application.CreateDetailView(os, theObjectToBeShown, true);
                dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
                Application.ShowViewStrategy.ShowViewInPopupWindow(dv);
            }));
            var a = 2;
        }
    }
}
