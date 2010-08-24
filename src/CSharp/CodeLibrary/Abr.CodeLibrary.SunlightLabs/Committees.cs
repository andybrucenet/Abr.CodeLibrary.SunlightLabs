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
  /// Interface to the Committees API
  /// </summary>
  public class Committees : Infrastructure.RESTReq
  {
    #region Instance vars
    /// <summary>
    /// Identifies the NOUN that this object implements
    /// </summary>
    public static readonly string NOUN = "committees";
    #endregion

    #region JSON classes
#pragma warning disable 1591
    public class JSONGetListResponse
    {
      public class JSONCommittee { public Committee committee { get; set; } }
      public class JSONCommittees { public List<JSONCommittee> committees { get; set; } }
      public JSONCommittees response { get; set; }
    }
    public class JSONGetResponse
    {
      public class JSONCommittee { public CommitteeMembership committee { get; set; } }
      public JSONCommittee response { get; set; }
    }
#pragma warning restore 1591
    #endregion

    #region Constructors
    /// <summary>
    /// Create the object
    /// </summary>
    /// <param name="apiKey"></param>
    public Committees(string apiKey) : base(apiKey, NOUN) { }
    #endregion

    #region Private methods
    private string ChamberString(Globals.CHAMBER_TYPE chamber)
    {
      var s = chamber.ToString().ToLowerInvariant();
      return Char.ToUpperInvariant(s[0]) + s.Substring(1);
    }
    #endregion

    #region Verbs
    /// <summary>
    /// Retrieve all committees for a chamber
    /// </summary>
    /// <param name="chamber">House, Senate, or Joint</param>
    /// <returns>Multiple committees</returns>
    public List<Committee> GetList(Globals.CHAMBER_TYPE chamber)
    {
      var response = HandleResponse<JSONGetListResponse>(base.Request(Verbs.GETLIST,
        "chamber", ChamberString(chamber)
      ));
      var result = new List<Committee>();
      response.response.committees.All(item => { result.Add(item.committee); return true; });
      return result;
    }

    /// <summary>
    /// Retrieve all information about a committee (including membership)
    /// </summary>
    /// <param name="id">Committee ID</param>
    /// <returns>All information about the committee</returns>
    public CommitteeMembership Get(string id)
    {
      var response = HandleResponse<JSONGetResponse>(base.Request(Verbs.GET,
        "id", id
      ));
      return response.response.committee;
    }

    /// <summary>
    /// Retrieve all committee membership information about a legislator
    /// </summary>
    /// <param name="bioguide_id">Legislator ID</param>
    /// <returns>All committee membership for the given legislator</returns>
    public List<Committee> AllForLegislator(string bioguide_id)
    {
      var response = HandleResponse<JSONGetListResponse>(base.Request(Verbs.ALLFORLEGISLATOR,
        "bioguide_id", bioguide_id
      ));
      var result = new List<Committee>();
      response.response.committees.All(item => { result.Add(item.committee); return true; });
      return result;
    }
    #endregion
  }
}
