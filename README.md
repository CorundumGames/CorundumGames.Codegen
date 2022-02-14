# CorundumGames.CodeGeneration.Plugins

A set of plugins that I think are useful for a wide variety of projects that use C#, Unity, and Entitas.

# How to Use

TODO: How to install in your project

## Installation

These plugins are intended to be used directly by the Jenny code generator. They are not intended to be used as a
dependency for your own plugins.

## Configuration

To use any plugin in this repository, add the following to your `Jenny.properties` file:

```properties
Jenny.SearchPaths = ...

Jenny.Plugins = CorundumGames.Codegen
```

Configuration specific to each plugin can be found in the sections below.

# Plugins

At the moment, this repository only includes one plugin. But I intend to add others.

## `CorundumGames.Codegen.DisposableComponent`

Generates systems that call `Dispose()` on components that implement `IDisposable`. No attributes necessary, simply
implement `IDisposable`.

Useful for components that contain data that must be cleaned up, but could be deleted at any time. In my case, I use this
plugin to clean up movement tweens belonging to game entities. Here's an example of a component that I'm using in
[Chromavaders](https://corundum.games):

```csharp
using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

/// This component is hand-written
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

This component will have `Dispose()` called in the following scenarios:

- `Systems.TearDown()` is called (usually coinciding with the game exiting)
- An entity is destroyed, or it's about to be
- An entity's component is replaced, or removed

## Configuration

This plugin does not have any custom configuration. To include it in your project, modify the following properties within
`Jenny.properties` file like so:

```properties
Jenny.DataProviders = CorundumGames.Codegen.DisposableComponent.DataProvider
Jenny.CodeGenerators = CorundumGames.Codegen.DisposableComponent.Generator
```

## Usage

This plugin generates a `DisposeDataFeature` in `Generated/Features`. Add it to your `Systems` instance to enable it in your project.

## Compatibility

Due to the use of `UnityEngine.Pool`, at least Unity 2021.1 is required


# Building

TODO: How to build this
