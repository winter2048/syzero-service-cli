@echo off
set nowPath=%cd%
cd \
cd %nowPath%
echo ��ʼ����

echo ��ʼ����syzero.test.IApplication
cd syzero.test.IApplication
del /f /s /q %USERPROFILE%\.nuget\packages\syzero.test.IApplication
del /f /s /q bin\Debug\*.nupkg
dotnet pack
copy /y bin\Debug\*.nupkg ..\..\..\SYZERO\nuget\*.nupkg
cd %nowPath%

echo ���
Pause
