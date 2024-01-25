// <copyright file="Error.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

namespace Cats.Main.Core;

/// <summary>
/// Represents an error.
/// </summary>
public abstract record Error(int Code, string Message);

/// <summary>
/// Represents an due to an exception.
/// </summary>
public record Exceptional(int Code, Exception Exception) : Error(Code, Exception.Message);
