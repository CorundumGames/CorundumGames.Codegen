# CorundumGames.Codegen

[![Nuget](https://img.shields.io/nuget/v/CorundumGames.Codegen?style=for-the-badge)](https://www.nuget.org/packages/CorundumGames.Codegen)
[![openupm](https://img.shields.io/npm/v/games.corundum.codegen?label=openupm&registry_uri=https://package.openupm.com&style=for-the-badge)](https://openupm.com/packages/games.corundum.codegen)

A set of Jenny plugins that I think are useful for a wide variety of projects that use C#, Unity, and Entitas.

# How to Use

These plugins are intended to be used directly by the Jenny code generator.
They are not intended to be used as a dependency for your own plugins.

## Installation

This section describes the ways in which you can add these plugins to your project.

As far as assemblies go, this project works a little differently.
The assembly is not intended to be used by Unity directly; it is intended for use by `Jenny.exe` on the command line.

### Via OpenUPM

Install the package `games.corundum.codegen` from OpenUPM through the instructions described [here](https://openupm.com/packages/games.corundum.codegen/#modal-manualinstallation).
When you do so, you'll need to add a specific directory to your `Jenny.SearchPaths` property. Add the following:

```properties
Jenny.SearchPaths = ... \
  Library/PackageCache/games.corundum.codegen@0.1.4 \
  ... # Other packages
```

Adjust the version number depending on which one you have installed.

### Manually

This is the least convenient option, but it will work if you're unable to use the UPM package for some reason.

1. Download the latest `.nupkg` from NuGet [here](https://www.nuget.org/api/v2/package/CorundumGames.Codegen).
2. Extract the archive's contents using a tool that supports `.nupkg` files, such as [7-Zip](https://www.7-zip.org).
3. Copy the file `lib/net472/CorundumGames.Codegen.dll` to wherever you keep your codegen assemblies. If you're installing with this method, this will probably be in your project's source tree.
4. Add the directory containing your codegen assemblies to your `Jenny.properties` file.

## Configuration

To use any plugin in this repository, add the following to your `Jenny.properties` file:

```properties
Jenny.SearchPaths = ... # Configure as described in the Installation section

Jenny.Plugins = CorundumGames.Codegen
```

Configuration specific to each plugin can be found in the following sections, where applicable.
If you don't need a particular plugin, simply don't include it in your `Jenny.properties` file.

# Plugins

At the moment, this repository only includes one plugin.
However, I may add more that I develop and use if they're interesting enough to reuse across projects.
If necessary, I will split these plugins into multiple assemblies within this source repository.

## `CorundumGames.Codegen.DisposableComponent`

Generates systems that call `Dispose()` on components that implement [`IDisposable`](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netstandard-2.1).
No attributes necessary, simply implement `IDisposable`.

This is useful for components that contain data that *must* be cleaned up, including pooled objects.
In my case, I use this plugin to reset [DOTween](http://dotween.demigiant.com)-based tweens.
Here's an example of a disposable component that I'm using in [Chromavaders](https://corundum.games):

```csharp
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

// This component is hand-written
[Game]
public sealed class PositionTweenComponent : IComponent, IDisposable
{
    public TweenerCore<Vector2, Vector2, VectorOptions> tween;

    public void Dispose()
    {
        if (tween is { active: true })
        { // If this position tween is not null and it's still active...
            tween.Kill();
        }

        tween = null;
    }
}
```

A disposable component processed with this plugin will have its `Dispose()` method
called when any of the following occurs:

- When [`Systems.TearDown()`](https://sschmid.github.io/Entitas-CSharp/class_entitas_1_1_systems.html#a7610d89dd9172d6dd881bd73f7cb0b48) is called, usually when the game ends.
- When its owning entity is about to be destroyed, via the [`Context.OnEntityWillBeDestroyed`](https://sschmid.github.io/Entitas-CSharp/class_entitas_1_1_context.html#ab8c74cb2adee934df32ec2a86fc607b2) event.
- When it's removed, via the [`Group.OnEntityRemoved`](https://sschmid.github.io/Entitas-CSharp/class_entitas_1_1_group.html#ad010b1c3944aa9aa54c5ff76c93c431e) event.
- When its value is changed, via the [`Group.OnEntityUpdated`](https://sschmid.github.io/Entitas-CSharp/class_entitas_1_1_group.html#a925d5a507d149042cfa728111c1a0d41) event. This currently occurs even if the component's value is replaced with itself.

### Configuration

To include it in your project, modify the following properties within `Jenny.properties` file like so:

```properties
Jenny.DataProviders = CorundumGames.Codegen.DisposableComponent.DataProvider
Jenny.CodeGenerators = CorundumGames.Codegen.DisposableComponent.Generator
```

### Usage

This plugin generates a `DisposeDataFeature` in `Generated/Features`.
Add it to your `Systems` instance to enable it in your project.
You may also add the generated systems to your `Systems` instances directly.

Disposable components may be added to as many `Context`s as you'd like.
This plugin will generate systems for each combination of component and context.

Entitas pools components, therefore your `Dispose()` method **must not** make them unusable.
This means your disposable components must not use a `disposed` flag,
nor should they ever throw [`ObjectDisposedException`](https://docs.microsoft.com/en-us/dotnet/api/system.objectdisposedexception?view=netstandard-2.1).

### Compatibility

The plugin itself should work on all platforms that Jenny supports (Windows, macOS, Linux).

The generated code requires at least Unity 2021.1,
due to its use of [`UnityEngine.Pool`](https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Pool.ObjectPool_1).
If you want to use this generator without using `UnityEngine.Pool`,
please [open a ticket](https://github.com/CorundumGames/CorundumGames.Codegen/issues) (or even better, a pull request).

# Building

This project can be built and used on Windows, macOS, and Linux.
Install the latest version of .NET and build it on the command-line.

Run the command `dotnet build`, passing in the `.sln` file if you're not in that directory.

```shell
# Either change directory to the cloned repo...
cd CorundumGames.Codegen
dotnet build

# ...or pass the solution file explicitly
dotnet build ./path/to/CorundumGames.Codegen.sln
```

# License

All files in this repository are released under the MIT license, unless otherwise noted.

You own the output of each plugin,
I make no ownership claims to it.
