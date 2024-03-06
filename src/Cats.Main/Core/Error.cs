// <copyright file="Error.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

namespace Cats.Main.Core;

/// <summary>
/// Represents an error.
/// </summary>
public abstract record Error
{
    /// <summary>
    /// Many errors code.
    /// </summary>
    public const int ManyErrorsCode = -2_000_000_006;

    /// <summary>
    /// Gets the error code.
    /// </summary>
    public virtual int Code => 0;

    /// <summary>
    /// Gets the error message.
    /// </summary>
    public abstract string Message { get; }
}

/// <summary>
/// Represents an due to an exception.
/// </summary>
public record Exceptional(Exception Exception) : Error
{
    /// <inheritdoc/>
    public override string Message => Exception.Message;
}

/// <summary>
/// A list of errors.
/// </summary>
/// <param name="Errors"></param>
public record ManyErrors(List<Error> Errors) : Error
{
    /// <inheritdoc/>
    public override int Code => Error.ManyErrorsCode;

    /// <inheritdoc/>
    public override string Message => string.Join(", ", Errors);
}
