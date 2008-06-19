@echo off
SET PATH=%PATH%;C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin


for %%i in ("src\*.txt") do resgen %%i
move "src\*.resources" "bin\"
copy "bin\*.resources" "..\bin\Debug\lang"
@echo on