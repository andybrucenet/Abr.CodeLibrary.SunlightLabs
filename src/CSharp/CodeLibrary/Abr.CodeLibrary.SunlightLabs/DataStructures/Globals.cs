using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.DataStructures
{
  /// <summary>
  /// All defined enumerations shared by data structures
  /// </summary>
  public static class Globals
  {
    /// <summary>
    /// Identifies the political affiliation of a legislator
    /// </summary>
    public enum POLITICAL_PARTY
    {
      /// <summary>
      /// Unset
      /// </summary>
      NONE = 0,

      /// <summary>
      /// Democrat
      /// </summary>
      D = 1,

      /// <summary>
      /// Independent
      /// </summary>
      I = 2,

      /// <summary>
      /// Republican
      /// </summary>
      R = 3
    }

    /// <summary>
    /// Gender for an individual (wait...Gender is *not* Sex!)
    /// </summary>
    public enum GENDER
    {
      /// <summary>
      /// Unset (invalid)
      /// </summary>
      NONE = 0,

      /// <summary>
      /// Male
      /// </summary>
      M = 1,

      /// <summary>
      /// Female
      /// </summary>
      F = 2
    }

    /// <summary>
    /// For senators I, II, or III depending on the Senator's election term
    /// </summary>
    public enum SENATE_CLASS
    {
      /// <summary>
      /// Unset (invalid)
      /// </summary>
      NONE = 0,

      /// <summary>
      /// Senate class #1
      /// </summary>
      I = 1,

      /// <summary>
      /// Senate class #2
      /// </summary>
      II = 2,

      /// <summary>
      /// Senate class #3
      /// </summary>
      III = 3
    }

    /// <summary>
    /// Type of chamber (for committees)
    /// </summary>
    public enum CHAMBER_TYPE
    {
      /// <summary>
      /// Unset (invalid)
      /// </summary>
      NONE = 0,

      /// <summary>
      /// House
      /// </summary>
      HOUSE = 1,

      /// <summary>
      /// Senate
      /// </summary>
      SENATE = 2,

      /// <summary>
      /// Joint
      /// </summary>
      JOINT = 3
    }

    /// <summary>
    /// Return a property information for the given object
    /// </summary>
    /// <param name="value">Object to interrogate</param>
    /// <param name="propertyName">Name of the property to return</param>
    /// <returns></returns>
    public static PropertyInfo PropertyInfo(object value, string propertyName)
    {
      return value.GetType().GetProperty(propertyName);
    }
  }
}
