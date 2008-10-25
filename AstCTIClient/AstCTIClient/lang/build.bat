@echo off
SET PATH=%PATH%;C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin
IF NOT EXIST "..\bin\Debug\lang" mkdir "..\bin\Debug\lang"
IF NOT EXIST "..\bin\Release\lang" mkdir "..\bin\Release\lang"

for %%i in ("src\*.txt") do resgen %%i
move "src\*.resources" "bin\"
copy "bin\*.resources" "..\..\..\Binaries\AstCTIClient\lang"
pause
@echo on