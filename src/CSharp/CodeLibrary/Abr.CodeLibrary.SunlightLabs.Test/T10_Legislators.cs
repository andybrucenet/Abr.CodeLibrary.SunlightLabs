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
  public class T10_Legislators
  {
    [Test]
    public void T001_GetList_AllCurrent()
    {
      var req = new Legislators(ConfigData.API_KEY);
      var result = req.GetList(false);
      Assert.IsTrue(result.Count() > 0, "GetList failed for currently elected legislators");
    }

    [Test]
    public void T010_Get_LastName_Obama()
    {
      var req = new Legislators(ConfigData.API_KEY);
      var result = req.Get(true, Legislator.Props.lastname, "obama");
      Assert.IsTrue(result != null, "Get failed for obama");
    }

    [Test]
    public void T020_Search_BarackObama()
    {
      var req = new Legislators(ConfigData.API_KEY);
      var result = req.Search(true, "barack");
      Assert.IsTrue(result.Count() > 0, "Search failed for Barack");
    }

    [Test]
    public void T030_AllForZip_22079()
    {
      var req = new Legislators(ConfigData.API_KEY);
      var result = req.AllForZip("22079");
      Assert.IsTrue(result.Count() > 0, "AllForZip failed for 22079");
    }

    [Test]
    public void T040_AllForLatLong_22079()
    {
      var req = new Legislators(ConfigData.API_KEY);
      var result = req.AllForLatLong(38.695, -77.214);
      Assert.IsTrue(result.Count() > 0, "AllForLatLong failed for 38.695, -77.214");

      // ZIP 220789 should contain at least as many
      var result2 = req.AllForZip("22079");
      Assert.IsTrue(result2.Count() >= result.Count(), "AllForLatLong didn't return matching value for AllForZip (22079)");
    }
  }
}
