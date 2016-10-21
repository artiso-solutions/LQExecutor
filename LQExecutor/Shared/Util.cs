namespace LQExecutor.Shared
{
   using System;

   public static class Util
   {
      public static string ReadLine()
      {
         return Console.ReadLine();
      }

      public static void ClearResults()
      {
         Console.Clear();
      }

      public class ProgressBar
      {
         public ProgressBar(string dummy, bool dummy2)
         {
            
         }
         public int Percent { get; set; }
      }
   }
}