@echo off
echo Restoring NuGet packages...

REM Download NuGet.exe if not exists
if not exist "nuget.exe" (
    echo Downloading NuGet.exe...
    powershell -Command "Invoke-WebRequest -Uri 'https://dist.nuget.org/win-x86-commandline/latest/nuget.exe' -OutFile 'nuget.exe'"
)

REM Restore packages
echo Restoring packages to ..\packages...
nuget.exe restore packages.config -PackagesDirectory "..\packages"

echo.
echo Package restore completed!
echo.
pause
