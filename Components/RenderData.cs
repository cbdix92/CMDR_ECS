﻿using System;
using System.Drawing;
using System.IO;

namespace CMDR.Components
{
    public struct RenderData : IComponent<RenderData>
    {
        #region IComponent
        public Component Handle { get; set; }
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        public Image Data { get; internal set; }

        public static void FromFile(Component component, string src)
        {
            RenderData renderData = component.Get<RenderData>();
            try
            {
                renderData.Data = Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"'{src}', RenderData");
            }
            component.Update<RenderData>(renderData);
        }
    }
}
