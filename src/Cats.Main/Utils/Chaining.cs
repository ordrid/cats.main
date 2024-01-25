// <copyright file="Chaining.cs" company="Michael B. Espeña">
// Copyright (c) Michael B. Espeña. All rights reserved.
// </copyright>

using Cats.Main.Core;

namespace Cats.Main.Utils;

public static class Chaining
{
    public static TOut Map<TIn, TOut>(this TIn t, Func<TIn, TOut> f) => f(t);

    public static TOut Fork<TIn, T1, T2, TOut>(
        this TIn t,
        Func<TIn, T1> f1,
        Func<TIn, T2> f2,
        Func<T1, T2, TOut> fout
    ) => fout(f1(t), f2(t));

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

    public static TOut Alt<TIn, TOut>(this TIn t, params Func<TIn, TOut>[] args) =>
        args.Select(x => x(t)).First(x => x is not null);

    public static Func<TIn, TOutNew> Compose<TIn, TOutOld, TOutNew>(
        this Func<TIn, TOutOld> t,
        Func<TOutOld, TOutNew> f
    ) => x => f(t(x));

    public static TFinalOut Transduce<TIn, TFilterOut, TFinalOut>(
        this IEnumerable<TIn> t,
        Func<IEnumerable<TIn>, IEnumerable<TFilterOut>> transformer,
        Func<IEnumerable<TFilterOut>, TFinalOut> aggregator
    ) => aggregator(transformer(t));

    public static Func<IEnumerable<TIn>, TO2> ToTransducer<TIn, TO1, TO2>(
        this Func<IEnumerable<TIn>, IEnumerable<TO1>> t,
        Func<IEnumerable<TO1>, TO2> aggregator
    ) => x => aggregator(t(x));

    public static T Tap<T>(this T t, Action<T> action)
    {
        action(t);
        return t;
    }

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