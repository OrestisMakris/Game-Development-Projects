# Game Development Projects (CEID_NE565) 2024–2025

**Course Website:** https://eclass.upatras.gr/courses/CEID1417/  
**Instructor:** Prof. K. Tsichlas  
**Authors:** Orestis Antonis Makris, Grigoris Delimpaltadakis

**Grading**: Grade: 10  Top 1% (minimum passing grade 5)

This repository contains the Unity exercises for the Game Development University Course (CEID_NE565), covering core Unity workflows, physics, and shader programming.

---

## Table of Contents

- [Overview](#overview)  
- [Prerequisites](#prerequisites)  
- [Setup & Usage](#setup--usage)  
- [Assignments](#assignments)  
  - [Exercise 1: Unity World Extension](#exercise-1-unity-world-extension)  
  - [Exercise 2: 2D Platformer Extension](#exercise-2-2d-platformer-extension)  
  - [Exercise 3: Complete Game Project (Expanded)](#exercise-3-complete-game-project-expanded)  
- [Technologies](#technologies)  
- [License](#license)  

---

## Overview

The goal of these exercises is to build hands-on experience with Unity, C#, ShaderLab, and HLSL. Each assignment comes with a PDF “ekfonisi” document describing the tasks and a Unity project template or asset archive to download.

---

## Prerequisites

- Unity 2023.3 LTS (or newer)  
- Git  
- A modern code editor (Visual Studio, Rider, or VS Code)  

---

## Setup & Usage

1. Clone this repository  
   ```bash
   git clone https://github.com/OrestisMakris/Game-Development-Projects.git
   cd Game-Development-Projects
   ```
2. Open the relevant `Exercise N` folder in Unity Hub.  
3. Import the provided `.rar` or project template as instructed in each PDF.  
4. Build & play to test your implementations.

---

## Assignments

### Exercise 1: Unity World Extension

**PDF Instructions:** [Unity_Exercise_01.pdf](Askisi%201/Unity_Exercise_01.pdf)

![Picture1](path/to/Picture1.png)
![Picture2](path/to/Picture2.png)

**Summary:**  
- Create a simple 3D scene  
- Place environment objects (ground, obstacles)  
- Implement a player‐controlled character using C#  
- Handle basic input (move, jump) and camera follow  

In this exercise we extended the Unity world we developed in Lab 3. Be mindful of Unity version differences—you may need to install or switch to a compatible Unity version to open the project.

Required tasks completed:  
1. **Added Enemy Health:**  
   - Extend the enemy health system so that each enemy requires multiple hits to be defeated.  
   - Hint: Add a new `health` variable to each enemy and decrement it on every successful hit.  
2. **Added Jumping Ability:**  
   - Give the player character the ability to jump by pressing a key of your choice.  
   - Use `CharacterController.IsGrounded()` or a similar check to ensure the player can only jump when on the ground.  
   - *Bonus:* Allow one additional mid-air (double) jump before landing.  
3. **Improved the Enemy AI:**  
   - The current Wandering AI moves enemies randomly. Improve it so enemies actively chase the player.  
   - Possible approach 1: Use a wide secondary raycast. If it detects the player, switch the enemy’s behavior to move toward them.  
   - Possible approach 2: Implement a field‐of‐view (FoV) check using mathematics from Lecture 4. Assume the enemy has a vision cone of angle θ and range r.  
   - Visualize each enemy’s FoV in the game (e.g., a faint red overlay).  

**Project Folder:** `Exercise 1/Code`  

---

### Exercise 2: 2D Platformer Extension

**PDF Instructions:** [2η ασκηση εργαστηριου.pdf](Askisi%202/2η%20ασκηση%20εργαστηριου.pdf)

**Images:**
![ss](path/to/ss.png)

**Summary**  
- Developed a 2d Game
- Experiment with physics (Rigidbody, Colliders)  
- Trigger zones and collision events  


In this exercise we extended the 2D platformer from Lab 5:

1. **Platform Drop-Through:**  
   - Allow the player to drop down through one-way platforms by pressing a key (e.g., Down Arrow or S).  
   - Hint: Use `Physics2D.IgnoreCollision` to temporarily disable collision with the platform.  
2. **Level Expansion:**  
   - Add new level elements:  
     - **Spikes** or gaps that damage or kill the player.  
     - **Moving platforms** that the player must ride to progress.  
     - A **trampoline** that forces continuous bouncing when stepped on.  
     - A **goal object** (e.g., a flag) that shows a victory message when reached.  
   - Ensure these elements function correctly and do not block game progression.  
3. **Enemies with Different AI:**  
   - **Patroller:** Moves left and right along a fixed path.  
   - **Ranged Attacker:**  
     - Detects the player (via Raycast or FoV).  
     - Shoots projectiles (e.g., fireballs) toward the player.  
     - Implement a simple 2–3 frame attack animation.  
   - You may recolor the player’s sprites and add frames for enemy animations.  
4. **Failure Counter System:**  
   - Track how many times the player has died. Save this count in `PlayerPrefs` so it persists across sessions.  
   - The player dies when:  
     - Hit by an enemy.  
     - Falling into spikes or a pit.  
   - Display the death count in the UI and restart the level cleanly on death.  
5. **Pause Menu & UI:**  
   - **Pause Menu** (triggered by Escape or P):  
     - “Restart” button that resets the level and death counter.  
     - Difficulty options (Easy / Medium / Hard) to adjust enemy and projectile speeds.  
     - “Continue” button to resume play.  
   - **Death Counter UI:** Show the current number of failures on screen.  

**Project Folder:** `Exercise 2/Code`  

---

### Exercise 3: Complete Game Project (Expanded)

**PDF Instructions:** [`Exercise 3/ekfonisi.pdf`](Askisi%203/3η%20ασκηση%20εργαστηριου.pdf)  
**Download Assets:** [Lab_10.rar](https://www.ceid.upatras.gr/webpages/faculty/ktsichlas/Unity/Lab_10.rar)  

**Summary:**  
- Advanced lighting setups  
- Create and apply custom shaders (ShaderLab + HLSL)  
- Post‐processing effects  
- Optimize material performance  

**Core Requirements:**  
1. **Build a Complete Game**  
   - Implement scene management for at least three distinct levels.  
   - Define a clear win condition per level (e.g., defeat all enemies, collect items, reach exit).  
   - Gate level progression using enemies or collectibles.  
2. **Smooth Door Animations**  
   - Integrate DOTween (or a similar tweening library) for smooth door open/close animations.  
3. **Walk/Run Toggle**  
   - Allow switching between walking and running (e.g., via Shift or Caps Lock).  
   - Update both movement speed and animation accordingly.  
4. **Player Abilities with Cooldowns**  
   - **Projectile Attack:** Face the mouse cursor and shoot damaging projectiles.  
   - **Shield:** Become invulnerable for a short time or until the next hit.  
   - **Dash:** Burst forward toward the mouse direction faster than running.  
   - Implement a cooldown system (e.g., shield every 5 seconds).  
5. **Audio & Settings Menu**  
   - Looping background music per level.  
   - At least two sound effects (e.g., ability use, enemy hit).  
   - Settings UI to adjust music and SFX volumes independently.  
   - Use CC0 (public domain) audio assets (e.g., from freesound.org).  

**Bonus & Evaluation Criteria:**  
- Code modularity and reusability  
- Visual polish (textures, particles, UI)  
- Documentation: controls table listing all keybindings  
- Creative enhancements earn extra credit  

---

## Technologies

- Unity (C# scripting)  
- ShaderLab & HLSL  
- DOTween (tweening)  
- Git for version control  

---

## License

This work is licensed under [MIT License](LICENSE).  
Feel free to use and adapt these exercises for personal learning or teaching.  
