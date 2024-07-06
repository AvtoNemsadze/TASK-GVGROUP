Slot Game Backend API
This project is a backend API for a slot game, built with .NET Core, using a CQRS pattern and Mediator for handling commands and queries. The API supports user authentication, betting, and game session management. It includes a SpinCommandHandler that processes spin requests, validates user balance, calculates spin outcomes, updates game statistics, and maintains bet records. Real-time notifications are implemented using SignalR, providing live updates and promotions to users. The backend is designed with a clean architecture, ensuring separation of concerns and scalability.
