namespace LQExecutor
{
   using System;
   using System.IO;
   using System.Linq;
   using System.Text;
   using System.Text.RegularExpressions;

   using LQExecutor.Data;

   public class Parser
   {
      private string ResolveReference(string reference)
      {
         return reference.Replace("&lt;RuntimeDirectory&gt;", Path.Combine(Environment.GetEnvironmentVariable("SystemRoot"), "Microsoft.NET", "Framework", "v4.0.30319"));
      }

      public Script Parse(string[] content)
      {
         var metaMode = true;
         var script = new Script();
         var scriptPart = new StringBuilder("using System;");
         scriptPart.AppendLine();
         scriptPart.AppendLine("using Util = LQExecutor.Shared.Util;");
         scriptPart.AppendLine("!usingDirectives!");
         scriptPart.AppendLine("namespace N1");
         scriptPart.AppendLine("{");
         scriptPart.AppendLine("public class C1");
         scriptPart.AppendLine("{");
         scriptPart.AppendLine("public C1() {}");
         foreach (var line in content)
         {
            if (metaMode)
            {
               var match = Regex.Match(line, @"\<Query Kind\=\""(?<Kind>.*)\""\>");
               if (match.Success)
               {
                  script.Type = match.Groups[1].Value;
                  continue;
               }

               match = Regex.Match(line, @"\<Reference\>(.*)\<\/Reference\>");
               if (match.Success)
               {
                  script.References.Add(ResolveReference(match.Groups[1].Value));
                  continue;
               }

               match = Regex.Match(line, @"\<Namespace\>(.*)\<\/Namespace\>");
               if (match.Success)
               {
                  script.Usings.Add(match.Groups[1].Value);
                  continue;
               }


               if (line == @"</Query>")
               {
                  metaMode = false;
                  continue;
               }
            }

            scriptPart.AppendLine(line);
         }

         scriptPart.AppendLine("}");
         scriptPart.AppendLine("}");
         script.Content = scriptPart.ToString().Trim(Environment.NewLine.ToCharArray());

         var usingDirectives = string.Empty;
         foreach (var usingDirective in script.Usings)
         {
            usingDirectives = $"{usingDirectives}{Environment.NewLine}using {usingDirective};";
         }
         usingDirectives = usingDirectives.Trim();
         script.Content = script.Content.Replace("!usingDirectives!", usingDirectives);

         return script;
      }

      public Script Parse(string content)
      {
         return Parse(content.Split(Environment.NewLine.ToCharArray()).Where(line => !string.IsNullOrWhiteSpace(line)).ToArray());
      }
   }
}