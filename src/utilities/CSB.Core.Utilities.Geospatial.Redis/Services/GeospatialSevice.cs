using CSB.Core.Utilities.Redis.Services;

namespace CSB.Core.Utilities.Geospatial.Redis.Services
{
    internal sealed class GeospatialSevice : IGeospatialSevice
    {
        private readonly IRedisGeospatialService _redisGeospatialService;
        public GeospatialSevice(IRedisGeospatialService redisGeospatialService)
        {
            _redisGeospatialService = redisGeospatialService;
        }

        public double? GetGeoDistance(string key, string from, string to, GeospatialUnit geospatialUnit)
        {
            return _redisGeospatialService.GetGeoDistance(key, from, to, geospatialUnit);
        }

        public string[] GetGeoHash(string key, string[] names)
        {
            return _redisGeospatialService.GetGeoHash(key, names);
        }

        public IGeospatialData[] GetGeoLocation(string key, string[] names)
        {
            return _redisGeospatialService.GetGeoLocation(key, names);
        }

        public IGeospatialRadiusResult[] GetGeoRadius(string key, string name, double radius, int count = -1, GeospatialUnit geospatialUnit = GeospatialUnit.Kilometer, GeospatialRadiusOption geospatialRadiusOption = GeospatialRadiusOption.Default, bool isAscending = true)
        {
            return _redisGeospatialService.GetGeoRadius(key, name, radius, count, geospatialUnit, geospatialRadiusOption, isAscending);
        }

        public IGeospatialRadiusResult[] GetGeoRadius(string key, double longitude, double latitude, int radius, int count, GeospatialUnit geospatialUnit = GeospatialUnit.Kilometer, GeospatialRadiusOption geospatialRadiusOption = GeospatialRadiusOption.Default, bool isAscending = true)
        {
            return _redisGeospatialService.GetGeoRadius(key, longitude, latitude, radius, count, geospatialUnit, geospatialRadiusOption, isAscending);
        }

        public void SetGeo(string key, IGeospatialData geospatialData)
        {
            _redisGeospatialService.SetGeo(key, geospatialData);
        }

        public void SetGeo(string key, IGeospatialData[] geospatialData)
        {
            _redisGeospatialService.SetGeo(key, geospatialData);
        }
    }
}