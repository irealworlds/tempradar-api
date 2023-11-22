using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Domain.Contracts.Configuration
{
    public class WeatherApiSettings
    {
        public required string BaseUri { get; init; }
        public required string? ApiKey { get; init; }
    }
}
