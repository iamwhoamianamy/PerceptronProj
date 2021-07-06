using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PerceptronProj
{
   struct Point
   {
      public float X { get; set; }
      public float Y { get; set; }
      public int Label { get; set; }

      public Point(float x, float y, int label)
      {
         X = x;
         Y = y;
         Label = label;
      }

      public static Point[] InitializeRandom(int n, float left, float right)
      {
         Point[] res = new Point[n];

         for (int i = 0; i < n; i++)
         {
            float x = Help.RandInRange(left, right);
            float y = Help.RandInRange(left, right);
            
            res[i] = new Point(x, y, y < x ? -1 : 1);
         }

         return res;
      }

      public void Show(float size)
      {
         GL.PointSize(size);
         GL.Color4(Label == 1 ? Color4.Red : Color4.Blue);
         GL.Begin(PrimitiveType.Points);
         GL.Vertex2(X, Y);
         GL.End();
      }
   }
}
