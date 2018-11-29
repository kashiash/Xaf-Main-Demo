using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.Win;
using DevExpress.ExpressApp.Xpo;
using DevExpress.ExpressApp.Security.ClientServer;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;

namespace MainDemo.Win {
    public partial class MainDemoWinApplication : WinApplication {
        #region Default XAF configuration options (https://www.devexpress.com/kb=T501418)
        static MainDemoWinApplication() {
            DevExpress.Persistent.Base.PasswordCryptographer.EnableRfc2898 = true;
            DevExpress.Persistent.Base.PasswordCryptographer.SupportLegacySha512 = false;
            DevExpress.Xpo.XpoDefault.TrackPropertiesModifications = true;
        }
        private void InitializeDefaults() {
            LinkNewObjectToParentImmediately = false;
            OptimizedControllersCreation = true;
            EnableModelCache = true;
            UseLightStyle = true;
        }
        #endregion
        public MainDemoWinApplication() {
            InitializeComponent();

            #region DEMO_REMOVE
            AuthenticationStandard designedAuthentication = this.authenticationStandard1;
            this.authenticationStandard1 = new AuthenticationStandardForTests();
            this.securityStrategyComplex1.Authentication = this.authenticationStandard1;
            this.authenticationStandard1.LogonParametersType = designedAuthentication.LogonParametersType;
            this.authenticationStandard1.UserType = designedAuthentication.UserType;
            #endregion

            InitializeDefaults();
            DevExpress.ExpressApp.ScriptRecorder.ScriptRecorderControllerBase.ScriptRecorderEnabled = true;

       
        }
        protected override void CreateDefaultObjectSpaceProvider(CreateCustomObjectSpaceProviderEventArgs args) {
            args.ObjectSpaceProviders.Add(new SecuredObjectSpaceProvider((ISelectDataSecurityProvider)Security, XPObjectSpaceProvider.GetDataStoreProvider(args.ConnectionString, args.Connection, true), false));
            args.ObjectSpaceProviders.Add(new NonPersistentObjectSpaceProvider(TypesInfo, null));
        }
        private void MainDemoWinApplication_DatabaseVersionMismatch(object sender, DatabaseVersionMismatchEventArgs e) {
            e.Updater.Update();
            e.Handled = true;
        }
        private void MainDemoWinApplication_LastLogonParametersRead(object sender, LastLogonParametersReadEventArgs e) {
            // Just to read demo user name for logon.
            AuthenticationStandardLogonParameters logonParameters = e.LogonObject as AuthenticationStandardLogonParameters;
            if(logonParameters != null) {
                if(String.IsNullOrEmpty(logonParameters.UserName)) {
                    logonParameters.UserName = "Sam";
                }
            }
        }
    }

    #region DEMO_REMOVE
    public class AuthenticationStandardForTests : AuthenticationStandard
    {
        public override bool AskLogonParametersViaUI
        {
            get { return string.IsNullOrEmpty(Program.LogonUserName); }
        }
        public override object Authenticate(IObjectSpace objectSpace)
        {
            if (!string.IsNullOrEmpty(Program.LogonUserName))
            {
                return objectSpace.FindObject<PermissionPolicyUser>(new DevExpress.Data.Filtering.BinaryOperator("UserName", Program.LogonUserName));
            }
            return base.Authenticate(objectSpace);
        }
    }
    #endregion
}

