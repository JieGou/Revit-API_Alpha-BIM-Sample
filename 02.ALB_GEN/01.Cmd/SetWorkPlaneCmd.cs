#region Namespaces

using System;
using System.IO;
using System.Reflection;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;

#endregion

namespace AlphaBIM
{
    /// <summary>
    /// This add-in helps to quickly set work plan for active view
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class SetWorkPlaneCmd : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
            ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            // Khi chạy bằng Add-in Manager thì comment 2 dòng bên dưới để tránh lỗi
            // When running with Add-in Manager, comment the 2 lines below to avoid errors
            string dllFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            AssemblyLoader.LoadAllRibbonAssemblies(dllFolder);

            using (Transaction tx = new Transaction(doc))
            {
                try
                {
                    tx.Start("Set Work Plane");

                    Plane workPlane =
                        Plane.CreateByNormalAndOrigin(doc.ActiveView.ViewDirection, doc.ActiveView.Origin);
                    SketchPlane sketchPlane = SketchPlane.Create(doc, workPlane);

                    doc.ActiveView.SketchPlane = sketchPlane;

                    MessageBox.Show("Set work plane is ok!",
                        AlphaBIMConstraint.MessageBoxCaption,
                        MessageBoxButton.OK, 
                        MessageBoxImage.Information);

                    tx.Commit();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString(),
                        AlphaBIMConstraint.MessageBoxCaption,
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }

            return Result.Succeeded;
        }
    }
}
