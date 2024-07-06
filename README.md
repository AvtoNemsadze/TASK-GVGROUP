<h2>Slot Game Backend API<h2></h2> <br/>
This project is a backend API for a slot game, built with .NET Core, using a CQRS pattern and Mediator for handling commands and queries. The API supports user authentication, betting, and game session management. It includes a SpinCommandHandler that processes spin requests, validates user balance, calculates spin outcomes, updates game statistics, and maintains bet records. <br /><br />

<strong>Game Mechanics</strong>
- Spin Logic: Players place a bet and choose a number. The game generates a random number between 1 and 10. If the player's chosen number matches the generated number, the player wins double their bet amount. <br />
- Winning Logic: The EvaluateSpinResult method determines the outcome by comparing the player's chosen number with the generated number, calculating the winnings, and providing a success or failure message. <br />
- Balance Updates: Based on the game outcome, the user's balance is updated accordingly <br />

<strong>Game Mechanics</strong>
- Real-Time Notifications: Implemented using SignalR to provide live updates and promotions to users. <br />
- Game Session Management: Efficiently manages game sessions and bet records. <br />
- Clean Architecture: Ensures separation of concerns and scalability for future enhancements. <br />
