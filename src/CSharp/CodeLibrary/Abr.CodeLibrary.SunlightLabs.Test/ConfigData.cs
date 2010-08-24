using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.Test
{
  /// <summary>
  /// Configuration info for the tests
  /// </summary>
  public static class ConfigData
  {
    /// <summary>
    /// Given a configuration variable, return the value. Looks in the
    /// environment, then in the local file Abr.CodeLibrary.SunlightLabs.Test.dll.config
    /// if it exists.
    /// </summary>
    /// <param name="variable">Variable to find</param>
    /// <returns>The configurated value or throws exception upon failure</returns>
    public static string RetrieveVariable(string variable)
    {
      // check in environment (overrides)
      try
      {
        // anything in environment?
        string envVar = Environment.GetEnvironmentVariable(variable);

        // if not, we fall thru...
        if (!string.IsNullOrEmpty(envVar)) return envVar;
      }
      catch (Exception)
      {
        // fall thru to the next check
      }

      // look for local configuration file
      string localConfigFile = "./Abr.CodeLibrary.SunlightLabs.Test.dll.config";
      System.Xml.Linq.XDocument doc = null;
      System.IO.FileStream fs = System.IO.File.Open(
          localConfigFile,
          System.IO.FileMode.Open,
          System.IO.FileAccess.Read,
          System.IO.FileShare.ReadWrite
      );
      using (System.IO.TextReader tr = new System.IO.StreamReader(fs))
      {
        var xmlDocumentData = tr.ReadToEnd();
        doc = System.Xml.Linq.XDocument.Parse(xmlDocumentData);
      } //using

      // locate the settings
      var appSettingsElement = doc.Element("configuration");
      foreach (var addNode in appSettingsElement.Descendants("add"))
      {
        string key = (string)addNode.Attribute("key");
        string value = (string)addNode.Attribute("value");
        if (key == variable)
        {
          return value;
        }
      }
      throw new ApplicationException("Unable to locate configuration element for " + variable);
    }

      // check in current config
    /// <summary>
    /// The API key to use for the tests
    /// </summary>
    public static string API_KEY { get { return RetrieveVariable("Abr.CodeLibrary.SunlightLabs.Test.ApiKey"); } }
  }
}
