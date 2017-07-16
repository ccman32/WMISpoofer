WMISpoofer
=================================

OVERVIEW
-----
WMISpoofer allows you to spoof information which other applications read from the Windows Management Instrumentation (WMI).
It uses the AppInit_DLLs registry value to inject a dll into all running programs. The loaded dll then checks if the program it has been injected into gets information from the WMI and spoofs the requested information according to the WMISpoofer settings.

INSTALLATION
-----
- Download the latest release from https://github.com/ccman32/WMISpoofer/releases
- Extract all files from the .rar archive
- Run the WMISpooferGUI.exe file
- Click the "Install" button to install WMISpoofer

HOW TO USE
-----
After installing WMISpoofer will ask you if you want to go to the settings to configure it.
Click on "Yes" to open the settings tab in the program:

![alt tag](https://i.imgur.com/1BazTSY.png)

- After selecting a class in the "Classes" list, the properties of the selected class will be listed in the "Class Properties" list
- Double click an entry in the "Class Properties" list to open a box to type in a new value for the selected property

![alt tag](https://i.imgur.com/JgRsmNb.png)

- After typing in a new value and clicking on "OK", the new value will be added to the "New Values" list

![alt tag](https://i.imgur.com/YGA0xeX.png)

- To edit a value in the "New Values" list, select it and click on "Edit Value"
- To delete a value from the "New Values" list, select it and click on "Delete Value"
- You can import values from a file to the list or export values from the list to a file by clicking on "Import" or "Export"
- Click on "Save" to save your settings. It is recommended to restart your computer after saving your settings
- Once you saved your settings, WMISpoofer will start to spoof the information which other applications read from the WMI

**Example (msinfo32)**
![alt tag](https://i.imgur.com/BMGalc9.png)
