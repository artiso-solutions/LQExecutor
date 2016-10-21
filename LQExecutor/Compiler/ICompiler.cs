namespace LQExecutor.Compiler
{
   using System.Collections.Generic;

   using LQExecutor.Data;

   public interface ICompiler
   {
      IEnumerable<object> Compile(Script script);
   }
}