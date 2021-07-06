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
      Perceptron brain;

      public Game(int width, int height, string title) :
           base(width, height, GraphicsMode.Default, title)
      {

      }

      protected override void OnLoad(EventArgs e)
      {
         random = new Random();
         Help.SetDim(Width, Height);

         ResetEverything();

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
            p.Draw(10);
         }

         GL.End();

         // Drawing perceptron output
         GL.Color4(Color4.Green);
         GL.LineWidth(1);
         brain.Draw();

         //// Drawing correct guesses
         //foreach (var p in trainingSet)
         //{

         //   float[] inputs = { p.X, p.Y, 1.0f };
         //   int guess = brain.Guess(inputs);

         //   if (guess == p.Label)
         //   {
         //      GL.Color4(Color4.White);
         //      Help.DrawRect(p.PixelX, p.PixelY, 10, 10);
         //   }
         //}
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

      private void ResetEverything()
      {
         float a = Help.RandInRange(-1.0f, 1.0f);
         float b = Help.RandInRange(-1.0f, 1.0f);
         trainingSet = Point.InitializeRandom(100, -1.0f, 1.0f, x => a * x + b);
         brain = new Perceptron(3);
      }
   }
}
