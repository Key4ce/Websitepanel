@echo off
RMDIR /S /Q "Bin"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"

IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin\MSBuild.exe"
	Set WSPVSVer=16.0
	Echo Found VS 2019 Community
	GOTO Build 
 )
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Professional\MSBuild\Current\Bin\MSBuild.exe"
	Set WSPVSVer=16.0
	Echo Found VS 2019 Professional
	GOTO Build 
 )
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
	Set WSPVSVer=16.0
	Echo Found VS 2019 Enterprise
	GOTO Build 
 )
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin\MSBuild.exe"
	Set WSPVSVer=15.0
	Echo Found VS 2017 Community
	GOTO Build 
 )
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Professional\MSBuild\15.0\Bin\MSBuild.exe"
	Set WSPVSVer=15.0
	Echo Found VS 2017 Professional
	GOTO Build 
 )
IF EXIST "%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\Microsoft Visual Studio\2017\Enterprise\MSBuild\15.0\Bin\MSBuild.exe"
	Set WSPVSVer=15.0
	Echo Found VS 2017 Enterprise
	GOTO Build 
 )
IF EXIST "%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe" (
	Set WSPMSBuild="%ProgramFiles(x86)%\MSBuild\14.0\Bin\MSBuild.exe"
	Set WSPVSVer=14.0
	Echo Found VS 2015
	GOTO Build 
 )

:Build

)

%WSPMSBuild% build.xml /target:Deploy /p:BuildConfiguration=Release /p:Version="2.1.0" /p:FileVersion="2.1.0.944" /p:VersionLabel="2.1.0.944" /v:n /fileLogger /m /p:VisualStudioVersion=%WSPVSVer%
