# ScanToCloud
A lightweight tool to scan and upload documents to the cloud.

## About
This software has been developed to test the interaction with TWAIN drivers and to serve as a codebase for the upcoming *TwainContentProvider* for the bigger *SimpleDMS* project.

I also needed a standalone scanning tool to be compiled against an earlier version of the .NET Framework, making it compatible with older WinXP based machines.

## Development status, history and releases
Please check the project releases page on GitHub for information: https://github.com/edonosotti/ScanToCloud/releases

## License
Check LICENSE.txt for more information.
This software includes third-party libraries, check their license as well.

## Development notes
This software has been developed with Microsoft Visual Studio Community 2015 RC.

As I stated in the _About_ section, this source code is set to be compiled against the .NET Framework v4.0. This means that you _should_ be able to import and compile it with Visual Studio 2010 (or any other newer version) as well. 
In that case, you will need to create a new solution and manually import the code, or edit the project and solution files, because the IDE will refuse to open a project created with a newer version. You will also need to install nuget and download all the referenced packages (or manually download and reference them).

I also managed to successfully open & compile the project in SharpDevelop v4.4.
You will need to edit ScanToCloud.WinUI.csproj and change: 
ToolsVersion="14.0" to ToolsVersion="4.0"
then run the nuget _restore packages_ procedure from the IDE or the console.
