using Autodesk.AutoCAD.Runtime;
using Autocad = Autodesk.AutoCAD.ApplicationServices;
using System.IO;
using System.Windows.Forms;

[assembly: CommandClass(typeof(AutoCAD.MyCommands))]

namespace AutoCAD
{
    public class MyCommands
    {
        #region Constants
        private const string _saveDialogFilter = "PDF|*.pdf";
        private const string _defaultPdfNameFormat = " PDF";
        #endregion Constants

        #region Private Properties
        private string _saveAsPdfPath { get; set; }
        private SaveFileDialog _saveDialog = new SaveFileDialog() { Filter = _saveDialogFilter };
        #endregion Private Properties

        #region Convert 2D dwg to Pdf
        [CommandMethod("myPdfConvert", CommandFlags.Modal | CommandFlags.Undefined)]
        public void ConvertDwgToPdf()
        {
            Autocad.Document dwg = Autocad.Application.DocumentManager.MdiActiveDocument;
            string drawingPath = Autocad.Application.DocumentManager.MdiActiveDocument.Database.Filename;

            _saveDialog.FileName = Path.GetFileNameWithoutExtension(drawingPath);

            if (_saveDialog.ShowDialog() == DialogResult.OK)
            {
                using (FileStream OpenedDocumentStream = new FileStream(drawingPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    Aspose.CAD.Image image = Aspose.CAD.Image.Load(OpenedDocumentStream);
                    Aspose.CAD.ImageOptions.CadRasterizationOptions rasterizationOptions = new Aspose.CAD.ImageOptions.CadRasterizationOptions();
                    rasterizationOptions.PageWidth = 1200;
                    rasterizationOptions.PageHeight = 1200;
                    rasterizationOptions.DrawType = Aspose.CAD.FileFormats.Cad.CadDrawTypeMode.UseObjectColor;

                    Aspose.CAD.ImageOptions.PdfOptions pdfOptions = new Aspose.CAD.ImageOptions.PdfOptions();
                    pdfOptions.VectorRasterizationOptions = rasterizationOptions;

                    _saveAsPdfPath = _saveDialog.FileName;

                    image.Save(_saveAsPdfPath, pdfOptions);
                }
            }
        }
        #endregion Convert 2D dwg to Pdf
    }
}
