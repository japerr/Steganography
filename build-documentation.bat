@ECHO OFF

"Tools\Doxygen\doxygen.exe" "Tools\Doxygen\doxyfile.config"

"Tools\MSXSL\msxsl.exe" .\documentation\xml\index.xml .\documentation\xml\combine.xslt -o .\documentation\xml\all.xml

"Tools\MSXSL\msxsl.exe" .\documentation\xml\all.xml .\CoverageToJSON.xsl -o .\Output.html projectName="Steganography"
PAUSE
REM START .\documentation\html\index.htm