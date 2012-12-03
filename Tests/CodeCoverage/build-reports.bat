@ECHO OFF

IF NOT EXIST .\CoverageReports (
	MKDIR .\CoverageReports
)

ECHO Generating Reports
FOR /F "tokens=*" %%G IN ('dir /b ^"Steganography.*^"') DO (
	ECHO Generating Report for %%G
	"..\..\Tools\MSXSL\msxsl.exe" %%G .\CodeCoverage.xsl -o .\CoverageReports\%%~nG.html
)
