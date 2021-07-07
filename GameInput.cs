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
   public partial class Game : GameWindow
   {
      protected override void OnKeyDown(KeyboardKeyEventArgs e)
      {

         switch (e.Key)
         {
            case Key.R:
            {
               ResetEverything();
               train = true;
               break;
            }
            case Key.Space:
            {
               train = !train;
               break;
            }
            case Key.C:
            {
               ResetPerceptron();
               train = true;
               break;
            }
         }

         base.OnKeyDown(e);
      }


      protected override void OnMouseDown(MouseButtonEventArgs e)
      {
         switch (e.Button)
         {
            case MouseButton.Left:
            {
               PerformTraining();
               break;
            }
            case MouseButton.Right:
            {
               break;
            }
         }

         base.OnMouseDown(e);
      }

      protected override void OnMouseMove(MouseMoveEventArgs e)
      {

         mouseX = e.X;
         mouseY = e.Y;

         base.OnMouseMove(e);
      }

      protected override void OnMouseWheel(MouseWheelEventArgs e)
      {

         base.OnMouseWheel(e);
      }
   }
}
