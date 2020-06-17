using System;
using System.Drawing;
using System.Windows.Input;

namespace CMDR.Systems
{
    public static class Debugger
    {
        private static bool _enableDebugger;
        public static bool EnableDebugger
        {
            get => _enableDebugger;
            set
            {
                if (value != _enableDebugger)
                {
                    if (value)
                        Input.AddKeyBind(Key.F5, () => { _drawSpatialLines = !_drawSpatialLines; });
                    else if (!value)
                        Input.RemoveKeyBind(Key.F5);
                }
                _enableDebugger = value;
            }
        }
        private static bool _drawSpatialLines { get; set; }
        internal static void Draw()
        {
            if (_drawSpatialLines)
            {
                foreach ((int, int) i in SpatialIndexer.GridCells.Keys)
                {
                    int s = SpatialIndexer.CellSize;
                    int x = i.Item2 * s;
                    int y = i.Item1 * s;
                    int cX = x + s / 2;
                    int cY = y + s / 2;
                    Render.Buffer.Graphics.DrawRectangle(new Pen(Brushes.Red), new Rectangle(x - (int)Camera.X, y - (int)Camera.Y, s, s));
                    Render.Buffer.Graphics.DrawString(SpatialIndexer.GridCells[i].Count.ToString(), Render.Display.Font, Brushes.Red, cX - Camera.X, cY - Camera.Y);
                    Render.Buffer.Graphics.DrawString("(" + i.Item1 + ", " + i.Item2 + ")", Render.Display.Font, Brushes.Red, x - (int)Camera.X + 5, y - (int)Camera.Y + 5);
                }
            }
        }
    }
}
