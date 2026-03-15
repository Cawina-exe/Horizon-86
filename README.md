# 🇵🇹  🇪🇺 Horizon-86

**Horizon-86** is a narrative-driven 3D exploration and puzzle game prototype that takes the player on a journey through Portugal's historical and technological development. Through interaction and problem-solving mechanics, the game explores the country's transition from joining the EEC in 1986 to modernity.

Developed as part of the **Video Games program at ETIC Algarve (24/26)**.

## 📖 Game Overview

In **Horizon-86**, players control a young explorer in a retro-futuristic setting where time and progress are fragmented. The goal is to overcome physical and temporal barriers to reconnect the country.

Each phase of the game represents a decade or a major milestone:
* **1986:** The start of the journey, focused on breaking down walls and opening borders.
* **1996:** Structural reconstruction, symbolized by repairing bridges through resource collection.
* **2006:** Digital Inovation and Economical crisis in Portugal (Ongoing).
* **2016:** The phase of digital innovation and expansion(Ongoing).
* **2026:** The present and the future(Ongoing).


## ✨ Key Features

### 🏃 Multi-State Movement
* Advanced locomotion system with walking, sprinting, and jumping.
* Fluid animation transitions managed via an Animator Controller.

### 🧩 Interaction & Puzzle Solving
* Inventory system for collecting essential items such as keys and construction rocks.
* Environmental interaction mechanics (pressing 'E' to activate mechanisms and build structures).

### 🏗️ World Evolution
* Real-time environment changes, including sliding barriers and bridges that reconstruct upon delivering resources.

## 🧑‍🚀 Character

**Horizon Explorer**
* A visionary young woman with a cyberpunk aesthetic.
* Represents the innovation and future of Portugal and the EU.
* Uses tools and technology to resolve environmental puzzles.

## 🌍 Project Phases

| Phase | Year | Main Objective | Key Mechanic |
| :--- | :--- | :--- | :--- |
| 1 | 1986 | Open the Great Frontier | Key Collection / Wall Activation |
| 2 | 1996 | Rebuild the Bridge | 3-Rock Collection / Prefab Swapping |
| 3 | 2006 | Under Development | Digital Inovation and Crisis |
| 4 | 2016 | Under Development | Digital Inovation and stabilization |
| 5 | 2026 | Under Development |The present and the future |

## 🎮 Controls

### Keyboard & Mouse
| Action | Key |
| :--- | :--- |
| **Move** | WASD |
| **Sprint** | Left Shift |
| **Jump** | Space Bar |
| **Interact (Collect/Build)** | E |
| **Pause Menu** | ESC |

## 🧠 Core Mechanics
* **Trigger-Based Actions:** Global event activation through proximity and specific inputs.
* **Physics Interaction:** Use of Rigidbodies and Colliders for realistic movement and collisions.
* **State Management:** Animator Controller managing multiple Mixamo animation layers.

## 🎨 Visual Style
* **Voxel / Low-Poly** aesthetic.
* Retro-futuristic environment inspired by Portuguese architecture.
* Stylized character with fluid root-motion animations.

## 🛠️ Technologies Used
* **Unity 6 (6000.2.8f1)**
* **C# Scripting**
* **Mixamo** (Animation Library)
* **Voxel Art Tools**

## 📂 Project Structure
```text
Horizon86/
│
├── Assets/
│   ├── Scripts/
│   ├── Scenes/
│   ├── Prefabs/
│   ├── Characters/
│   ├── Environment/
│   └── Animations/
├── ProjectSettings/
└── README.md
