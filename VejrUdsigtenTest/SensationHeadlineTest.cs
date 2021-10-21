using Vejrudsigten.Services;
using Xunit;

namespace VejrUdsigtenTest
{
    public class SensationHeadlineTest
    {
        [InlineData("Andet", "Klart vejr", 0, -10, "Nu bliver det MEGET koldere! - Det solrige vejr kommer!")]
        [InlineData("Skyet", "Regn", 0, -9.9, "Nu bliver det koldere - Tøm regnmåleren, vandet kommer!")]
        [InlineData("Sne", "Sne", -5, -5, "Temperaturen varer ved - På med vanten, sneen fortsætter...")]
        [InlineData("Regn", "Andet", 5, 14.9, "Nu bliver det varmere - Det ukendte kommer!")]
        [InlineData("Klart vejr", "Skyet", 15, 25, "Nu bliver det MEGET varmere! - Det overskyede vejr kommer!")]
        [Theory]
        public void Can_detect_an_invalid_headline(string yesterdaysCondition, string todaysCondition, double yesterdaysTemperature, double todaysTemperature, string expectedHeadline)
        {
            // Arrange
            var todaysWeatherInfo = new WeatherInfo { Conditions = todaysCondition, Temperature = todaysTemperature };
            var yesterdaysWeatherInfo = new WeatherInfo { Conditions = yesterdaysCondition, Temperature = yesterdaysTemperature };

            // Act 
            var sensationHeadline = WeatherForecast.GetSensationHeadline(todaysWeatherInfo, yesterdaysWeatherInfo);

            // Assert
            Assert.Equal(expectedHeadline, sensationHeadline);
        }
    }
}
