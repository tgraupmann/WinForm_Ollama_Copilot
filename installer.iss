[Setup]
AppName=Ollama Copilot
AppVerName=Ollama Copilot v1.0.4
AppPublisher=TAGENIGMA LLC
AppPublisherURL=https://tagenigma.com
AppSupportURL=https://tagenigma.com
AppUpdatesURL=https://tagenigma.com
DefaultDirName={localappdata}\TAGENIGMA\OllamaCopilot
DefaultGroupName=TAGENIGMA\Ollama Copilot
OutputBaseFilename=OllamaCopilotSetup_v1.0.4
SetupIconFile=favicon.ico
UninstallDisplayIcon=favicon.ico
Compression=lzma
SolidCompression=yes
InfoBeforeFile=eula.txt

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\*"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; Excludes: "*.pdb"; CopyMode: alwaysoverwrite

[Icons]
Name: "{group}\Ollama Copilot"; Filename: "{localappdata}\TAGENIGMA\OllamaCopilot\WinForm_Ollama_Copilot.exe"; WorkingDir: "{app}";
Name: "{commondesktop}\Ollama Copilot"; Filename: "{localappdata}\TAGENIGMA\OllamaCopilot\WinForm_Ollama_Copilot.exe"; WorkingDir: "{app}";
Name: "{group}\Uninstall Ollama Copilot"; Filename: "{uninstallexe}"

[Run]
Filename: "{localappdata}\TAGENIGMA\OllamaCopilot\WinForm_Ollama_Copilot.exe"; Description: "Launch Ollama Copilot"; Flags: postinstall skipifsilent runascurrentuser; WorkingDir: "{app}"
