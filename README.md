# Unity-Scripts-Starter
A Unity project starter that contains:
- Language management
- Scene management
- Game data persistence
- Utilities class

## Language management
Package ```Scripts\Localization```  
Supports language loading into a dictionary to translate your application at runtime. (No need to restart to change language).

## Scene management
Package ```Scripts\Framework```  
Supports scene management in a stack to allow "back action" to the previous scene.

## Game data persistence
Package ```Scripts\Framework\Data```  
Handle save, load and reload process of your game data from a binary file persisted locally.

## Utils
- OnlyDebug.cs: attach the script to a gameobject in the editor and it will disappear when the project is not in debug mode.
- Preferences.cs: PlayerPrefs overlay.
