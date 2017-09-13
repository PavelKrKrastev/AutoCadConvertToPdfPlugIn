using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace AutoCAD
{
    public class AddTabToRibbon
    {
        #region Static variables
        private static string _projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
        private static string _pathToFile = Path.Combine(_projectDirectory, "Resources");
        private static string _fileName = "convert.png";
        private static string _filePath = Path.Combine(_pathToFile, _fileName);
        private static Uri _imageUri = new Uri(_filePath);
        private static BitmapImage ButtonImage = new BitmapImage(_imageUri);
        #endregion Static variables

        #region Add tab and button to AutoCad ribbon
        [CommandMethod("AddCustomTabToRibbon", CommandFlags.Transparent)]
        public void AddTabToAutoCadRibbon()
        {
            RibbonControl AutoCadRibbon = ComponentManager.Ribbon;
            RibbonTab AutoCadRibbonTab = new RibbonTab() { Title = "Convert to PDF", Id = "PdfConvert" };
            AutoCadRibbon.Tabs.Add(AutoCadRibbonTab);
            AddContentToRibbon(AutoCadRibbonTab);
        }

        static void AddContentToRibbon(RibbonTab RibbonTab)
        {
            RibbonTab.Panels.Add(AddPanelAndButtonToRibbon());
        }

        static RibbonPanel AddPanelAndButtonToRibbon()
        {
            RibbonPanelSource AutoCadRibbonSource = new RibbonPanelSource() { Title = "Convert" };
            RibbonPanel AutoCadRibbonPanel = new RibbonPanel() { Source = AutoCadRibbonSource };
            RibbonButton PanelButton = new RibbonButton() { Image = ButtonImage, Name = "Pdf" };
            RibbonButton AutoCadConvertToPdfButton = new RibbonButton()
            {
                Name = "PdfConvert",
                Size = RibbonItemSize.Large,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                ShowText = true,
                Text = "Convert 2D to Pdf",
                LargeImage = ButtonImage,
                CommandHandler = new MyRibbonButtonCommandHandler()
            };
            AutoCadConvertToPdfButton.CommandParameter = "._myPdfConvert ";
            AutoCadRibbonSource.Items.Add(AutoCadConvertToPdfButton);
            return AutoCadRibbonPanel;
        }
        #endregion Add tab and button to AutoCad ribbon

        #region Button click event
        public class MyRibbonButtonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                RibbonCommandItem cmd = parameter as RibbonCommandItem;
                Document dwg = Application.DocumentManager.MdiActiveDocument;
                dwg.SendStringToExecute((string)cmd.CommandParameter, true, false, true);
            }
        }
        #endregion Button click event
    }
}