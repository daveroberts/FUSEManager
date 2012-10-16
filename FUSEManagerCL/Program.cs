using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FUSEManagerLib;

namespace FUSEManagerCL
{
    class Program
    {
        static void Main(string[] args)
        {
            string errorMessage;
            bool success = FSMounter.MountDoubleMirrorFS("q", out errorMessage, "C:\\test");
            if (success)
            {
                Console.WriteLine("Filesystem mounted successfully");
            }
            else
            {
                Console.WriteLine("Filesystem not mounted: " + errorMessage);
            }
        }
    }
}
