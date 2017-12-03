# priHash #

<!-- Personal Rough Implementation of pHASH -->

C# Implementation of pHash (<http://phash.org>).
Based on phash-0.9.4 for Windows.

## TestApp ##

Download Image sets from <http://phash.org/download/> and extract into predefined `compr`, `blur`, `rotd`, `misc` directories.
Or you can create test Image sets by yourself.

## License ##

GNU General Public License version 3 or later
<http://www.gnu.org/licenses/>

## NuGet packages ##

[Shipwreck.Phash](https://www.nuget.org/packages/Shipwreck.Phash/) - C# Implementation of phash-0.9.4.

[Shipwreck.Phash.Bitmaps](https://www.nuget.org/packages/Shipwreck.Phash.Bitmaps/) - C# Implementation of phash-0.9.4 for bitmaps.

[Shipwreck.Phash.Data](https://www.nuget.org/packages/Shipwreck.Phash.Data/) - Provides Stored Function Implementations for pHash Digests

[Shipwreck.Phash.CrossCorrelation](https://www.nuget.org/packages/Shipwreck.Phash.CrossCorrelation/) - C# Implementation of phash-0.9.4 that provides Only GetCrossCorrelation functionality. Intended to be referenced from SQL CLR.

## Hashing an image (Bitmap) ##

```C#
var hash = ImagePhash.ComputeDigest(fullPathToImage);
```

## Image similarity score ##

```C#
var score = ImagePhash.GetCrossCorrelation(hash1, hash2);
```

## Multithreaded hashing of all images in a folder ##

Example below required .NET 4.7+ since the function returns a tuple of results.

```C#
public static (ConcurrentDictionary<string, Digest> filePathsToHashes, ConcurrentDictionary<Digest, HashSet<string>> hashesToFiles) GetHashes(string dirPath, string searchPattern)
{
	var filePathsToHashes = new ConcurrentDictionary<string, Digest>();
	var hashesToFiles = new ConcurrentDictionary<Digest, HashSet<string>>();

	var files = Directory.GetFiles(dirPath, searchPattern);

	Parallel.ForEach(files, (currentFile) =>
	{
		var hash = ImagePhash.ComputeDigest(currentFile);
		filePathsToHashes[currentFile] = hash;

		HashSet<string> currentFilesForHash;

		lock (hashesToFiles)
		{
			if (!hashesToFiles.TryGetValue(hash, out currentFilesForHash))
			{
				currentFilesForHash = new HashSet<string>();
				hashesToFiles[hash] = currentFilesForHash;
			}
		}

		lock (currentFilesForHash)
		{
			currentFilesForHash.Add(currentFile);
		}
	});

	return (filePathsToHashes, hashesToFiles);
}
```

Then you can call it like this:

```C#
(ConcurrentDictionary<string, Digest> gilePathsToHashes, ConcurrentDictionary<Digest, HashSet<string>> hashesToFiles) =
	GetHashes(
		dirPath: @"C:\some\path\",
		searchPattern: "*.jpg");
```
