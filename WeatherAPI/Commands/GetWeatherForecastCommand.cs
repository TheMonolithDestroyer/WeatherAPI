using FluentValidation;

namespace WeatherAPI.Commands
{
    public class GetWeatherForecastCommand
    {
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public DateTime Date { get; set; }
    }

    public class GetWeatherForecastCommandValidator : AbstractValidator<GetWeatherForecastCommand>
    {
        public GetWeatherForecastCommandValidator()
        {
            RuleFor(arg => arg.Latitude)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");

            RuleFor(arg => arg.Longitude)
                .Must(p => !string.IsNullOrWhiteSpace(p))
                .WithMessage("'{PropertyName}' is required field.");

            // After impl integration, check this validator
            RuleFor(arg => arg.Date)
                .Must(p => p > DateTime.MinValue && p <= DateTime.MaxValue);
        }
    }
}
