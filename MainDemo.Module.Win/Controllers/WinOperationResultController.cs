using System;
using System.Windows.Forms;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Win.Templates;


namespace XafApplication51.Module.Win.Controllers
{
    public class WinOperationResultController : ViewController<DetailView>
    {

        Timer timer;

        protected override void OnActivated()
        {
            base.OnActivated();
            PopupForm popupForm = Frame.Template as PopupForm;
            if (popupForm != null)
            {
             //   popupForm.ControlBox = ViewCurrentObject.ShowClose;
                popupForm.Shown += popupForm_Shown;
            }
        }

        void popupForm_Shown(object sender, EventArgs e)
        {
            Form templateForm = (Form)sender;
            templateForm.Shown -= popupForm_Shown;
            templateForm.Size = new System.Drawing.Size(800,800);// new System.Drawing.Size(400, 200);
            //if (ViewCurrentObject.AutoCloseIn > 0)
            //{
            //    timer = new Timer();
            //    timer.Interval = ViewCurrentObject.AutoCloseIn;
            //    timer.Tick += timer_Tick;
            //    timer.Start();
            //}
        }

        void timer_Tick(object sender, EventArgs e)
        {
            DisposeTimer();
            ((Form)Frame.Template).Close();
        }

        private void DisposeTimer()
        {
            if (timer != null)
            {
                timer.Tick -= timer_Tick;
                timer.Dispose();
                timer = null;
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            DisposeTimer();
        }
    }
}