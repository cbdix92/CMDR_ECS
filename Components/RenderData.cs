﻿using System;
using System.Drawing;
using System.IO;

namespace CMDR.Components
{
    public struct RenderData : IComponent<RenderData>
    {
        #region IComponent
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        public Image ImgData { get; internal set; }

        public void FromFile(string src)
        {
            Receive();
            try
            {
                ImgData = Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"'{src}', RenderData");
            }
            Send();
        }
        public void Receive()
        {
            this = Scene.Get<RenderData>(ID);
        }
        public void Send()
        {
            Scene.Update<RenderData>(this);
        }
    }
}
