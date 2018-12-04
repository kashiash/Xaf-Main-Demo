using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using System;
using System.Linq;

namespace MainDemo.Module.Win.Controllers
{
    public class CustomStartupNavigationItemController : WindowController
    {
        private ShowNavigationItemController navigationController;

        private bool viewIsInitailized = false;
        public CustomStartupNavigationItemController()
        {
            TargetWindowType = WindowType.Main;
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            this.ViewClosed += OnClosingApp;
            navigationController = Frame.GetController<ShowNavigationItemController>();
            if (navigationController != null)
            {
                navigationController.CustomShowNavigationItem += OnCustomShowNavigationItem;
            }
        }

        private void OnClosingApp(object sender, EventArgs e)
        {

            if (this.Window.IsMain && viewIsInitailized)
            {
                ((DevExpress.ExpressApp.Win.WinShowViewStrategyBase)Application.ShowViewStrategy).CloseAllWindows();
            }
        }


        void OnCustomShowNavigationItem(object sender, CustomShowNavigationItemEventArgs e)
        {
            navigationController.CustomShowNavigationItem -= OnCustomShowNavigationItem;//It is important to unsubscribe from this event immediately.
            ShowViewParameters svp = e.ActionArguments.ShowViewParameters;
            IObjectSpace os = Application.CreateObjectSpace();

            //object theObjectToBeShown = os.FindObject<Contact>(null);
            //DetailView dv = Application.CreateDetailView(os, theObjectToBeShown, true);
            //dv.ViewEditMode = DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            //svp.CreatedView = dv;
            //e.Handled = true;

            Type objectType = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(x => x.GetTypes())
                       .FirstOrDefault(x => x.Name == "Payment");

            //string listViewId = Application.FindLookupListViewId(objectType); // "Person_ListView";//
            //string listViewId = Application.FindDetailViewId(objectType);

            DevExpress.ExpressApp.View listView = Application.CreateListView(Application.CreateObjectSpace(), objectType, false);

            //string listViewId = Application.FindListViewId(objectType);
            //CollectionSourceBase collectionSource = Application.CreateCollectionSource(Application.CreateObjectSpace(), objectType, listViewId);
            //DevExpress.ExpressApp.View listView = Application.CreateListView(listViewId, collectionSource, true);
            svp.TargetWindow = TargetWindow.Current;
            svp.CreatedView = listView;
            viewIsInitailized = true;
            e.Handled = true;
        }
        protected override void OnDeactivated()
        {
            if (navigationController != null)
            {
                navigationController.CustomShowNavigationItem -= OnCustomShowNavigationItem;
            }
            this.ViewClosed -= OnClosingApp;
            base.OnDeactivated();
        }
    }


}
