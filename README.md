# SnapperSolver #
A solver for the game [Snappers](https://play.google.com/store/apps/details?id=com.emerginggames.snappers&hl=en), including a level editor, running version of the game and history of previously played games.

### Create, solve & play ###
The game features a simple level editor that can be used to add or remove various types of blocks. After creating a level, you can play the game manually, or run a solver. The solver requires you to select the maximum number of moves, after which it attempts to calculate possible solutions. The solver returns solutions in row|column format, so '12 34' means first move is in the first row, second column, and the second move is third row, fourth column.
![Level editor](https://raw.githubusercontent.com/tinc2k/snapperSolver/master/docs/levelEditor.gif)

### Load from history ###
Every level you create or play gets saved in your history. Levels are also coded inside URLs, so by sharing a URL between devices, you're effectively copying the entire level data.
![History](https://raw.githubusercontent.com/tinc2k/snapperSolver/master/docs/history.gif)

## Tech ##
The game, it's level editor and game history were build using JavaScript, HTML5 and CSS3. The solver is written in C#, and the whole thing is glued together with some ASP.NET MVC4. Displayed history of previously played games uses HTML5 [Web Storage](http://www.w3.org/TR/webstorage/), so be sure to use a browser that supports the feature. The solution can be compiled in Visual Studio 2012 or newer and used via it's Development Server, or deployed on Azure and similar hosting platforms.

## Code quality ##
This is a *quick-and-dirty*, *hackathon-quality* application written for [Clarity Consulting](www.claritycon.com)'s first Tech Challenge for Offshore Teams. 

## Brought to you by communism ##
Copyright (C) 2013. Tin Crnkovic (tinc2k@gmail.com) and Ines Jankovic (injankovic@gmail.com)

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program. If not, see http://www.gnu.org/licenses/.
