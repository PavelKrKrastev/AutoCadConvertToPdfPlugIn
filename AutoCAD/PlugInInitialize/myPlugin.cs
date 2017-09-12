using Autodesk.AutoCAD.Runtime;

// This line is not mandatory, but improves loading performances
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
