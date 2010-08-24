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
  public class T20_Districts
  {
    [Test]
    public void T001_GetDistrictsFromZip_22079()
    {
      var req = new Districts(ConfigData.API_KEY);
      var result = req.GetDistrictsFromZip("22079");
      Assert.IsTrue(result.Count() > 0, "GetDistrictsFromZip failed for 22079");
    }

    [Test]
    public void T010_GetZipsFromDistrict_NY_29()
    {
      var req = new Districts(ConfigData.API_KEY);
      var result = req.GetZipsFromDistrict("NY", 29);
      Assert.IsTrue(result.Count() > 0, "GetZipsFromDistrict failed for NY-29");
    }

    [Test]
    public void T020_GetDistrictFromLatLong_22079()
    {
      var req = new Districts(ConfigData.API_KEY);
      var result = req.GetDistrictFromLatLong(38.695, -77.214);
      Assert.IsTrue(result.Count() > 0, "GetDistrictFromLatLong failed for 38.695, -77.214");
    }
  }
}
