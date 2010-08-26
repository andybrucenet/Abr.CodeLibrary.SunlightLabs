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

#if SILVERLIGHT
    /// <summary>
    /// Try invoking the service via a direct call
    /// </summary>
    /// <param name="requestUri">URI to invoke</param>
    protected virtual System.IO.Stream InvokeSilverlight(Uri requestUri)
    {
      System.Net.HttpWebResponse webResponse = null;

      string flag = "";
      try
      {
        // TODO: drive from config??
        const int DEFAULT_TIMEOUT = 1 * 30 * 1000;

        flag = "create_request";
        var webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(requestUri);

        // setup handler
        System.Exception lastException = null;
#if SILVERLIGHT_SUPPORTS_WAITFORSINGLEOBJECT
        System.Threading.ManualResetEvent allDone = new System.Threading.ManualResetEvent(false);
#else
        bool requestedCompleted = false;
        object requestCompletedMutex = new Object();
#endif
        flag = "create_response";
        IAsyncResult asyncResult = webRequest.BeginGetResponse(
          delegate(IAsyncResult result)
          {
            try
            {
              webResponse = (System.Net.HttpWebResponse)webRequest.EndGetResponse(result);
            }
            catch (System.Exception ex)
            {
              lastException = ex;
            }
            finally
            {
              try
              {
#if SILVERLIGHT_SUPPORTS_WAITFORSINGLEOBJECT
                rallDone.Set();
#else
                // no WAITFORSINGLEOBJECT, so we set our flag
                lock (requestCompletedMutex)
                {
                  requestedCompleted = true;
                } //lock
              }
              catch
              {
                // can't do anything here...too bad
              }
            }
#endif
          }
          , null
        );

#if SILVERLIGHT_SUPPORTS_WAITFORSINGLEOBJECT
        // setup a timeout
        flag = "wait_single_object";
        System.Threading.ThreadPool.RegisterWaitForSingleObject(
          asyncResult.AsyncWaitHandle,
          delegate(object state, bool timedOut)
          {
            if (timedOut)
            {
              webRequest.Abort();
            } //if
          },
          webRequest, DEFAULT_TIMEOUT, true
        );

        // wait for the response
        flag = "wait_one";
        allDone.WaitOne();
#else
        // seconds to wait
        int counter = 0;
        int limit = DEFAULT_TIMEOUT / 100;
        while (counter < limit)
        {
          // sleep
          try
          {
            System.Threading.Thread.Sleep(100);
          }
          catch (System.Threading.ThreadAbortException)
          {
            // capture this, return now, no exception passed up
          }
          catch (Exception)
          {
            // same notes as above
          } //try

          lock (requestCompletedMutex)
          {
            if (requestedCompleted) break;
          } //lock
          ++counter;
        } //while
        if (!requestedCompleted)
        {
          // end the request
          try { webRequest.Abort(); }
          catch { }

          // tell caller we failed
          throw new Exception("Timeout waiting for " + requestUri);
        } //if
#endif

        // if we had an exception, we're done
        if (lastException != null) throw lastException;

        // read the result
        flag = "access_response_stream";
        var stream = webResponse.GetResponseStream();
        return stream;
      }
      catch (Exception ex)
      {
        throw new Exception("Problem at " + flag + ": " + ex.Message, ex);
      }
    } //InvokeViaDirect
#endif

    /// <summary>
    /// Given an input URI, return the response stream that should contain data
    /// </summary>
    /// <param name="requestUri">The URI to query</param>
    /// <returns>Response stream for reading data</returns>
    protected virtual System.IO.Stream GetResponseStream(Uri requestUri)
    {
      try
      {
#if !SILVERLIGHT
        // get the response
        var request = System.Net.WebRequest.Create(requestUri);

        // extract the response stream
        return request.GetResponse().GetResponseStream();
#else
        // get the response
        var response = InvokeSilverlight(requestUri);
        return response;
#endif
      }
      catch (System.Net.WebException ex)
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
