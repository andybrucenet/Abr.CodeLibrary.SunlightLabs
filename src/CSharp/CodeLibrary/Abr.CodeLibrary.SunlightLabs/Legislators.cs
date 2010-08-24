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
  /// Interface to the Legislators API
  /// </summary>
  public class Legislators : Infrastructure.RESTReq
  {
    #region Instance vars
    /// <summary>
    /// Identifies the NOUN that this object implements
    /// </summary>
    public static readonly string NOUN = "legislators";
    #endregion

    #region JSON classes
#pragma warning disable 1591
    public class JSONGetResponse
    {
      public class JSONLegislator { public Legislator legislator { get; set; } }
      public JSONLegislator response { get; set; }
    }
    public class JSONGetListResponse
    {
      public class JSONLegislator { public Legislator legislator { get; set; } }
      public class JSONLegislators { public List<JSONLegislator> legislators { get; set; } }
      public JSONLegislators response { get; set; }
    }
    public class JSONSearchResponse
    {
      public class JSONLegislatorSearch { public LegislatorSearch result { get; set; } }
      public class JSONLegislatorsSearch { public List<JSONLegislatorSearch> results { get; set; } }
      public JSONLegislatorsSearch response { get; set; }
    }
#pragma warning restore 1591
    #endregion

    #region Constructors
    /// <summary>
    /// Create the object
    /// </summary>
    /// <param name="apiKey"></param>
    public Legislators(string apiKey) : base(apiKey, NOUN) { }
    #endregion

    #region Verbs
    /// <summary>
    /// Retrieve all info for a given legislator
    /// </summary>
    /// <param name="allLegislators">Should all legislators (including out-of-office) be included?</param>
    /// <param name="parms">List of parameters</param>
    /// <returns>Single legislator</returns>
    public Legislator Get(bool allLegislators, params object[] parms)
    {
      var new_parm = new Infrastructure.RequestParam("all_legislators", typeof(int), allLegislators ? 1 : 0);
      var new_parms = InsertParms(new_parm, parms);
      var response = HandleResponse<JSONGetResponse>(base.Request(Verbs.GET, new_parms));
      return response.response.legislator;
    }

    /// <summary>
    /// Retrieve all legislators
    /// </summary>
    /// <param name="allLegislators">Should all legislators (including out-of-office) be included?</param>
    /// <param name="parms">List of parameters</param>
    /// <returns>Multiple legislators meeting criteria</returns>
    public List<Legislator> GetList(bool allLegislators, params object[] parms)
    {
      var new_parm = new Infrastructure.RequestParam("all_legislators", typeof(int), allLegislators ? 1 : 0);
      var new_parms = InsertParms(new_parm, parms);
      var response = HandleResponse<JSONGetListResponse>(base.Request(Verbs.GETLIST, new_parms));
      var result = new List<Legislator>();
      response.response.legislators.All(item => { result.Add(item.legislator); return true; });
      return result;
    }

    /// <summary>
    /// Search for legislators
    /// </summary>
    /// <param name="allLegislators">Should all legislators (including out-of-office) be included?</param>
    /// <param name="name">Fuzzy search</param>
    /// <returns>Multiple criteria meeting criteria</returns>
    public List<LegislatorSearch> Search(bool allLegislators, string name)
    {
      var response = HandleResponse<JSONSearchResponse>(base.Request(Verbs.SEARCH,
        "all_legislators", allLegislators ? 1 : 0, "name", name
      ));
      var result = new List<LegislatorSearch>();
      response.response.results.All(item => { result.Add(item.result); return true; });
      return result;
    }

    /// <summary>
    /// Find legislator for 5-digit ZIP Code
    /// </summary>
    /// <param name="zipCode">5-digit ZIP Code</param>
    /// <returns>Multiple legislators in the ZIP Code</returns>
    public List<Legislator> AllForZip(string zipCode)
    {
      var response = HandleResponse<JSONGetListResponse>(base.Request(Verbs.ALLFORZIP,
        "zip", zipCode
      ));
      var result = new List<Legislator>();
      response.response.legislators.All(item => { result.Add(item.legislator); return true; });
      return result;
    }

    /// <summary>
    /// Find legislators for lat/lon
    /// </summary>
    /// <param name="latitude">Latitude</param>
    /// <param name="longitude">Longitude</param>
    /// <returns>Multiple legislators for the point</returns>
    public List<Legislator> AllForLatLong(double latitude, double longitude)
    {
      var response = HandleResponse<JSONGetListResponse>(base.Request(Verbs.ALLFORLATLONG,
        "latitude", latitude, "longitude", longitude
      ));
      var result = new List<Legislator>();
      response.response.legislators.All(item => { result.Add(item.legislator); return true; });
      return result;
    }
    #endregion
  }
}
