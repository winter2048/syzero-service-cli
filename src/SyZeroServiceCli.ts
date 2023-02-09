import * as readline from 'readline';
import * as compressing from 'compressing';
import * as fs from 'fs';
import * as path from 'path';

export default class SyZeroServiceCli {
    public projectName: string = "test";
    public nameSpace: string = "syzero";
    public author: String = "author";
    public version: String = "1.0.5";

    constructor() {

    }

    public async start() {
        await this.init();
        const projectRootDirNew = `${this.nameSpace}/${this.projectName}`;
        const projectRootDirOld = `${this.nameSpace}/template`;
        if (fs.existsSync(projectRootDirNew) && fs.readdirSync(projectRootDirNew).length > 0) {
            console.log(path.join(process.cwd(), projectRootDirNew) + " 必须是空文件夹！！！");
            return;
        }
        console.log("开始创建 ...");
        //解压文件模板
        await this.unZip(path.join(__dirname, "template.bin"), this.nameSpace);
        const files: string[] = [];
        const dirs: string[] = [];
        this.readFileList(projectRootDirOld, files, dirs);
        const typeList = [".sln", ".cs", ".csproj", ".xml", ".user", ".bat"];

        //1.修改项目文件名称
        files.forEach(filePath => {
            //处理项目文件
            const fileExt = path.extname(filePath);
            const fileName = path.basename(filePath);
            //处理项目文件
            if (typeList.indexOf(fileExt) >= 0) {
                this.replaceText(filePath, "Template2", this.projectName);
                this.replaceText(filePath, "Template1", this.nameSpace);
                if (fileName.indexOf("Template") >= 0) {
                    fs.renameSync(filePath, path.join(path.dirname(filePath), fileName.replace("Template2", this.projectName).replace("Template1", this.nameSpace)));
                }
            }
            //处理k8s yaml文件
            if (fileName == "template1-template2-service.yaml") {
                this.replaceText(filePath, "Template2", this.projectName.toLowerCase());
                this.replaceText(filePath, "Template1", this.nameSpace.toLowerCase());
                fs.renameSync(filePath, path.join(path.dirname(filePath), fileName.replace("template2", this.projectName.toLowerCase()).replace("template1", this.nameSpace.toLowerCase())));

            }
            //处理Dockerfile文件
            if (fileName == "Dockerfile") {
                this.replaceText(filePath, "Template2", this.projectName);
                this.replaceText(filePath, "Template1", this.nameSpace);
            }
            //处理appsettings.json文件
            if (fileName == "appsettings.json") {
                this.replaceText(filePath, "Template2", this.projectName);
                this.replaceText(filePath, "Template1", this.nameSpace);
            }
            //处理Jenkinsfile文件
            if (fileName == "Jenkinsfile") {
                this.replaceText(filePath, "Template2", this.projectName);
                this.replaceText(filePath, "Template1", this.nameSpace);
                this.replaceText(filePath, "template2", this.projectName.toLowerCase());
                this.replaceText(filePath, "template1", this.nameSpace.toLowerCase());
            }
        });

        //2.修改项目文件夹名称
        dirs.forEach(dir => {
            if (fs.existsSync(dir)) {
                fs.renameSync(dir, path.join(path.dirname(dir), path.basename(dir).replace("Template2", this.projectName).replace("Template1", this.nameSpace).replace("template", this.projectName)));
            }
        });

        //3.修改根目录名称
        fs.renameSync(projectRootDirOld, projectRootDirNew);

        this.readFileList(projectRootDirNew, files, dirs);
        files.forEach(file =>  {
            console.log(file);
        });

        console.log("创建成功!")
        this.show();
    }

    public async init() {
        console.log("开始创建SYZERO1.0.5项目：");
        this.projectName = await this.readlineMsg(`请输入项目名称(默认${this.projectName}): \n`) || this.projectName;
        this.nameSpace = await this.readlineMsg(`请输入命名空间(默认${this.nameSpace}): \n`) || this.nameSpace;
        this.author = await this.readlineMsg(`请输入作者(默认${this.author}): \n`) || this.author;
        this.version = await this.readlineMsg(`请输入版本号(默认${this.version}): \n`) || this.version;
    }

    public async readlineMsg(msg: string): Promise<string> {
        return new Promise((resolve, reject) => {
            const rl = readline.createInterface({
                input: process.stdin,
                output: process.stdout
            });
            rl.question(msg, (value) => {
                rl.close();
                resolve(value);
            });
        });
    }

    public readFileList(dir: string, filesList: string[] = [], dirList: string[] = []) {
        const files = fs.readdirSync(dir);
        files.forEach((item, index) => {
            var fullPath = path.join(dir, item);
            const stat = fs.statSync(fullPath);
            if (stat.isDirectory()) {
                dirList.push(fullPath);
                this.readFileList(path.join(dir, item), filesList, dirList); //递归读取文件
            } else {
                filesList.push(fullPath);
            }
        });
    }

    public replaceText(filePath: string, oldText: string, newText: string) {
        let fileText = fs.readFileSync(filePath).toString();
        fileText = fileText.replace(new RegExp(oldText, 'g'), newText);
        fs.writeFileSync(filePath, fileText);
    }

    public async unZip(path: string, dirName: string) {
        await compressing.zip.uncompress(path, dirName);
    }

    public async zip(path: string, dirName: string) {
        await compressing.zip.compressDir(path, dirName);
    }

    public async sleep(s:number) {
        return new Promise<void>((resolve, reject) => {
           setTimeout(()=>{
               resolve();
           },s);
        })
    }

    public show() {
        console.log("项目名称:" + this.projectName);
        console.log("命名空间:" + this.nameSpace);
        console.log("项目作者:" + this.author);
        console.log("项目版本号:" + this.version);
    }
}