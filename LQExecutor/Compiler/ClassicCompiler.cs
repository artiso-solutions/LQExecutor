namespace LQExecutor.Compiler
{
   using System;
   using System.CodeDom.Compiler;
   using System.Collections.Generic;
   using System.Reflection;

   using LQExecutor.Data;

   using Microsoft.CSharp;

   public class ClassicCompiler : ICompiler
   {
      public IEnumerable<object> Compile(Script script)
      {
         var provider = new CSharpCodeProvider();
         var parameters = new CompilerParameters();
         parameters.ReferencedAssemblies.AddRange(script.References.ToArray());
         parameters.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(Shared.Util)).Location);
         parameters.GenerateInMemory = true;
         parameters.GenerateExecutable = false;

         var results = provider.CompileAssemblyFromSource(parameters, script.Content);
         if (results.Errors.HasErrors)
         {
            foreach (var error in results.Errors)
            {
               yield return error;
            }
            yield break;
         }
         var assembly = results.CompiledAssembly;
         var program = Activator.CreateInstance(assembly.GetType("N1.C1"));
         var method = program.GetType().GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Instance);
         method.Invoke(program, null);
      }
   }
}