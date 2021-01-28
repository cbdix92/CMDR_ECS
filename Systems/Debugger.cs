using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Input;
using CMDR.Components;

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

        private static long _lastCount;
        private static byte _fpsCounter;
        private static byte _fpsCurrent;

        // Spatial line vars
        private static int s;
        private static int x;
        private static int y;
        private static int cX;
        private static int cY;

        private static Pen _redPen = new Pen(Brushes.Red);
        private static Pen _greenPen = new Pen(Brushes.Green);

        internal static void Draw(long ticks)
        {
            if (ticks >= _lastCount+Stopwatch.Frequency)
            {
                _fpsCurrent = _fpsCounter;
                _lastCount = ticks;
                _fpsCounter = 0;
            }
            else
            {
                _fpsCounter++;
            }
            if (_drawSpatialLines)
            {
                foreach ((int, int) i in SpatialIndexer.GridCells.Keys)
                {
                    s = SpatialIndexer.CellSize;
                    x = i.Item2 * s;
                    y = i.Item1 * s;
                    cX = x + s / 2;
                    cY = y + s / 2;
                    Render.Buffer.Graphics.DrawRectangle(_redPen, new Rectangle(x - (int)Camera.X, y - (int)Camera.Y, s, s));
                    Render.Buffer.Graphics.DrawString(SpatialIndexer.GridCells[i].Count.ToString(), Render.Display.Font, Brushes.Red, cX - Camera.X, cY - Camera.Y);
                    Render.Buffer.Graphics.DrawString("(" + i.Item1 + ", " + i.Item2 + ")", Render.Display.Font, Brushes.Red, x - (int)Camera.X + 5, y - (int)Camera.Y + 5);
                }
            }
            Render.Buffer.Graphics.DrawString("FPS:" + _fpsCurrent.ToString(), Render.Display.Font, Brushes.Red, 5F, 5F);
            Render.Buffer.Graphics.DrawString("GRID:" + SpatialIndexer.GridCells.Count.ToString(), Render.Display.Font, Brushes.Red, 100F, 5F);
        }

        internal static void DrawBoundingBox(SGameObject gameObject)
        {
            Collider collider = gameObject.SGet<Collider>();
            Transform transform = gameObject.SGet<Transform>();
            Render.Buffer.Graphics.DrawRectangle(_greenPen, new Rectangle((int)transform.X - (int)Camera.X, (int)transform.Y - (int)Camera.Y, collider.Width, collider.Height));

        }
    }
}
