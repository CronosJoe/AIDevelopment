AI Development Chase Game designed by Joseph Huskey.

This game was run against Unity3D version 2019.3.6f1 and Visual studio 2019.

In this folder you should find a windows version of an exe for the project there is a lot that needs to be done right now it just spawns in the grid and the enemy, a player and a non intractable obstacle and shows the steering behaviors. To build your own version open up the project in unity>build settings, here confirm the projects scene has been added into scenes in build, select your target platform and hit Build to make an exe, or build and run if you'd like to test it after building.

This is an incomplete chase game designed by me! So here is the list of things that you're looking for and what I need to complete.

AI using a pathfinding system,

I have begun re-adding A* back in to find a route around obstacles, this currently does not work but A* is in the code to correctly return a list of tiles from point a to point b.

AI using a suitable steering behavior,


This works! The AI flees when the player gets too close and seeks when the player gets too far away, just need to add in the attack and make something happen if the enemy is caught.

