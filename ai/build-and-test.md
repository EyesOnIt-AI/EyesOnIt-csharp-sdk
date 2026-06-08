# Build And Test

## Build

From Windows with .NET/Visual Studio tooling:

```powershell
dotnet build eyesonit-csharp-sdk.sln
```

If legacy MSBuild is required, build the solution from Visual Studio or Developer Command Prompt.

## Package Validation

For release changes, validate package metadata, package contents, README, license file, and version.

## Contract Checks

Compare request/response classes under `src/API/` with backend Pydantic/API behavior and TypeScript SDK models.

