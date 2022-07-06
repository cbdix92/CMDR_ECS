using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using CMDR;

namespace CMDR.Native
{
    internal static class Log
    {

        internal static void LogWin32Error(int error, string name, bool glLoad = false)
        {
            FileSystem.CheckPath();

            using (StreamWriter SR = new StreamWriter(_logPath, true, Encoding.UTF8))
            {
                SR.WriteLine($"WIN32 ERROR CODE {error} -> glLoad:{glLoad} Name:{name}: {new Win32Exception(error)}");
            }

            Win.SetLastError(0);
        }

        internal static void LogError(string error)
        {
            FileSystem.CheckPath();

            using (StreamWriter SR = new StreamWriter(_logPath, true, Encoding.UTF8))
            {
                SR.WriteLine(error);
            }
        }

        /// <summary>
        /// Log the contents of a matrix.
        /// </summary>
        /// <param name="matrix"> Matrix to be logged. </param>
        /// <param name="name"> Name of the Matrix. </param>
        internal static void LogMatrix4(Matrix4 matrix, string name)
        {
            FileSystem.CheckPath();

            using (StreamWriter sr = new StreamWriter(_logPath, true, Encoding.UTF8))
            {
                sr.WriteLine("Name:"+name+"\n"+matrix.ToString());
            }
        }
    }
}
