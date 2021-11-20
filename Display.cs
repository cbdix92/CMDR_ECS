﻿using System;
using CMDR.Systems;
using GLFW;
using OpenGL;

namespace CMDR
{
    public sealed class Display
    {
        internal static Window Window;
        public Display(int width, int height, string title)
        {
            (Camera.Width, Camera.Height) = ((float)width, (float)height);
            

            if(!Glfw.Init())
            {
                GLFW.Exception.GetErrorMessage(Glfw.GetError(out string error));
                throw new System.Exception(error);
            }
			Glfw.WindowHint(Hint.ContextVersionMajor, 4);
			Glfw.WindowHint(Hint.ContextVersionMinor, 5);
			Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
			Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, 1);
            Glfw.WindowHint(Hint.ScaleToMonitor, 0);

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            if (Window == null)
                throw new NullReferenceException("Window returned Null");

            Glfw.MakeContextCurrent(Window);

        }

        public void Start()
        {
            GL.Init();
            GL.ClearColor(Color.Red);

            GameLoop.Start();
        }
    }
}
