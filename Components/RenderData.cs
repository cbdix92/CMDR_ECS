using System;
using System.Drawing;
using System.IO;

namespace CMDR.Components
{
    public struct RenderData : IComponent
    {
        #region IComponent
        public Component Handle { get; set; }
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        public Image Data { get; internal set; }

        public static void FromFile(ref RenderData renderData, string src)
        {
            try
            {
                renderData.Data = Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"'{src}', RenderData");
            }
        }


    }
}
