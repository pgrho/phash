# priHash #

<!-- Personal Rough Implementation of pHASH -->

C# Implementation of pHash (<http://phash.org>).
Based on phash-0.9.4 for Windows.


## NuGet packages ##

- [Shipwreck.Phash](https://www.nuget.org/packages/Shipwreck.Phash/) - C# Implementation of phash-0.9.4.

The `Shipwreck.Phash` accepts only `IByteImage` interface. The package does not contain any method to load an image.
So additional packages provide extension methods to instantiate `ByteImage`

- [Shipwreck.Phash.Bitmaps](https://www.nuget.org/packages/Shipwreck.Phash.Bitmaps/) - for System.Drawing.dll including Mono implementation.
- [Shipwreck.Phash.PresentationCore](https://www.nuget.org/packages/Shipwreck.Phash.PresentationCore/) - for PresentationCore.dll. Used for WPF, ASP.NET or Windows Service.

There are some more packages for uncommon usage.

- [Shipwreck.Phash.Data](https://www.nuget.org/packages/Shipwreck.Phash.Data/) - Provides Stored Function Implementations for pHash Digests
- [Shipwreck.Phash.CrossCorrelation](https://www.nuget.org/packages/Shipwreck.Phash.CrossCorrelation/) - C# Implementation of phash-0.9.4 that provides Only GetCrossCorrelation functionality. Intended to be referenced from SQL CLR.

## Hashing an image (Bitmap) ##

```C#
var bitmap = (Bitmap)Image.FromFile(fullPathToImage);
var hash = ImagePhash.ComputeDigest(bitmap.ToLuminanceImage());
```

## Hashing an image (BitmapSource) ##

```C#
var bitmapSource = BitmapFrame.Create(stream);
var hash = ImagePhash.ComputeDigest(bitmapSource.ToLuminanceImage());
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
        var bitmap = (Bitmap)Image.FromFile(currentFile);
        var hash = ImagePhash.ComputeDigest(bitmap);
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

## TestApp ##

Download Image sets from <http://phash.org/download/> and extract into the `/data/compr`, `/data/blur`, `/data/rotd`, `/data/misc` directories.
Or you can create test Image sets by yourself.

## License ##

GNU General Public License version 3 or later
<http://www.gnu.org/licenses/>