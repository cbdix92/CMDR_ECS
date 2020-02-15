using System;
using System.Drawing;
using System.IO;

namespace CMDR.Components
{
    public struct RenderData : IComponent
    {
        #region IComponent
        public int Handle { get; set; }
        public Type ID { get; set; }
        public ParentList Parents { get; set; }
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
                throw new FileNotFoundException($"The file: '{src}' Could not be found!");
            }
        }


    }
}
