using System;
using System.Threading.Tasks;

namespace Vejrudsigten.Services
{
    public static class WeatherForecast
    {
        public static async Task<string> GetForecastAsync(string key)
        {
            WeatherService service = new WeatherService();
            var todayInfo = service.GetTodaysWeather(key, "Kolding");
            var yesterdayInfo = service.GetYesterdaysWeather(key, "Kolding");

            return GetSensationHeadline(await todayInfo, await yesterdayInfo);
        }

        public static string GetSensationHeadline(WeatherInfo todaysWeatherInfo, WeatherInfo yesterdaysWeatherInfo)
        {
            var partTemperature = GetHeadlinePartTemperature(todaysWeatherInfo, yesterdaysWeatherInfo);
            var partCondition = GetHeadlinePartCondition(todaysWeatherInfo, yesterdaysWeatherInfo);

            return $"{partTemperature} - {partCondition}";
        }

        private static string GetHeadlinePartTemperature(WeatherInfo todaysWeatherInfo, WeatherInfo yesterdaysWeatherInfo)
        {
            var diff = Math.Abs(todaysWeatherInfo.Temperature - yesterdaysWeatherInfo.Temperature);
            if (diff == 0)
            {
                return "Temperaturen varer ved";
            }
            var tempChangeOver = todaysWeatherInfo.Temperature > yesterdaysWeatherInfo.Temperature ? "varmere" : "koldere";
            if (diff < 10)
            {
                return $"Nu bliver det {tempChangeOver}";
            }
            return $"Nu bliver det MEGET {tempChangeOver}!";
        }

        private static string GetHeadlinePartCondition(WeatherInfo todaysWeatherInfo, WeatherInfo yesterdaysWeatherInfo)
        {
            string condition;
            var weatherConditionsChange = !string.Equals(todaysWeatherInfo.Conditions, yesterdaysWeatherInfo.Conditions);
            switch (todaysWeatherInfo.Conditions?.ToLower())
            {
                case "klart vejr":
                    condition = "Det solrige vejr";
                    break;
                case "regn":
                    condition = "Tøm regnmåleren, vandet";
                    break;
                case "sne":
                    condition = "På med vanten, sneen";
                    break;
                case "skyet":
                    condition = "Det overskyede vejr";
                    break;
                default:
                    condition = "Det ukendte";
                    break;
            }
            condition += weatherConditionsChange ? " kommer!" : " fortsætter...";
            return condition;
        }

    }
}
