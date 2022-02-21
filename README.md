# CorundumGames.CodeGeneration.Plugins

[![Nuget](https://img.shields.io/nuget/v/CorundumGames.Codegen?style=for-the-badge)](https://www.nuget.org/packages/CorundumGames.Codegen)

A set of plugins that I think are useful for a wide variety of projects that use C#, Unity, and Entitas.

# How to Use

TODO: How to install in your project

## Installation

These plugins are intended to be used directly by the Jenny code generator. They are not intended to be used as a
dependency for your own plugins.

## Configuration

To use any plugin in this repository, add the following to your `Jenny.properties` file:

```properties
Jenny.SearchPaths = ... # Wherever you keep your codegen assemblies

Jenny.Plugins = CorundumGames.Codegen
```

Configuration specific to each plugin can be found in the sections below.

# Plugins

At the moment, this repository only includes one plugin. But I intend to add others.

All plugins will be contained in one assembly. If you don't need a plugin, simply don't include it in your `Jenny.properties`
file.

## `CorundumGames.Codegen.DisposableComponent`

Generates systems that call `Dispose()` on components that implement [`IDisposable`](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netstandard-2.1).
No attributes necessary, simply implement `IDisposable`.

This is useful for components that contain data that *must* be cleaned up, including pooled objects.
In my case, I use this plugin to reset [DOTween](http://dotween.demigiant.com/)-based tweens. Here's an example of a
component that I'm using in [Chromavaders](https://corundum.games):

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

A disposable component processed with this plugin will have its `Dispose()` method called when any of the following occurs:

- When `Systems.TearDown()` is called (usually coinciding with the game ending).
- When its owning entity is about to be destroyed (via a `Context.OnEntityWillBeDestroyed` event).
- When it's removed (via a `Group.OnEntityRemoved` event).
- When its value is changed (via a `Group.OnEntityUpdated` event). This currently occurs even if the component's value is replaced with itself.

### Configuration

To include it in your project, modify the following properties within `Jenny.properties` file like so:

```properties
Jenny.DataProviders = CorundumGames.Codegen.DisposableComponent.DataProvider
Jenny.CodeGenerators = CorundumGames.Codegen.DisposableComponent.Generator
```

### Usage

This plugin generates a `DisposeDataFeature` in `Generated/Features`. Add it to your `Systems` instance to enable it in your project.

### Compatibility

The plugin itself should work on all platforms that Jenny supports (Windows, macOS, Linux).

The generated code requires at least Unity 2021.1, due to its use of [`UnityEngine.Pool`](https://docs.unity3d.com/2022.1/Documentation/ScriptReference/Pool.ObjectPool_1).
If you want to use this generator without using `UnityEngine.Pool`, please [open a ticket](/issues) (or even better, a pull request).

# Building

Install the latest version of .NET and build it on the command-line. This should work on Windows, macOS, and Linux.

Run the command `dotnet build`, passing in the `.sln` file if you're not in that directory.

```shell
# Either change directory to the cloned repo...
cd CorundumGames.Codegen
dotnet build

# ...or pass the solution file explicitly
dotnet build ./path/to/CorundumGames.Codegen.sln
```
