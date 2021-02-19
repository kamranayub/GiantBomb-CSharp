# Changelog

## 2.5.0

- Update to .NET 5.0
- Update `RestSharp.Portable` to `RestSharp`
- Fixes to tests
- Fixes to CI pipeline

Thanks to [@chyyran](https://github.com/chyyran) for their work on this release.

## 2.4.0

- Switch to [.NET Standard 1.2](https://docs.microsoft.com/en-us/dotnet/standard/library). Adds support for:
    - .NET Core 1.0+
    - .NET 4.5.1+
    - Mono 4.6+
    - Xamarin.iOS 10.0+
    - Xamarin.Android 7.0+
    - UWP 10.0+
    - Windows 8.1
    - Windows Phone 8.1
- Switch to [Fubar.RestSharp.Portable](https://github.com/FubarDevelopment/restsharp.portable)
- More descriptive error handling
- **Temporary:** Remove `Game.ProductCodeType` because API returns incorrect response

## 2.3.0

- **BUG:** Fixed deserialization exception for single resource errors (e.g. Object Not Found)
- **BUG:** Fixed synchronous API to not deadlock
- Removed fastJSON DLL since it wasn't *actually* being used
- Upgrade to RestSharp 105.2.3
- Added .NET 4.6 and 4.6.1 support

## 2.2.1

- Better error handling for API exceptions

## 2.2.0

- Upgrade to .NET 4.5.2 (you should too!)
- Upgrade RestSharp to 105.1
- `async/await` support for all methods
- Better fatal error handling
  - If GiantBomb returns a non-JSON response, the library will throw a `GiantBombHttpException`
    containing the raw response body
- **FIX:** Issue with searches returning duplicate games from GiantBomb

## 2.1.0

- Add new fields:
    - `Platform.Aliases`
    - `Platform.InstallBase`
    - `Platform.OnlineOnly`
    - `Platform.OriginalPrice`
    - `Game.Franchises`
    - `Release.Game`
- Add exception handling and wrapping for GiantBomb API errors (`GiantBombApiException` class)
    - For example, exceeding the 400 requests in 15 minutes rate limit
- Allow overriding `Execute` methods
- Update RestSharp to 105 (fixes #13)

## 2.0.3

- Fixes issue with search paging (`offset` vs. `page` parameter)

## API v2 Support

GiantBomb-C# 2.0+ is only compatible with GiantBomb APIv2. Keep using the old packages if you need v1 support, as there are breaking changes in v2!

### Notable Changes

* Search is now MUCH better in GiantBomb's v2 API, there's little to no need for `SearchAllGames` now unless you expect/want more than 100 results
* List resources support new `sort` and `filter` options
* All single resource requests must use a resource ID, e.g. "game/3030-33394", which GBCS implements transparently for you
* `Game` and `Release` now have `ExpectedReleaseDay`
* Search results now include platforms
* `Game` has two new fields:
	- `Aliases` - newline delimited aliases
	- `OriginalGameRating`
* You can now use `GetReleasesForGame()` to directly retrieve releases for a game in one request
* Using [FastJSON](http://www.codeproject.com/Articles/159450/fastJSON) to deserialize, which is... fast, obviously
