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
                .Must(p => p >= double.MinValue && p <= double.MaxValue)
                .WithMessage("'{PropertyName}' is required field.");

            RuleFor(arg => arg.Longitude)
                .Must(p => p >= double.MinValue && p <= double.MaxValue)
                .WithMessage("'{PropertyName}' is required field.");
        }
    }
}
