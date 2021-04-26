// OLE DB Provider for XML
// Copyright (C) 1997-2002 Bjarke Viksoe
// All rights reserved.
//
// The code and information is provided "as-is" without
// warranty of any kind, either expressed or implied.
//
// www.viksoe.dk/code
// Beware of bugs.

// Setup program for the OLE DB Provider for XML

main();

function main()
{
	var bDebug = false;
	var Args = WScript.Arguments;
	if(Args.length > 0 && Args(0) == "/debug")
		bDebug = true;

	// Create shell object
	var WSShell = WScript.CreateObject("WScript.Shell");
	// Create file system object
	var FileSys = WScript.CreateObject("Scripting.FileSystemObject");

	var strValue = FileSys.GetAbsolutePathName(".");
	if(strValue == null || strValue == "")
		strValue = ".";

	var strSourceFolder = strValue;
	if(bDebug)
		WScript.Echo("Source: " + strSourceFolder);

	try
	{
		var oXML = WScript.CreateObject("MSXML2.DOMDocument.4.0");
	}
	catch(e)
	{
		WScript.Echo("ERROR: Microsoft XML Parser 4.0 must be installed.");
		return;
	}

	var strCmd = "regsvr32 -s \"" + strSourceFolder + "\\" + "XmlOleDb.dll\"";
	if(bDebug)
		WScript.Echo("Cmd: " + strCmd);
	ret = WSShell.Run(strCmd, 1, true)

	WScript.Echo("Provider successfully installed!");
}
