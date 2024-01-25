// <copyright file="Unwrapping.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

using Cats.Main.Core;

namespace Cats.Main.Utils;

public static class Unwrapping
{
    public static T Unwrap<T>(this Try<T> result) =>
        result switch
        {
            Success<T> ok => ok.Value,
            _ => throw new NotSupportedException("Cannot unwrap the value of an error.")
        };

    public static T Unwrap<T>(this Option<T> option) =>
        option switch
        {
            Some<T> some => some.Value,
            _ => throw new NotSupportedException("Cannot unwrap the value of None.")
        };

    public static T UnwrapOrElse<T>(this Try<T> result, T alternative) =>
        result switch
        {
            Success<T> ok => ok.Value,
            _ => alternative,
        };

    public static T UnwrapOrElse<T>(this Option<T> option, T alternative) =>
        option switch
        {
            Some<T> some => some.Value,
            _ => alternative,
        };
}
