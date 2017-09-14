using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using System;

[assembly: ExtensionApplication(typeof(AutoCAD.MyPlugin))]

namespace AutoCAD
{
    public class MyPlugin : IExtensionApplication
    {

        void IExtensionApplication.Initialize()
        {
            Application.Idle += callback_Idle;
        }

        void IExtensionApplication.Terminate()
        {

        }

        private void callback_Idle(Object sender, EventArgs e)
        {
            AddTabToRibbon NewRibbonTab = new AddTabToRibbon();
            NewRibbonTab.AddTabToAutoCadRibbon();
            Application.Idle -= callback_Idle;
        }

    }

}
