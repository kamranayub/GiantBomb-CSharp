GiantBomb C#
------------

## 2.3.0
- Updated to .NET Framework 4.6
- Total overhaul of async code to be truly async using ContinueWith, no more 'await' in the library

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

## Readme

This library aims to wrap the GiantBomb REST API in C# with strongly-typed models and is built on top of [RestSharp](https://github.com/johnsheehan/RestSharp).

It also helps make your life easier when dealing with searching because it recursively fetches your search results all at once to enable better sorting.

**Note: This is not really needed anymore due to search improvements in APIv2**

	var giantBomb = new GiantBombRestClient();

	// Get all search results
	var results = giantBomb.SearchForAllGames("assassin's creed");

	// Display
	return results.OrderByDescending(g => g.DateAdded)


## Nuget
Download and install the GiantBomb.Api Nuget package:

	PM> Install-Package GiantBomb.Api

## Contributing
Read about [contributing on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki). If you plan to contribute, you **must** read this.

## Examples
Read about [examples on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki).

## License
Dual-licensed on MIT & GPL
