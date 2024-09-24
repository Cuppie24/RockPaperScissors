# Rock, Paper, Scissors

## Overview

"Rock, Paper, Scissors" is a console-based game developed in C#. The game pits the player against the computer in a classic matchup with unique features designed to enhance gameplay integrity and user experience. 

### Key Features

- **Odd Number of Rounds:** The game requires an odd number of rounds, ensuring a definitive winner.
- **Cryptographic Security:** A cryptographically secure key is generated before each round to enhance gameplay security.
- **HMAC Generation:** Each move's text (e.g., "rock," "paper," "scissors") is used to generate an HMAC (Hash-based Message Authentication Code), ensuring the integrity of the game's outcomes.
- **Help Menu:** An accessible help menu provides comprehensive information on move interactions, clarifying which moves defeat others.

## Functionality

1. **Key Generation:** A new cryptographically secure key is generated at the start of each round.
2. **HMAC Creation:** The game generates an HMAC based on the player's move and the generated key, allowing for secure verification.
3. **Gameplay:** Players make their selections, after which the results and the original key are displayed, enabling verification of the game's integrity through third-party services.
4. **Help Menu:** Players can access a help menu to view the interactions between moves, facilitating informed decision-making during gameplay.
