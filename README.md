# DungeonCrawler

<h2>Project Overview</h2>

This project is a slice of a top‑down action RPG framework built around modular systems for combat, AI, inventory, progression, and world interaction. The gameplay consists of movement, attacking, taking damage, collecting loot, gaining XP, leveling up, and interacting with enemies driven by finite state machines. The architecture is highly decoupled through an event‑driven system and relies heavily on ScriptableObjects for data‑driven design.

<img width="1100" height="620" alt="Start_Position" src="https://github.com/user-attachments/assets/4897f20e-9700-42d7-90fb-4d3f3e4a1878" />


<h3>Damage System and Calculation</h3>
The damage system is unified through the IDamageable interface, which all damage‑receiving entities implement, including the player, enemies, and breakable objects. Damage is represented by a DamageData structure containing the damage amount, damage type, optional slow effects, and the source object. Both player and enemies calculate final damage using a defense‑based reduction formula, that I came up with after examining how different games calculate the damage taken, based on certain stats.

```
damageTaken = damage * (100 / (100 + defense));
```
Breakable objects simply die immediately and trigger drop events to drop loot.

<h3>Custom Events and Event Bus</h3>
A global EventBus coordinates communication between systems. It exposes strongly‑typed events such as PlayerDamaged, PlayerHealed, PlayerDeath, EnemyDamaged, EnemyDie, CurrencyCollected, ItemTaken, LevelUp, and StatsReset. Systems like XPManager, CurrencyManager, DropSystem, UI can subscribe to these events without needing direct references to each other, keeping the architecture clean and modular.

<h3>Enemy System and Behaviors, Finite-State machine</h3>
Enemies inherit from a shared Enemy base class, which handles stats, health, mana, movement speed, defense, attack logic, XP dropped when killed, item drop tables, and death behavior. Each enemy has a Blackboard that stores runtime data such as animator, NavMeshAgent, movement speed, attack interval, target position, and flags like isDamaged or isDead. Enemy behavior is controlled by a dedicated FSM per enemy type. Goblins, Flying Devils, and Wizards each have their own FSMs composed of states such as Idle, Chase, Attack, Damaged, Die, and for the Wizard, Summon. Transitions are based on distance to the player, damage taken, attack timing, or special conditions like the Wizard’s phase system. The Wizard enters phase two when its health drops below 50%, unlocking summoning behavior controlled by SummonState, which spawns spawner objects that generate additional enemies.

<h3>Player</h3>
The player system includes movement via NavMeshAgent, mouse‑based attack targeting, projectile firing, damage handling, leveling, healing, and inventory interaction. Player stats come from BaseStatsData and PlayerProgressData, allowing persistent progression and enabling simple save files. The player uses a PlayerFSM with states for Idle, Move, Attack, Damaged, and Die. Transitions depend on movement velocity, attack input, damage flags, and death state.

<h3>Projectile Types</h3>
Projectiles are implemented through an abstract Projectile class. The player fires Bullets, while the Wizard fires WizardProjectiles. Both use Rigidbody movement and destroy themselves after a set time or upon collision. Bullets damage enemies, while Wizard projectiles damage the player.

<h3>Inventory System, Pickup System and Items</h3>
The inventory system uses ItemData ScriptableObjects to define items, including their type, icon, stackability, max stack size, and world prefab. The Inventory component manages slots, stacking logic, and item removal. Slot 0 is reserved for HP potions. Items in the world are represented by Item objects that despawn after a timer and trigger pickup logic when the player collides with them. Successful pickups fire an ItemTaken event.

<img width="1100" height="620" alt="Inventory" src="https://github.com/user-attachments/assets/738504f5-b5f7-49b7-a284-b9b91394c7b4" />

<h3>Fully Customisable Drop System</h3>
The DropSystem listens for EnemyDie events and spawns currency and items based on the enemy’s ItemDropTable. Currency objects publish CurrencyCollected events when picked up. Item drops use DropEntry definitions with drop chance and min/max amounts, spawning items around the enemy’s death position.

<h3>Level and Currency Management</h3>
Progression is handled by XPManager, which listens for EnemyDie events and awards XP to the player. When XP exceeds the threshold, the player levels up, increasing stats and recalculating the next XP requirement. CurrencyManager tracks collected currency and updates totals when CurrencyCollected events fire.

<h3>Customisable Quest System and Spawners</h3>
Additional systems include QuestData for defining kill or fetch quests, SceneLoader for scene transitions, and Spawner objects that generate enemies over time within a defined area. The FSM and State classes provide the foundation for all AI behavior, with Transition objects defining conditions for switching between states. The Blackboard class provides shared runtime data for both player and enemy FSMs.

<img width="1100" height="620" alt="Quest_System" src="https://github.com/user-attachments/assets/292b412e-96d6-4ff9-a38c-922accb80756" />

<img width="1100" height="705" alt="Enemy_Layout" src="https://github.com/user-attachments/assets/fe2a8043-e10c-4d23-be6f-8bc2274cbef8"/>



