AI Development Chase Game designed by Joseph Huskey.

This game was run against Unity3D version 2019.3.6f1 and Visual studio 2019.

In this folder you should find a windows version of an exe for the project there is a lot that needs to be done right now it just spawns in the grid and the enemy, a player and a non intractable obstacle and shows the steering behaviors. To build your own version open up the project in unity>build settings, here confirm the projects scene has been added into scenes in build, select your target platform and hit Build to make an exe, or build and run if you'd like to test it after building.

This is a chase game designed by me! So here is the list of things you should know

Basic controls to move, w forward, s backwards, a to the left, and f to move to the right, plus an addition of the space bar for some more mobility.

The enemy uses some A* pathfinding to move around the obstacles that it spawns and shifts between fleeing/pursuing to spawn obstacles for the player.

The player loses if they touch an obstacle and the player wins if they touch the enemy. 
At any time the player can quit the game by hitting the escape key.

