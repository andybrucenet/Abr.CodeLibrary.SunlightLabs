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
  public class T30_Committees
  {
    [Test]
    public void T001_GetList_All()
    {
      var req = new Committees(ConfigData.API_KEY);
      var result = req.GetList(Globals.CHAMBER_TYPE.HOUSE);
      Assert.IsTrue(result.Count() > 0, "GetList failed for HOUSE");
      result = req.GetList(Globals.CHAMBER_TYPE.JOINT);
      Assert.IsTrue(result.Count() > 0, "GetList failed for JOINT");
      result = req.GetList(Globals.CHAMBER_TYPE.SENATE);
      Assert.IsTrue(result.Count() > 0, "GetList failed for SENATE");
    }

    [Test]
    public void T010_Get_JSPR()
    {
      var req = new Committees(ConfigData.API_KEY);
      var result = req.Get("JSPR");
      Assert.IsNotNull(result, "Get failed to read info about JSPR");
    }

    [Test]
    public void T020_AllForLegislator_FirstOnList()
    {
      // first, read all legislators
      var legislators_req = new Legislators(ConfigData.API_KEY);
      var legislators_result = legislators_req.GetList(false);
      Assert.IsTrue(legislators_result.Count() > 0, "Legislators.GetList failed to return even one current legislator");

      // read all committee memberships, allowing an error to occur
      bool ok = false;
      foreach (var legislator in legislators_result)
      {
        var name = legislator.firstname + " " + legislator.lastname;
        try
        {
          // now read the committee membership for legislator
          var req = new Committees(ConfigData.API_KEY);
          var result = req.AllForLegislator(legislator.bioguide_id);
          Assert.Greater(result.Count(), 0, "AllForLegislator failed to return values for " + name);
          ok = true;
          break;
        }
        catch (Infrastructure.ErrorException)
        {
          // this is OK
          System.Console.WriteLine("Failed reading data for " + name + " (retrying)");
        }
      }
      if (!ok)
      {
        Assert.Fail("Could find no committee memberships in the entire house");
      }
    }
  }
}
