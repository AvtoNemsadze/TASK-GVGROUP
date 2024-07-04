using FluentValidation;


namespace SlotGameServer.Application.Spin.Commands.Games
{
    public class SpinCommandModelValidator : AbstractValidator<SpinCommandModel>
    {
        public SpinCommandModelValidator()
        {
            RuleFor(x => x.SessionId)
                .GreaterThan(0)
                .WithMessage("Session ID must be greater than zero.");

            RuleFor(x => x.ChosenNumber)
                .InclusiveBetween(1, 10)  
                .WithMessage("Chosen number must be between 1 and 10.");

            RuleFor(x => x.BetAmount)
                .GreaterThan(0)
                .WithMessage("Bet amount must be greater than zero.");
        }
    }
}
