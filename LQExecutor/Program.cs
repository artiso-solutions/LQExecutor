using System;
namespace LQExecutor
{
   using System.IO;
   using System.Linq;

   using LQExecutor.Compiler;

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("LQExecutor");
         var scriptPath = string.Empty;
         if (args.Length > 0)
         {
            scriptPath = args[0];
            Console.WriteLine("Script to execute : {0}", scriptPath);
         }
         else
         {
            Console.WriteLine("No script input.");
            return;
         }

         if (!File.Exists(scriptPath))
         {
            Console.WriteLine("Script doesn't exist.");
            return;
         }

         var parser = new Parser();
         var compiler = new ClassicCompiler();

         Console.WriteLine("Parsing script...");

         var script = parser.Parse(File.ReadAllLines(scriptPath));

         Console.WriteLine("References:");
         script.References.ForEach(Console.WriteLine);

         Console.WriteLine("Compiling and executing script...");

         var errors = compiler.Compile(script);
         if (errors.Any())
         {
            Console.ForegroundColor = ConsoleColor.Red;
            errors.ToList().ForEach(Console.WriteLine);
            Console.ReadLine();
         }
      }
   }
}
