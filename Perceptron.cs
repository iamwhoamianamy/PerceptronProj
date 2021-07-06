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
   class Perceptron
   {
      float[] weights;
      private readonly float drawRes = 1.5f;
      private readonly float learningRate = 0.01f;

      public Perceptron(int n)
      {
         weights = new float[n];

         for (int i = 0; i < n; i++)
         {
            weights[i] = Help.RandInRange(-1.0f, 1.0f);
         }
      }

      public float Funct(float x)
      {
         return -(weights[2] / weights[1]) - (weights[0] / weights[1]) * x;
      }

      public void Draw()
      {
         GL.Begin(PrimitiveType.Lines);

         for (float x = -1.0f; x < 1.0f; x += drawRes)
         {
            var p = new Point(x, Funct(x), 0);
            GL.Vertex2(p.PixelX, p.PixelY);

            p = new Point(x + drawRes, Funct(x + drawRes), 0);
            GL.Vertex2(p.PixelX, p.PixelY);
         }

         GL.End();
      }

      public int Guess(float[] inputs)
      {
         float sum = 0;

         for (int i = 0; i < weights.Length; i++)
         {
            sum += weights[i] * inputs[i];
         }

         return Math.Sign(sum);
      }

      public void Train(float[] inputs, int target)
      {
         int guess = Guess(inputs);
         int error = target - guess;

         for (int i = 0; i < weights.Length; i++)
         {
            weights[i] += error * inputs[i] * learningRate;
         }
      }
   }
}
