// <copyright file="Option.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

namespace Cats.Main.Core;

/// <summary>
/// Represents a value that may not be present.
/// </summary>
public abstract record Option<T>
{
    /// <summary>
    /// Gets a value indicating whether the optional value is of the <see cref="Some"/> variant.
    /// </summary>
    public abstract bool IsSome { get; }

    /// <summary>
    /// Gets a value indicating whether the optional value is of the <see cref="None"/> variant.
    /// </summary>
    public bool IsNone => !IsSome;
}

/// <summary>
/// Represents the variant of the option type that has a value.
/// </summary>
/// <value></value>
public sealed record Some<T> : Option<T>
{
    private Some(T value) => Value = value;

    /// <summary>
    /// Gets the encapsulated value of the <see cref="Some"/> type.
    /// </summary>
    public T Value { get; }

    /// <inheritdoc/>
    public override bool IsSome => true;

    /// <summary>
    /// Creates a new instance of <see cref="Some"/>.
    /// </summary>
    /// <param name="value">The encapsulated value.</param>
    /// <returns>A new instance of <see cref="None"/>.</returns>
    public static Some<T> New(T value) => new(value);
}

/// <summary>
/// Represents the none variant of the option type.
/// </summary>
public sealed record None<T> : Option<T>
{
    private None() { }

    /// <inheritdoc/>
    public override bool IsSome => false;

    /// <summary>
    /// Returns a new instance of <see cref="None"/>.
    /// </summary>
    /// <returns>An instance of <see cref="None"/>.</returns>
    public static None<T> New() => new();
}
