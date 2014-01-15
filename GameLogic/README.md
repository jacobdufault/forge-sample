# Files

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