using System;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;

namespace PickForSetWorkPlane
{
	[TransactionAttribute(TransactionMode.Manual)]
	[RegenerationAttribute(RegenerationOption.Manual)]
	public class Command : IExternalCommand
	{

		public Result Execute(ExternalCommandData commandData,
			ref string messege,
			ElementSet elements)
		{
			UIApplication uiApp = commandData.Application;
			Document doc = uiApp.ActiveUIDocument.Document;

			try
			{
				Reference faceRef = uiApp.ActiveUIDocument.Selection.PickObject(ObjectType.Face
				, new PlanarFacePickFilter(doc), "Please pick a planar face to set the work plane. ESC for cancel");
				GeometryObject geomObject = doc.GetElement(faceRef).GetGeometryObjectFromReference(faceRef);
				PlanarFace pf = geomObject as PlanarFace;

				using (Transaction t = new Transaction(doc, "SetWorkPlane"))
				{
					t.Start("SetWorkPlane");
					doc.ActiveView.SketchPlane = SketchPlane.Create(doc, pf.GetSurface() as Plane);
					t.Commit();
				}
				return Result.Succeeded;
			}
			catch (Autodesk.Revit.Exceptions.OperationCanceledException)
			{
				return Result.Cancelled;
			}
			catch (Exception ex)
			{
				messege = ex.Message;
				return Result.Failed;
			}
		}
	}
}