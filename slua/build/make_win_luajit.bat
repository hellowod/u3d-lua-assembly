@echo off
copy /Y slua.c luajit-2.1.0\src\

if exist "%VS140COMNTOOLS%" (
	set VCVARS="%VS140COMNTOOLS%..\..\VC\bin\"
	goto build
	) 
else (
	goto check2013
)

:check2013
if exist "%VS120COMNTOOLS%" (
	set VCVARS="%VS120COMNTOOLS%..\..\VC\bin\"
	)
else (
	goto missing
)

:build

@set ENV32="%VCVARS%vcvars32.bat"
@set ENV64="%VCVARS%amd64\vcvars64.bat"

call "%ENV32%"
echo Swtich to x86 build env
cd luajit-2.1.0\src
call msvcbuild.bat
md ..\..\plugin_luajit\Plugins\x86
copy /Y lua51.dll ..\..\plugin_luajit\Plugins\x86\slua.dll
cd ..\..

call "%ENV64%"
echo Swtich to x64 build env
cd luajit-2.1.0\src
call msvcbuild.bat
md ..\..\plugin_luajit\Plugins\x86_64
copy /Y lua51.dll ..\..\plugin_luajit\Plugins\x86_64\slua.dll
cd ..\..


goto :eof

:missing
echo Can't find Visual Studio 2013.
goto :eof