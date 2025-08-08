# Kernel2D (K2D) Developer Manual

> *Kernel2D (K2D)* is a modular 2D game framework built on top of MonoGame, intended to power small-to-medium games with rapid prototyping, scene management, sprite animation, input handling, and UI features. This document outlines its key systems and serves as a developer reference and onboarding guide.

---

## ✨ Features Overview

### ✅ Screen System

* Abstract base `ScreenBase` class defines the screen lifecycle: `LoadContent()`, `UnloadContent()`, `Update()`, `Draw()`.
* `ScreenManager` handles transitions between screens (via `FadeTransition`, `ScreenTransitionPair`, etc.).
* Built-in screen types: `GameScreen`, `MenuScreen`, `SettingsScreen`.
* Supports both hard switches ("smash cuts") and animated fade transitions.

### ✅ Input System

* Unified input abstraction layer:
  * `InputBridge` base class
  * `PlatformerInputBridge` (for character/platformer control)
  * `MenuInputBridge` (for menu navigation)
* Supports input polling, state tracking (Pressed, Released, Held, etc.), and dynamic binding (planned).

### ✅ Drawing & Render Queue

* `DrawContext` carries per-frame draw state (e.g., GameTime, GraphicsDevice, SpriteBatch, white pixel).
* `DrawQueue` is a draw command queue that collects render instructions.    
* Supported commands:
  * `SpriteDrawCommand` (2D sprites)
  * `TextDrawCommand` (text rendering)
* Final render is flushed with layer-based draw ordering.

### ✅ Animation System

* `AnimationPlayer` handles playback of `SpriteAnimation` assets.
* `AnimationSystem` manages multiple players and renders them as a batch.
* Animations are constructed from embedded JSON using `EntitySpritesheetLoader`.

### ✅ Spritesheet & JSON Asset Loading

* `Spritesheet` domain model encapsulates all `SpriteAnimation` instances and the `Texture2D` asset.
* `EntitySpritesheetLoader.LoadEntitySpritesheet<T>()` loads from embedded JSON (`.Resources` namespace) and associates the texture using the content pipeline.
* DTOs: `SpritesheetDTO`, `SpriteAnimationDTO`, `AnimationFrameDTO`.

---

## 🧩 Core Components

### Screen System

* **`ScreenBase`**: Abstract class; implement `LoadContent`, `Update`, and `Draw`.
* **`ScreenManager`**: Singleton that switches screens and applies transitions.
* **Transitions**:
  * `FadeTransition` (fade in/out)
  * `ScreenTransitionPair` (entry/exit combo)

### Input

* **`InputBridge`**: Base input polling abstraction.
* **`PlatformerInputBridge`**: Wires keyboard input for directional and action controls.
* **`MenuInputBridge`**: Meant for menu selections and navigation.

### Animation

* **`AnimationPlayer`**: Handles single animation playback.
* **`AnimationSystem`**: Optional helper that tracks multiple `AnimationPlayer` instances by key.
* **`SpriteAnimation` / `AnimationFrame`**: Represent individual animation logic and frames.

### Drawing

* **`DrawContext`**:
  * Holds references to `DrawQueue`, transform matrix, `GameTime`, `GraphicsDevice`,
  and white pixel.
  * Passed down to all draw calls for consistency.

* **`DrawQueue`**:
  * Collects `IDrawCommand` instances.
  * Flushes each frame to draw sorted by layer via a `SpriteBatch` injection.

### Spritesheets

* **`Spritesheet`**: Holds animation data and its `Texture2D`.
* **`EntitySpritesheetLoader`**: Loads embedded JSON + texture via ContentManager.
* **Embedded Resource Format**:
  * Uses embedded JSON structured as DTOs
  * Rectangle frame definitions are loaded and mapped into MonoGame types
  * JSON resource and relative image texture path must be the same for the loader
  to work
  * JSON format to construct mobile sprite entities:
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

---

## 🔧 Suggested Project Structure

```
Solution/
├── Kernel2D/                           # Core engine
│   ├── Animation/
│   ├── Drawing/
│   ├── Input/
│   ├── Screens/
│   ├── Helpers/
│   ├── Physics/
│   └── Resources/
└── YourGameProject/                    # Game-specific logic
    ├── Core/
    │   ├── GameEntities/
    │   └── Screens/
    ├── Content/
    │   ├── Fonts/
    │   ├── Player/
    │   │   └── playerspritesheet.gif   # Texure content file
    │   └── Enemies/
    │       ├── enemyspritesheet1.gif   # Texture file, must match resource path
    |       └── enemyspritesheet2.gif   # Texture file, must match resource path
    └── Resources/
        ├── Player/
        │   └── playerspritesheet.json  # Sprite-mapping file, must match texture path
        └── Enemies/
            ├── enemyspritesheet1.json  # Sprite-mapping file, must match texture path
            └── enemyspritesheet2.json  # Sprite-mapping file, must match texture path
```

---

## 📈 Coming Soon / In Progress

* Settings system with persistence
* Dynamic input rebinding
* Textbox and basic UI widgets
* Tiled map loading & collisions
* Save/load config to filesystem
* Expanded draw command types (shapes, borders, icons)
* Sound management (hopefully not too far into the future)

---

## 🧠 Developer Notes

* Code heavily favors explicit over implicit to support modifiability.
* DrawContext + Queue aims to decouple logic and drawing for more testable code.
* Animation and input bridges are designed to be swappable for future genres
(e.g., VN, puzzle, etc.)
* SpriteBatch use is centralized; no rogue `Draw()` calls allowed outside the queue.

---

## 📝 See Also

* [`EntitySpritesheetLoader.md`](./EntitySpritesheetLoader.md)
* [MonoGame Content Pipeline](https://docs.monogame.net/articles/tools/mgcb_editor.html)
* [System.Text.Json Documentation](https://learn.microsoft.com/en-us/dotnet/api/system.text.json)

---

*This file will grow and evolve with Kernel2D as more systems and utilities are added.*
