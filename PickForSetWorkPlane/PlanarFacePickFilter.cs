using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace PickForSetWorkPlane
{
	class PlanarFacePickFilter : ISelectionFilter
	{
		Document doc;
		public PlanarFacePickFilter(Document doc)
		{
			this.doc = doc;
		}
		public bool AllowElement(Element e)
		{
			return true;
		}
		public bool AllowReference(Reference r, XYZ p)
		{
			if(doc.GetElement(r).GetGeometryObjectFromReference(r) is PlanarFace)
			{
				return true;
			}
			else { return false; }
		}
	}
}