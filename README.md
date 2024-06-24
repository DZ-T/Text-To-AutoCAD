# txt2ac AutoCAD Plugin

## Overview

txt2ac is a plugin for AutoCAD that facilitates the importing of points from a text file into the AutoCAD drawing environment. This plugin simplifies the process of bringing coordinate data from external sources directly into your AutoCAD drawings, enhancing productivity and accuracy in CAD workflows.

![app](/Example/txt2ac.png)

## Features

- **Import Points**: Load points defined in a structured text file (CSV, TSV) into AutoCAD as drawing entities.
- **Customizable Input**: Configure column positions for X, Y, and optionally Z coordinates directly from the plugin interface.
- **Error Handling**: Provides informative error messages for file reading issues or malformed data in the input file.

## Installation

1. **Download**: Obtain the latest release of txt2ac plugin from the [Releases](https://github.com/DZ-T/Text-To-AutoCAD/releases/) page.
2. **Installation**: 
   - Copy `txt2ac.dll` to a directory on your computer.
   - In AutoCAD, use the `NETLOAD` command to load `txt2ac.dll` into the AutoCAD environment.

## Usage

1. **Open AutoCAD**: Launch AutoCAD or ensure it is running.
2. **NETLOAD Command**: 
   - Type `NETLOAD` in the AutoCAD command line.
   - Navigate to the directory where `txt2ac.dll` is located and select it to load the plugin.
3. **Interface**:
   - The txt2ac plugin will appear as a new command  in AutoCAD.
   - Use the command `txt2ac` to open the plugin interface.
4. **Select File**: 
   - Use the interface to select a text file (.txt) containing point data.
5. **Import Points**: 
   - Configure column positions for X, Y, and Z coordinates.
   - Click "Import" to load points into the current AutoCAD drawing.
6. **Review Results**: 
   - Confirm successful import of points via messages in plugin interface.

## Example Input File
![example of input file](/Example/image.png)

## Compatibility
- Compatible with AutoCAD versions: AutoCAD 2018 and later.
- Tested on Windows operating systems.
## Contributing
Contributions are welcome! If you have any improvements or new features to add, please fork this repository and submit a pull request.

## License
This project is licensed under the [MIT License](LICENSE).

## Support
For any questions, issues, or feedback, please [open an issue](https://github.com/DZ-T/Text-To-AutoCAD/issues) here on GitHub.
