using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.Infrastructure
{
  /// <summary>
  /// A single request parameter
  /// </summary>
  public interface IRequestParam
  {
    /// <summary>
    /// The name associated with this request parameter
    /// </summary>
    string OutputName { get; }

    /// <summary>
    /// The value associated with this request parameter
    /// </summary>
    string OutputValue { get; }
  }
}
