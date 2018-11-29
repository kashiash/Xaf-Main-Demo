using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Win;
using System;

public class TaskbarJumpListHandleStartupItemController : WindowController
{
    public TaskbarJumpListHandleStartupItemController()
    {
        TargetWindowType = WindowType.Main;
    }

    void TaskbarJumpListHandleStartupItemController_StartupWindowLoad(object sender, EventArgs e)
    {
        //((WinShowViewStrategyBase)Application.ShowViewStrategy).StartupWindowLoad -= TaskbarJumpListHandleStartupItemController_StartupWindowLoad;
        //var controller = Window.GetController<ShowNavigationItemController>();
        //ShowStartupNavigationItem(controller);
    }
    protected virtual void ShowStartupNavigationItem(ShowNavigationItemController controller)
    {
        var args = Environment.GetCommandLineArgs();

        //if (args.Length >= 3)
        //{
        //    var command = "ViewID=Contact_ListView&ObjectKey=&ObjectClassName=Contact";
        //    var sc = ViewShortcut.FromString(command);// (args[2]);

        //    var item = new ChoiceActionItem("CommandLineArgument", sc);
        //    controller.ShowNavigationItemAction.DoExecute(item);
        //}
    }

    protected override void OnActivated()
    {
        base.OnActivated();
        //    ((WinShowViewStrategyBase)Application.ShowViewStrategy).StartupWindowLoad += TaskbarJumpListHandleStartupItemController_StartupWindowLoad;
        //
    }

    protected override void OnDeactivated()
    {
        //((WinShowViewStrategyBase)Application.ShowViewStrategy).StartupWindowLoad -= TaskbarJumpListHandleStartupItemController_StartupWindowLoad;
        base.OnDeactivated();
    }
}