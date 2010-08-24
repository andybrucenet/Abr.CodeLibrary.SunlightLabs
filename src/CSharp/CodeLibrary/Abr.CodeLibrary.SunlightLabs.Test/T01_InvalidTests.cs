using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NUnit.Framework;
using Abr.CodeLibrary.SunlightLabs.DataStructures;
using Abr.CodeLibrary.SunlightLabs.Infrastructure;

namespace Abr.CodeLibrary.SunlightLabs.Test
{
  [TestFixture]
  public class T01_InvalidTests
  {
    [Test]
    public void T001_NoAPIKey()
    {
      try
      {
        var req = new DummyRESTReq(null, Legislators.NOUN);
        req.DoRequest(Verbs.GET, Legislator.Props.lastname, "obama");
      }
      catch (ArgumentNullException)
      {
        System.Console.WriteLine("Caught no API key correctly");
      }
    }

    [Test]
    public void T010_NullValues()
    {
      try
      {
        System.Console.WriteLine("Call with no noun...");
        var req = new DummyRESTReq("123", null);
        req.DoRequest(Verbs.GET, Legislator.Props.lastname, "obama");
      }
      catch (ArgumentNullException)
      {
        System.Console.WriteLine("Caught no noun correctly");
      }

      try
      {
        System.Console.WriteLine("Call with no verb...");
        var req = new DummyRESTReq("123", "456");
        req.DoRequest(null, Legislator.Props.lastname, "obama");
      }
      catch (ArgumentNullException)
      {
        System.Console.WriteLine("Caught no verb correctly");
      }
    }

    [Test]
    public void T020_InvalidParameters()
    {
      try
      {
        System.Console.WriteLine("Call with invalid parameters (name only)...");
        var req = new DummyRESTReq("123", "456");
        req.DoRequest(Verbs.GET, "only passing the name");
      }
      catch (ArgumentException)
      {
        System.Console.WriteLine("Caught just passing the name OK");
      }

      try
      {
        System.Console.WriteLine("Call with invalid parameters (name is NULL)...");
        var req = new DummyRESTReq("123", "456");
        req.DoRequest(Verbs.GET, null, null);
      }
      catch (ArgumentNullException)
      {
        System.Console.WriteLine("Caught NULL name parameter correctly");
      }
    }

    [Test]
    public void T030_InvalidAPIKey()
    {
      try
      {
        System.Console.WriteLine("Call with invalid API key...");
        var req = new DummyRESTReq("123", Legislators.NOUN);
        req.DoRequest(Verbs.GET, Legislator.Props.lastname, "obama");
      }
      catch (AuthenticationException)
      {
        System.Console.WriteLine("Caught invalid API key OK");
      }
    }

    [Test]
    public void T040_InvalidAPINoun()
    {
      try
      {
        System.Console.WriteLine("Call with invalid API noun...");
        var req = new DummyRESTReq(ConfigData.API_KEY, "foobar");
        req.DoRequest(Verbs.GET, Legislator.Props.lastname, "obama");
      }
      catch (NotFoundException)
      {
        System.Console.WriteLine("Caught invalid API NOUN OK");
      }
    }

    [Test]
    public void T050_InvalidAPIParm()
    {
      try
      {
        System.Console.WriteLine("Call with invalid API parm...");
        var req = new DummyRESTReq(ConfigData.API_KEY, Legislators.NOUN);
        req.DoRequest(Verbs.GET, "foobar", "obama");
      }
      catch (ErrorException)
      {
        System.Console.WriteLine("Caught invalid API parm OK");
      }
    }
  }
}
