using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

using Abr.CodeLibrary.SunlightLabs.DataStructures;

namespace Abr.CodeLibrary.SunlightLabs
{
  /// <summary>
  /// Interface to the Districts API
  /// </summary>
  public class Districts : Infrastructure.RESTReq
  {
    #region Instance vars
    /// <summary>
    /// Identifies the NOUN that this object implements
    /// </summary>
    public static readonly string NOUN = "districts";
    #endregion

    #region JSON classes
#pragma warning disable 1591
    public class JSONDistrictResponse
    {
      public class JSONDistrict { public District district { get; set; } }
      public class JSONDistricts { public List<JSONDistrict> districts { get; set; } }
      public JSONDistricts response { get; set; }
    }

    public class JSONZipResponse
    {
      public class JSONZips { public List<string> zips { get; set; } }
      public JSONZips response { get; set; }
    }
#pragma warning restore 1591
    #endregion

    #region Constructors
    /// <summary>
    /// Create the object
    /// </summary>
    /// <param name="apiKey"></param>
    public Districts(string apiKey) : base(apiKey, NOUN) { }
    #endregion

    #region Verbs
    /// <summary>
    /// Gets all districts that overlap the area for a given zip code
    /// </summary>
    /// <param name="zipCode">5-digit ZIP Code</param>
    /// <returns>Multiple legislators in the ZIP Code</returns>
    public List<District> GetDistrictsFromZip(string zipCode)
    {
      var response = HandleResponse<JSONDistrictResponse>(base.Request(Verbs.GETDISTRICTSFROMZIP,
        "zip", zipCode
      ));
      var result = new List<District>();
      response.response.districts.All(item => { result.Add(item.district); return true; });
      return result;
    }

    /// <summary>
    /// Gets all ZIPs within a given congressional district
    /// </summary>
    /// <param name="state">State abbreviation</param>
    /// <param name="district">District number (zero for At-Large)</param>
    /// <returns>All ZIP Codes in the district</returns>
    public List<string> GetZipsFromDistrict(string state, int district)
    {
      var response = HandleResponse<JSONZipResponse>(base.Request(Verbs.GETZIPSFROMDISTRICT,
        "state", state, "district", district
      ));
      return response.response.zips;
    }

    /// <summary>
    /// Find districts for lat/lon
    /// </summary>
    /// <param name="latitude">Latitude</param>
    /// <param name="longitude">Longitude</param>
    /// <returns>Multiple districtss for the point</returns>
    public List<District> GetDistrictFromLatLong(double latitude, double longitude)
    {
      var response = HandleResponse<JSONDistrictResponse>(base.Request(Verbs.GETDISTRICTFROMLATLONG,
        "latitude", latitude, "longitude", longitude
      ));
      var result = new List<District>();
      response.response.districts.All(item => { result.Add(item.district); return true; });
      return result;
    }
    #endregion
  }
}
