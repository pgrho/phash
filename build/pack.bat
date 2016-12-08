"%~dp0nuget.exe" pack^
 "%~dp0..\src\Shipwreck.Phash\Shipwreck.Phash.csproj"^
 -OutputDirectory "%~dp0..\src\Shipwreck.Phash\bin\Release"

"%~dp0nuget.exe" pack^
 "%~dp0..\src\Shipwreck.Phash.Data\Shipwreck.Phash.Data.csproj"^
 -OutputDirectory "%~dp0..\src\Shipwreck.Phash.Data\bin\Release"
