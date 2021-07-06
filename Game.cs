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
      int perceptronN = 8;
      int polynomN = 4;
      bool train = true;

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
         if (train)
            PerformTraining();
         Render();

         Context.SwapBuffers();
         base.OnRenderFrame(e);
      }

      private void Render()
      {
         GL.ClearColor(0.0f, 0.0f, 0.0f, 1f);

         // Showing training data
         foreach (var p in trainingSet)
         {
            p.Draw(5);
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
         float[] polynomArgs = new float[polynomN];

         for (int i = 0; i < polynomN; i++)
         {
            polynomArgs[i] = Help.RandInRange(-1.0f, 1.0f);
         }

         trainingSet = Point.InitializeRandom(200, -1.0f, 1.0f, polynomN, polynomArgs);
         ResetPerceptron();
      }

      private void ResetPerceptron()
      {
         brain = new Perceptron(perceptronN + 1);
      }

      private void PerformTraining()
      {
         foreach (var p in trainingSet)
         {
            float[] inputs = new float[perceptronN + 1];

            for (int i = 0; i < perceptronN; i++)
            {
               inputs[i] = (float)Math.Pow(p.X, i);
            }
            inputs[perceptronN] = p.Y;

            brain.Train(inputs, p.Label);
         }
      }
   }
}
