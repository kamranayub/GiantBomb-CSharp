GiantBomb C#
------------

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

### Breaking Changes/Known Issues

* Platforms return `null` for abbreviation (#11)
* Platforms `LastUpdated` is spelled incorrectly on the API (#10)
* Releases don't return all associations (#9)

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