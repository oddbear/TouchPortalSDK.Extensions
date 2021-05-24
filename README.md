# TouchPortalSDK.Extensions

Building entry.tp file by analyzing the code.

What I want this to do:
* Create entry.tp file
* Call the correct actions etc.
* Create .tpp file

Things I have tried:
* Souce Generators:
> Should be used for generating source code, not resources ([may change](https://github.com/dotnet/roslyn/issues/49935)).<br />
> Will need to be crated per language, ex. one for C# one for VB.Net etc.

* MSBuild custom task:
> Uses standard reflection.<br />
> Issues like locks from MSBuild.exe etc.<br />
> Hard to make work for both .Net 4x and netstandard.

Things I have not tried yet:
* dotnet CLI tools:
> Might only work for new csproj type, and that would be Ok.
