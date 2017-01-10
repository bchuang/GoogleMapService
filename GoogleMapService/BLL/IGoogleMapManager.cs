using GoogleMapService.Models;

namespace GoogleMapService.BLL
{
    public interface IGoogleMapManager
    {
        CoordinateResult GetCoordinateWithGeocode(string address,ServiceResultTypes type = ServiceResultTypes.street_address);
    }
}
