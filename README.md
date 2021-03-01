# CPSC565A2
CPSC 565 Assignment 2

UNITY VERSION 2020.2.0f1

Introduction

This project models the game of quidditch. The snitch moves around the play area randomly while the players move due to forces attracting or repelling them from the snitch, other players, and play area boundaries. A point is awarded each time a team's player collides with the snitch and an additional point is awarded if that team also scored the last point.

Camera Controls

The camera can shift its view angle by left clicking the mouse and dragging across the screen. To move forwards, backwards or side to side the WASD keys may be used. There is also a TimeControl script attached to the main camera that allows the game to be sped up or slowed down by using the slider under TimeControl script in the inspector.

Snitch

The snitch moves around the area randomly and also feels a force pushing it away from the walls.

Players

There are two teams, Griffindor and Slytherin, each represented by the GriffindorPlayer and SlytherinPlayer prefab pieces. The bahaviour of these pieces are determined by the scripts GriffindorPlayer and SlytherinPlayer. The players are all initialized by the PlayerGenerator script. Relevant player information can be seen in the inspector listed inside the section for the associated script. However the player mass will be visible in the Rigidbody component.

PlayerGenerator Script

This script is referenced by the empty Instantiator object to create the players at the beginning of the game from the SlytherinPlayer and GriffindorPlayer prefrabs. It contains all the values for the player traits and selects randomly from the given distribution when creating a new player. The number of players on each team can be modified within this script by changing the value of the variable numPlayers.

Player Movement

Players feel an attractive force to the snitch and are repelled by walls, teammates and opponents. When not unconscious, player exhaustion is incremented every 100 frames. When a player would exceed maximum exhaustion they instead are limited to 1/8th of their maximum velocity for 15 seconds and decrement their exhaustion by one every 100 frames.

Player Collisions

Player collisions are handled by the equations given in the assignment description to determine whether unconciousness occurrs. The OnColissionEnter function in both the SlytherinPlayer and GriffindorPlayer scripts determine the result of a collision. Since it is run by both parties of a colission, when teamates collide they each have a 2.5% chance of going unconscious for a total of 5% chance per collision. For collisions between opponents this is handled entirely from the SlytherinPlayer script to avoid redundancy. The SnitchBehaviour script handles collisions with the snitch.

Unconsciousness

An unconscious player will fall to the ground and remain there for 10 seconds. Their exhaustion will be set to 0 upon regaining consciousness.

Additional Player Traits

The two additional player traits added are blocking and violent. For the blocking trait, each team member that is not closest to the snitch will target the closest opponent to the snitch and move in its direction to hopefully initiate a collision. This would allow that team's closest member to the snitch a little more room and freedom from colliding with the other team's closest player. The violent player trait will cause a team's players to almost entirely ignore the snitch and instead move towards the other teams nearest player to attempt to initiate a collision. These player traits are defaulted to off by can be turned on by changing the violent and blocking boolean values to true inside the SlytherinPlayer or GriffindorPlayer scripts.

Scoreboard UI

The scoreboard is implemented via text displayed to the screen via the text UI components with an attached script to access the score which is held within the snitch.


References: 

Unity documentation https://docs.unity3d.com/Manual/index.html

The SampleGaussian function used in PlayerGenerator was taken from https://gist.github.com/tansey/1444070

Camera control script taken from https://gist.github.com/McFunkypants/5a9dad582461cb8d9de3

