using System;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Templates;
using MainDemo.Module.BusinessObjects;

namespace MainDemo.Module.Win.Controllers
{
    public class CustomViewController : ViewController
    {


        protected override void OnActivated()
        {
            base.OnActivated();
            //   PopupForm popupForm = Frame.Template as PopupForm;
            Form popupForm = Frame.Template as Form;
            if (popupForm != null)
            {
                popupForm.Shown += popupForm_Shown;
           
            }


            View.Closed += ClosedView;
        }

        private void ClosedView(object sender, EventArgs e)
        {
            if (View == SingleViewParameters.createdView)

            {
                ((DevExpress.ExpressApp.Win.WinShowViewStrategyBase)Application.ShowViewStrategy).CloseAllWindows();
                if (System.Windows.Forms.Application.MessageLoop)
                {
                    System.Windows.Forms.Application.Exit();
                }
                else
                {
                    System.Environment.Exit(1);
                }
            }
        }


        void popupForm_Shown(object sender, EventArgs e)
        {
            Form templateForm = (Form)sender;
            templateForm.Shown -= popupForm_Shown;
            templateForm.WindowState = FormWindowState.Maximized;
            templateForm.MinimumSize = templateForm.Size;
            templateForm.MaximumSize = templateForm.Size;
        }
    }
}