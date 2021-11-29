#!/usr/bin/env node
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var SyZeroServiceCli_1 = require("./SyZeroServiceCli");
function run(argv) {
    if (argv[0] === '-v' || argv[0] === '--version') {
        console.log('  version is 1.0.5');
    }
    else if (argv[0] === '-h' || argv[0] === '--help') {
        console.log('  usage:\n');
        console.log('  -v --version [show version]');
    }
    else {
        var syZeroServiceCli = new SyZeroServiceCli_1.default();
        syZeroServiceCli.start();
    }
}
run(process.argv.slice(2));
//# sourceMappingURL=Index.js.map