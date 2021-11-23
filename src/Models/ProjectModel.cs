using System;
using System.Collections.Generic;
using System.Text;

namespace syzero.service.cli.Models
{
    /// <summary>
    /// 项目信息
    /// </summary>
    public class ProjectModel
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string NameSpace { get; set; } = "SyZero";

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; } = "Test";

        /// <summary>
        /// 作者
        /// </summary>
        public string Author { get; set; } = "syzero";

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; } = "1.0.0";

        public ProjectModel(string nameSpace, string pojectName, string author, string version)
        {
            if (!String.IsNullOrEmpty(nameSpace))
            {
                this.NameSpace = nameSpace;
            }
            if (!String.IsNullOrEmpty(pojectName))
            {
                this.ProjectName = pojectName;
            }
            if (!String.IsNullOrEmpty(author))
            {
                this.Author = author;
            }
            if (!String.IsNullOrEmpty(version))
            {
                this.Version = version;
            }
        }
    }
}
