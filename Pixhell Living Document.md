# Team Info \- Pixhell

## Names and Roles:

Joshua Knowles \- SCM, Deadline Management, Design  
Brendan Laus \- Design \- Map Focus, Item Creation  
Max Russell \- Sound Design, Stage Design  
James Osborn \- Item creation, Menu Mechanics and Transitions  
Tanush Ojha \- Art Manager, Movement, Design  
Kiet Bui \- Art, Sound Design  
Chris Dutton \- Design Manager, Level Developer

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
Google Doc Version of this document \- [Here](#external-requirements)   
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

Pixhell is a bullet-hell rogue-lite that challenges players to survive and conquer nine stages \- the nine circles of Hell \- combining fast-paced action and unique environments with a progression system that encourages experimentation across multiple runs. Players navigate through increasingly more difficult stages, collecting upgrades along the way to eventually defeat the Devil in the center of Hell.

## Goal

We are creating a replayable rogue-lite bullet-hell game involving making it through the nine circles of Hell to eventually battle the Devil. The game will include meta-progression, different characters, weapons, and enemies. The goal is to provide users with an enjoyable experience of progression and discovery in a chaotic challenge.

## Current Practice

There are many examples of games similar to ours that we are taking inspiration from, such as Hades and Vampire Survivors, which use many different game engines, one of which is Unity which we will be using. This allows us to skip some of the heavy lifting and let Unity manage it. However, it constrains us to the bounds of Unity which limits us within its jurisdiction, and we have to worry about the performance issues of running a game on computers with limited hardware. 

## Novelty

The thematic structure of combining a narrative like the Nine Circles of Hell with the action and tight mechanics of a bullet-hell roguelike creates a more immersive and stylistic experience. Navigating a symbolic descent while the difficulty escalates makes for a memorable gaming venture. Using the Nine Circles of Hell creates a new theme not seen before that creates a new experience for people playing our game, while still being able to pick up the game quickly with its similarities to other games. Our nine arena progression is also a novel concept, which allows us to create a lot of variety between individual arenas with novel enemy types and environments, while finally our three unique characters can be mixed and matched with different arenas and items to make each run in our game itself feel different from the next. 

## Effects

Games are a huge source of fun and a way for people to escape from their regular lives for a few hours at a time. We hope to make it so people can delve into the world of Pixhell and lose themselves in enjoyment and even learn a little bit about Dante's circles of hell. Games like Pixhell can also create friendly competition within social groups which creates stronger connections with other people as games are often a way that people bond. 

## Use Cases (Functional Requirements)

1.  Joshua Knowles  
   1. Actors: Player  
   2. Triggers: Left clicking while controlling the character  
   3. Preconditions:   
      1. The player has entered the game  
      2. The player is not in a menu  
      3. The player is not in a loading zone  
      4. The player is not currently on their attack cooldown  
         1. The player can not attack on every frame  
   4. Postconditions:   
      1. A player attack animation is played  
      2. Enemies in the range of the attack get damaged  
      3. A attack cooldown is triggered where the player can not attack again until it ends  
   5. List of Steps:   
      1. The player enters the game and chooses a character to play  
      2. The player left clicks once in control of their character  
         1. The player can attack in the lobby. There are no enemies to attack, but the animation takes place.   
      3. An attack has been performed, and enemy damage has been processed.   
      4. The player may attack again when the attack cooldown has ended  
   6. Extensions/Variations:   
      1. Each character has a variation  
         1. Specific characters may vary in setup. Specific character design is not fleshed out, so this section will be complete when it is  
   7. Failure:   
      1. The player tries to attack when not in control of the character \- Nothing happens  
      2. The player tries to attack when on their attack cooldown \- Nothing Happens  
2. Brendan Laus  
   1. Actors: Player, Map elements, and Sound system  
   2. Triggers:   
      1. The player character's hitbox colliding with a map selection trigger  
   3. Preconditions:   
      1. Completion of previous maps (if applicable)  
      2. The new map trigger is currently occupied by the player  
   4. Postconditions:   
      1. The loading screen is displayed   
      2. The new map loads in with its dependencies  
      3. The character spawns in an appropriate location on the new map  
      4. The new map’s music begins to play  
   5. List of Steps:   
      1. The player character will move toward the desired map  
      2. The player character’s hitbox collides with the new map trigger  
      3. The game checks that the character is eligible to play on the new map  
      4. After a successful check, a confirmation screen is displayed  
      5. Upon final conformation, the loading screen displays  
      6. Old and unnecessary assets get dropped while the map loads with its associated dependencies  
      7. The player spawns into the new map at a designated spawn point  
      8. The correct map music begins to play  
   6. Extensions/Variations:   
      1. Each different map will have a different trigger the character will need to access.   
      2. Each map will have different spawn points, visuals, and the proper soundtracks.  
   7. Failure:   
      1. If the player doesn’t pass the eligibility check, there will be a pop-up informing them that they need to complete certain tasks to unlock the level they attempted to access and they will not be permitted to go to the next map.  
3.  Max Russell  
   1. Actors: Player, Sound System  
   2. Triggers: The player enters a new stage or enters a boss fight (stage boss or final boss).  
   3. Preconditions: The stage or boss fight has been loaded and the sound system is already initialized and functioning.  
   4. Postconditions: The background music changes based on the specific event triggered or new location entered.  
   5. List of Steps:   
      1. The game detects the player has entered a new stage or has transitioned into a boss fight.  
      2. The sound system prepares the corresponding music for the new encounter.  
      3. The previous music fades away and is replaced with the new track.  
      4. The player hears the matching music integrated with what they are experiencing in terms of gameplay.  
   6. Extensions/Variations:   
      1. During loading screens or in the hub/main area, ambient (elevator) music plays.  
      2. The music is intensified (faster BPM) if the player’s health gets critically low.  
   7. Failure:   
      1. A file might be corrupted or missing entirely meaning there is just silence during gameplay. This could be avoided by having a default track to play in the event that there is not one already set)  
      2. Tracks might lag or stutter. This could be avoided by pre-loading music assets ahead of time.  
4.  James Osborn  
   The player moves from the first level to the second.  
   1. Actors: Player, Shopkeeper  
   2. Triggers: The player completes the first level by defeating its final boss  
   3. Preconditions: The player has obtained powerful enough items to complete the first level and has beaten it, and talks to the shopkeeper  
   4. Postconditions: The player enters level two  
   5. List of Steps:   
      1. The player beats the final boss of the first arena.  
      2. The player exits the arena and goes to the lobby.  
      3. The player walks over to the shopkeeper to obtain the key to arena two’s portal.  
      4. The player uses the key to open the portal.  
      5. The player enters the portal to start arena two.  
   6. Extensions/Variations:  
      1. If the player has already picked up the key from the shopkeeper previously, they can just walk straight over to the portal and open it.  
      2. If the player has already opened the portal, they can enter the portal immediately without talking to the shopkeeper or opening the portal.  
   7. Failure:   
      1. The player attempts to get the key from the shopkeeper without completing arena one.  
      2. The player attempts to use the key on the wrong portal.  
5.  Tanush Ojha  
   1. Actors: Player, Enemies  
   2. Triggers: The player collects enough EXP to level up  
   3. Preconditions: The player is in an arena, the player has killed enough enemies to level up, enemies drop EXP  
   4. Postconditions: The player gets to choose a level-up reward, level-up reward is correctly applied and works, EXP count resets, MAX exp increases  
   5. List of Steps:   
      1. Player levels up  
      2. Game ‘pauses’ and level-up screen comes on  
      3. Player selects the level up reward  
      4. Level-up screen disappears and game is ‘unpaused’  
      5. Player can use level up reward, or is correctly applied  
      6. Level-up reward is reset on level fail or completion (upon level exit)  
   6. Extensions/Variations:   
      1. Player can get level up reward twice for additional boost, or a extension of the skill previously gained  
      2. If player achieves all level up rewards, only money is gained  
   7. Failure:   
      1. Player doesn’t choose any level up reward  
      2. Levelup screen pops up before level up  
      3. Reward is not correctly given to player  
6.  Kiet Bui  
   1. Actors: Player, NPCs  
   2. Triggers: Interact with the NPCs  
   3. Preconditions: Player is in the lobby, NPCs are present  
   4. Postconditions: Player enters a screen where you can talk to the NPCs with multiple dialogue options.  
   5. List of Steps:   
      1. Player talks to the NPCs by using the interact key.  
      2. A dialogue screen is displayed.  
      3. Player choose one of many dialogue options.  
      4. Player exits the dialogue screen by clicking on the “x” button or finishing the dialogue.  
      5. That NPC is no longer able to be interacted with anymore.  
   6. Extensions/Variations:  
      1. Players can get rewards if they choose a certain dialogue option.  
      2. Players can get punished if they choose certain dialogue options i.e the player gets debuffs.  
   7. Failure:   
      1. Player attempts to talk to NPCs again but can’t. Pop up saying the NPC has already been talked to.  
7.  Chris Dutton  
   1. Actors: Player  
   2. Triggers: Hitting shift while controlling the character  
   3. Preconditions:  
      1. The player is in a level  
      2. The player is not in a menu  
      3. The player is not in a loading zone  
      4. The dodge cooldown is not currently active  
   4. Postconditions:  
      1. A dodge roll animation is played out  
      2. Invincibility frames are gained for half the duration of the roll animation  
      3. A dodge roll cool down is activated for 1 second to ensure roll is not able to  constantly abused for invincible frames  
   5. List of Steps:  
      1. The player enters a level  
      2. The player shift clicks when they have control of their character  
      3. The dodge roll is initiated and invincible frames are given to the player  
      4. A cooldown is applied to the shift ability   
   6. Extensions/Variations:  
      1. The activation of the animation allows for the player to jump over small projectiles before invincibility frames are activated   
   7. Failure:  
      1. The player attempts to use the dodge roll ability while it is on cooldown and it does not go off  
      2. The player presses shift while not in control of the menu and nothing happens

## Non-functional Requirement

1. Usability:  
   1. Menus and interfaces should be simple, clear, and easy for users to navigate to make sure players do not spend too much time on non-gameplay actions.  
2. Performance  
   1. The game will easily run at 60 FPS on any modern laptop or desktop and not dip below half of that, even during intense bullet-hell sequences with many projectiles on screen at once.  
3. Modularity/Maintainability  
   1. Core systems of the game (enemy generation and behaviors, item/upgrade mechanics, etc.) must be modular so that new additions of new features can be implemented seamlessly.

## External Requirements {#external-requirements}

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
	Joshua Knowles \- SCM, Deadline Management, Design

* Every project needs an SCM to manage deadlines, version control, documentation, and overall keep the team connected and knowledgeable. Without the SCM, team members may not know what to do to be productive and work may be wasted if something worked on is no longer needed.   
* Having a specific person dedicated to managing deadlines also ensures that things will get turned in on time and that work is done based on its priority.

Brendan Laus \- Design \- Map Focus, Item creation,

* Look into and implement effective 2d map creation elements that provide both visually appealing elements as well as effective gameplay variation if applicable  
* Research and create items that will provide meaningful and understandable changes to gameplay that improve playability and synergize with certain builds  
* Aiding others with general design on core gameplay elements, especially as they relate to both map design and potential items to be implemented.  
* Each of the roles above will be massively important to this game, as both items and maps will provide key elements of the replayability appeal that Pixhell is striving for. 

Max Russell \- Sound Design, Stage Design

* Develops audio elements throughout the game and assists in designing stages for thematic consistency.  
* Sound design and general stage appearance is important for the player’s enjoyment of any game and makes this game more memorable and unique.

James Osborn \- Item creation, Menu mechanics and transitions

* Will bring the core elements of the gameplay together through the menu and lobby, making the game playable.  
* Add variety through character items for replayability

Tanush Ojha \- Art Manager, Movement, Enemy Design

* Will source and create art, including basic character, boss, and enemy design, creating both models and small animations for everything. Art will create a depth of character in our game, enhancing the gameplay experience.  
* Create movement mechanics such as dashing, teleporting, and any other movement boosts we come up with in our game. Good movement mechanics will allow the user to come up with different techniques of movement, combining dashing with teleporting or running, adding a simple but fun additional aspect of difficulty and skill.  
* Creating boss mechanics, including weapon attacks, phases, and more will give the user an enhanced gameplay experience, having difficult bosses that take multiple tries to beat will give the user more time to learn and have fun with our game.

Kiet Bui \- Art, Sound Design, Animation, 

* Create arts and music, which allows the user to experience what the world inside the game looks and sounds like.  
* Putting animation for characters and enemies gives the game life.

Chris Dutton \- Design Manager, level developer

* Makes sure all design elements fit the backdrop of the game   
* Ensures that levels have a challenge and fit within the games universe

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

## Risks

Our greatest potential risk would be picking a project that is far too large for our group size. Other potential risks include meshing everyone’s work while maintaining a clear and effective theme/style for the project. To help combat these issues, we will prioritize frequent and honest communication that focuses on work quality and open communication. Aside from this, we will set clear checkpoints for each weekly meeting, this will help us not only keep pace but check our goals to see how realistic our goals are.

## Main Goals

* Eight arenas of increasing difficulty, with one mini-boss and a final boss. Beating the final boss of an arena unlocks the next one. The ninth and final arena is a single final boss  
* The lobby area provides progressive upgrades that make future runs easier by improving player abilities and unlocking new features  
* During each arena, temporary abilities are applied to the character to mix up gameplay  
* There are at least 3 distinct characters which each provide a unique and enjoyable gameplay experience

## Stretch Goals

* Art and effects match the theme and have continuity with one another, and are implemented smoothly  
* Implement sound design to make the game pop, both through sound effects and music  
* Add in-depth lore and story element

## Team Info
See top of document

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

## Software Design

## Coding Guideline

## Process Description
### Risk Assessment

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

### Test Plan & Bugs
Brendan Laus:

We will isolate and test each small change to the game software as required. These different requirements and testing implementations will be listed below:

Mechanics:
Game mechanics will be tested in a mostly empty map with an enemy, the player character, and whatever additional hazards the character will frequently interact with. There will also be a custom item spawner to test different components and how they interact with each other. This means that each new element can be tested as it relates to the standard gameplay loop. This enables us to see if the desired mechanical change will properly affect the player, enemies, and whatever items may relate to it.

Items:
Items will follow the same path, primarily being spawned into this test world. This will allow us to isolate item effects and see how they change the player’s movement, health, damage, attacks, and any additional impacted core game mechanics. This will permit us to see potential edge cases, as well as how each item feels to use, and permit minor changes to be made and allow us to see how these changes impact gameplay.

Maps:
Maps will be a bit tougher to isolate given elements, so we will mostly be trying to test out individual maps while incrementally adding the designated features of said map. For example, the circle of greed will first have its basic shape and structure implemented and tested. This would mean that the foundation of the map would be tested, and then any additional unique features would be tested after. The final test of any map will be the enemy entities since these will likely be the most difficult to effectively balance. But by testing all other elements of a given map first, we will be able to see how the map feels to play before adding and changing the enemy code.

UI and Misc:
For any additional changes that may be seen frequently, we will hopefully be thorough before implementing any new features. If bugs are seen in these elements, be it UI or random bugs, we will isolate these issues in either the testing map or the main menu to best see how our changes will affect the game’s look and feel. If these bugs are more foundational, we will call an emergency meeting or directly @ the people whose design would be most affected. This will keep our group on the same page, as well as keep each design element in the hands of those who will be most knowledgeable about it.

### Documentation Plans
