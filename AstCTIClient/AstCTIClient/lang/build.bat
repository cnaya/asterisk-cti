@echo off
SET PATH=%PATH%;%PROGRAMFILES%\Microsoft Visual Studio 8\SDK\v2.0\Bin;%PROGRAMFILES%\Microsoft.NET\SDK\v2.0\Bin

for %%i in ("src\*.txt") do resgen %%i
move "src\*.resources" "bin\"
copy "bin\*.resources" "..\..\..\Binaries\AstCTIClient\lang"
pause
@echo on