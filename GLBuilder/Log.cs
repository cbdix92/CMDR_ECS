﻿using System;
using System.IO;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Reflection;
using CMDR;

namespace OpenGL
{
    internal static class Log
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern void SetLastError(uint dwErrorCode);


        private static readonly string _tempDir = @"temp\";
        private static readonly string _dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private static readonly string _path = Path.Combine(_dir, _tempDir);
        private static readonly string _logPath = Path.Combine(_path, "log.txt");
        internal static void Init()
        {
            if (!Directory.Exists(_path))
                Directory.CreateDirectory(_path);
            using (File.CreateText(_logPath)) { }
        }
        internal static void LogWin32Error(int error, string name, bool glLoad = false)
        {
            using (StreamWriter SR = new StreamWriter(_logPath, true, Encoding.UTF8))
            {
                SR.WriteLine($"WIN32 ERROR CODE {error} -> glLoad:{glLoad} Name:{name}: {new Win32Exception(error)}");
            }
            SetLastError(0);
        }

        internal static void LogError(string error)
        {
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
            using (StreamWriter sr = new StreamWriter(_logPath, true, Encoding.UTF8))
            {
                sr.WriteLine("Name:"+name+"\n"+matrix.ToString());
            }
        }
    }
}
