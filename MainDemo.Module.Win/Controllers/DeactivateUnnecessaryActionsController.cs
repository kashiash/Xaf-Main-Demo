using DevExpress.ExpressApp;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win.SystemModule;
using DevExpress.ExpressApp.Win.Templates;
using DevExpress.XtraBars.Docking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDemo.Module.Win.Controllers
{

    public class DeactivateUnnecessaryActionsController : Controller
    {
        private const string Key = "DeactivateUnnecessaryActionsController";
        private bool singleViewExecution = false;

        protected override void OnActivated()
        {
            base.OnActivated();
            singleViewExecution = CheckSingleVievExecution();
            if (singleViewExecution)
            {
                ShowNavigationItemController navigationController = Frame.GetController<ShowNavigationItemController>();
                if (navigationController != null)
                    navigationController.Active[Key] = false;
                EditModelController modelController = Frame.GetController<EditModelController>();
                if (modelController != null)
                    modelController.Active[Key] = false; 
            }

      
        }

        private bool CheckSingleVievExecution()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                string param = (string)args.GetValue(2);
                if (param.Contains("TargetView="))
                {
                    return true;
                }
            }
            return false;
        }
           
     

        protected override void OnDeactivated()
        {
            if (singleViewExecution)
            {
                ShowNavigationItemController navigationController = Frame.GetController<ShowNavigationItemController>();

                if (navigationController != null)
                    navigationController.Active[Key] = true;
                EditModelController modelController = Frame.GetController<EditModelController>();
                if (modelController != null)
                    modelController.Active[Key] = true; 
            }
            base.OnDeactivated();
        }
    }
}
