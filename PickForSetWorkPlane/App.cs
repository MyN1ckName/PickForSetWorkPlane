using System;
using Autodesk.Revit.UI;

namespace PickForSetWorkPlane
{
	class App : IExternalApplication
	{
		static readonly string ExecutingAssemblyPath = System.Reflection.Assembly
			.GetExecutingAssembly().Location;

		public Result OnStartup(UIControlledApplication app)
		{
			RibbonPanel rvtRibbonPanel = app.CreateRibbonPanel("SetWorkPlane");
			PushButton button = rvtRibbonPanel.AddItem
				(new PushButtonData("Button", "SetWorkPlane"
				, ExecutingAssemblyPath, "PickForSetWorkPlane.Command")) as PushButton;

			button.LargeImage = new System.Windows.Media.Imaging.BitmapImage
				(new Uri("pack://application:,,,/PickForSetWorkPlane;component/icon.ico"
				, UriKind.Absolute));

			button.ToolTip = "Set a Work Plane by Selected Face";

			return Result.Succeeded;
		}

		public Result OnShutdown(UIControlledApplication app)
		{
			return Result.Succeeded;
		}
	}
}