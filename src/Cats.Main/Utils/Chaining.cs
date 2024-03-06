// <copyright file="Chaining.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

using Cats.Main.Core;

namespace Cats.Main.Utils;

/// <summary>
/// Provides extension methods for functional chaining operations.
/// </summary>
public static class Chaining
{
    /// <summary>
    /// Maps a value of type <typeparamref name="TIn"/> to a value of type <typeparamref name="TOut"/> using the provided function.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="t">The input value.</param>
    /// <param name="f">The function to apply.</param>
    /// <returns>The result of applying the function to the input value.</returns>
    public static TOut Map<TIn, TOut>(this TIn t, Func<TIn, TOut> f) => f(t);

    /// <summary>
    /// Applies two functions to the input value and combines their results using the provided function.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="T1">The type of the first function result.</typeparam>
    /// <typeparam name="T2">The type of the second function result.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="t">The input value.</param>
    /// <param name="f1">The first function to apply.</param>
    /// <param name="f2">The second function to apply.</param>
    /// <param name="fout">The function to combine the results of the first two functions.</param>
    /// <returns>The result of combining the results of the two functions.</returns>
    public static TOut Fork<TIn, T1, T2, TOut>(
        this TIn t,
        Func<TIn, T1> f1,
        Func<TIn, T2> f2,
        Func<T1, T2, TOut> fout
    ) => fout(f1(t), f2(t));

    /// <summary>
    /// Applies multiple functions to the input value and combines their results using the provided function.
    /// </summary>
    /// <typeparam name="TStart">The input type.</typeparam>
    /// <typeparam name="TMiddle">The intermediate type.</typeparam>
    /// <typeparam name="TEnd">The output type.</typeparam>
    /// <param name="t">The input value.</param>
    /// <param name="joinFunc">The function to combine the results of the prong functions.</param>
    /// <param name="prongs">The prong functions to apply.</param>
    /// <returns>The result of combining the results of the prong functions.</returns>
    public static TEnd Fork<TStart, TMiddle, TEnd>(
        this TStart t,
        Func<TMiddle, TEnd> joinFunc,
        params Func<TStart, TMiddle>[] prongs
    )
    {
        var intermediateValues = prongs.Select(x => x(t));
        var returnValue = joinFunc((TMiddle)intermediateValues);
        return returnValue;
    }

    /// <summary>
    /// Selects the first non-null result from a sequence of functions applied to the input value.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="t">The input value.</param>
    /// <param name="args">The functions to apply.</param>
    /// <returns>The result of the first non-null function application.</returns>
    public static TOut Alt<TIn, TOut>(this TIn t, params Func<TIn, TOut>[] args) =>
        args.Select(x => x(t)).First(x => x is not null);

    /// <summary>
    /// Composes two functions together.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TOutOld">The output type of the first function.</typeparam>
    /// <typeparam name="TOutNew">The output type of the composed function.</typeparam>
    /// <param name="t">The first function.</param>
    /// <param name="f">The second function.</param>
    /// <returns>A composed function.</returns>
    public static Func<TIn, TOutNew> Compose<TIn, TOutOld, TOutNew>(
        this Func<TIn, TOutOld> t,
        Func<TOutOld, TOutNew> f
    ) => x => f(t(x));

    /// <summary>
    /// Transforms and aggregates a sequence of input values.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TFilterOut">The filtered output type.</typeparam>
    /// <typeparam name="TFinalOut">The final output type.</typeparam>
    /// <param name="t">The input sequence.</param>
    /// <param name="transformer">The transformation function.</param>
    /// <param name="aggregator">The aggregation function.</param>
    /// <returns>The aggregated result of applying the transformation function.</returns>
    public static TFinalOut Transduce<TIn, TFilterOut, TFinalOut>(
        this IEnumerable<TIn> t,
        Func<IEnumerable<TIn>, IEnumerable<TFilterOut>> transformer,
        Func<IEnumerable<TFilterOut>, TFinalOut> aggregator
    ) => aggregator(transformer(t));

    /// <summary>
    /// Converts a transformation function into a transducer function.
    /// </summary>
    /// <typeparam name="TIn">The input type.</typeparam>
    /// <typeparam name="TO1">The intermediate type.</typeparam>
    /// <typeparam name="TO2">The output type.</typeparam>
    /// <param name="t">The transformation function.</param>
    /// <param name="aggregator">The aggregation function.</param>
    /// <returns>The transducer function.</returns>
    public static Func<IEnumerable<TIn>, TO2> ToTransducer<TIn, TO1, TO2>(
        this Func<IEnumerable<TIn>, IEnumerable<TO1>> t,
        Func<IEnumerable<TO1>, TO2> aggregator
    ) => x => aggregator(t(x));

#pragma warning disable SA1625
    /// <summary>
    /// Executes an action on the input value and returns the value.
    /// </summary>
    /// <typeparam name="T">The input type.</typeparam>
    /// <param name="t">The input value.</param>
    /// <param name="action">The action to execute.</param>
    /// <returns>The input value.</returns>
    public static T Tap<T>(this T t, Action<T> action)
    {
        action(t);
        return t;
    }
#pragma warning restore SA1625

    /// <summary>
    /// Binds an <see cref="Option{TIn}"/> to a function.
    /// </summary>
    /// <typeparam name="TIn">The input type of the option.</typeparam>
    /// <typeparam name="TOut">The output type.</typeparam>
    /// <param name="t">The input option.</param>
    /// <param name="f">The function to bind.</param>
    /// <returns>An <see cref="Option{TOut}"/> resulting from the bind operation.</returns>
    public static Option<TOut> Bind<TIn, TOut>(this Option<TIn> t, Func<TIn, TOut> f)
    {
        return t switch
        {
            Some<TIn> s when !EqualityComparer<TIn>.Default.Equals(s.Value, default)
                => Some<TOut>.New(f(s.Value)),
            Some<TIn> _ => None<TOut>.New(),
            _ => None<TOut>.New(),
        };
    }
}
