"%~dp0nuget.exe" pack^
 "%~dp0..\src\Shipwreck.Phash\Shipwreck.Phash.csproj"^
 -OutputDirectory "%~dp0..\src\Shipwreck.Phash\bin\Release" -IncludeReferencedProjects -Build -Properties Configuration=Release
 
"%~dp0nuget.exe" pack^
 "%~dp0..\src\Shipwreck.Phash.CrossCorrelation\Shipwreck.Phash.CrossCorrelation.csproj"^
 -OutputDirectory "%~dp0..\src\Shipwreck.Phash.CrossCorrelation\bin\Release" -IncludeReferencedProjects -Build -Properties Configuration=Release

"%~dp0nuget.exe" pack^
 "%~dp0..\src\Shipwreck.Phash.Data\Shipwreck.Phash.Data.csproj"^
 -OutputDirectory "%~dp0..\src\Shipwreck.Phash.Data\bin\Release" -IncludeReferencedProjects -Build -Properties Configuration=Release
