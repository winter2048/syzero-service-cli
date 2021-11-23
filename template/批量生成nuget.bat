@echo off
set nowPath=%cd%
cd \
cd %nowPath%
echo ��ʼ����

echo ��ʼ����Template1.Template2.IApplication
cd Template1.Template2.IApplication
del /f /s /q %USERPROFILE%\.nuget\packages\Template1.Template2.IApplication
del /f /s /q bin\Debug\*.nupkg
dotnet pack
copy /y bin\Debug\*.nupkg ..\..\..\SYZERO\nuget\*.nupkg
cd %nowPath%

echo ���
Pause
