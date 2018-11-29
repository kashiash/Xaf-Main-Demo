using System;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Updating;

namespace MainDemo.Module.Win {
	[ToolboxItemFilter("Xaf.Platform.Win")]
	public sealed partial class MainDemoWinModule : ModuleBase {
		public MainDemoWinModule() {
			InitializeComponent();
			DevExpress.ExpressApp.Scheduler.Win.SchedulerListEditor.DailyPrintStyleCalendarHeaderVisible = false;
            DevExpress.Persistent.Base.ReportsV2.DataSourceBase.EnableAsyncLoading = false;
            DevExpress.ExpressApp.ReportsV2.Win.WinReportServiceController.UseNewWizard = true;
        }
        public override IEnumerable<ModuleUpdater> GetModuleUpdaters(IObjectSpace objectSpace, Version versionFromDB) {
            ModuleUpdater updater = new DatabaseUpdate.Updater(objectSpace, versionFromDB);
            return new ModuleUpdater[] { updater };
        }
        public override void Setup(XafApplication application) {
            base.Setup(application);
            Demos.Feedback.XAFFeedbackHelper helper = new Demos.Feedback.XAFFeedbackHelper(application);
        }
    }
}
