using GoogleMapService.BLL;
using Configuration;

namespace GoogleMapService.Factories
{
    public static class GoogleMapFactory
    { 
        public static IGoogleMapManager GetGoogleMapManager(ISettings settings)
        {
            return new GoogleMapManager(settings);
        }
    }
}
