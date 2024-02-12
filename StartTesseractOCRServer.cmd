REM On Windows, stop Python from opening in Microsoft Store, Run in PowerShell
REM Remove-Item $env:USERPROFILE\AppData\Local\Microsoft\WindowsApps\python*.exe
REM Edit the user environment variables to add Python to your path
REM %LOCALAPPDATA%\Microsoft\WindowsApps\PythonSoftwareFoundation.Python.3.11_qbz5n2kfra8p0
REM Add Tesseract to your system path: C:\Program Files\Tesseract-OCR
python3 -m uvicorn TesseractOCRServer:app --reload --port 11439 --log-level error
