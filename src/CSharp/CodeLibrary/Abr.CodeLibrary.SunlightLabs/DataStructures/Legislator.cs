using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.DataStructures
{
  /// <summary>
  /// A single legislator
  /// </summary>
  public class Legislator
  {
    #region Properties support class
    /// <summary>
    /// Defines all known properties for the <seealso cref="Legislator"/> class
    /// </summary>
    public static class Props
    {
      #region Instance vars
      private static Legislator s_value = new Legislator();
      #endregion

      #region Properties
      /// <summary>
      /// Title held by this legislator, either Sen or Rep
      /// </summary>
      public static PropertyInfo title { get { return Globals.PropertyInfo(s_value, "title"); } }

      /// <summary>
      /// Legislator's first name
      /// </summary>
      public static PropertyInfo firstname { get { return Globals.PropertyInfo(s_value, "firstname"); } }

      /// <summary>
      /// Legislator's middle name or initial
      /// </summary>
      public static PropertyInfo middlename { get { return Globals.PropertyInfo(s_value, "middlename"); } }

      /// <summary>
      /// Legislator's last name
      /// </summary>
      public static PropertyInfo lastname { get { return Globals.PropertyInfo(s_value, "lastname"); } }

      /// <summary>
      /// Legislator's suffix (Jr., III, etc.) 
      /// </summary>
      public static PropertyInfo name_suffix { get { return Globals.PropertyInfo(s_value, "name_suffix"); } }

      /// <summary>
      /// Preferred nickname of legislator (if any)
      /// </summary>
      public static PropertyInfo nickname { get { return Globals.PropertyInfo(s_value, "nickname"); } }

      /// <summary>
      /// Legislator's political party (D, I, or R)
      /// </summary>
      public static PropertyInfo party { get { return Globals.PropertyInfo(s_value, "party"); } }

      /// <summary>
      /// 2 letter abbreviation of legislator's state
      /// </summary>
      public static PropertyInfo state { get { return Globals.PropertyInfo(s_value, "state"); } }

      /// <summary>
      /// If legislator is a representative, their district. 0 is used for At-Large districts
      /// </summary>
      public static PropertyInfo district { get { return Globals.PropertyInfo(s_value, "district"); } }

      /// <summary>
      /// Is this an at-large district?
      /// </summary>
      public static PropertyInfo IsAtLarge { get { return Globals.PropertyInfo(s_value, "IsAtLarge"); } }

      /// <summary>
      /// TRUE if legislator is currently serving, FALSE if legislator is no longer in office due to defeat/resignation/death/etc.
      /// </summary>
      public static PropertyInfo in_office { get { return Globals.PropertyInfo(s_value, "in_office"); } }

      /// <summary>
      /// M or F
      /// </summary>
      public static PropertyInfo gender { get { return Globals.PropertyInfo(s_value, "gender"); } }

      /// <summary>
      /// Congressional office phone number
      /// </summary>
      public static PropertyInfo phone { get { return Globals.PropertyInfo(s_value, "phone"); } }

      /// <summary>
      /// Congressional office fax number
      /// </summary>
      public static PropertyInfo fax { get { return Globals.PropertyInfo(s_value, "fax"); } }

      /// <summary>
      /// URL of Congressional website
      /// </summary>
      public static PropertyInfo website { get { return Globals.PropertyInfo(s_value, "website"); } }

      /// <summary>
      /// URL of web contact form
      /// </summary>
      public static PropertyInfo webform { get { return Globals.PropertyInfo(s_value, "webform"); } }

      /// <summary>
      /// Legislator's email address (if known)
      /// </summary>
      public static PropertyInfo email { get { return Globals.PropertyInfo(s_value, "email"); } }

      /// <summary>
      /// Legislator's Washington DC Office Address
      /// </summary>
      public static PropertyInfo congress_office { get { return Globals.PropertyInfo(s_value, "congress_office"); } }

      /// <summary>
      /// Legislator ID assigned by [http://bioguide.congress.gov/biosearch/biosearch.asp Congressional Biographical Directory] (also used by Washington Post/NY Times)
      /// </summary>
      public static PropertyInfo bioguide_id { get { return Globals.PropertyInfo(s_value, "bioguide_id"); } }

      /// <summary>
      /// Legislator ID assigned by [http://votesmart.org Project Vote Smart]
      /// </summary>
      public static PropertyInfo votesmart_id { get { return Globals.PropertyInfo(s_value, "votesmart_id"); } }

      /// <summary>
      /// [http://fec.gov Federal Election Commission] ID
      /// </summary>
      public static PropertyInfo fec_id { get { return Globals.PropertyInfo(s_value, "fec_id"); } }

      /// <summary>
      /// ID assigned by [http://govtrack.us Govtrack.us]
      /// </summary>
      public static PropertyInfo govtrack_id { get { return Globals.PropertyInfo(s_value, "govtrack_id"); } }

      /// <summary>
      /// ID provided by [http://opensecrets.org Center for Responsive Politics]
      /// </summary>
      public static PropertyInfo crp_id { get { return Globals.PropertyInfo(s_value, "crp_id"); } }

      /// <summary>
      /// Official news feed
      /// </summary>
      public static PropertyInfo official_rss { get { return Globals.PropertyInfo(s_value, "official_rss"); } }

      /// <summary>
      /// Performer ID on [http://eventful.com eventful.com]
      /// </summary>
      public static PropertyInfo eventful_id { get { return Globals.PropertyInfo(s_value, "eventful_id"); } }

      /// <summary>
      /// URL of Legislator's entry on [http://congresspedia.org Congresspedia]
      /// </summary>
      public static PropertyInfo congresspedia_url { get { return Globals.PropertyInfo(s_value, "congresspedia_url"); } }

      /// <summary>
      /// Congressperson's official [http://twitter.com Twitter] account
      /// </summary>
      public static PropertyInfo twitter_id { get { return Globals.PropertyInfo(s_value, "twitter_id"); } }

      /// <summary>
      /// Congressperson's official [http://youtube.com Youtube] account
      /// </summary>
      public static PropertyInfo youtube_url { get { return Globals.PropertyInfo(s_value, "youtube_url"); } }

      /// <summary>
      /// For senators I, II, or III depending on the Senator's election term
      /// </summary>
      public static PropertyInfo senate_class { get { return Globals.PropertyInfo(s_value, "senate_class"); } }

      /// <summary>
      /// YYYY-MM-DD formatted birth date
      /// </summary>
      public static PropertyInfo birthdate { get { return Globals.PropertyInfo(s_value, "birthdate"); } }
      #endregion
    }
    #endregion

    #region Properties
    /// <summary>
    /// Title held by this legislator, either Sen or Rep
    /// </summary>
    public string title { get; set; }

    /// <summary>
    /// Legislator's first name
    /// </summary>
    public string firstname { get; set; }

    /// <summary>
    /// Legislator's middle name or initial
    /// </summary>
    public string middlename { get; set; }

    /// <summary>
    /// Legislator's last name
    /// </summary>
    public string lastname { get; set; }

    /// <summary>
    /// Legislator's suffix (Jr., III, etc.) 
    /// </summary>
    public string name_suffix { get; set; }

    /// <summary>
    /// Preferred nickname of legislator (if any)
    /// </summary>
    public string nickname { get; set; }

    /// <summary>
    /// Legislator's political party (D, I, or R)
    /// </summary>
    public Globals.POLITICAL_PARTY party { get; set; }

    /// <summary>
    /// 2 letter abbreviation of legislator's state
    /// </summary>
    public string state { get; set; }

    /// <summary>
    /// If legislator is a representative, their district. 0 is used for At-Large districts
    /// </summary>
    public string district { get; set; }

    /// <summary>
    /// Is this an at-large district?
    /// </summary>
    public bool IsAtLarge {
      get
      {
        if (string.IsNullOrEmpty(district)) return false;
        int output;
        if (!Int32.TryParse(district, out output)) return false;
        return output < 1;
      }
    }

    /// <summary>
    /// TRUE if legislator is currently serving, FALSE if legislator is no longer in office due to defeat/resignation/death/etc.
    /// </summary>
    public bool in_office { get; set; }

    /// <summary>
    /// M or F
    /// </summary>
    public Globals.GENDER gender { get; set; }

    /// <summary>
    /// Congressional office phone number
    /// </summary>
    public string phone { get; set; }

    /// <summary>
    /// Congressional office fax number
    /// </summary>
    public string fax { get; set; }

    /// <summary>
    /// URL of Congressional website
    /// </summary>
    public string website { get; set; }

    /// <summary>
    /// URL of web contact form
    /// </summary>
    public string webform { get; set; }

    /// <summary>
    /// Legislator's email address (if known)
    /// </summary>
    public string email { get; set; }

    /// <summary>
    /// Legislator's Washington DC Office Address
    /// </summary>
    public string congress_office { get; set; }

    /// <summary>
    /// Legislator ID assigned by [http://bioguide.congress.gov/biosearch/biosearch.asp Congressional Biographical Directory] (also used by Washington Post/NY Times)
    /// </summary>
    public string bioguide_id { get; set; }

    /// <summary>
    /// Legislator ID assigned by [http://votesmart.org Project Vote Smart]
    /// </summary>
    public string votesmart_id { get; set; }

    /// <summary>
    /// [http://fec.gov Federal Election Commission] ID
    /// </summary>
    public string fec_id { get; set; }

    /// <summary>
    /// ID assigned by [http://govtrack.us Govtrack.us]
    /// </summary>
    public string govtrack_id { get; set; }

    /// <summary>
    /// ID provided by [http://opensecrets.org Center for Responsive Politics]
    /// </summary>
    public string crp_id { get; set; }

    /// <summary>
    /// Official news feed
    /// </summary>
    public string official_rss { get; set; }

    /// <summary>
    /// Performer ID on [http://eventful.com eventful.com]
    /// </summary>
    public string eventful_id { get; set; }

    /// <summary>
    /// URL of Legislator's entry on [http://congresspedia.org Congresspedia]
    /// </summary>
    public string congresspedia_url { get; set; }

    /// <summary>
    /// Congressperson's official [http://twitter.com Twitter] account
    /// </summary>
    public string twitter_id { get; set; }

    /// <summary>
    /// Congressperson's official [http://youtube.com Youtube] account
    /// </summary>
    public string youtube_url { get; set; }

    /// <summary>
    /// For senators I, II, or III depending on the Senator's election term
    /// </summary>
    public string senate_class { get; set; }

    /// <summary>
    /// YYYY-MM-DD formatted birth date
    /// </summary>
    public DateTime birthdate { get; set; }
    #endregion
  }

  /// <summary>
  /// Response from a legislator Search function
  /// </summary>
  public class LegislatorSearch
  {
    /// <summary>
    /// Confidence in the returned value
    /// </summary>
    public double score { get; set; }

    /// <summary>
    /// The found legislator
    /// </summary>
    public Legislator legislator { get; set; }
  }
}
