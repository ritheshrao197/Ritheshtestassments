# CardMatch
Certainly, here's the README with "we" replaced by "I" as per your request:

# Match Cards - A Memory Card Game in Unity

**Project Overview:**

- **Project Name:** Match Cards
- **Development Period:** October 21 - October 22
- **Developer:** Rithesh
- **Drive Link:** [Unity Package and macOS Build](https://drive.google.com/drive/folders/1oK9n4RvO5puI12svLGeZJFw3XzUz7IYH?usp=sharing)

**Introduction:**

Welcome to Match Cards, I developed this game using Unity and followed an architectural approach that emphasizes modularity and event-based communication between systems.

**Development Process:**

**Architecture:**

- I utilized the Models-Views-Systems architectural pattern.
- All systems are independent, with no direct communication between them.
- I employed an event-based communication approach for every action in the game, promoting loose coupling.

**Project Setup:**

- Art assets were sourced from the Unity Asset Store .

**Card Prefab:**

- I created a Card Prefab, complete with a sprite renderer for both the front and back of the card.
- I added a custom script to manage card behavior, including flipping and matching logic.

**Grid Layout:**

- I implemented an efficient grid layout system to arrange cards in rows and columns.
- The grid was populated with pairs of card prefabs to create a challenging game environment.


**Game Flow:**

- I initialized the game, including shuffling card positions and initially displaying cards facedown.

**Player Input:**

- I gave players the ability to interact with the game by clicking on cards to flip them.

**Matching Logic:**

- I established logic to determine if two flipped cards matched. When a match occurred, the cards remained face-up; otherwise, they were flipped back.

**Win Condition:**

- I created a win condition, with a check to determine if all pairs had been successfully matched. Winning the game was the ultimate goal.

**UI and Feedback:**

- I thoughtfully designed and implemented user interface elements, providing players with essential information such as score and a timer.

**Scoring:**

- I introduced a scoring system to based on no of matches he makes

**Timer:**

- I incorporated a timer into the game.



**Built With:**

- Unity - The game development engine.
- C# - The primary programming language used for scripting.

**License:**
