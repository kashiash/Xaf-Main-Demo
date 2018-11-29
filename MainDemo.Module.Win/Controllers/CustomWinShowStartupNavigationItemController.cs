using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Win.SystemModule;
using MainDemo.Module.BusinessObjects;
using System;
using System.Linq;
using System.Windows.Forms;

namespace MyXafModule
{
    public class CustomWinShowStartupNavigationItemController : WinShowStartupNavigationItemController
    {
        protected override void ShowStartupNavigationItem(ShowNavigationItemController controller)
        {
            base.ShowStartupNavigationItem(controller);
            ((WinWindow)Application.MainWindow).Form.BeginInvoke(new MethodInvoker(() =>
            {
               ShowListView();
            }));
        
        }

        private void ShowListView()
        {
            IObjectSpace os = Application.CreateObjectSpace();
            Type objectType = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(x => x.GetTypes())
                       .FirstOrDefault(x => x.Name == "Payment");

            //string listViewId = Application.FindLookupListViewId(objectType); // "Person_ListView";//
            //string listViewId = Application.FindDetailViewId(objectType);

            DevExpress.ExpressApp.View view = Application.CreateListView(os, objectType, false);
            ShowViewParameters svp = new ShowViewParameters();

            Application.ShowViewStrategy.ShowViewInPopupWindow(view,  OkCode, CancelCode);




        }

        private void ShowDetailView()
        {
            IObjectSpace os = Application.CreateObjectSpace();
            object theObjectToBeShown = os.FindObject<Contact>(null);
            DetailView dv = Application.CreateDetailView(os, theObjectToBeShown, true);
            dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            Application.ShowViewStrategy.ShowViewInPopupWindow(dv, OkCode, CancelCode);
        }

        private void CancelCode()
        {
            ((DevExpress.ExpressApp.Win.WinShowViewStrategyBase)Application.ShowViewStrategy).CloseAllWindows();
        }

        private void OkCode()
        {
            ((DevExpress.ExpressApp.Win.WinShowViewStrategyBase)Application.ShowViewStrategy).CloseAllWindows();
        }
    }
}
