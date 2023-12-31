﻿using API.Domain.Dto;

namespace API.Domain.Contracts.Services;

public interface IPinnedCityWeatherService
{
    Task<PinnedCityWeatherDetailsDto?> GetWeatherDetailsAsync(Guid id);
    Task<List<CityDailyTemperatureDto>?> GetWeatherHistoryAsync(Guid id);
}