using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs
{
  /// <summary>
  /// List of known verbs
  /// </summary>
  public static class Verbs
  {
    /// <summary>
    /// GET verb
    /// </summary>
    public static readonly string GET = "get";

    /// <summary>
    /// GETLIST verb
    /// </summary>
    public static readonly string GETLIST = "getList";

    /// <summary>
    /// SEARCH verb
    /// </summary>
    public static readonly string SEARCH = "search";

    /// <summary>
    /// All legislators in ZIP Code
    /// </summary>
    public static readonly string ALLFORZIP = "allForZip";

    /// <summary>
    /// All legislators for a lat/lon
    /// </summary>
    public static readonly string ALLFORLATLONG = "allForLatLong";

    /// <summary>
    /// GETDISTRICTSFROMZIP verb
    /// </summary>
    public static readonly string GETDISTRICTSFROMZIP = "getDistrictsFromZip";

    /// <summary>
    /// GETZIPSFROMDISTRICT verb
    /// </summary>
    public static readonly string GETZIPSFROMDISTRICT = "getZipsFromDistrict";

    /// <summary>
    /// GETDISTRICTFROMLATLONG verb
    /// </summary>
    public static readonly string GETDISTRICTFROMLATLONG = "getDistrictFromLatLong";

    /// <summary>
    /// ALLFORLEGISLATOR verb
    /// </summary>
    public static readonly string ALLFORLEGISLATOR = "allForLegislator";
  }
}
