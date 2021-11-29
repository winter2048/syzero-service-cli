export default class SyZeroServiceCli {
    projectName: string;
    nameSpace: string;
    author: String;
    version: String;
    constructor();
    start(): Promise<void>;
    init(): Promise<void>;
    readlineMsg(msg: string): Promise<string>;
    readFileList(dir: string, filesList?: string[], dirList?: string[]): void;
    replaceText(filePath: string, oldText: string, newText: string): void;
    unZip(path: string, dirName: string): Promise<void>;
    sleep(s: number): Promise<void>;
    show(): void;
}
