using System.Diagnostics;
using System.IO;
using Debug = SubrightEngine2.EngineStuff.Debug;

namespace SubrightEngine2.CookieCompiler
{
    public class Compile
    {
        //compile game!
        public static void CompileGame(string path)
        {
            //grab compiler.
            //find compiler
            string frameworkFolder = "C:/Windows/Microsoft.NET/Framework/";
            if (Directory.Exists(frameworkFolder))
            {
                //grab the latest version folder
                string[] folders = System.IO.Directory.GetDirectories(frameworkFolder);
                string latestVersion = folders[folders.Length - 1];
                string folderName = latestVersion.Substring(latestVersion.LastIndexOf('\\') + 1);
                //grab the compiler
                string compiler = latestVersion + "csc.exe";
                //check if compiler works by executing version
                bool vercheck = false;
                Process process = ExecuteCompiler(compiler, "/version");
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                //output good but /version is not a valid argument
                string expectedOutput = "Microsoft (R) Visual C# Compiler version " + folderName;
                Debug.WriteLine("got " + output + " wanted " + expectedOutput);
                if (output.ToLower().Contains(expectedOutput.ToLower()))
                {
                    //version message found check it to true!
                    vercheck = true;
                    Debug.WriteLine("Version check passed!");
                }
                Debug.WriteLine("Waiting for exit...");
                process.WaitForExit();
                Debug.WriteLine("Exited!");
                //found framework continuing
                if (vercheck == true)
                {
                    Debug.WriteLine("Compiling...");
                    //compile the source and load it into this as a dll
                    string source = path;
                    bool codeCompileComplete = false;
                    if (Directory.Exists(source))
                    {
                        //source directory exists!
                        //grab every cs file in that directory and compile them all into a dll
                        Process compilerP = ExecuteCompiler(compiler, "-target:library -out:CompiledGame.dll -nologo ./externalcode/*.cs");
                        compilerP.Start();
                        string output2 = compilerP.StandardOutput.ReadToEnd();
                        if (output2.ToLower().Contains("error"))
                        {
                            Debug.WriteLine("Error compiling!");
                            Debug.WriteLine(output2);
                        }
                        else
                        {
                            Debug.WriteLine("Compiled!");
                            codeCompileComplete = true;
                        }
                        compilerP.WaitForExit();
                        Debug.WriteLine("Compiler Exited as finished with it");
                    }
                    else
                    {
                        if (File.Exists(source))
                        {
                            //source is a file
                            Process compilerS = ExecuteCompiler(compiler, "-target:library -out:CompiledGame.dll -nologo " + source);
                            compilerS.Start();
                            string output2 = compilerS.StandardOutput.ReadToEnd();
                            if (output2.ToLower().Contains("error"))
                            {
                                Debug.WriteLine("Error compiling!");
                                Debug.WriteLine(output2);
                            }
                            else
                            {
                                Debug.WriteLine("Compiled!");
                                codeCompileComplete = true;
                            }
                            compilerS.WaitForExit();
                            Debug.WriteLine("Compiler Exited as finished with it");
                        }
                        else
                        {
                            Debug.WriteLine("Source file or folder is not found!");
                        }
                    }

                    if (codeCompileComplete == true)
                    {
                        //code compiled
                        Debug.WriteLine("Code Sucessfully Compiled into a dll! now organising assets.");
                        if (Directory.Exists(Path.Combine(path, "/prefabs")))
                        {
                            //read all the prefabs and allow them to organise everything...
                            string[] prefabs = Directory.GetFiles(Path.Combine(path, "/prefabs"), "*.prefab", SearchOption.AllDirectories);
                            for (int i = 0; i < prefabs.Length; i++)
                            {
                                //check through all prefabs if they are valid
                                string prefab = prefabs[i];
                                if (File.Exists(prefab))
                                {
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("Framework folder not found!");
            }
        }

        public static Process ExecuteCompiler(string compiler, string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(compiler);
            startInfo.Arguments = arguments;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            Process process = new Process();
            process.StartInfo = startInfo;
            return process;
        }
    }
}