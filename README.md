> **⚠️ Archived:** This repository is archived and no longer actively maintained.

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

## Trademarks

This project references third-party products for build and CI purposes.
All trademarks are the property of their respective owners. Use of these
names does not imply endorsement or affiliation with GeneXus S.A.

- Microsoft®, Visual Studio®, Azure DevOps®, NuGet®, and .NET® are trademarks or registered trademarks of Microsoft Corporation.

## License

Licensed under the [Apache License 2.0](LICENSE).
Copyright 2021 GeneXus S.A.

