using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.Test
{
  /// <summary>
  /// Dummy REST request used to test invalid calls
  /// </summary>
  public class DummyRESTReq: Infrastructure.RESTReq
  {

    /// <summary>
    /// Create the dummy object
    /// </summary>
    /// <param name="apiKey"></param>
    /// <param name="apiNoun"></param>
    public DummyRESTReq(string apiKey, string apiNoun) : base(apiKey, apiNoun) { }

    /// <summary>
    /// Perform a dummy REST call
    /// </summary>
    /// <param name="apiVerb">Verb to invoke</param>
    /// <param name="parms">List of parameters</param>
    /// <returns></returns>
    public System.IO.Stream DoRequest(string apiVerb, params object[] parms)
    {
      return base.Request(apiVerb, parms);
    }
  }
}
