#!/usr/bin/env node
import SyZeroServiceCli from './SyZeroServiceCli';

function run(argv: string[]) {
    if (argv[0] === '-v' || argv[0] === '--version') {
        console.log('  version is 1.0.5');
    } else if (argv[0] === '-h' || argv[0] === '--help') {
        console.log('  usage:\n');
        console.log('  -v --version [show version]');
    } else {
        var syZeroServiceCli = new SyZeroServiceCli();
        syZeroServiceCli.start();
    }
}
run(process.argv.slice(2));