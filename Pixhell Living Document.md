# Team Info \- Pixhell

## Names and Roles:

Joshua Knowles \- SCM, Deadline Management, Upgrade Design and Development, Programmer  
Brendan Laus \- Item Design, Map Design, Sound Design, Programmer  
Max Russell \- Sound Manager, Programmer  
James Osborn \- Enemy Design, Menu Mechanics and Transitions, Programmer  
Tanush Ojha \- Art Manager, Movement, Enemy Design, Programmer  
Kiet Bui \- Character Design, Art Design, Sound Design, Programmer  
Chris Dutton \- Character Development, Programmer

## Links:

Github \- [https://github.com/CS362-Team12/Pixhell](https://github.com/CS362-Team12/Pixhell)

Software/Online:  
Piskel \- [www.piskelapp.com](http://www.piskelapp.com)  
Ableton \- [https://www.ableton.com/en/](https://www.ableton.com/en/)  
Free Sound \- [https://freesound.org/](https://freesound.org/)  
Audacity \- [https://www.audacityteam.org/download/](https://www.audacityteam.org/download/)   
Unity \- [https://unity.com/](https://unity.com/)  
VSCode \- [https://code.visualstudio.com/download](https://code.visualstudio.com/download) 

Management:  
Google Doc Version of this document \- [https://docs.google.com/document/d/1xVvEEf0EtcPMxhUlb37Tiand0Ma5XY8CIZj35kNzp0U/edit?usp=sharing](https://docs.google.com/document/d/1xVvEEf0EtcPMxhUlb37Tiand0Ma5XY8CIZj35kNzp0U/edit?usp=sharing)

Trello \- [https://trello.com/b/1FqZ28jJ/pt12pixhell](https://trello.com/b/1FqZ28jJ/pt12pixhell) 

## Communication: 

Primary \- Discord Server  
Secondary \- Email

### Rules:

* Prompt response (within 8 hours, excluding 12 am \- 8 am)  
* Whenever a member is stuck or confused, reach out for help  
* If you have an emergency, communicate that to the team as soon as possible so they are aware   
* Be respectful in all communications and keep communications professional

# Product Description

## Title \- Pixhell

## Abstract

Pixhell is a bullet-hell rogue-lite that challenges players to survive and conquer nine stages \- the nine circles of Hell \- combining fast-paced action and unique environments with a progression system that encourages experimentation across multiple runs. Players navigate through increasingly more difficult stages, collecting upgrades along the way to eventually defeat the Devil in the center of Hell. While similar to other bullet-hells, Pixhell focuses on creating intelligent enemy AI that feels like you’re battling against actual enemies instead of a swarm of enemies with no intelligence, which is unique within the genre. 

## Goal

We are creating a replayable rogue-lite bullet-hell game involving making it through the nine circles of Hell to eventually battle the Devil. The game will include meta-progression, different characters, weapons, and enemies. The goal is to provide users with an enjoyable experience of progression and discovery. We also aim to make our game stand out by making it more strategic than the average bullet-hell where a player can make intelligent decisions based on enemy AI decisions as opposed to just building the strongest possible character. That being said, we still plan on maintaining the satisfying  “strength through upgrades” part of bullet-hells. 

## Current Practice

There are many examples of games similar to ours that we are taking inspiration from, such as Hades and Vampire Survivors, which use many different game engines, one of which is Unity which we will be using. This allows us to skip some of the heavy lifting and let Unity manage it. However, it constrains us to the bounds of Unity which limits us within its jurisdiction, and we have to worry about the performance issues of running a game on computers with limited hardware. 

## Novelty

The thematic structure of combining a narrative like the Nine Circles of Hell with the action and tight mechanics of a bullet-hell roguelike creates a more immersive and stylistic experience. Navigating a symbolic descent while the difficulty escalates makes for a memorable gaming venture. Using the Nine Circles of Hell creates a new theme not seen before that creates a new experience for people playing our game, while still being able to pick up the game quickly with its similarities to other games. Our nine arena progression is also a novel concept, which allows us to create a lot of variety between individual arenas with novel enemy types and environments, while finally our three unique characters can be mixed and matched with different arenas and items to make each run in our game itself feel different from the next.   
Each arena additionally has unique mechanisms themed around their circle of hell, such as foggy vision for a bathhouse vibe (Lust) or larger, exploding enemies (Gluttony). This separates each arena itself into novel experiences, while also being novel from other games in of itself. These arenas will have intelligent AI that work with each other and are aware of their surroundings, making decisions based on their health, position, and the positions of others around them. This is unique from most bullet-hells, which usually just involves swarms of enemies that follow a very basic attack pattern. 

## Effects

Games are a huge source of fun and a way for people to escape from their regular lives for a few hours at a time. We hope to make it so people can delve into the world of Pixhell and lose themselves in enjoyment and even learn a little bit about Dante's circles of hell. Games like Pixhell can also create friendly competition within social groups which creates stronger connections with other people as games are often a way that people bond. 

## Use Cases (Functional Requirements)

1.  Joshua Knowles  
   * Actors: Player  
   * Triggers: Left clicking while controlling the character  
   * Preconditions:   
      * The player has entered the game  
      * The player is not in a menu  
      * The player is not in a loading zone  
      * The player is not currently on their attack cooldown  
         * The player can not attack on every frame  
   * Postconditions:   
      * A player attack animation is played  
      * Enemies in the range of the attack get damaged  
      * A attack cooldown is triggered where the player can not attack again until it ends  
   * List of Steps:   
      * The player enters the game and chooses a character to play  
      * The player left clicks once in control of their character  
         * The player can attack in the lobby. There are no enemies to attack, but the animation takes place.   
      * An attack has been performed, and enemy damage has been processed.   
      * The player may attack again when the attack cooldown has ended  
   * Extensions/Variations:   
      * Each character has a variation  
         * Specific characters may vary in setup. Specific character design is not fleshed out, so this section will be complete when it is  
   * Failure:   
      * The player tries to attack when not in control of the character \- Nothing happens  
      * The player tries to attack when on their attack cooldown \- Nothing Happens  
2. Brendan Laus  
   * Actors: Player, Map elements, and Sound system 
   * Triggers: 
      * The player character's hitbox colliding with a map selection trigger 
   * Preconditions: 
      * Completion of previous maps (if applicable) 
      * The player interacts with the corresponding portal 
   * Postconditions: 
      * The new map loads in with its dependencies
      * The character spawns in an appropriate location on the new map
      * The new map’s music begins to play
   * List of Steps: 
      * The player character will move toward the desired map trigger (portal)
      * The player character’s hitbox collides with the new map trigger
      * The game checks that the character is eligible to play on the new map
      * The new map gets loaded
      * Old and unnecessary assets get dropped while the map loads with its associated dependencies
      * The player spawns into the new map at a designated spawn point
      * The correct map music begins to play
   * Extensions/Variations: 
      * Each map will be accessible only through its corresponding portal
      * Each map will have different spawn points, visuals, and the proper soundtracks.
   * Failure: 
      * If the player is ineligible to go to a given level, the portal for that level will not teleport the player at all

3.  Max Russell  
   * Actors: Player, Sound System  
   * Triggers: The player enters a new stage or enters a boss fight.  
   * Preconditions: The stage or boss fight has been loaded and the sound system is already initialized and functioning.  
   * Postconditions: The background music changes based on the specific event triggered or new location entered.  
   * List of Steps:   
      * The game detects the player has entered a new stage or has transitioned into a boss fight.  
      * The sound system prepares the corresponding music for the new encounter.  
      * The previous music stops and is replaced with the new track.  
      * The player hears the matching music integrated with what they are experiencing in terms of gameplay (boss music, lobby music).  
   * Extensions/Variations:   
      * In the main menu, a unique theme track plays.  
      * The music is intensified (faster BPM) if the player is critically low on health.
   * Failure:   
      * A file might be corrupted or missing entirely meaning there is just silence during gameplay. This could be avoided by having a default track to play in the event that there is not one already set)  
      * Tracks might lag or stutter. This could be avoided by pre-loading music assets ahead of time.  
4.  James Osborn  
   The player moves from the first level to the second.  
   * Actors: Player, Shopkeeper  
   * Triggers: The player completes the first level by defeating its final boss  
   * Preconditions: The player has obtained powerful enough items to complete the first level and has beaten it, and talks to the shopkeeper  
   * Postconditions: The player enters level two  
   * List of Steps:   
      * The player beats the final boss of the first arena.  
      * The player exits the arena and goes to the lobby.  
      * The player walks over to the shopkeeper to obtain the key to arena two’s portal.  
      * The player uses the key to open the portal.  
      * The player enters the portal to start arena two.  
   * Extensions/Variations:  
      * If the player has already picked up the key from the shopkeeper previously, they can just walk straight over to the portal and open it.  
      * If the player has already opened the portal, they can enter the portal immediately without talking to the shopkeeper or opening the portal.  
   * Failure:   
      * The player attempts to get the key from the shopkeeper without completing arena one.  
      * The player attempts to use the key on the wrong portal.  
5.  Tanush Ojha  
   * Actors: Player, Enemies  
   * Triggers: The player collects enough EXP to level up  
   * Preconditions: The player is in an arena, the player has killed enough enemies to level up, enemies drop EXP  
   * Postconditions: The player gets to choose a level-up reward, level-up reward is correctly applied and works, EXP count resets, MAX exp increases  
   * List of Steps:   
      * Player levels up  
      * Game ‘pauses’ and level-up screen comes on  
      * Player selects the level up reward  
      * Level-up screen disappears and game is ‘unpaused’  
      * Player can use level up reward, or is correctly applied  
      * Level-up reward is reset on level fail or completion (upon level exit)  
   * Extensions/Variations:   
      * Player can get level up reward twice for additional boost, or a extension of the skill previously gained  
      * If player achieves all level up rewards, only money is gained  
   * Failure:   
      * Player doesn’t choose any level up reward  
      * Levelup screen pops up before level up  
      * Reward is not correctly given to player  
6.  Kiet Bui  
   * Actors: Player
   * Triggers: Pressing ability button
   * Preconditions: 
      * Player character is loaded
      * Ability is off cooldown
   * Postconditions: Ability is used
   * List of Steps:   
      * Player press an ability button
      * Ability is triggered depends on the class
      * Ability buff, debuff, or damage and enemy
      * Ability is on cooldown
   * Extensions/Variations:  
      * Ultimate where you need to charge it instead of time based
   * Failure:   
      * Player tried to use an ability but it is on cooldown, nothing happens.  
7.  Chris Dutton  
   * Actors: Player  
   * Triggers: Hitting shift while controlling the character  
   * Preconditions:  
      * The player is in a level  
      * The player is not in a menu  
      * The player is not in a loading zone  
      * The dodge cooldown is not currently active  
   * Postconditions:  
      * A dodge roll animation is played out  
      * Invincibility frames are gained for half the duration of the roll animation  
      * A dodge roll cool down is activated for 1 second to ensure roll is not able to  constantly abused for invincible frames  
   * List of Steps:  
      * The player enters a level  
      * The player shift clicks when they have control of their character  
      * The dodge roll is initiated and invincible frames are given to the player  
      * A cooldown is applied to the shift ability   
   * Extensions/Variations:  
      * The activation of the animation allows for the player to jump over small projectiles before invincibility frames are activated   
   * Failure:  
      * The player attempts to use the dodge roll ability while it is on cooldown and it does not go off  
      * The player presses shift while not in control of the menu and nothing happens

## Non-functional Requirement

1. Usability:  
   1. Menus and interfaces should be simple, clear, and easy for users to navigate to make sure players do not spend too much time on non-gameplay actions.  
2. Performance  
   1. The game will easily run at 60 FPS on any modern laptop or desktop and not dip below half of that, even during intense bullet-hell sequences with many projectiles on screen at once.  
3. Modularity/Maintainability  
   1. Core systems of the game (enemy generation and behaviors, item/upgrade mechanics, etc.) must be modular so that new additions of new features can be implemented seamlessly.

## External Requirements

1. Error Robustness  
   1. The game must be able to handle incorrect user input gracefully. Errors during the generation of each stage should be handled and not interrupt gameplay.  
2. Ease of Installation  
   1. The game will have a single executable easily able to be downloaded and installed and with one click it will run properly.  
3. Developer Documentation  
   1. We will have source files available and have a simple quick walkthrough guide on how to have unity build it into an executable. 

4. Project Scope  
   1. The game will consist of ten distinct levels (one for each circle and one for the center), a hub area, and a modular system for upgrades, enemies, and bosses on each stage to ensure scalability.

## Team Process Description

Toolset Justification:

* We chose Unity for its powerful 2D development capabilities and extensive library of assets, which will help streamline game development. Additionally, due to the lack of experience among the teammates on any single game development platform, Unity’s simple learning curve greatly benefits our team with our strict deadline, by bringing almost all aspects of game development to one place.

Team Roles Justification:  
	Joshua Knowles \- SCM, Deadline Management, Upgrade Design and Development, Programmer

* Every project needs an SCM to manage deadlines, version control, documentation, and overall keep the team connected and knowledgeable. Without the SCM, team members may not know what to do to be productive and work may be wasted if something worked on is no longer needed. 
* Having a specific person dedicated to managing deadlines also ensures that things will get turned in on time and that work is done based on its priority.
* Design and implement the upgrade system so that upgrades are balanced and provide variety to every run
* Design enemy baseplate, allowing for easy duplication to eliminate time waste
* Test and merge branches to eliminate as many bugs on main as possible and deal with conflicts between different branches. 


Brendan Laus \- Map Design and minor help with Sound Design

* Created custom tile palettes to then layer 2d tilemaps to provide physical depth and variety while avoiding potential texture meshing issues
* Created custom wall prefabs to enable proper collisions and logical map boundaries
* Researching Dante’s inferno to create and implement a design grounded in the themes and descriptions provided in his epic
* Utilized the descriptions above to fully create and organize the Limbo visuals (except for the portals and item shop) as well as all of the Lust visuals
* Aided Max on the creation and implementation of Gluttony
* Aiding others with general design on core gameplay elements, especially as they relate to both map design and potential items to be implemented.
* Each of the roles above will be important to this game, as maps will host the player and enemies at all times, so both texture size, design, and coloration will be important in creating a curated experience.



Max Russell \- Sound Manager, Programmer

* Develops audio elements throughout the game and assists in designing stages for thematic consistency.  
* Sound design and general stage appearance is important for the player’s enjoyment of any game and makes this game more memorable and unique.

James Osborn \- Enemy Design, Menu Mechanics and Transitions, Data Storage Coordinator, Item Creator, Programmer

* Will bring the core elements of the gameplay together through the menu and lobby, making the game playable.  
  * Includes a starting menu, run selection screen, item shop UI, pause menu, and win screen  
* Add variety through character items for replayability  
  * Includes both implementing inventory system for players and sprites and behaviors for items  
* Add enemy AI with unique attack patterns  
* Create link between arenas and portals  
  * Will include locking and unlocking portals depending on player progression  
* Program walls to keep players and enemies within the arena  
* Implement backend file storage system for all player data, allowing for multiple run saves at a time  
  * Will include a run selection screen where players can choose to add or delete run saves  
* Implement dynamic data storage system that continuously updates during gameplay

Tanush Ojha \- Art Manager, Movement, Enemy Design, Programmer

* Will source and create art, including basic character, boss, and enemy design, creating both models and small animations for everything. Art will create a depth of character in our game, enhancing the gameplay experience.  
* Create movement mechanics such as dashing, teleporting, and any other movement boosts we come up with in our game. Good movement mechanics will allow the user to come up with different techniques of movement, combining dashing with teleporting or running, adding a simple but fun additional aspect of difficulty and skill.  
* Creating boss mechanics, including weapon attacks, phases, and more will give the user an enhanced gameplay experience, having difficult bosses that take multiple tries to beat will give the user more time to learn and have fun with our game.

Kiet Bui \- Character Design, Art Design, Sound Design, Programmer 

* Unique characters will enhance the replayability and feel of the game  
* Create arts and music, which allows the user to experience what the world inside the game looks and sounds like.  
* Putting animation for characters and enemies gives the game life.

Chris Dutton \- Character Development, Programmer
* Create a polymorphic class that can allow for easy control of all unique characters added to the game.
* Develops three unique characters that fit into the overarching theme of the game
* Implement projectiles for both enemies and the characters
* Design hit boxes for characters that are fair and balanced
* Add Hit boxes for enemy and characters that allow for smooth detection of damage
* Create logic for health packs that the character can pick up from an enemy that has died
* Animated the warrior character.
* Designed the ability for a user to select and properly load each individual character


Schedule for each member:  
Design

* Week 1-3  
  * Finalize game concept, mechanics  
    * Design movement, UI  
    * Design basic attack mechanics, enemies, basic weapons  
  * Week 4-6  
    * Design the first 3 levels  
    * Design characters and effects  
    * Finalize level progression  
    * Use Case 2 (Map transition) and Use Case 6 (Level-up screen) are designed  
  * Week 7-8  
    * Design the remaining 6 levels  
    * Use Case 3 (Boss) is designed for each level  
    * Shopkeeper UI is designed  
  * Week 9  
    * All 9 levels are designed and tested

  Programming

  * Week 1-3  
    * Set up game engine  
    * Implement initial movement and functionality  
    * Use Case 1 (Left-click attack) works  
    * Base UI is implemented (main menu)  
    * Use Case 7 (Dash) works  
  * Week 4-6  
    * Program map transition (Use Case 2\)  
    * Implement a leveling system (Use Case 6\)  
    * Implement the first level with basic items and enemy AI  
    * Implement shopkeeper for opening new levels (Use Case 4\)  
    * Moving between levels (Use Case 5\) works  
  * Week 7-8  
    * Implement random spawning and enemy behavior  
    * Use Case 3 (Boss) works  
  * Week 9  
    * Final debugging and playtesting  
* Art  
  * Week 1-3  
    * Create initial character and environment art  
    * Find and implement basic animations for the player and basic enemies  
  * Week 4-6  
    * Create artwork for first 3 levels  
    * Create transition screen artwork  
    * Create lobby artwork  
    * Basic animations for weapons  
    * Create pixel art for shop items  
  * Week 7-8  
    * Create artwork for remaining 6 levels  
    * Implement art and animations for bosses and NPCs  
  * Week 9  
    * Finalize art for items, arenas, NPCs, players, and bosses  
* Sound  
  * Week 1-3  
    * Find base sounds for attacks and NPC interactions  
  * Week 4-6  
    * Design background music and basic sound effects for menus  
    * Design sound for leveling system  
  * Week 7-8  
    * Create sounds for bosses and minibosses  
  * Week 9  
    * Implement final boss music for each level  
    * Test and polish sounds across the game

	  
Feedback:

* External feedback will be most useful at the major stages of the development process. For example, once the character movement and basic stage design is created and playtesting can take place to some extent, insights into balancing, mechanics, and overall enjoyment of the game will be vital in adjusting later stages/core mechanics.

## Technical Approach

* Unity  
  * Unity is a free game engine that handles a lot of the heavy lifting of game creation for us, as well as having extensive documentation and tutorials for those who are less familiar with Unity.   
  * Unity has an asset store to help get placeholder assets (art), and in some cases permanent and free art fixtures in our project to help speed up the development process and get straight to the programming.   
* Github  
  * Git/Github will be used as our primary version control system to manage teamwork within our project. This allows us to create pull requests, create issues and task lists, and deal with merge conflicts when programming different portions of the game.   
  * We’ve found online tutorials on how to use Github together with Unity to ensure that the two mesh and don't cause issues.  
* C\#  
  * Enemy Ai, game logic (such as random selection of upgrades) will be programmed using C\#, Unity's main language.  
* VSCode  
  * Unity has built in plugin support to link with VsCode so VsCode will be our main IDE as we already have our own likes for Programming extensions.  
* Piskel  
  * Piskel will be our primary asset creation program, which is free on a web browser. Piskel will be used in conjunction with Unity’s asset store to get a combination of art from free open source areas, as well as creating our own art when we can’t find free assets that fit our requirements.  
* Ableton  
  * The background music for levels and boss fights will be made in Ableton, a music creation software, to make the game more immersive.  
* Freesound  
  * Some enemy sound effects, sword swipes, taking damage etc. will be found from Freesound.  
* Audacity  
  * Some sounds may also be recorded on our own with a microphone. Any sounds created in this way instead of found from Freesound will be edited with the free software Audacity, an audio editing software. 

## 

## Main Goals

* Eight arenas of increasing difficulty, with one mini-boss and a final boss. Beating the final boss of an arena unlocks the next one. The ninth and final arena is a single final boss  
* The lobby area provides progressive upgrades that make future runs easier by improving player abilities and unlocking new features  
* During each arena, temporary abilities are applied to the character to mix up gameplay  
* There are at least 3 distinct characters which each provide a unique and enjoyable gameplay experience

## Stretch Goals

* Art and effects match the theme and have continuity with one another, and are implemented smoothly  
* Implement sound design to make the game pop, both through sound effects and music  
* Add in-depth lore and story element

## Timeline

Week 1: Get the basics worked out on what our game should be and look like  
Week 2: Focus our design and development process. Fully develop our vision of the game  
Week 3: Work on player movement, camera movement and weapon play, Start menu.  
Week 4: Enemy development and weapon development  
Week 5: Hub design for the level Limbo and layout. Possibly completing the level Lust  
Week 6: Adding roguelite cards and probability to the game. Working on one to two levels  
Week 7: Sound design for player and some enemies and boss is done. Animation for enemies and design for enemies. Working on one to two levels  
Week 8: Working on one to two levels. Developing enemies for the levels. Sound design and general debugging as we playtest.  
Week 9: Mostly this week the game will roughly be done and the main focus for this will be the final levels and debugging as we go through and test more and more.  
Week 10: Publish the finished product.

# Project Architecture and Design  

## Software Architecture

**Functionality:**

1. Scenes  
   1. Using Unity’s built-in scenes to store permanent features like menu buttons, lobby design, and arenas  
      1. Includes scenes such as a main menu, run loading screen, lobby, and arenas  
2. UI Management  
   1. Manage the movement between scenes and menus through the use of interactable buttons  
   2. Display different HUDs or ‘heads-up display’  
3. Player Data Storage  
   1. Load and save player data to allow multiple gameplay saves  
      1. Data includes arena progress, items, level, and money  
      2. Using a “Runs” folder that stores text files within it with information such as current player arena, inventory items, and money  
         1. These get continually updated as the player progresses through the game  
4. Item Registry  
   1. Store item functionality in individual scripts for each item, with links to the asset art  
5. Assets  
   1. Visuals and animations for maps, characters, enemies, and UI  
      1. Will include character and enemy attack animations, pixel art, and item styling  
6. Game Logic  
   1. Calculates all instances of damage that a player or enemy does  
      1. Incorporates player upgrades and items  
   2. Updates the stats for the player  
   3. Update player item inventory and shop inventory

**Interfacing:**

| Function | Interface | How |
| :---- | :---- | :---- |
| Scenes | UI Management | Scenes are moved between using buttons on the UI, which are controlled by UI management. |
| UI Management | Scenes, Assets, Item Registry | Controls the transitions between scenes (including menus), and loads up assets in places like the shopkeeper |
| Player Data Storage | Game Logic | Saves and loads the items that the player has, allowing the Game Logic to read and calculate numbers based on the player's inventory and leveling data |
| Item Registry | Game Logic, Assets | Items properties are pulled from the registry to be used in the game logic, and the registry contains file locations in the item assets folder |
| Assets | UI Management, Game Logic | Art and animations are loaded through triggers like opening the shopkeeper (UI Management), as well as when the player or an enemy attack (Game Logic) |
| Game Logic | Assets, Item Registry | Display different sprites requested from Assets. Using items’ properties from the Item Registry to calculate instances of damage. |

**Assumptions:**

1. Scenes  
   1. We are assuming that Unity’s built-in scene function is working without bugs and can be manipulated in the way we want  
   2. We are assuming these scenes will meet our performance requirements.  
2. Asset imports  
   1. We are assuming that both our custom-made sprites and ones we find elsewhere (like the Unity store) will be easily imported into our game and look and function the same way as we expected  
3. Buttons and UI  
   1. We will be using Unity’s built-in event system to interact with the buttons and other UI features, like a scrollable menu, so are assuming that these features are easy to work with and customizable in the way we want  
4. Global Data Storage  
   1. We are assuming that it is possible to store and pass information about the player across scenes in order to do calculations and modify game behavior accordingly

**Alternative Decisions:**

1. Player Run Data Storage  
   1. **Current Choice:** A single folder containing text files for each individual run, with each file containing the current highest arena, inventory, and coins.  
   2. **Alternative**: A MySQL database with tables to store player and individual run information  
      1. Pros  
         1. Permanent data storage that allows players to access their runs from multiple computers  
         2. Acts an easily backupable system to prevent players from losing save data  
      2. Cons  
         1. Unnecessarily complex for a single-player game, and would require some form of account authentication to ensure that the correct  
         2. Would turn an offline single-player game into one that requires some form of internet connection  
         3. Would require constant maintenance and fees to keep a database up and running  
2. Scene Storage  
   1. **Current Choice**: Using Unity’s individual scene system and editor to store UI elements and other parts of game design directly on the scene, with several scenes that are constantly transitioned between in gameplay  
   2. **Alternative:** Have a single scene that has UI elements and other parts of the game loaded through scripts in real time.  
      1. Pros  
         1. Requires less overall storage due to the reduction in scenes  
         2. Makes viewing changes to scenes through GitHub much easier, rather than having to completely reload the Unity build to view changes  
      2. Cons  
         1. Requires much more scripting for simple features that Unity already handles  
         2. Making small changes to scenes becomes much more tedious as there is no way to view changes in real-time  
         3. Generally removes the purpose of using a game engine

## Software Design

1. Scenes

   Scenes in Unity are self-contained environments that store the game’s visual and functional elements. They are used to organize different aspects of the game, such as menus, lobbies, and gameplay arenas.

   Functionality:  
   Separates UI elements from gameplay mechanics.  
   Loads different parts of the game depending on the player’s progression.

   Key Scenes:  
   Main Menu: Displays options like new game, load game, and settings.  
   Run Loading Screen: Handles transitions between game states.  
   Lobby: A hub where players can prepare before entering an arena.  
   Arenas: The core gameplay environments where combat and progression take place.

2. UI Management  
   Handles user interactions, scene transitions, and HUD updates.  
     
   Functionality:  
   Handles transitions between menus and scenes.  
   Displays and updates the HUD dynamically.  
     
   Key Elements:  
   Scene Manager: Handles switching between game scenes.  
   HUD Manager: Displays player stats, inventory, and game notifications.

   Button Handlers: Manages UI interactions such as clicking buttons or toggling settings.

     
3. Player Data Storage  
   Stores and manages persistent player data such as progress, items, and money, ensuring it can be saved and reloaded.  
     
   Functionality:  
   Saves and loads multiple gameplay runs.

   Maintains player-specific data such as arena progress, inventory, level, and money.

     
   Storage Mechanism:  
   Uses a folder containing text files that store player state.  
     
4. Item Registry  
   Stores and defines item functionality, linking individual items to their corresponding assets and scripts.  
     
   Functionality:  
   Stores functionality for each item in a dedicated script.  
   Connects item scripts with corresponding visual assets.  
     
   Key Elements:  
   Item Scripts: Defines behavior and effects of each item.  
   Asset Links: Stores paths to corresponding images or animations.

5. Assets  
   Holds all graphical and audio elements, such as character animations, enemy designs, UI elements, and pixel art.  
     
   Functionality:  
   Provides animations for characters, enemies, and UI elements.  
   Stores pixel art for maps, items, and environments.  
     
   Key Elements:  
   Sprite Sheets: Contains all pixel art for 2D rendering.  
   Animation Controllers: Defines animation states and transitions.  
     
     
6. Game Logic

	Handles damage calculations, player and enemy interactions, and all stat updates.

	Functionality:  
Computes damage dealt and received.  
Applies player upgrades and item effects.  
Updates player stats dynamically.

Key Elements:  
Combat System: Determines attack damage and effects.  
Upgrade Manager: Applies player skill upgrades and enhancements.  
Inventory System: Manages player and shop inventory.

## Coding Guideline

[https://google.github.io/styleguide/csharp-style.html](https://google.github.io/styleguide/csharp-style.html)  
I looked through multiple style guidelines for C\# and this one stood out as the most detailed, easiest to read, and easiest to implement. I believe once a script is finished, the writer can ask a teammate to double-check their code, and while doing so the aforementioned teammate could verify the stylization via the guidelines simultaneously.

## Process Description

### Risks

1. **Scope Creep:**  
   1. Adding too many features beyond our initial plan can result in delays and being unable to fully finish the project in a timely manner.  
2. **Technical Challenges:**  
   1. Issues can arise when attempting to program in collision detection or performance optimization, especially as we all are not too familiar with Unitys game engine.
3. **Bugs and Debugging:**  
   1. As most of us are inexperienced with the 2d Unity game engine, bugs and logic errors will come about and be a significant challenge for us to fix. We have already seen bugs and are working hard to fix them.
4. **Level Layout:**  
   1. Complex 2D level designs like in Lust and Gluttony might cause player or enemy clipping through walls if Unity’s tilemap colliders aren’t aligned properly, risking stuck characters or unreachable areas.
5. **Game Balancing:**  
   1. Boss mechanics could be too hard or too easy if health, damage, or timing isn’t calibrated, disrupting the intended difficulty curve. Balancing character stats might lead to unpredictable difficulty spikes if damage or health values aren’t tuned properly in playtesting.
6. **Character Swapping Bugs:**  
   1. Swapping between characters in CharacterSelect could carry over incorrect stats or abilities if PlayerController isn’t fully reset, leading to gameplay inconsistencies.
7. **Level Unlock Dependency:**  
   1. If a boss defeat in one circle (e.g., Lust) fails to update GameManager.maxArena, players could be locked out of the subsequent circle (Gluttony), halting progression entirely.
8. **Projectile Misses:**
    1. Player-fired projectiles (e.g., arrows, mage attacks) might pass through enemies if 2D collision layers or hitbox sizes in prefabs are misconfigured.

### Project Schedule

| Milestone | Tasks | Effort Estimation | Dependencies |
| :---- | :---- | :---- | :---- |
| Game Concept | Define game theme, mechanics, design, and progression | 3 weeks | N/A |
| Player Movement | Player movement, dashing | 1 week | GitHub and Unity setup |
| Player attacks | Left click attack for swordsman, archer, and mage, as well as single special attack | 3 weeks | Player Movement |
| Base Menu (Start Menu, Run Selection, Pause Menus) | Implement working buttons, screen transitions, backend file system for storing data for swapping screens | 3 weeks | N/A |
| Lobby Functionality | Design and implement portal loading system forr each arena, as well as shopkeeper UI | 2 weeks | N/A |
| Basic enemy spawning | Implement a wave spawning system that will be used universally, design simple enemy AI to move towards and attack the player, health and stats for each enemy | 3 weeks | Player Movement, Player attacks |
| Player and Enemy health and attacks | Allow player attacks to damage enemies, and enemy attacks to damage the player, and implement death mechanics for both. | 2 weeks | Basic Enemy Spawning |
| Coin and Leveling system | Implement dropable experience and coins from enemies that the player can obtain and spend on upgrades | 1 week | Player and Enemy Health and Attacks |
| Shopkeeper and Item progression | Implement shopkeeper functionality and allow players to purchase items with coins | 2 weeks | Coin and Leveling System |
| Implement Item Functionality | Introduce purchasable items for the player that impact gameplay (Increase damage, alter attack, increase health, etc) | 3 weeks | Shopkeeper and Item Progression |
| Arena Design | Design Arenas for the player to fight in | 5 weeks | N/A |
| Enemy and Player Visual Design | Design player and enemy animations and sounds | 2 weeks | N/A |
| Lobby Design | Design the lobby and its portals, as well as the shopkeeper UI and item UI | 2 weeks | N/A |
| Bosses | Implement Mini-Bosses and Final Bosses that unlock the next level for the player to access | 3 weeks | Shopkeeper and Item Progression |
| In-Run Leveling Upgrades | Implement on-the-go upgrades for players to purchase through the leveling system to make them stronger during arena fights. Will include upgrades like health, damage, or attack speed. | 2 weeks | Coin and Leveling System |
| General Play Testing | Ensure gameplay seamlessly transitions between levels, attacks from the player and enemies work, and the player can upgrade their character and save run data | 1 week | In-Run Leveling Upgrades (Includes all previous mechanics-focused goals) |

### Team Structure

Over the course of the project, we’ve further developed our team into 5 distinct categories:  
Design, Programming, Art, Sound, and Managerial Work. 

Design:   
Design is in charge of documenting and creating the concepts of the game and its features. For example, someone in charge of enemy design would create enemy concepts and ideas that could be implemented. Just because someone designs a concept does not mean that they are going to be the ones to program that concept.   
Generally, everyone is in charge of some aspect of design, but it’s also meant to be a very brainstormy category. No one person should entirely design one section. That being said, Kiet is generally in charge of characters, Joshua is generally in charge of upgrades, Brendan is generally in charge of items, James is generally in charge of enemies, and Chris is generally in charge of levels. Chris is also the overall manager of all design categories and he is the one to be reported to if there are questions or issues. 

Programming:   
Programming is in charge of implementing the design concepts into the game. In general, everyone is a programmer, and what is programmed by who will be determined more in depth at a later date, as we’re still in our design portion of the project. As SCM, Joshua will generally manage merges and commits. 

Art:   
Art could generally fall into a sub category design, but has large enough differences and a large enough amount of work to be separated into its own category. Art is in control of determining how the game looks. Most art is going to be found from Unity’s asset store, but the missing bits and pieces are going to have to be created by hand.   
Tanush is the art manager and will oversee most of the work in this area, while Kiet helps. 

Sound:   
Much like art, sound could fall into a design sub category but is big enough to separate. Sound is in control of determining how the game sounds, which in combination with art determines how the game feels as a whole. Music is going to be created by hand while sound effects are going to be either recorded or found from free websites.  
Max is the sound manager and will oversee most of the work in this area, with assistance from Brendan and Kiet. 

Managerial Work:   
As the SCM, Joshua is in charge of most of the managerial work that comes with the project. This includes but is not limited to, managing the trello, managing the weekly report, organizing documents and turning in assignments. There is an expectation that team members will update some of these things as they do them, but Joshua is in charge of helping people remember and adding details when needed.   
To see the overall update to roles, check the top of this document, where the roles have been updated. 

### Test Plan & Bugs

We will isolate and test each small change to the game software as required. These different requirements and testing implementations will be listed below:

Mechanics:
For our mechanics, we would run them through a testing and debug script, which would ensure that all necessary elements exist in the right screen. Once the mechanics pass these tests, we will then playtest and tweak each element as we go to ensure that each ability has reasonable values associated with it, be it damage, cooldown, or otherwise.

Items:
Items will follow a similar path, with them first being run through the applicable script, which will test to ensure that it is incorporated into the right scene and is accessible to the player through the proper actions. After this, each item will have multiple elements to adjust, notably price and damage/speed modifiers, each of which will be adjusted directly in the code base until it feels right for the expected game stage it will be acquired in.

Maps:  
Maps will be a bit tougher to isolate given elements, so we will mostly be trying to test out individual maps while incrementally adding the designated features of said map. For example, the circle of greed will first have its basic shape and structure implemented and tested. This would mean that the foundation of the map would be tested, and then any additional unique features would be tested after. The final test of any map will be the enemy entities since these will likely be the most difficult to effectively balance. But by testing all other elements of a given map first, we will be able to see how the map feels to play before adding and changing the enemy code.

UI and Misc:  
For any additional changes that may be seen frequently, we will hopefully be thorough before implementing any new features. If bugs are seen in these elements, be it UI or random bugs, we will isolate these issues in either the testing map or the main menu to best see how our changes will affect the game’s look and feel. If these bugs are more foundational, we will call an emergency meeting or directly @ the people whose design would be most affected. This will keep our group on the same page, as well as keep each design element in the hands of those who will be most knowledgeable about it.

### Documentation Plan

To ensure that the user experience is smooth and comprehensive, Pixhell will include two primary forms of documentation.

1. User Guide (Player Manual)  
   1. This document will target players and provide basic instructions on how to play the game, understand core game mechanics, and troubleshoot any common issues with a FAQ section.  
   2. The User Guide will contain:  
      1. A general game overview  
      2. Basic controls & combat mechanics  
      3. Stage progression information  
      4. Upgrade information  
      5. FAQ (common troubleshooting steps)

   The User Guide will be in PDF form and will be bundled with the game. This will be created once major gameplay features are finalized conceptually and have begun implementation.

2. In-Game Help Menu  
   1. This document will target players and mirror some of the essential knowledge laid out in the User Guide, but in a much more concise and readable way for reference in the menu of the game.  
   2. The Help Menu will contain:  
      1. Quick reference for controls and key bindings  
      2. Quick and accessible explanations for mechanics

   The Help Menu will be available at all times inside the game alongside where settings will be.

### Reflections

**Joshua Knowles**

One thing I learned was that oftentimes the hardest part about any project is starting it. We had a really hard time getting the project started because most of us weren’t sure how to start. However, once we got started, things generally started moving a bit quicker. In the future, I would ensure to either start the project myself so that others can get a place to work off of, or be more direct with delegating work early on so that people have direction in what to work on near the beginning of the project.
One other thing that I learned was that working in a group can be extremely frustrating. People don’t always tell the truth, they don’t always get their work done on time, and they don’t always communicate with the group. If I could do this project differently, I would be more assertive in my delegation, as I gave too much trust to my group members to be able to get the work done on their own. 
One thing that went well was managing branches and PRs. While we had a few branches that went wild, a large majority of our branches were merged cleanly, with most of the issues stemming from Unity not always meshing with git/github. This is one thing that I would have kept the same if doing the project again; having one SCM manage most of the merges (besides their own) so that one person has a good idea of all of the progress and can more easily deal with merge conflicts that arise. 

**James Osborn**

While working on Pixhell, I learned to manage projects a lot better, and learn the importance of designing and understanding the overall requirements of an idea before beginning to implement it. Not fully understanding what you are trying to implement can lead to code that isn’t modular with new ideas, making future progress slow.   
	I also found that plans are not concrete; and sticking to them completely is near impossible. It is important to learn to adapt and come up with new plans as you go, because it is inevitable that deadlines will be missed, some features will take longer than expected, or people forget to do tasks they were assigned. Just going with it and continuing to communicate new plans is important.  
	I also learned the importance of documenting issues and bugs. At the beginning of this project, I would try to perfect every piece of code I committed, so that there were never any bugs related to a given piece of code. This proved almost impossible, especially as we added more features and I learned more about Unity, it was very hard to predict every single possible way that the code could be broken. Instead, have dedicated play testing time and then documenting any issues that occurred was much more effective, as it would communicate to all team members there was an issue, and then someone could go ahead and fix the issue when possible.

**Chris Dutton**

The lesson that I learned the hardest was why it is important to make constant commits and ensure that each person knows who is working on what so they do not collide. There was a point where I had 8 hours of work not uploaded to the github and it caused major commit errors when I did finally push it. It took an entire day of github fixes to get the changes I had made integrated properly into the full game. 
I also learned that people can not be fully trusted to do work on their own when they say they will. I need to be able to manage my group better and constantly instruct individuals to make sure all tasks are finished in a timely manner.
One positive that I was able to bring from this project as a whole was that I learned a lot about reading documentation and how to overcome challenges in programming. A lot of unity is really well documented. Every function that can be used has at least a paragraph of text that comes with it and learning to skim and read the functions to be able to understand how it works made me much better understand how documentation works in this industry.

**Tanush Ojha**

I learnt a lot from this project, and one of the most important skills I picked up was specifically GitHub. This project gave me a lot of in-depth experience working with GitHub repositories, including branches, merging, pushing, and pulling, and a general grasp of how version control systems work. Another lesson I learned is about Unity and its animation system. Unity’s animation controller is a very convoluted and difficult tool to pick up, and even once you have in-depth knowledge of how to use it, it tends to throw curveballs at you, making errors you didn’t even know possible. I know how to animate in 2d now, and it is super fun to do and very rewarding once an animation system is working perfectly. Finally, I learnt about project management. Keeping a team of this size is difficult and communicating, plus coordinating, was very difficult at this size. At the end, I think I have a grasp on how to manage teams, and how to effectively work with and communicate with other project members efficiently and effectively.

**Brendan Laus**

This project was incredibly informative for me, especially in how I handle stress and asking for help. I was one of the group members that had both no experience in Unity as well as coding in C#, so this project pushed me to learn and apply information at a speed that I hadn’t gone before. This made me realize that I often feel bad for needing to ask for help, this was something I had to overcome to meaningfully provide my prior promises to my team. Another lesson I learned was that I work best in new environments by learning the basics, practicing them in a separate “sandbox” environment, and then implementing my refined skills on the main project. Lastly, was communication, my group had large goals that could only be achieved through effective and frequent communication. Over time, I made sure to inform my team of any issues I encountered that would impact them, or of any instances in which I was available to help others. If I were transported back into week one, I would focus on first learning the basics of the application we were using, asking frequent questions, and “getting up to speed”. After this, I would ask my team what I can do to reduce their bottlenecks, and focus on these tasks first. Following this, I would try to check in more with my team not just on their workload, but on how comfortable they feel with what they are working on, this way we can help each other find solutions we may not have seen by ourselves.

**Max Russell**

One of the most important lessons I learned from this project involved getting better at learning new tools or software. Unity is not a simple tool by any means and figuring out the basics, nonetheless the audio system, was a steep climb; I had to be very adaptable. I’ve learned a lot about Unity and C# scripting throughout this process. Another big lesson I learned from this project was utilizing all of the resources available to me. I used my teammates, youtube tutorials, and many websites to implement and polish my additions to the game. Possibly the biggest lesson I learned was prioritization and time management. Balancing a group project with regular homework assignments, all while juggling multiple other CS classes made for a challenging term from start to finish, and I feel like I have improved at managing such stress and can learn a lot about starting early and staying late, especially in a group setting.

**Kiet Bui**

Working on this project, I learned to use new tools and to work with a big group. I learned to work with a distributed version control system, which I haven’t used with many other people before. I learned to merge, rebase, fetch, and resolve any conflicts those might’ve created. I learned to use Unity, although I have used Unity before I haven’t used a lot of the features I’ve implemented, like animation, collision, trigger, and audio. Along with Unity, it allows me to practice coding in C#, which is similar to other languages that I’m proficient in, with a few knick knacks. I learned to work in a team with a team leader/manager. Most of the time when I work in a group, it is very chaotic to know what features other people are working on. Having a team leader to keep track and make sure everyone is working on different things really helped out my work.
