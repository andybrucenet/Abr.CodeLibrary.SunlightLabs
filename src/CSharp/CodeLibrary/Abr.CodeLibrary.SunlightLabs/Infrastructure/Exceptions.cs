using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Abr.CodeLibrary.SunlightLabs.Infrastructure
{
  /// <summary>
  /// Base class (under Silverlight, ApplicationException does not exist)
  /// </summary>
  public class BaseException: ApplicationException
  {
    /// <summary>
    /// Uninitialized exception
    /// </summary>
    public BaseException() {}

    /// <summary>
    /// Create based on message
    /// </summary>
    /// <param name="message">Message to display</param>
    public BaseException(string message): base(message) {}

    /// <summary>
    /// Create based on message and exception
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="innerException">Inner exception</param>
    public BaseException(string message, Exception innerException): base(message, innerException) {}
  }

  /// <summary>
  /// HTTP status code 403 from SunlightLabs
  /// </summary>
  public class AuthenticationException: BaseException
  {
    /// <summary>
    /// Uninitialized exception
    /// </summary>
    public AuthenticationException() {}

    /// <summary>
    /// Create based on message
    /// </summary>
    /// <param name="message">Message to display</param>
    public AuthenticationException(string message): base(message) {}

    /// <summary>
    /// Create based on message and exception
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="innerException">Inner exception</param>
    public AuthenticationException(string message, Exception innerException) : base(message, innerException) { }
  }

  /// <summary>
  /// HTTP code 404 (invalid URL)
  /// </summary>
  public class NotFoundException : BaseException
  {
    /// <summary>
    /// Uninitialized exception
    /// </summary>
    public NotFoundException() { }

    /// <summary>
    /// Create based on message
    /// </summary>
    /// <param name="message">Message to display</param>
    public NotFoundException(string message) : base(message) { }

    /// <summary>
    /// Create based on message and exception
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="innerException">Inner exception</param>
    public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
  }

  /// <summary>
  /// HTTP code 400 (response is plaintext error)
  /// </summary>
  public class ErrorException : BaseException
  {
    /// <summary>
    /// Uninitialized exception
    /// </summary>
    public ErrorException() { }

    /// <summary>
    /// Create based on message
    /// </summary>
    /// <param name="message">Message to display</param>
    public ErrorException(string message) : base(message) { }

    /// <summary>
    /// Create based on message and exception
    /// </summary>
    /// <param name="message">Message to display</param>
    /// <param name="innerException">Inner exception</param>
    public ErrorException(string message, Exception innerException) : base(message, innerException) { }
  }
}
