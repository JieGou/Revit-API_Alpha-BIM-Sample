#region Namespaces
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Application = Autodesk.Revit.ApplicationServices.Application;
using View = Autodesk.Revit.DB.View;

#endregion

namespace AlphaBIM
{
    /// <summary>
    /// This add-in helps to quickly activate the selected view.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    public class GotoViewCmd : IExternalCommand
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

            List<Element> elementsList = uidoc.Selection.GetElementIds()
                .Select(id => doc.GetElement(id))
                .Where(el => el.Category.Id.IntegerValue.Equals((int)BuiltInCategory.OST_Views))
                .ToList();

            if (!elementsList.Any())
            {
                MessageBox.Show("Please select one section before run this Add-in!",
                    AlphaBIMConstraint.MessageBoxCaption,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return Result.Cancelled;
            }

            foreach (var e in elementsList)
            {
                View view = new FilteredElementCollector(doc)
                    .OfClass(typeof(View)).Cast<View>()
                    .FirstOrDefault(item => item.Name.Equals(e.Name));

                uidoc.ActiveView = view;
                break;
            }

            return Result.Succeeded;
        }
    }
}
