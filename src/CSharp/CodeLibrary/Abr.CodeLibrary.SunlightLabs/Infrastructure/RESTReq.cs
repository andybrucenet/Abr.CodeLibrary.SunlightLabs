using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;

namespace Abr.CodeLibrary.SunlightLabs.Infrastructure
{
  /// <summary>
  /// Perform a generic REST request
  /// </summary>
  public class RESTReq
  {
    #region Mnemonics
    /// <summary>
    /// The base URL to the SunlightLabs API
    /// </summary>
    public static readonly string SUNLIGHT_LABS_API = "http://services.sunlightlabs.com/api";
    #endregion

    #region Instance vars
    private string _apiKey;
    private string _apiNoun;
    #endregion

    #region Constructors
    /// <summary>
    /// Create an object for querying the object
    /// </summary>
    /// <param name="apiKey"><see cref="ApiKey"/></param>
    /// <param name="apiNoun"><see cref="ApiNoun"/></param>
    protected RESTReq(string apiKey, string apiNoun)
    {
      _apiKey = apiKey;
      _apiNoun = apiNoun;
    }
    #endregion

    #region Properties
    /// <summary>
    /// By default, returns <see cref="SUNLIGHT_LABS_API"/>
    /// </summary>
    public virtual string SunlightLabsAPI { get { return SUNLIGHT_LABS_API; } }

    /// <summary>
    /// The API key associated with this developer
    /// </summary>
    public virtual string ApiKey { get { return _apiKey; } }

    /// <summary>
    /// Identifies the type of API being invoked ("legislators", "districts", etc.)
    /// </summary>
    public virtual string ApiNoun { get { return _apiNoun; } }

    /// <summary>
    /// Return the base URL for a request in form [SunlightLabsAPI]/[ApiMethod]?apikey=[ApiKey]
    /// </summary>
    /// <param name="apiVerb">The specific verb being invoked</param>
    /// <returns>The URL to use for requests</returns>
    public virtual string BaseRequestUrl(string apiVerb)
    {
      var api = SunlightLabsAPI;
      var noun = ApiNoun;
      var apiKey = ApiKey;
      if (string.IsNullOrEmpty(api)) throw new ArgumentNullException("Empty SunlightLabs API URL");
      if (string.IsNullOrEmpty(noun)) throw new ArgumentNullException("You must specify a valid noun like 'legislators'");
      if (string.IsNullOrEmpty(apiVerb)) throw new ArgumentNullException("You must specify a verb for the noun " + noun);
      if (string.IsNullOrEmpty(apiKey)) throw new ArgumentNullException("You must specify a SunlightLabs API key");
      return string.Format("{0}/{1}.{2}?apikey={3}", api, noun, apiVerb, apiKey);
    }
    #endregion

    #region Methods
    /// <summary>
    /// Given a set of input parameters, return the URI to invoke
    /// </summary>
    /// <param name="apiVerb">The specific verb being invoked</param>
    /// <param name="parms">List of parameters to use for the call</param>
    /// <returns></returns>
    protected virtual Uri BuildRequestUri(string apiVerb, IRequestParam[] parms)
    {
      string url = BaseRequestUrl(apiVerb);
      if (parms != null)
      {
        foreach (var parm in parms)
        {
          url += "&" + parm.OutputName + "=" + parm.OutputValue;
        }
      }
      return new Uri(url);
    }

    /// <summary>
    /// Given an input URI, return the response stream that should contain data
    /// </summary>
    /// <param name="requestUri">The URI to query</param>
    /// <returns>Response stream for reading data</returns>
    protected virtual System.IO.Stream GetResponseStream(Uri requestUri)
    {
      try
      {
        // get the response
        var request = System.Net.WebRequest.Create(requestUri);

        // extract the response stream
        return request.GetResponse().GetResponseStream();
      }
      catch (System.Net.WebException ex)
      {
        if (ex.Status == System.Net.WebExceptionStatus.ProtocolError)
        {
          var response = ex.Response as System.Net.HttpWebResponse;
          if (response != null)
          {
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
              throw new AuthenticationException("Missing or invalid SunlightLabs API key", ex);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
              throw new NotFoundException(response.StatusDescription, ex);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
              throw new ErrorException(response.StatusDescription, ex);
            }
          }
        }

        throw;
      }
    }

    /// <summary>
    /// Perform a request to get the SunlightLabs data
    /// </summary>
    /// <param name="apiVerb">The specific verb being invoked</param>
    /// <param name="parms">List of parameters to use for the call</param>
    /// <returns></returns>
    protected virtual System.IO.Stream Request(string apiVerb, IRequestParam[] parms)
    {
      // build the request string
      Uri requestUri = BuildRequestUri(apiVerb, parms);

      // get the output stream
      return GetResponseStream(requestUri);
    }

    /// <summary>
    /// Given a set of parameters that could either be <c>params</c> form or a valid
    /// list of IRequestParam objects, normalize them to be a an IRequestParam array.
    /// </summary>
    /// <param name="parms">List of parameters to analyze</param>
    /// <returns>Normalized parameters</returns>
    protected virtual Infrastructure.IRequestParam[] NormalizedParms(params object[] parms)
    {
      // if we are already of the correct type, use that
      var result = parms as Infrastructure.IRequestParam[];
      if (result != null)
      {
        return result;
      }

      // if we have exactly one parameter, allow that to be a list of objects
      if (parms.Count() == 1)
      {
        var requestParams = parms[0] as IRequestParam[];
        if (requestParams != null)
        {
          return requestParams;
        }
      }

      // is this valid?
      if (0 != (parms.Count() % 2))
      {
        throw new ArgumentException("Parameters must be passed as [PropertyInfo],[Value] pairs");
      }

      // create the parameters
      List<IRequestParam> the_parms = new List<IRequestParam>();
      int parm_idx = 0;
      while (parm_idx < parms.Count())
      {
        // deref
        object parmInfo = parms[parm_idx++];
        object parmValue = parms[parm_idx++];

        // validate
        if (parmInfo == null)
        {
          throw new ArgumentNullException("Parameter info #" + (parm_idx - 2) + " is NULL");
        } //if

        // is this a PropertyInfo
        string parmName;
        PropertyInfo propertyInfo = parmInfo as PropertyInfo;
        if (propertyInfo == null)
        {
          // assume it's a printable name
          parmName = parmInfo.ToString();
        }
        else
        {
          // get the name from the property
          parmName = propertyInfo.Name;
        } //if
        if (string.IsNullOrEmpty(parmName))
        {
          throw new ArgumentNullException("Parameter info #" + (parm_idx - 2) + " does not return a valid property name");
        } //if

        // now create the request parameter dynamically
        IRequestParam requestParam = null;
        if (propertyInfo == null)
        {
          // just make a simple request parameter
          requestParam = new RequestParam(parmName, typeof(string), parmValue);
        }
        else
        {
          // use the property info type to get the type we want this to be coerced to
          requestParam = new RequestParam(parmName, propertyInfo.PropertyType, parmValue);
        }

        // add it
        the_parms.Add(requestParam);
      }

      // convert to array
      return the_parms.ToArray();
    }

    /// <summary>
    /// Extension: allow caller to pass in just a list of variables and we'll sort
    /// them out.
    /// </summary>
    /// <param name="apiVerb">The specific verb being invoked</param>
    /// <param name="parms">Should be sets of (name,value) pairs</param>
    /// <returns></returns>
    protected virtual System.IO.Stream Request(string apiVerb, params object[] parms)
    {
      // make the call
      var the_parms = NormalizedParms(parms);
      return Request(apiVerb, the_parms.ToArray());
    }

    /// <summary>
    /// Create the serializer to use for returned JSON data
    /// </summary>
    protected virtual JsonSerializer CreateSerializer
    {
      get
      {
        var result = new JsonSerializer();
        result.MissingMemberHandling = MissingMemberHandling.Error;
        return result;
      }
    }

    /// <summary>
    /// Given a new parameter and an existing set, insert the new parameter
    /// </summary>
    /// <param name="requestParm">New parameter to insert</param>
    /// <param name="parms">Existing parameters to process</param>
    /// <returns>Updated list of parameters</returns>
    protected virtual object[] InsertParms(Infrastructure.IRequestParam requestParm, object[] parms)
    {
      var normalizedParms = NormalizedParms(parms);
      var result = new List<Infrastructure.IRequestParam>(normalizedParms);
      result.Insert(0, requestParm);
      return result.ToArray();
    }

    /// <summary>
    /// Standard processing for any type of request
    /// </summary>
    /// <param name="response">The stream from the Web service</param>
    /// <returns>The finalized object</returns>
    protected virtual T HandleResponse<T>(System.IO.Stream response)
    {
      var serializer = CreateSerializer;
      using (var sr = new System.IO.StreamReader(response))
      {
        using (var jr = new JsonTextReader(sr))
        {
          var result = serializer.Deserialize<T>(jr);
          return result;
        }
      }
    }
    #endregion
  }
}
