// <copyright file="Unit.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

namespace Cats.Main.Core;

/// <summary>
/// The Unit type has exactly one value unit, and is used when there is no other meaningful value that could be returned.
/// </summary>
public readonly record struct Unit
{
    /// <summary>
    /// Gets an instance of <see cref="Unit"/>.
    /// </summary>
#pragma warning disable SA1307 // Accessible fields should begin with upper-case letter
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
#pragma warning disable IDE1006 // Naming Styles
    public static readonly Unit unit;
#pragma warning restore IDE1006 // Naming Styles
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter
#pragma warning restore SA1307 // Accessible fields should begin with upper-case letter
}
