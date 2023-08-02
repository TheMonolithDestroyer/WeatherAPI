using FluentValidation;

namespace WeatherAPI.Commands
{
    public class GetWeatherForecastCommand
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class GetWeatherForecastCommandValidator : AbstractValidator<GetWeatherForecastCommand>
    {
        public GetWeatherForecastCommandValidator()
        {
            RuleFor(arg => arg.Latitude)
                .Must(p => p >= -90.0 && p <= 90.0)
                .WithMessage("'{PropertyName}' is out of range.");

            RuleFor(arg => arg.Longitude)
                .Must(p => p >= -180.0 && p <= 180.0)
                .WithMessage("'{PropertyName}' is out of range.");
        }
    }
}
