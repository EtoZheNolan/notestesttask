using System.Net;

namespace Notes.Application.Results;

public class Result<T>
{
    public bool IsSuccess { get; }

    public string? ErrorMessage { get; }
    
    public T? Data { get; }

    public HttpStatusCode HttpStatusCode { get; }
    
    private Result(bool isSuccess, HttpStatusCode httpStatusCode, string? errorMessage, T? data)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Data = data;
        HttpStatusCode = httpStatusCode;
    }
    
    public static Result<T> Success(T data) 
        => new(true, HttpStatusCode.OK, default, data);
    

    public static Result<T> Failure(HttpStatusCode httpStatusCode, string errorMessage) 
        => new(false, httpStatusCode, errorMessage, default);
}