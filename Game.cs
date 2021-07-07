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
      bool train = true;

      int perceptronPolynomN = 8;
      int testPolynomN = 8;
      float[] testPolynomArgs;

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
            p.Draw(2);
         }

         GL.Color4(Color4.Gray);
         GL.LineWidth(1);
         DrawTrainingSeparator();

         // Drawing perceptron output
         GL.Color4(Color4.Green);
         GL.LineWidth(2);
         brain.Draw();
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
         testPolynomArgs = new float[testPolynomN + 1];

         for (int i = 0; i < testPolynomN + 1; i++)
         {
            testPolynomArgs[i] = Help.RandInRange(-1.0f, 1.0f);
         }

         trainingSet = Point.InitializeRandom(5000, -1.0f, 1.0f, testPolynomN + 1, testPolynomArgs);
         ResetPerceptron();
      }

      private void DrawTrainingSeparator()
      {
         GL.Begin(PrimitiveType.Lines);

         float drawRes = 0.005f;
         for (float x = -1.0f; x < 1.0f; x += drawRes)
         {
            var p = new Point(x, Help.Polynom(x, testPolynomN + 1, testPolynomArgs), 0);
            GL.Vertex2(p.PixelX, p.PixelY);

            p = new Point(x + drawRes, Help.Polynom(x + drawRes, testPolynomN + 1, testPolynomArgs), 0);
            GL.Vertex2(p.PixelX, p.PixelY);
         }

         GL.End();
      }

      private void ResetPerceptron()
      {
         brain = new Perceptron(perceptronPolynomN + 2);
      }

      private void PerformTraining()
      {
         foreach (var p in trainingSet)
         {
            float[] inputs = new float[perceptronPolynomN + 2];

            for (int i = 0; i < perceptronPolynomN + 1; i++)
            {
               inputs[i] = (float)Math.Pow(p.X, i);
            }
            inputs[perceptronPolynomN + 1] = p.Y;

            brain.Train(inputs, p.Label);
         }
      }
   }
}
