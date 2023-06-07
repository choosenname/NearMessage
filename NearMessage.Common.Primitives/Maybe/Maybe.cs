using System;

namespace NearMessage.Common.Primitives.Maybe;

public sealed class Maybe<TValue>
{
    private readonly TValue? _value;

    private Maybe(TValue? value) => _value = value;

    public static Maybe<TValue> None => new(default);

    public bool HasValue => !HasNoValue;

    public bool HasNoValue => _value is null;

    public TValue Value => HasValue
        ? _value!
        : throw new InvalidOperationException("The value can not be accessed because it does not exist.");

    public static implicit operator Maybe<TValue>(TValue? value) => From(value);

    public static Maybe<TValue> From(TValue? value) => new(value);
}