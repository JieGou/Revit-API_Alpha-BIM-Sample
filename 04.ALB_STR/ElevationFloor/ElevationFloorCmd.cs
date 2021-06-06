#region Namespaces

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
#endregion

namespace AlphaBIM
{
    [Transaction(TransactionMode.Manual)]
    public class ElevationFloorCmd : IExternalCommand
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

            try
            {
                using (Transaction t = new Transaction(doc))
                {
                    t.Start("Set Top Elevation Floor");

                    ElevationFloorViewModel viewModel = new ElevationFloorViewModel(uidoc);
                    if (!viewModel.SelectedFloor.Any()) return Result.Cancelled;
                    ElevationFloorWindow window = new ElevationFloorWindow(viewModel);
                    if (window.ShowDialog() == false) return Result.Cancelled;

                    t.Commit();
                    return Result.Succeeded;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(),
                    AlphaBIMConstraint.MessageBoxCaption,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return Result.Cancelled;
            }
        }
    }
}
