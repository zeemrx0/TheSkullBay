# TheSkullBay

# Introduction

The Skull Bay is a casual game where the player controls a pirate ship attacking to steal loots of other boats.

## How to play

- **WASD** – Move
- **1** – Shoot cannon
- **Left mouse** – Confirm shooting

## Features

- Physics-based Movement with Rigidbody
- Physics-based Projectile: Apply Kinematic equations and Newton’s Law of Inertia
- AI Behaviors:
  - Spawn in a chosen area
  - Physics-based auto rotation to target position
  - Patrol in a chosen area
  - Drop loot on death
- Combat system:
  - Health system
  - Ability system with Targeting strategy and Effect strategy
- Hunger system: Currently behaves as a time limitation of a level.

# Tech stack

- Game engine: Unity 2022.3.19f1
- Language: C#
- Architectures and Design Patterns:
  - Model – View – Presenter (MVP)
  - Entity – Component – System (ESC – Unity default)
  - Strategy Pattern
- Packages:
  - Zenject 9.2.0: Dependency Injection

## MVP and Dependency Injection with Zenject

As an addition to the original MVP, there are Scriptable Objects called Data. They save the settings, and constants of the system. E.g. Boat max movement speed, Ability aim range, etc.

Compared to Data, the Model is used to represent states and snapshots of data.

![MVP and Dependency Injection with Zenject](https://file.notion.so/f/f/4e680220-0ac4-48a7-81c7-963c237151f1/84a0313b-b189-4e86-98af-02bc672e4764/Screenshot_2024-02-27_at_10.31.51.png?id=f68f3efb-be77-4ade-bac0-192603f04ef6&table=block&spaceId=4e680220-0ac4-48a7-81c7-963c237151f1&expirationTimestamp=1709164800000&signature=mr4JFHl_gu-xYGLCTevTRQVabLUBHLKanu-NBjbTu68&downloadName=Screenshot+2024-02-27+at+10.31.51.png)

# Apply Strategy Pattern to Ability System

![Apply Strategy Pattern to Ability System](https://file.notion.so/f/f/4e680220-0ac4-48a7-81c7-963c237151f1/8b568369-1ce3-4d71-944f-fef97daa7f59/Screenshot_2024-02-27_at_11.32.03.png?id=08c35b32-1d79-4d70-b082-972441e05e9d&table=block&spaceId=4e680220-0ac4-48a7-81c7-963c237151f1&expirationTimestamp=1709164800000&signature=BONwCBG7LFw1FNi6K-d3nVhi7ObcGvWUtRgQBVrAEjQ&downloadName=Screenshot+2024-02-27+at+11.32.03.png)
