RMDIR /S /Q ..\binaries
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\SimpleHost.sln -t:Clean
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild ..\..\src\SimpleHost.sln -p:Configuration=Debug

xcopy /Y binaries\*.* SimpleID\lib
xcopy /Y /S /I ..\..\src\SimpleHost\Content\SimpleID\*.* SimpleID\content\Content\SimpleID
xcopy /Y /S /I ..\..\src\SimpleHost\Scripts\SimpleID\*.* SimpleID\content\Scripts\SimpleID

xcopy /Y /S /I ..\..\src\SimpleHost\Views\OpenID\*.* SimpleID\content\Views\OpenID
xcopy /Y /S /I ..\..\src\SimpleHost\Views\Xrds\*.* SimpleID\content\Views\Xrds

xcopy /Y /S /I ..\..\src\SimpleID\*.cs SimpleID\src\SimpleID
nuget pack SimpleID/SimpleID.nuspec -symbols
