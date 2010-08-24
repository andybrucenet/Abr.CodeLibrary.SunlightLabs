using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.DataStructures
{
  /// <summary>
  /// A single district
  /// </summary>
  public class District
  {
    #region Properties
    /// <summary>
    /// State abbreviation
    /// </summary>
    public string state { get; set; }

    /// <summary>
    /// District Number
    /// </summary>
    public int number { get; set; }
    #endregion
  }
}
