using System;
using System.IO;
using Autodesk.Revit.ApplicationServices;

namespace AlphaBIM
{
    internal class AlphaBIMConstraint
    {
        /// <summary>
        /// C:\ProgramData\Autodesk\ApplicationPlugins\AlphaBIM.bundle\Contents
        /// </summary>
        internal string ContentsFolder;

        /// <summary>
        /// C:\ProgramData\Autodesk\ApplicationPlugins\AlphaBIM.bundle\Contents\Resources
        /// </summary>
        internal string ResourcesFolder;

        /// <summary>
        /// "C:\ProgramData\Autodesk\ApplicationPlugins\AlphaBIM.bundle\Contents\Resources\Help"
        /// </summary>
        internal string HelpFolder;

        /// <summary>
        /// C:\ProgramData\Autodesk\ApplicationPlugins\AlphaBIM.bundle\Contents\Resources\Image
        /// </summary>
        internal string ImageFolder;

        /// <summary>
        /// C:\ProgramData\Autodesk\ApplicationPlugins\AlphaBIM.bundle\Contents\Resources\RebarShape
        /// </summary>
        internal string RebarShapeFolder;

        /// <summary>
        /// C:\Users\AlphaBIM\AppData\Roaming\Autodesk\ApplicationPlugins\AlphaBIM
        /// </summary>
        internal static string SettingFolder
            = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "Autodesk", "ApplicationPlugins", "AlphaBIM");

        /// <summary>
        ///  C:\ProgramData\Autodesk\ApplicationPlugins\AlphaBIM.bundle\Contents\2017\dll
        /// </summary>
        internal string DllFolder;

        /// <summary>
        /// ALPHA BIM - LEAD BY THE TRUST
        /// </summary>
        internal static string MessageBoxCaption = "ALPHA BIM - LEAD ON TRUST";



        internal AlphaBIMConstraint(ControlledApplication a = null)
        {
            ContentsFolder =
                "C:\\ProgramData\\Autodesk\\ApplicationPlugins\\AlphaBIM.bundle\\Contents";
            ResourcesFolder = Path.Combine(ContentsFolder, "Resources");
            HelpFolder = Path.Combine(ResourcesFolder, "Help");
            ImageFolder = Path.Combine(ResourcesFolder, "Image");
            RebarShapeFolder = Path.Combine(ResourcesFolder, "RebarShape");
        }
    }
}