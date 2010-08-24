using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.DataStructures
{
  /// <summary>
  /// A single committee
  /// </summary>
  public class SubCommittee
  {
    #region Properties
    /// <summary>
    /// House, Senate, or Joint
    /// </summary>
    public Globals.CHAMBER_TYPE chamber { get; set; }

    /// <summary>
    /// Committee ID
    /// </summary>
    public string id { get; set; }

    /// <summary>
    /// Committee name
    /// </summary>
    public string name { get; set; }
    #endregion
  }

  /// <summary>
  /// A real committee can have sub-committees
  /// </summary>
  public class Committee : SubCommittee
  {
    /// <summary>
    /// Embedded class required by JSON syntax
    /// </summary>
    public class SubCommitteesWrapper {
      /// <summary>
      /// Contains the actual sub-committee information
      /// </summary>
      public SubCommittee committee { get; set; }
    }

    #region Properties
    /// <summary>
    /// List of subcommittees
    /// </summary>
    public List<SubCommitteesWrapper> subcommittees { get; set; }
    #endregion
  }

  /// <summary>
  /// Committee with sub-committees and membership
  /// </summary>
  public class CommitteeMembership : Committee
  {
    /// <summary>
    /// Embedded class required by JSON syntax
    /// </summary>
    public class LegislatorWrapper
    {
      /// <summary>
      /// Contains the actual member information
      /// </summary>
      public Legislator legislator { get; set; }
    }

    #region Properties
    /// <summary>
    /// List of members
    /// </summary>
    public List<LegislatorWrapper> members { get; set; }
    #endregion
  }
}
