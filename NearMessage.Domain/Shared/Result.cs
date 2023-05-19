using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearMessage.Domain.Shared;

public class Result
{
    public Result(bool isSucceeded, List<Exception>? errors)
    {
        IsSucceeded = isSucceeded;
        Errors = errors;
    }

    public bool IsSucceeded { get; init; }

    public List<Exception>? Errors { get; init; }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(List<Exception> errors)
    {
        return new Result(false, errors);
    }
}


public class Result<T>
{
    public Result(bool isSucceeded, List<Exception>? errors, T? data)
    {
        IsSucceeded = isSucceeded;
        Errors = errors;
        Data = data;
    }

    public bool IsSucceeded { get; init; }

    public List<Exception>? Errors { get; init; }

    public T? Data { get; init; }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, null, data);
    }

    public static Result<T> Failure(List<Exception> errors)
    {
        return new Result<T>(false, errors, default);
    }
}