using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using syzero.service.cli.Models;
using syzero.service.cli.Utils;

namespace syzero.service.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("开始创建SYZERO1.0.5项目：");
            Console.WriteLine("请输入项目名称(默认Test):");
            string projectName = Console.ReadLine();

            Console.WriteLine("请输入命名空间(默认SyZero):");
            string nameSpace = Console.ReadLine();

            Console.WriteLine("请输入作者(默认syzero):");
            string author = Console.ReadLine();

            Console.WriteLine("请输入版本号(默认1.0.0):");
            string version = Console.ReadLine();

            ProjectModel projectModel = new ProjectModel(nameSpace, projectName, author, version);

            DirectoryInfo srcDir = new DirectoryInfo(Directory.GetCurrentDirectory() + "/src");
            if (srcDir.Exists && srcDir.GetFiles().Length > 0)
            {
                Console.WriteLine("src必须是空文件夹！！！");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("开始创建 ...");
            // var git = new CommandRunner("git", Directory.GetCurrentDirectory());
            // git.Run("clone https://gitee.com/syzero/syzero-template-service.git");

            var archive = ArchiveFactory.Open(Path.Combine(AppContext.BaseDirectory, "template.bin"));
            foreach (var entry in archive.Entries)
            {
                if (!entry.IsDirectory)
                {
                    Console.WriteLine(entry.Key);
                    entry.WriteToDirectory(Directory.GetCurrentDirectory(), new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                }
            }

            FileUtil fileUtil = new FileUtil(Directory.GetCurrentDirectory() + "/" + "template");

            //修改文件项目名称
            foreach (DirectoryInfo dif in fileUtil.FileList)
            {
                //取得每个目录的所有文件
                FileInfo[] fils = dif.GetFiles();
                //循环取得每个文件
                foreach (FileInfo fil in fils)
                {
                    //处理项目文件a
                    List<string> typeList = new List<string> { ".sln", ".cs", ".csproj", ".xml", ".user", ".bat" };
                    if (typeList.Contains(fil.Extension))
                    {
                        //取得文件内容
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template2", projectModel.ProjectName);
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template1", projectModel.NameSpace);
                        if (fil.Name.Contains("Template"))
                        {
                            fil.MoveTo(fil.DirectoryName + "/" + fil.Name.Replace("Template2", projectModel.ProjectName).Replace("Template1", projectModel.NameSpace));
                        }
                    }
                    //处理k8s yaml文件
                    if (fil.Name == "template1-template2-service.yaml")
                    {
                        fil.MoveTo(fil.DirectoryName + "/" + fil.Name.Replace("template2", projectModel.ProjectName.ToLower()).Replace("template1", projectModel.NameSpace.ToLower()));
                        fileUtil.ReplaceText(fil.FullName.ToString(), "template2", projectModel.ProjectName.ToLower());
                        fileUtil.ReplaceText(fil.FullName.ToString(), "template1", projectModel.NameSpace.ToLower());
                    }
                    //处理Dockerfile文件
                    if (fil.Name == "Dockerfile")
                    {
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template2", projectModel.ProjectName);
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template1", projectModel.NameSpace);
                    }
                    //处理appsettings.json文件
                    if (fil.Name == "appsettings.json")
                    {
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template2", projectModel.ProjectName);
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template1", projectModel.NameSpace);
                    }
                    //处理Jenkinsfile文件
                    if (fil.Name == "Jenkinsfile")
                    {
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template2", projectModel.ProjectName);
                        fileUtil.ReplaceText(fil.FullName.ToString(), "Template1", projectModel.NameSpace);
                        fileUtil.ReplaceText(fil.FullName.ToString(), "template2", projectModel.ProjectName.ToLower());
                        fileUtil.ReplaceText(fil.FullName.ToString(), "template1", projectModel.NameSpace.ToLower());
                    }
                }
            }
            //修改文件夹名称
            foreach (DirectoryInfo dif in fileUtil.FileList)
            {
                if (dif.Name.Contains("Template"))
                {
                    dif.MoveTo(dif.Parent + "/" + dif.Name.Replace("Template2", projectModel.ProjectName).Replace("Template1", projectModel.NameSpace));
                }
            }

            if (srcDir.Exists)
            {
                srcDir.Delete(true);
            }

            fileUtil.BaseDir.MoveTo(srcDir.FullName);

            Console.WriteLine("创建成功!");
            Console.ReadKey();
        }

      

    }
}
