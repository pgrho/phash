# TargetFrameworks Design Note

## Depended Features

|Constant|Feature|.NET Framework|.NET Core|.NET Standard|
|-|-|-|-|-|
|`NO_UNSAFE`|`unsafe`|-|-|-|
|`NO_SERIALIZABLE`|[`[Serializable]`](https://docs.microsoft.com/en-us/dotnet/api/system.serializableattribute)|1.1|2.0|2.0|
|`NO_MATHF`|[System.MathF](https://docs.microsoft.com/en-us/dotnet/api/system.mathf)|-|2.0|2.1|
|`NO_VECTOR`|[Vector&lt;T&gt; struct](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.vector-1) ([NuGet](https://www.nuget.org/packages/System.Numerics.Vectors/))|4.5 (NuGet)|2.0 (NuGet)<br/>3.0|1.0 (NuGet)<br/>2.1|
|`NO_SPAN`|[Span&lt;T&gt; struct](https://docs.microsoft.com/en-us/dotnet/api/system.span-1) ([NuGet](https://www.nuget.org/packages/System.Memory/))|4.5 (NuGet)|2.1|1.1 (NuGet)<br/>2.1|
|`NO_X86_INSTRINSICS`|[System.Runtime.Intrinsics.X86](https://docs.microsoft.com/en-us/dotnet/api/system.runtime.intrinsics.x86)|-|3.0|-|
||[System.Windows.Media.Imaging](https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.imaging.bitmapsource)|3.0|3.0|-|
||[System.Drawing.Bitmap](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.bitmap) ([NuGet](https://www.nuget.org/packages/System.Drawing.Common))|1.1|2.1|2.0 (NuGet)|

## Project Configurations

### `Shipwreck.Phash`

|Target Framework|.NET Standard|`NO_SERIALIZABLE`|`NO_MATHF`|`NO_VECTOR`|`NO_SPAN`|`NO_X86_INSTRINSICS`|Comment|
|-|-|-|-|-|-|-|-|
|net4|||||||Not Supported|
|net45|1.1|-|〇|NuGet|NuGet|〇|
|net461|2.0|-|〇|NuGet|NuGet|〇|
|netcoreapp1.0|1.6|〇|〇|NuGet|NuGet|〇|
|netcoreapp2.0|2.0|-|-|NuGet|NuGet|〇|
|netcoreapp2.1||-|-|-|-|〇|
|netcoreapp3.0||-|-|-|-|-|
|netstandard1.0|-|〇|〇|NuGet|〇|〇|
|netstandard1.1|-|〇|〇|NuGet|NuGet|〇|
|netstandard2.1|-|-|-|-|-|〇|
### `Shipwreck.Phash.PresentationCore`

- `net45`
- `net461`
- `netcoreapp3.0`

### `Shipwreck.Phash.Bitmaps`

- `net45`
- `net461`
- `netcoreapp2.1`
- `netcoreapp3.0`
- `netstandard2.0`
- `netstandard2.1`

## `Shipwreck.Phash.CrossCorrelation`

Targeting `net4` and `netstandard1.0` with no dependencies.