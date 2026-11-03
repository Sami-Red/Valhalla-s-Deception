# Valhalla's Deception

A 2D action-platformer with hack-and-slash combat, developed in **C# with MonoGame** as a university group project for the **Game Development Group Project 1** module.

In *Valhalla's Deception*, you play as **Ragnar**, a Viking god betrayed by other gods. With his family in danger and his throne taken from him, Ragnar must fight through hostile realms, confront the gods who betrayed him, and reclaim what was lost.

The project combines side-scrolling platforming, melee combat, enemy encounters, hazards, boss fights and story sequences.

## Game Overview

- **Genre:** 2D action-platformer / hack-and-slash
- **Technology:** C# and MonoGame
- **Resolution:** 1280 × 720
- **Visual style:** 2D sprite-based characters, environments and animations
- **Structure:** Three themed realms, story sequences, boss encounters and credits

## Gameplay

Players guide Ragnar through platforming sections and combat encounters.

The player controller supports:

- Horizontal movement
- Jumping and gravity
- Directional sprite flipping
- Animated idle, walking, jumping, hurt and attack states
- Melee attacks with a defined attack area
- A three-point health system
- Temporary invulnerability after taking damage

Enemies patrol between defined positions, can damage the player on contact and can be defeated through attacks. Levels also include environmental hazards such as spikes, alongside ladders and platform geometry.

A health HUD displays the player's remaining health during gameplay.

## Realms and Progression

The game is organised around three themed playable realms:

1. **Hell** — The opening combat and platforming realm, featuring a layered environment and enemy patrols.
2. **Earth** — A further platforming area with enemies and a boss encounter.
3. **Valhalla** — The final themed realm, featuring additional enemy encounters and a boss fight.

A scene manager controls progression between the menu, introduction, levels, story sequences, boss encounters and credits. The story is presented through illustrated cutscene sequences between sections.

## My Contribution

**Role: Project Manager and 2D Level Artist**

I contributed to the project as part of a seven-person development team. My work focused on project coordination and creating 2D pixel-art environment assets for the levels.

My environment-art contributions included:

- **32 × 32 pixel tiles:** Creating environmental tiles used to construct level spaces, including wall and road elements.
- **Hazard tiles:** Producing visual tiles for level hazards such as poison blocks and spikes.
- **Environmental props:** Creating torch sprites to add detail and atmosphere to the environments.
- **Level visual consistency:** Designing assets to work within the project's tile-based environment and shared pixel-art style.

Alongside the art work, I took on the project manager role, helping coordinate the group project and its development.

The game and its other systems were developed collaboratively. The contributions listed above describe my areas of responsibility rather than claiming sole authorship of the entire game.

## Development and Technical Structure

The game is built around MonoGame's game loop and uses a scene-based structure.

Key systems visible in the source include:

- **Game initialisation and rendering:** The main game class configures the display, loads content and delegates update and draw calls.
- **Scene management:** A scene manager loads and transitions between the menu, story sequences, levels, boss encounters and credits.
- **Reusable level logic:** A shared level class handles player updates, tile-based collision checks, hazards, enemies, background drawing and the HUD.
- **Player and enemy entities:** Animated game objects manage movement, animation states, health and combat interactions.
- **Camera and backgrounds:** A camera transform follows the player, while layered backgrounds use different scroll speeds to create a parallax effect.
- **Content-driven environments:** Level layout textures are read to place tiles and hazards, allowing level geometry to be constructed from a layout map.

The source code is organised into individual classes for the player, enemies, game objects, levels, camera, scene management, animation and background handling.

## Controls

| Input | Action |
|---|---|
| A | Move left |
| D | Move right |
| Space | Jump |
| Right Shift | Attack |
| Enter | Start from the main menu |

## Repository Structure

```text
Valhalla-s-Deception/
├── README.md
└── Valhalla's Deception/
    ├── Game1.cs
    ├── Player.cs
    ├── Enemy.cs
    ├── Level.cs
    ├── GameObject.cs
    ├── AnimatedObject.cs
    ├── Camera.cs
    ├── SceneManager.cs
    ├── BackgroundManager/
    └── Scenes/
        ├── Menu.cs
        ├── Intro.cs
        ├── HellLevel.cs
        ├── EarthLevel.cs
        ├── ValhallaLevel.cs
        ├── CutScene2.cs
        ├── CutScene3.cs
        ├── CutScene4.cs
        └── Credits.cs
```

## Building and Running

This repository contains the C# source files. The checked-in repository does not currently provide a complete, documented build setup or a packaged release, so the exact build steps and required dependency versions are not specified here.

The project was developed using MonoGame. To run it locally, you will need a compatible C# development environment and the MonoGame framework, together with the project's game content assets. The source references a MonoGame content directory named `Content`.

## Project Context

This was a collaborative university group project completed for the **Game Development Group Project 1** module. It provided experience working as part of a multi-person game development team, coordinating project work and producing environment art for a 2D platform game.

## Links

- [GitHub Repository](https://github.com/Sami-Red/Valhalla-s-Deception)

---

*Valhalla's Deception — University group game development project.*
