kOS_IDE
=======

A lightweight, powerful editing tool for kOS Autopilot Mod on KSP.

## Want to contribute? Setup of ScintillaNET ##
Scintilla is the component used for syntax highlighted content with autocorrect and more. In order to make it work you have to do these things:

1. Set the PATH user environment variable to the location of the repository
2. On Visual Studio, right-click on the **ToolBox** and select **Choose Items ...** from the menu.
3. Be sure to be on the **.NET Framework Components** tab and then click the **Browse ...** button, and locate **ScintillaNET.dll** which should be on the repo too.
4. Ensure that all Scintilla controls are selected and checked before clicking the **OK** button to confirm adding these controls.

The Scintilla control should now be listed in the ToolBox and you should not have problems trying to work on the GUI.