// <copyright file="Try.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

namespace Cats.Main.Core;

/// <summary>
/// Represents a discriminated union of a result type that is either <see cref="Ok"/> or <see cref="Err"/>.
/// </summary>
/// <typeparam name="T">The type of the wrapped value of the result.</typeparam>
public abstract record Try<T>
{
    /// <summary>
    /// Gets a value indicating whether the <see cref="Result"/> is of the <see cref="Ok"/> variant.
    /// </summary>
    public abstract bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the <see cref="Result"/> is of the <see cref="Err"/> variant.
    /// </summary>
    public bool IsFailure => !IsSuccess;
}

/// <summary>
/// The success variant of the <see cref="Result"/> type.
/// </summary>
public sealed record Success<T> : Try<T>
{
    private Success(T value) => Value = value;

    /// <summary>
    /// Gets the encapsulated value of the result.
    /// </summary>
    public T Value { get; }

    /// <inheritdoc/>
    public override bool IsSuccess => true;

    /// <summary>
    /// Creates a new instance of the <see cref="Ok"/> result type.
    /// </summary>
    /// <param name="value">The encapsulated value of the result.</param>
    /// <returns>A new instance of <see cref="Ok"/> result.</returns>
    public static Success<T> New(T value) => new(value);
}

/// <summary>
/// Represents the error variant of the <see cref="Result"/> type.
/// </summary>
/// <typeparam name="T">The type of the wrapped value of the result.</typeparam>
public sealed record Failure<T> : Try<T>
{
    private Failure(Error error) => Error = error;

    /// <summary>
    /// Gets the encapsulated error of the <see cref="Err"/> result type.
    /// </summary>
    public Error Error { get; }

    /// <inheritdoc/>
    public override bool IsSuccess => false;

    /// <summary>
    /// Creates a new instance of <see cref="Err"/>.
    /// </summary>
    /// <param name="error">The encapsulated error.</param>
    /// <returns>An instance of <see cref="Err"/>.</returns>
    public static Failure<T> New(Error error) => new(error);
}
