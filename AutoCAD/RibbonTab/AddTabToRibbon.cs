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
        #region Add tab and button to AutoCad ribbon
        [CommandMethod("AddCustomTabToRibbon", CommandFlags.Transparent)]
        public void AddTabToAutoCadRibbon()
        {
            RibbonControl AutoCadRibbon = ComponentManager.Ribbon;
            RibbonTab AutoCadRibbonTab = new RibbonTab() { Title = "Convert to PDF", Id = "PdfConvert" };
            AutoCadRibbon.Tabs.Add(AutoCadRibbonTab);
            AddContentToRibbon(AutoCadRibbonTab);
        }

         private void AddContentToRibbon(RibbonTab RibbonTab)
        {
            RibbonTab.Panels.Add(AddPanelAndButtonToRibbon());
        }

         RibbonPanel AddPanelAndButtonToRibbon()
        {
            RibbonPanelSource AutoCadRibbonSource = new RibbonPanelSource() { Title = "Convert" };
            RibbonPanel AutoCadRibbonPanel = new RibbonPanel() { Source = AutoCadRibbonSource };
            RibbonButton PanelButton = new RibbonButton() { Image = RibbonButtonImageSource("AutoCAD.Resources.convert.png"), Name = "Pdf" };
            RibbonButton AutoCadConvertToPdfButton = new RibbonButton()
            {
                Name = "PdfConvert",
                Size = RibbonItemSize.Large,
                Orientation = System.Windows.Controls.Orientation.Vertical,
                ShowText = true,
                Text = "Convert" + Environment.NewLine + "2D to Pdf",
                LargeImage = RibbonButtonImageSource("AutoCAD.Resources.convert.png"),
                CommandHandler = new RibbonButtonCommandHandler()
            };
        
            AutoCadConvertToPdfButton.CommandParameter = "._myPdfConvert ";
            AutoCadRibbonSource.Items.Add(AutoCadConvertToPdfButton);
            return AutoCadRibbonPanel;
        }
        #endregion Add tab and button to AutoCad ribbon

        #region Button click event
        public class RibbonButtonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true;
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                RibbonCommandItem CommandItem = parameter as RibbonCommandItem;
                Document DwgDocument = Application.DocumentManager.MdiActiveDocument;
                DwgDocument.SendStringToExecute((string)CommandItem.CommandParameter, true, false, true);
            }
        }
        #endregion Button click event

        #region Get image for ribbon button from resources
        private System.Windows.Media.ImageSource RibbonButtonImageSource(string EmbeddedPath)
        {
            Stream ImageSream = this.GetType().Assembly.GetManifestResourceStream(EmbeddedPath);
            var PngDecoder = new System.Windows.Media.Imaging.PngBitmapDecoder(ImageSream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
            return PngDecoder.Frames[0];
        }
        #endregion Get image for ribbon button from resources
    }
}