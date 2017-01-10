using Configuration;
using GoogleMapService.Models;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System;

namespace GoogleMapService.BLL
{
    internal sealed class GoogleMapManager : IGoogleMapManager
    {
        private readonly string googleMapGeocodeApiUrl;
        private readonly string googleMapApiKey;
        private readonly string googleMapClientId;
        private readonly string googleMapcryptKey;

        public GoogleMapManager(ISettings settings)
        {
            googleMapGeocodeApiUrl = settings["GoogleMapGeocodeApiUrl"];
            googleMapApiKey = settings["GoogleMapApiKey"];
            googleMapClientId = settings["GoogleMapClientId"];
            googleMapcryptKey = settings["GoogleMapcryptKey"];
        }

        public CoordinateResult GetCoordinateWithGeocode(string address, ServiceResultTypes resultType = ServiceResultTypes.street_address)
        {

            var result = new CoordinateResult() { Coordinate = new Coordinate(), Status = ServiceResultStatus.NOT_DEFINED };

            if (!CheckAddress(address) || !CheckFilterAddress(address))
            {
                result.Status = ServiceResultStatus.INVALID_REQUEST;
                return result;
            }
            return GetGeocodeApiResult(address, resultType);
        }

        private bool CheckAddress(string address)
        {
            var result = (string.IsNullOrEmpty(address) || !address.Contains("號"));
            return !result;
        }

        private bool CheckFilterAddress(string address)
        {
            //var result = (
            //    !address.Contains("XX區") && !address.Contains("XX區")
            //    );
            return !false;
        }

        private CoordinateResult GetGeocodeApiResult(string address, ServiceResultTypes resultType)
        {
            var url = googleMapGeocodeApiUrl;

            address = address.Substring(0, address.LastIndexOf('號') + 1);

            if (!string.IsNullOrEmpty(googleMapApiKey))
            {
                url = string.Format("{0}key={1}&address={2}", url, googleMapApiKey, address);
            }
            else if (!string.IsNullOrEmpty(googleMapClientId))
            {
                url = string.Format("{0}client={1}&address={2}", url, googleMapClientId, address);
                var signUrl = Signature.GoogleSignedUrl.Sign(url, googleMapcryptKey);
                return RequestGeoCode(signUrl, resultType);
            }
            else
            {
                url = string.Format("{0}address={1}", url, address);
            }

            return RequestGeoCode(url, resultType);
        }

        private CoordinateResult RequestGeoCode(string url, ServiceResultTypes resultType)
        {
            return HandleResponseGeocode(WebRequest.Create(url).GetResponse(), resultType);
        }

        private CoordinateResult HandleResponseGeocode(WebResponse response, ServiceResultTypes resultType)
        {
            var result = new CoordinateResult() { Coordinate = new Coordinate(), Status = ServiceResultStatus.NOT_DEFINED };

            StreamReader sr = new StreamReader(response.GetResponseStream());
            var apiResult = JsonConvert.DeserializeObject<ApiGeocodeResult>(sr.ReadToEnd());

            result.Status = SetResultStatus(apiResult.status);

            if (result.Status == ServiceResultStatus.OK)
            {
                if (apiResult.results.Count() == 1)
                {
                    var finalResult = apiResult.results.FirstOrDefault();

                    if (finalResult == null)
                    {
                        result.Status = ServiceResultStatus.ZERO_RESULTS;
                        return result;
                    }

                    if (finalResult.partial_match == false && finalResult.types.Contains(resultType.ToString()))
                    {
                        result.Coordinate.Latitudine = finalResult.geometry.location.lat;
                        result.Coordinate.Longitudine = finalResult.geometry.location.lng;
                        return result;
                    }
                }
            }

            result.Status = ServiceResultStatus.ZERO_RESULTS;
            return result;
        }

        private ServiceResultStatus SetResultStatus(string status)
        {
            switch (status)
            {
                case "ERROR":
                    return ServiceResultStatus.ERROR;
                case "INVALID_REQUEST":
                    return ServiceResultStatus.INVALID_REQUEST;
                case "OK":
                    return ServiceResultStatus.OK;
                case "OVER_QUERY_LIMIT":
                    return ServiceResultStatus.OVER_QUERY_LIMIT;
                case "NOT_FOUND":
                    return ServiceResultStatus.NOT_DEFINED;
                case "REQUEST_DENIED":
                    return ServiceResultStatus.REQUEST_DENIED;
                default:
                    return ServiceResultStatus.ERROR;
            }
        }
    }
}
