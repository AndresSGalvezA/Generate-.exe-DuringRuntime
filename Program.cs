using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading;
using System.IO;

namespace FinalLFA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Escáner - AG 1024718";
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();
            var UserOp = "";

            while (UserOp != "x")
            {
                Console.Clear();
                Console.WriteLine("ESCÁNER");
                Console.WriteLine("Para salir, escriba \"x\"");
                Console.WriteLine("Escriba algo:");
                UserOp = Console.ReadLine();

                if (UserOp != "x")
                {
                    Console.WriteLine();
                    Compile(UserOp);
                }
            }

            //var Reader = new StreamReader()
        }

        static void Compile(string FirstPath)
        {
            var File = new FileStream(Path.GetFullPath("File.cs"), FileMode.Create);
            var Writer = new StreamWriter(File);
            Writer.WriteLine("using System;");
            Writer.WriteLine("using System.IO;");
            Writer.WriteLine("public class Program");
            Writer.WriteLine("{");
            Writer.WriteLine("static void Main()");
            Writer.WriteLine("{");
            Writer.WriteLine("Console.Title = \"Validador de tokens\";");
            Writer.WriteLine("Console.WriteLine(\"Mensaje generado por compilación en ejecución.\");");
            Writer.WriteLine("Console.ReadKey();");
            Writer.WriteLine("}");
            Writer.WriteLine("}");
            Writer.Close();
            File.Close();
            string errors = "";
            var codeProvider = CodeDomProvider.CreateProvider("CSharp");
            var parameters = new CompilerParameters
            {
                GenerateExecutable = true, //Esto permite crear el .exe.
                OutputAssembly = "File.exe" //Este es el nombre que tendrá el ejecutable.
            };

            var results = codeProvider.CompileAssemblyFromFile(parameters, "File.cs"); //El File.cs es lo que se compilará.

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    errors = errors + "Line number " + CompErr.Line + ", Error Number: " + CompErr.ErrorNumber + ", '" + CompErr.ErrorText + ";\n";
                }

                Console.WriteLine(errors);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Compilación iniciada exitosamente.");
                Console.WriteLine("Se abrirá una nueva ventana.");
                Thread.Sleep(3000);
                Process.Start("File.exe");
            }
        }
    }
}