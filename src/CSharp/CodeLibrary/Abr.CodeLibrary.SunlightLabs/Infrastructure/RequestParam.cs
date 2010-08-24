using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.Infrastructure
{
  /// <summary>
  /// A single request parameter
  /// </summary>
  public class RequestParam : IRequestParam
  {
    #region Instance vars
    private string _name;
    private Type _valueType;
    object _value;
    #endregion

    #region Constructors
    /// <summary>
    /// Uninitialized object
    /// </summary>
    public RequestParam()
    {
    }

    /// <summary>
    /// Initialized with name
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    public RequestParam(string name)
    {
      _name = name;
    }

    /// <summary>
    /// Name and type provided
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="valueType"><see cref="ValueType"/></param>
    public RequestParam(string name, Type valueType) : this(name) { _valueType = valueType; }

    /// <summary>
    /// Fully initialized
    /// </summary>
    /// <param name="name"><see cref="Name"/></param>
    /// <param name="valueType"><see cref="ValueType"/></param>
    /// <param name="value"><see cref="Value"/></param>
    public RequestParam(string name, Type valueType, object value) : this(name, valueType) { _value = value; }
    #endregion

    #region Properties
    /// <summary>
    /// Support get/set for Name
    /// </summary>
    public virtual string Name { get { return _name; } set { _name = value; } }

    /// <summary>
    /// The type for this value
    /// </summary>
    public virtual Type ValueType { get { return _valueType; } }

    /// <summary>
    /// The value associated with this parameter
    /// </summary>
    public virtual object Value { get { return _value; } set { _value = value; } }
    #endregion

    #region IRequestParam
    /// <summary>
    /// <see cref="IRequestParam.OutputName"/>
    /// </summary>
    public string OutputName { get { return Name; } }

    /// <summary>
    /// <see cref="IRequestParam.OutputValue"/>
    /// </summary>
    public virtual string OutputValue { get { return Value.ToString(); } }
    #endregion
  }
}
