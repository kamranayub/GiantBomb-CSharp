GiantBomb C#
------------

This library aims to wrap the GiantBomb REST API in C# with strongly-typed models and is built on top of [RestSharp](https://github.com/johnsheehan/RestSharp).

It also helps make your life easier when dealing with searching because it recursively fetches your search results all at once to enable better sorting.

```
var giantBomb = new GiantBombRestClient();

// Get all search results
var results = giantBomb.SearchForAllGames("assassin's creed");

// Display
return results.OrderByDescending(g => g.DateAdded)
```

### Nuget
A Nuget package will be released soon. The API is still limited to games only right now.

### Contributing
Read about [contributing on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki). If you plan to contribute, you **must** read this.

### Examples
Read about [examples on the wiki](https://github.com/kamranayub/GiantBomb-CSharp/wiki).

### License
Dual-licensed on MIT & GPL