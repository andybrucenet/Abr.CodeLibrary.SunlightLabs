using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newtonsoft.Json;

namespace Abr.CodeLibrary.SunlightLabs.Infrastructure
{
  /// <summary>
  /// A basic "response" object to hold parsed JSON data
  /// </summary>
  public class JSONResponseI<T>
    where T : new()
  {
    #region Instance vars
    private List<T> _value = new List<T>();
    #endregion

    #region Properties
    /// <summary>
    /// The wrapped raw value for this JSON object
    /// </summary>
    protected List<T> Value { get { return _value; } }
    #endregion
  }

  /// <summary>
  /// A basic "response" object to hold parsed JSON data
  /// </summary>
  public class JSONResponse<T1,T2>
    where T1 : JSONResponseI<T2>
    where T2: new()
  {
    #region Properties
    /// <summary>
    /// The wrapped raw value for this JSON object
    /// </summary>
    public T1 response { get; set; }
    #endregion
  }
}
