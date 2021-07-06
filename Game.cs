using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform.Windows;
using OpenTK.Input;
using System.Diagnostics;

namespace PerceptronProj
{
   public partial class Game : GameWindow
   {
      
      float mouseX = 0, mouseY = 0;
      Random random;
      Point[] trainingSet;

      public Game(int width, int height, string title) :
           base(width, height, GraphicsMode.Default, title)
      {

      }

      protected override void OnLoad(EventArgs e)
      {
         random = new Random();
         Help.SetDim(Width, Height);

         trainingSet = Point.InitializeRandom(100, 0, Width);

         GL.Enable(EnableCap.PointSmooth);

         base.OnLoad(e);
      }

      protected override void OnRenderFrame(FrameEventArgs e)
      {
         GL.Clear(ClearBufferMask.ColorBufferBit);

         UpdatePhysics();
         Render();

         Context.SwapBuffers();
         base.OnRenderFrame(e);
      }

      private void UpdatePhysics()
      {

      }

      private void Render()
      {
         GL.ClearColor(0.0f, 0.0f, 0.0f, 1f);

         // Showing training data
         foreach (var p in trainingSet)
         {
            p.Show(5);
         }

         GL.Color4(Color4.White);
         GL.Begin(PrimitiveType.Lines);

         GL.Vertex2(0, 0);
         GL.Vertex2(Width, Height);

         GL.End();

      }

      protected override void OnResize(EventArgs e)
      {
         GL.Disable(EnableCap.DepthTest);
         GL.Viewport(0, 0, Width, Height);
         GL.MatrixMode(MatrixMode.Projection);
         GL.LoadIdentity();
         GL.Ortho(0, Width, Height, 0, -1.0, 1.0);
         GL.MatrixMode(MatrixMode.Modelview);
         GL.LoadIdentity();

         Help.SetDim(Width, Height);
         base.OnResize(e);
      }
   }
}
