GiantBomb C#
------------

### Pre-Release Notes

This a pre-release package, to test your project against new versions of the GiantBomb API. You'll need to instantiate your client like this:

	var client = new GiantBombRestClient(
		apiToken, new Uri("http://seaserpent.giantbomb.com/api"));

#### Notable Changes

* `GetReleasesForGame()` doesn't quite work yet (filtering seems broken)
* Search is now MUCH better in GiantBomb's v2 API, there's little to no need for `SearchAllGames` now unless you expect/want more than 100 results
* List resources support new `sort` and `filter` options
* All single resource requests must use a new resource ID, e.g. "game/3030-33394", which this implements transparently for you

### Readme

This library aims to wrap the GiantBomb REST API in C# with strongly-typed models and is built on top of [RestSharp](https://github.com/johnsheehan/RestSharp).

It also helps make your life easier when dealing with searching because it recursively fetches your search results all at once to enable better sorting.

	var giantBomb = new GiantBombRestClient();

	// Get all search results
	var results = giantBomb.SearchForAllGames("assassin's creed");

	// Display
	return results.OrderByDescending(g => g.DateAdded)


### Nuget
Download and install the GiantBomb.Api Nuget package:

	PM> Install-Package GiantBomb.Api -Pre

### Contributing
Read about [contributing on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki). If you plan to contribute, you **must** read this.

### Examples
Read about [examples on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki).

### License
Dual-licensed on MIT & GPL