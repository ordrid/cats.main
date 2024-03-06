// <copyright file="Either.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

namespace Cats.Main.Core;

/// <summary>
/// The Either type represents values with two possibilities.
/// </summary>
/// <typeparam name="T1">The type of <see cref="Left{T1, T2}"/> variant of <see cref="Either{T1, T2}"/>.</typeparam>
/// <typeparam name="T2">The type of <see cref="Right{T1, T2}"/> variant of <see cref="Either{T1, T2}"/>.</typeparam>
public abstract record Either<T1, T2>
{
    /// <summary>
    /// Gets a value indicating whether <see cref="Either{T1, T2}"/> is of the <see cref="Left{T1, T2}"/> variant.
    /// </summary>
    public abstract bool IsLeft { get; }

    /// <summary>
    /// Gets a value indicating whether <see cref="Either{T1, T2}"/> is of the <see cref="Right{T1, T2}"/> variant.
    /// </summary>
    public bool IsRight => !IsLeft;
}

/// <summary>
/// Represents the left variant of the <see cref="Either{T1, T2}"/> type, which conventionally holds an error value.
/// </summary>
public sealed record Left<T1, T2> : Either<T1, T2>
{
    private Left(T1 value) => Value = value;

    /// <summary>
    /// Gets the encapsulated value of the <see cref="Left{T1, T2}"/> variant.
    /// </summary>
    public T1 Value { get; }

    /// <inheritdoc/>
    public override bool IsLeft => true;

    /// <summary>
    /// Gets a new instance of the <see cref="Left{T1, T2}"/> variant.
    /// </summary>
    /// <param name="value">The encapsulated value.</param>
    /// <returns>A new instance of the left variant.</returns>
    public static Left<T1, T2> New(T1 value) => new(value);
}

/// <summary>
/// Represents the left variant of the <see cref="Either{T1, T2}"/> type, which conventionally holds a success value.
/// </summary>
public sealed record Right<T1, T2> : Either<T1, T2>
{
    private Right(T2 value) => Value = value;

    /// <summary>
    /// Gets the encapsulated value of the <see cref="Right{T1, T2}"/> variant.
    /// </summary>
    public T2 Value { get; }

    /// <inheritdoc/>
    public override bool IsLeft => false;

    /// <summary>
    /// Gets a new instance of the <see cref="Right{T1, T2}"/> variant.
    /// </summary>
    /// <param name="value">The encapsulated value.</param>
    /// <returns>A new instance of the right variant.</returns>
    public static Right<T1, T2> New(T2 value) => new(value);
}
