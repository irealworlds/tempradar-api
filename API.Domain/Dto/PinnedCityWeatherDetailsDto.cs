namespace API.Domain.Dto
{
    public class PinnedCityWeatherDetailsDto
    {
        public double TempC { get; set; }
        public double FeelsLikeC { get; set; }
        public double WindKph { get; set; }
        public double PressureMb { get; set; }
        public double UV { get; set; }
        public double CO { get; set; }
        public double NO2 { get; set; }
        public double O3 { get; set; }
        public double SO2 { get; set; }
        public double PM2_5 { get; set; }
        public double PM10 { get; set; }
        public int USEPAIndex { get; set; }
        public int GBDefraIndex { get; set; }
    }
}
