[Setup]
AppName=Ollama Copilot
AppVerName=Ollama Copilot v1.0.5
AppPublisher=TAGENIGMA LLC
AppPublisherURL=https://tagenigma.com
AppSupportURL=https://tagenigma.com
AppUpdatesURL=https://tagenigma.com
DefaultDirName={localappdata}\TAGENIGMA\OllamaCopilot
DefaultGroupName=TAGENIGMA\Ollama Copilot
OutputBaseFilename=OllamaCopilotSetup_v1.0.5
SetupIconFile=favicon.ico
UninstallDisplayIcon=favicon.ico
Compression=lzma
SolidCompression=yes
InfoBeforeFile=eula.txt

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Files]
Source: "bin\Release\*"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; Excludes: "*.pdb;*.history"; CopyMode: alwaysoverwrite
Source: "Pyttsx3Server.py"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; CopyMode: alwaysoverwrite
Source: "start_whisper_server.sh"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; CopyMode: alwaysoverwrite
Source: "StartPyttsx3Server.cmd"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; CopyMode: alwaysoverwrite
Source: "StartTesseractOCRServer.cmd"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; CopyMode: alwaysoverwrite
Source: "TesseractOCRServer.py"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; CopyMode: alwaysoverwrite
Source: "WhisperServer.py"; DestDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; CopyMode: alwaysoverwrite

[Icons]
Name: "{group}\Ollama Copilot"; Filename: "{localappdata}\TAGENIGMA\OllamaCopilot\WinForm_Ollama_Copilot.exe"; WorkingDir: "{app}";
Name: "{commondesktop}\Ollama Copilot"; Filename: "{localappdata}\TAGENIGMA\OllamaCopilot\WinForm_Ollama_Copilot.exe"; WorkingDir: "{app}";
Name: "{group}\Uninstall Ollama Copilot"; Filename: "{uninstallexe}"

[Run]
Filename: "{localappdata}\TAGENIGMA\OllamaCopilot\WinForm_Ollama_Copilot.exe"; Description: "Launch Ollama Copilot"; Flags: shellexec postinstall skipifsilent runascurrentuser; WorkingDir: "{localappdata}\TAGENIGMA\OllamaCopilot"
Filename: "{win}\explorer.exe"; Description: "Open Application Folder"; Flags: shellexec postinstall skipifsilent runascurrentuser; WorkingDir: "{localappdata}\TAGENIGMA\OllamaCopilot"; Parameters: "{localappdata}\TAGENIGMA\OllamaCopilot"
