using Autodesk.AutoCAD.Runtime;

[assembly: ExtensionApplication(typeof(AutoCAD.MyPlugin))]

namespace AutoCAD
{
    public class MyPlugin : IExtensionApplication
    {

        void IExtensionApplication.Initialize()
        {
            AddTabToRibbon NewRibbonTab = new AddTabToRibbon();
            NewRibbonTab.AddTabToAutoCadRibbon();
        }

        void IExtensionApplication.Terminate()
        {

        }

    }

}
