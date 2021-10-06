# gxtest-tools
Tools that may help on projects that use GeneXus and GXtest

[![Build](https://github.com/genexuslabs/gxtest-tools/actions/workflows/dotnet.yml/badge.svg)](https://github.com/genexuslabs/gxtest-tools/actions/workflows/dotnet.yml)

## TestConverter
Helps converting tests from GXtest v3 to v4.

### Usage
```TestConverter.exe -f gxtestv3Sample.xml [--vars name=value[, name2=value2]] [-v] [-c] [-m] [--help] [--version]```

| Option              | Description |
|---------------------|-------------|
| `-f`, `--file`      | Required. Path to the source XML file. |
| `-v`, `--verbosity` | Verbosity level on code comments. Valid values: Quiet, Minimal, Normal, Detailed, Diagnostic |
| `--vars`            | List of semicolon separated 'name=value' variable substitutions. Example: "--vars testmain=TestMain.Link()". |
| `-c`, `--compact`   | Don't generate a blank line to separate commands from different elements.|
| `-m`, `--noEndMethod` | Don't generate a final '&driver.End()' line.|
| `--help`            | Display this help screen.|
| `--version`         | Display version information.|

