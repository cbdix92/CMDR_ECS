using System;
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
            try
            {
                ImgData = Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"'{src}', RenderData");
            }
            Data.Update<RenderData>(this);
        }
    }
}
