import * as readline from 'readline';
import * as unzip from 'unzip';
import * as fs from 'fs';


export default class SyZeroServiceCli {
    public projectName: string = "test";
    public nameSpace: string = "syzero";
    public author: String = "author";
    public version: String = "1.0.0";

    constructor() {

    }

    public async start() {
        await this.init();
        this.show();
    }

    public async init() {
        console.log("开始创建SYZERO1.0.5项目：");
        this.projectName = await this.readlineMsg("请输入项目名称(默认test): \n") || this.projectName;
        this.nameSpace = await this.readlineMsg("请输入命名空间(默认syzero): \n") || this.nameSpace;
        this.author = await this.readlineMsg("请输入作者(默认author): \n") || this.author;
        this.version = await this.readlineMsg("请输入版本号(默认1.0.0): \n") || this.version;
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

    public show() {
        console.log("项目名称:" + this.projectName);
        console.log("命名空间:" + this.nameSpace);
        console.log("项目作者:" + this.author);
        console.log("项目版本号:" + this.version);
    }
}