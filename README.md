# Game Sample

The game sample demos nearly every important feature in Forge, including integration into both Unity and XNA!


## Running in Unity

Sorry, the gifs are recorded at a low FPS. The games are both actually extremely smooth due to interpolation of position data (implemented in both Unity and XNA; see the `PositionRenderer`), despite the fact that they are both only updating 15 times a second.

![The game running in Unity](demo_unity.gif) 

## Running in XNA

Open up XNA.sln (if you have XNA installed) to run this demo locally.

![The game running in XNA](demo_xna.gif)

# Overview

There are four folders.

- `Assets`: A snapshot.json and templates.json file that will allow you to start up a simple game. These inject systems and data values into the engine. Feel free to look at the JSON files; they describe the serialization format of the game. Forge save games will be serialized into another snapshot.json. One way of viewing the `Unity` editor is a designer of save-game files, and when you start a new game you're really just loading a saved file.
- `GameLogic`: Game logic that is independent from any framework. This builds directly on top of Forge APIs and implements the gameplay.
- `Unity`: Integration into the Unity game engine. This requires the Forge Unity plug-in, which will be available soon.
- `XNA`: Integration into XNA/MonoGame. This currently uses an unpublished version of the XNA Forge bindings, which will be made available soon.

# Game Logic Files

The `GameLogic` folder contains the interesting game-specific code.

`BallCollisionSystem` causes balls to reverse direction when they hit entities with `PaddleData`.

`BallData` defines what a ball is, and contains data that causes collision response to be temporarily disabled when a ball hits a paddle.

`BallSpawningData` is data that is stored in the `GlobalEntity` that the `BallSpawningSystem` uses to spawn balls.

`BallSpawningSystem` instantiates an `ITemplate` (that is defined in `snapshot.json` and `templates.json`) which is defined as a ball.

`MapBoundsData` defines the size of the map and is only contained on the `GlobalEntity`.

`MovementData` defines a velocity for an entity so that it can move around the map.

`MovementSystem` takes entities with both `PositionData` and `MovementData` and moves them.

`PaddleCollectionSystem` merely collects all entities that have `PaddleData`. This system is used by the `BallCollisionSystem`.

`PaddleData` tags entities so that the `PaddleCollectionSystem` will contain said entity,

`PositionData` defines the position of an entity on the map.

`TemporaryData` gives an entity a limited lifetime; after a certain number of ticks, the `TemporarySystem` will destroy it.

`TemporarySystem` destroys entities whose `TemporaryData` has reached 0. This system also demos input; `StopDestroyingInput` disables destroying entities and `StartDestroyingInput` enables destroying entities. Actually sending the input into the game is engine specific (as it depends on the input layer); in the XNA sample pressing the 1 key enables destruction and the 2 key disables it.