# TrapSweeper
Maze emulator which supports any Maze Vendor generator implementing IMazeIntegration.

## Technology
* .NET C#
* AutoFac to load IMazeIntegration modules
* Moq framework for unit testing mocking
* Resharper
* Visual Studio Static Analysis

## Phase 1
* Full working skeleton for navigating through mazes till hunter dies or find the treasure.
* Testing coverage of emulator exit points
** Find a Treasure
** Die from Traps

## Phase 2
* Store visited rooms in Set DS and color edges as red when drawing the room
* Add a generator to be able to fully test the emulator
* More testing coverage 
