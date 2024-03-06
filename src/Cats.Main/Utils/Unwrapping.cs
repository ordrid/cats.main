// <copyright file="Unwrapping.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

using Cats.Main.Core;

namespace Cats.Main.Utils;

/// <summary>
/// Provides extension methods for unwrapping values from <see cref="Try{T}"/> and <see cref="Option{T}"/>.
/// </summary>
public static class Unwrapping
{
    /// <summary>
    /// Unwraps the value from the <see cref="Try{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Try{T}"/>.</typeparam>
    /// <param name="result">The <see cref="Try{T}"/> instance.</param>
    /// <returns>The unwrapped value if the <see cref="Try{T}"/> represents a success; otherwise, throws <see cref="NotSupportedException"/>.</returns>
    public static T Unwrap<T>(this Try<T> result) =>
        result switch
        {
            Success<T> ok => ok.Value,
            _ => throw new NotSupportedException("Cannot unwrap the value of an error.")
        };

    /// <summary>
    /// Unwraps the value from the <see cref="Option{T}"/> instance.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Option{T}"/>.</typeparam>
    /// <param name="option">The <see cref="Option{T}"/> instance.</param>
    /// <returns>The unwrapped value if the <see cref="Option{T}"/> is Some; otherwise, throws <see cref="NotSupportedException"/>.</returns>
    public static T Unwrap<T>(this Option<T> option) =>
        option switch
        {
            Some<T> some => some.Value,
            _ => throw new NotSupportedException("Cannot unwrap the value of None.")
        };

    /// <summary>
    /// Unwraps the value from the <see cref="Try{T}"/> instance or returns an alternative value.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Try{T}"/>.</typeparam>
    /// <param name="result">The <see cref="Try{T}"/> instance.</param>
    /// <param name="alternative">The alternative value to return if the <see cref="Try{T}"/> represents an error.</param>
    /// <returns>The unwrapped value if the <see cref="Try{T}"/> represents a success; otherwise, returns the alternative value.</returns>
    public static T UnwrapOrElse<T>(this Try<T> result, T alternative) =>
        result switch
        {
            Success<T> ok => ok.Value,
            _ => alternative,
        };

    /// <summary>
    /// Unwraps the value from the <see cref="Option{T}"/> instance or returns an alternative value.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the <see cref="Option{T}"/>.</typeparam>
    /// <param name="option">The <see cref="Option{T}"/> instance.</param>
    /// <param name="alternative">The alternative value to return if the <see cref="Option{T}"/> is None.</param>
    /// <returns>The unwrapped value if the <see cref="Option{T}"/> is Some; otherwise, returns the alternative value.</returns>
    public static T UnwrapOrElse<T>(this Option<T> option, T alternative) =>
        option switch
        {
            Some<T> some => some.Value,
            _ => alternative,
        };
}
