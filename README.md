XNATools
========

Overview

  After playing around with XNA for a short time, I decided I would begin developing tools to create a 2D XNA game. It is created in C# in Visual Studio using the XNA framework. Currently, the tools are as follows:
    -Embedding
    -MapEditor
    -Sprite Engine
    -Tile Engine
    -Test Client
    -Test Game
    
Embedding

  Embedding is a library that makes it simple to embed a control that work using XNA's functionaly (i.e. Graphics Device, SpriteBatch, Content Manager, etc.) into a winform.  To make a control, inherit from XNAControl and it is ready to be used on a winform.
  
MapEditor

  MapEditor is a 2D tiling editor. The goal is to create map "projects".  These projects will allow you to load one sprite sheet, build objects -combinations of smaller tiles into, say, a 4 tile rock for reuse - for that sprite sheet, build a map with the objects, and save the object for use in any game.  It makes use of embedding for the GUI and TileEngine for the functionality.
  
SpriteEngine
  
  Sprite is a library to manage sprites.  There are different types of Sprite Classes to Inherit from depending on what the sprite is supposed to do.  Each game will create specific Sprite classes for its needs, but inheriting these classes will allow the user to plug in a bit of information about the sprite sheet animations and be ready to go.  Example, a game would create sprite "Avatar" as its main character and inherit "UserControlledSprite" and set up it's walking animations on the sprite sheet, and then Avatar will work.
  
TileEngine

  TileEngine is a library that will create and show a tile map.
  
TestClient

  TestClient is used to test winform tools from the solution.
  
TestGame

  TestGame is used to test Game tools from the solution
  
