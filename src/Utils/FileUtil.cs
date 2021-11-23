using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace syzero.service.cli.Utils
{
    public class FileUtil
    {

        public List<DirectoryInfo> FileList = new List<DirectoryInfo>();

        public string Path { get; set; }

        public DirectoryInfo BaseDir { get; set; }

        public FileUtil(string path)
        {
            this.Path = path;
            this.BaseDir = new DirectoryInfo(Path);
            //把文件目录信息存到集合中
            FileList.Add(this.BaseDir);
            //取得文件目录中的子目录
            DirectoryInfo[] dii = this.BaseDir.GetDirectories();
            GetFileUrl1(FileList, dii);
        }

        /// <summary>
        /// 获取目录下的所有子目录
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="dir"></param>
        private void GetFileUrl1(List<DirectoryInfo> fileList, DirectoryInfo[] dir)
        {
            //取得子目录中的子目录
            foreach (DirectoryInfo dif in dir)
            {
                //把子目路信息存到集合
                fileList.Add(dif);
                //取得子目录中的子目录
                DirectoryInfo[] dii1 = dif.GetDirectories();
                //要是子目录中还有目录，则取得子目录中的子目录
                if (dii1.Length > 0)
                {
                    GetFileUrl1(fileList, dii1);
                }
            }
        }


        public void DeleteDirectory()
        {
            DeleteDirectory(this.Path);
        }

        private void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }

            Directory.Delete(target_dir, false);
        }

        /// <summary>
        /// 替换文件文本
        /// </summary>
        /// <param name="path"></param>
        /// <param name="oldStr"></param>
        /// <param name="newStr"></param>
        public void ReplaceText(string path, string oldStr, string newStr)
        {
            string con = "";
            //取得文件的内容
            using FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
            using StreamReader sr = new StreamReader(fs, Encoding.UTF8, true);
            con = sr.ReadToEnd();
            //替换内容
            con = con.Replace(oldStr, newStr);
            sr.Close();
            fs.Close();

            //取得文件的内容
            using FileStream fs2 = new FileStream(path, FileMode.Open, FileAccess.Write);
            fs2.SetLength(0);
            using StreamWriter sw = new StreamWriter(fs2, Encoding.UTF8);
            //把替换后的文本内容存到文本中
            sw.WriteLine(con);
            sw.Close();
            fs2.Close();
        }
    }
}
