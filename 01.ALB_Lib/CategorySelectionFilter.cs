#region Namespaces

using System.Collections.Generic;
using System.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

#endregion

namespace AlphaBIM
{
    internal class CategorySelectionFilter : ISelectionFilter
    {
        private List<ElementId> _categoryIds = new List<ElementId>();

        internal CategorySelectionFilter(Category category)
        {
            _categoryIds.Add(category.Id);
        }

        internal CategorySelectionFilter(IEnumerable<Category> categories)
        {
            _categoryIds = 
                categories.Where(category => category != null)
                .Where(category => category.Id!=ElementId.InvalidElementId)
                .Select(category => category.Id).ToList();
        }

        public bool AllowElement(Element elem)
        {
            return elem.Category != null && _categoryIds.Contains(elem.Category.Id);
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }
}
