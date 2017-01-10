using Microsoft.VisualStudio.TestTools.UnitTesting;
using Configuration;
using Rhino.Mocks;

namespace GoogleMapService.BLL.Tests
{
    [TestClass()]
    public class GoogleMapManagerTests
    {
        [TestMethod()]
        public void GetCoordinateWithGeocodeTest()
        {
            var result = GetGoogleMapManager().GetCoordinateWithGeocode("台北市重慶南路一段2號");

            Assert.AreEqual(result.Status, Models.ServiceResultStatus.OK);
            Assert.AreEqual(result.Coordinate.Latitudine, (decimal)25.046905);
            Assert.AreEqual(result.Coordinate.Longitudine, (decimal)121.5129069);
        }

        private IGoogleMapManager GetGoogleMapManager()
        {
            return Factories.GoogleMapFactory.GetGoogleMapManager(GetSettings());
        }

        private ISettings GetSettings()
        {
            var settings = MockRepository.GenerateStub<ISettings>();
            settings
                .Stub(o => o["GoogleMapGeocodeApiUrl"])
                .Return("https://maps.googleapis.com/maps/api/geocode/json?");
            //settings
            //    .Stub(o => o["GoogleMapApiKey"])
            //    .Return("");

            return settings;
        }
    }
}