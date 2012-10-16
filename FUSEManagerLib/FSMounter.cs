using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dokan;

namespace FUSEManagerLib
{
    public class FSMounter
    {
        public static bool MountMirrorFS(string driveLetter, out string errorMessage, string mirrorPath)
        {
            DokanOperations dok = (DokanOperations)new Mirror(mirrorPath);
            bool success = MountFS(dok, driveLetter, "Mirror", out errorMessage);
            return success;
        }
        public static bool MountDoubleMirrorFS(string driveLetter, out string errorMessage, string mirrorPath)
        {
            DokanOperations dok = (DokanOperations)new DoubleMirror(mirrorPath);
            bool success = MountFS(dok, driveLetter, "DoubleMirror", out errorMessage);
            return success;
        }
        private static bool MountFS(DokanOperations dok, string driveLetter, string driveLabel, out string errorMessage)
        {
            DokanOptions opt = new DokanOptions();
            opt.DebugMode = true;
            opt.MountPoint = driveLetter + ":\\";
            opt.VolumeLabel = driveLabel;
            opt.ThreadCount = 5;
            int status = DokanNet.DokanMain(opt, new LoggingFS(dok, true));
            switch (status)
            {
                case DokanNet.DOKAN_DRIVE_LETTER_ERROR:
                    errorMessage = "Drvie letter error";
                    return false;
                case DokanNet.DOKAN_DRIVER_INSTALL_ERROR:
                    errorMessage = "Driver install error";
                    return false;
                case DokanNet.DOKAN_MOUNT_ERROR:
                    errorMessage = "Mount error";
                    return false;
                case DokanNet.DOKAN_START_ERROR:
                    errorMessage = "Start error";
                    return false;
                case DokanNet.DOKAN_ERROR:
                    errorMessage = "Unknown error";
                    return false;
                case DokanNet.DOKAN_SUCCESS:
                    errorMessage = "";
                    return true;
                default:
                    errorMessage = "Unknown status: " + status;
                    return false;
            }
        }
    }
}
