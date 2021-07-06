using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerceptronProj
{
   class Program
   {
      public static void Main()
      {
         using (Game game = new Game(800, 800, "Perceptron"))
         {

            game.Run(60.0);
         }
      }
   }
}
