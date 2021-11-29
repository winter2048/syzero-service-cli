"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
Object.defineProperty(exports, "__esModule", { value: true });
var readline = require("readline");
var compressing = require("compressing");
var fs = require("fs");
var path = require("path");
var SyZeroServiceCli = /** @class */ (function () {
    function SyZeroServiceCli() {
        this.projectName = "test";
        this.nameSpace = "syzero";
        this.author = "author";
        this.version = "1.0.5";
    }
    SyZeroServiceCli.prototype.start = function () {
        return __awaiter(this, void 0, void 0, function () {
            var files, dirs, typeList;
            var _this = this;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this.init()];
                    case 1:
                        _a.sent();
                        if (fs.existsSync(this.projectName) && fs.readdirSync(this.projectName).length > 0) {
                            console.log(this.projectName + "必须是空文件夹！！！");
                            return [2 /*return*/];
                        }
                        console.log("开始创建 ...");
                        //解压文件模板
                        return [4 /*yield*/, this.unZip(path.join(__dirname, "template.bin"), this.projectName)];
                    case 2:
                        //解压文件模板
                        _a.sent();
                        files = [];
                        dirs = [];
                        this.readFileList(this.projectName, files, dirs);
                        typeList = [".sln", ".cs", ".csproj", ".xml", ".user", ".bat"];
                        files.forEach(function (filePath) {
                            //处理项目文件
                            var fileExt = path.extname(filePath);
                            var fileName = path.basename(filePath);
                            //处理项目文件
                            if (typeList.indexOf(fileExt) >= 0) {
                                _this.replaceText(filePath, "Template2", _this.projectName);
                                _this.replaceText(filePath, "Template1", _this.nameSpace);
                                if (fileName.indexOf("Template") >= 0) {
                                    fs.renameSync(filePath, path.join(path.dirname(filePath), fileName.replace("Template2", _this.projectName).replace("Template1", _this.nameSpace)));
                                }
                            }
                            //处理k8s yaml文件
                            if (fileName == "template1-template2-service.yaml") {
                                _this.replaceText(filePath, "Template2", _this.projectName.toLowerCase());
                                _this.replaceText(filePath, "Template1", _this.nameSpace.toLowerCase());
                                fs.renameSync(filePath, path.join(path.dirname(filePath), fileName.replace("template2", _this.projectName.toLowerCase()).replace("template1", _this.nameSpace.toLowerCase())));
                            }
                            //处理Dockerfile文件
                            if (fileName == "Dockerfile") {
                                _this.replaceText(filePath, "Template2", _this.projectName);
                                _this.replaceText(filePath, "Template1", _this.nameSpace);
                            }
                            //处理appsettings.json文件
                            if (fileName == "appsettings.json") {
                                _this.replaceText(filePath, "Template2", _this.projectName);
                                _this.replaceText(filePath, "Template1", _this.nameSpace);
                            }
                            //处理Jenkinsfile文件
                            if (fileName == "Jenkinsfile") {
                                _this.replaceText(filePath, "Template2", _this.projectName);
                                _this.replaceText(filePath, "Template1", _this.nameSpace);
                                _this.replaceText(filePath, "template2", _this.projectName.toLowerCase());
                                _this.replaceText(filePath, "template1", _this.nameSpace.toLowerCase());
                            }
                        });
                        //修改文件夹名称
                        dirs.forEach(function (dir) {
                            if (fs.existsSync(dir)) {
                                fs.renameSync(dir, path.join(path.dirname(dir), path.basename(dir).replace("Template2", _this.projectName).replace("Template1", _this.nameSpace)));
                            }
                        });
                        this.readFileList(this.projectName, files, dirs);
                        files.forEach(function (file) {
                            console.log(file);
                        });
                        console.log("创建成功!");
                        this.show();
                        return [2 /*return*/];
                }
            });
        });
    };
    SyZeroServiceCli.prototype.init = function () {
        return __awaiter(this, void 0, void 0, function () {
            var _a, _b, _c, _d;
            return __generator(this, function (_e) {
                switch (_e.label) {
                    case 0:
                        console.log("开始创建SYZERO1.0.5项目：");
                        _a = this;
                        return [4 /*yield*/, this.readlineMsg("\u8BF7\u8F93\u5165\u9879\u76EE\u540D\u79F0(\u9ED8\u8BA4" + this.projectName + "): \n")];
                    case 1:
                        _a.projectName = (_e.sent()) || this.projectName;
                        _b = this;
                        return [4 /*yield*/, this.readlineMsg("\u8BF7\u8F93\u5165\u547D\u540D\u7A7A\u95F4(\u9ED8\u8BA4" + this.nameSpace + "): \n")];
                    case 2:
                        _b.nameSpace = (_e.sent()) || this.nameSpace;
                        _c = this;
                        return [4 /*yield*/, this.readlineMsg("\u8BF7\u8F93\u5165\u4F5C\u8005(\u9ED8\u8BA4" + this.author + "): \n")];
                    case 3:
                        _c.author = (_e.sent()) || this.author;
                        _d = this;
                        return [4 /*yield*/, this.readlineMsg("\u8BF7\u8F93\u5165\u7248\u672C\u53F7(\u9ED8\u8BA4" + this.version + "): \n")];
                    case 4:
                        _d.version = (_e.sent()) || this.version;
                        return [2 /*return*/];
                }
            });
        });
    };
    SyZeroServiceCli.prototype.readlineMsg = function (msg) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, new Promise(function (resolve, reject) {
                        var rl = readline.createInterface({
                            input: process.stdin,
                            output: process.stdout
                        });
                        rl.question(msg, function (value) {
                            rl.close();
                            resolve(value);
                        });
                    })];
            });
        });
    };
    SyZeroServiceCli.prototype.readFileList = function (dir, filesList, dirList) {
        var _this = this;
        if (filesList === void 0) { filesList = []; }
        if (dirList === void 0) { dirList = []; }
        var files = fs.readdirSync(dir);
        files.forEach(function (item, index) {
            var fullPath = path.join(dir, item);
            var stat = fs.statSync(fullPath);
            if (stat.isDirectory()) {
                dirList.push(fullPath);
                _this.readFileList(path.join(dir, item), filesList, dirList); //递归读取文件
            }
            else {
                filesList.push(fullPath);
            }
        });
    };
    SyZeroServiceCli.prototype.replaceText = function (filePath, oldText, newText) {
        var fileText = fs.readFileSync(filePath).toString();
        fileText = fileText.replace(new RegExp(oldText, 'g'), newText);
        fs.writeFileSync(filePath, fileText);
    };
    SyZeroServiceCli.prototype.unZip = function (path, dirName) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, compressing.zip.uncompress(path, dirName)];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    SyZeroServiceCli.prototype.sleep = function (s) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                return [2 /*return*/, new Promise(function (resolve, reject) {
                        setTimeout(function () {
                            resolve();
                        }, s);
                    })];
            });
        });
    };
    SyZeroServiceCli.prototype.show = function () {
        console.log("项目名称:" + this.projectName);
        console.log("命名空间:" + this.nameSpace);
        console.log("项目作者:" + this.author);
        console.log("项目版本号:" + this.version);
    };
    return SyZeroServiceCli;
}());
exports.default = SyZeroServiceCli;
//# sourceMappingURL=SyZeroServiceCli.js.map