GiantBomb C#
------------

[![Build status](https://ci.appveyor.com/api/projects/status/hh268dmpd08qfjqt?svg=true)](https://ci.appveyor.com/project/kamranayub/giantbomb-csharp)

This library aims to wrap the GiantBomb REST API in C# with strongly-typed models and is built on top of [RestSharp](https://github.com/johnsheehan/RestSharp).

It also helps make your life easier when dealing with searching because it recursively fetches your search results all at once to enable better sorting.

**Note: This is not really needed anymore due to search improvements in APIv2**

```c#
var giantBomb = new GiantBombRestClient();

// Get all search results
var results = giantBomb.SearchForAllGames("assassin's creed");

// Display
return results.OrderByDescending(g => g.DateAdded)
```

It's also easy to extend to support other GiantBomb resource types that aren't included by default. 
Just create a C# class representing the model and use `GetSingleResource` or `GetListResource`:

```c#
var giantBomb = new GiantBombRestClient();

// Get video
var video = await giantBomb.GetSingleResourceAsync<MyCustomVideoModel>("video", 2300, 123456);
```

## Nuget
Download and install the GiantBomb.Api Nuget package:

	PM> Install-Package GiantBomb.Api

## Supported Platforms

This project targets [.NET Standard 1.2](https://docs.microsoft.com/en-us/dotnet/standard/library)

- .NET Core 1.0+
- .NET 4.5.1+
- Mono 4.6+
- Xamarin.iOS 10.0+
- Xamarin.Android 7.0+
- UWP 10.0+
- Windows 8.1
- Windows Phone 8.1

## Contributing
Read about [contributing on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki). If you plan to contribute, you **must** read this.

## Examples
Read about [examples on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki).

## License
Dual-licensed on MIT & GPL

## Changelog

See [CHANGELOG](CHANGELOG.md)