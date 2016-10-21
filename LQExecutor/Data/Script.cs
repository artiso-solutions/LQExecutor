namespace LQExecutor.Data
{
   using System.Collections.Generic;

   public class Script
   {
      public Script()
      {
         this.References = new List<string>();
         this.Usings = new List<string>();
         this.Content = string.Empty;
         this.Type = string.Empty;
      }
      public string Type { get; set; }
      public string Content { get; set; }
      public List<string> References { get; }
      public List<string> Usings { get; }
   }
}