## Contributing

If you pull down the project and try to run the tests you will get an error about a file not being found. In order to keep API keys private, my `gitignore` will ignore any file with the extension "private".

To get the tests working, [sign up for a GiantBomb API key](http://api.giantbomb.com/) and add a new file in the `Tests/Support` folder (physically, not in Visual Studio) called `api_token.private` and simply paste your API key into it.

## Releasing

To release a new version of the package, update `GiantBomb.Api/GiantBomb.Api.csproj` and modify the `<version>` field. Appveyor will automatically publish all pushes to master with whatever the version is so **right now** this is a manual semver release process.

For breaking changes, please update the **major** version. For backwards-compatible features, **minor version**, and for patches/fixes, the **patch version**.

### Workflow

I try to use [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) now to aid in automatic release versioning (not yet implemented). If you have a major issue or feature, please submit an issue first to discuss the implementation.

### Architecture

The API uses a simple architecture where every resource (plural includes both List and Single resources) has its own `partial` class file (i.e. `Games.cs`, `Platforms.cs`). The base code is in `Core.cs`.

You can implement common functions like getting lists using the protected `GetListResource` or `GetSingleResource` functions. See my existing code for my conventions.

### Conventions
Try to match my conventions, I won't accept any pull requests that end up formatting every file. If you want to propose a new architectural change or convention, create an Issue and we'll discuss it. I'm definitely open to it, but I want those commits to be separated from added logic/resources.
