#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

#endregion

namespace AlphaBIM
{
    public class ElevationFloorViewModel : ViewModelBase
    {
        public ElevationFloorViewModel(UIDocument uidoc)
        {
            Doc = uidoc.Document;
            UiDoc = uidoc;
            Initialize();
        }

        private void Initialize()
        {
            // Get all floor in current selection
            SelectedFloor = new FilteredElementCollector(Doc, UiDoc.Selection.GetElementIds())
                .OfCategory(BuiltInCategory.OST_Floors)
                .ToList();
        }

        #region public property

        public Document Doc;
        public UIDocument UiDoc;
        public List<Element> SelectedFloor = new List<Element>();
        public double TopElevation { get; set; }
      

        public double Percent
        {
            get => _percent;
            set { _percent = value; OnPropertyChanged(); }
        }
        #endregion public property

        #region private variable

        private double _percent;

        #endregion private variable


        // Các method khác viết ở dưới đây | Other methods written below

        internal void SetTopElevation()
        {
            foreach (Element floor in SelectedFloor)
            {
                ElementId refLevelId = floor
                    .get_Parameter(BuiltInParameter.LEVEL_PARAM)
                    .AsElementId();
                Level refLevel = Doc.GetElement(refLevelId) as Level;
                if (refLevel == null) continue;

                double refElevation = refLevel.Elevation;
                double denta = UnitUtils.ConvertToInternalUnits(TopElevation, UnitTypeId.Meters)
                               - refElevation;

                floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM)
                    ?.Set(denta);
            }
        }
    }
}
