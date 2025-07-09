# MonoGame.Kernel2D.EntitySpritesheetLoader

## Overview

High-level API to load spritesheet assets from embedded resources.

## API Surface

Call `SpritesheetLoader.GetSpritesheetFromResources("Path.to.resource.name", content)`
to receive a fully constructed `Spritesheet` instance for the game project.

Example usage:
```csharp
// Resolves to Spritesheet
var myCharacter =
    SpritesheetLoader.GetSpritesheetFromResources("Player.PlayerCharacter", content);
// Using Spritesheet directly
Spritesheet myEnemy =
    SpritesheetLoader.GetSpritesheetFromResources("Enemies.Enemy1", content);
```
Refer to examples below on pathing structure.

## Design Goals
- Fully embedded metadata (.json) into the game project
- Fully MGCB-managed image data (.gif, .png, etc.)
- Unified by logical path only
- Zero leaks of inner system to end-user
- Texture and JSON path must match exactly 1:1 to ensure correspondence
- DTOs and domain models are decoupled fully cleanly
- No need for end-user to know about the underlying class structure
- No need for end-user to know about the underlying serialization format
- Texture loading directly from MGCB is fully supported and enforced
- Anchor type ensures assembly and namespace are correctly resolved without
reflection hacks

## Requirements
1. Spritesheets must be in either PNG or GIF format.
2. The spritemap must be embedded in the project with the build action
set to `Build Action: Embedded Resource`.
3. The spritesheet image must be located in the game-defined content folder so that
it is processed by MonoGame's content pipeline into the `.MGCB` package file.
4. The spritemap must be defined in a JSON file with the 
**_exact same name and relative path_** as the spritesheet image, but in the project's
`Resources` folder.
- Examples:
  - Player character:
    - Spritesheet: `Content/Player/PlayerCharacter.png`
    - Spritemap: `Resources/Player/PlayerCharacter.json`
    - Path to resource name: `Player.PlayerCharacter`
  - Enemy character:
    - Spritesheet: `Content/Enemies/Enemy1.gif`
    - Spritemap: `Resources/Enemies/Enemy1.json`
    - Path to resource name: `Enemies.Enemy1`
5. The JSON file must contain a valid spritemap definition with the following structure:
```json
{
    "Name": "myspritesheet",
    "Animations": [
        {
            "Name": "spritegroup1",
            "Loop": true,
            "Frames": [
                {
                    "Name": "sprite001",
                    "Frame": {
                        "X": 167,
                        "Y": 4,
                        "Width": 34,
                        "Height": 41
                    },
                    "Duration": 1000
                },
                {
                    "Name": "sprite002",
                    "Frame": {
                        "X": 240,
                        "Y": 4,
                        "Width": 34,
                        "Height": 41
                    },
                    "Duration": 166
                }
            ]
        },
        {
            "Name": "spritegroup2",
            "Loop": false,
            "Frames": [
                {
                    "Name": "sprite003",
                    "Frame": {
                        "X": 94,
                        "Y": 81,
                        "Width": 39,
                        "Height": 39
                    },
                    "Duration": 88
                }
            ]
        }
    ]
}
```

## Class flow
- `SpritesheetLoader.GetSpritesheetFromResources(...)`
  - `EntitySpritesheetLoader.LoadEntitySpritesheet<TAnchor>()`
    - `EmbeddedJsonLoader.LoadFromResource<T, TAnchor>()`
      - `JsonSerializer.Deserialize<T>()`
        - `SpriteMapTranslator.ConvertToDomainModel(...)`

# Current structure map
```
[Game Project]
     |
     |--- [Resources/Foo/Bar.json] -- Embedded
     |--- [Content/Foo/Bar.png] ----- MGCB-compiled
     |
     ---> SpritesheetLoader
           |
           ---> EntitySpritesheetLoader
                 |
                 ---> EmbeddedJsonLoader
                 ---> SpriteMapTranslator
                       |
                       ---> Spritesheet (domain model)
```

# TODO
- [ ] Level map loader using similar system?
- [ ] Visual spritesheet preview system for debugging?
- [ ] Menu sprites/elements loader using similar system?