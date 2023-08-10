using Microsoft.AspNetCore.Mvc;
using Notes.Application.Results;

namespace Notes.Api.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result) =>
        new ObjectResult(result.IsSuccess ? result.Data : result.ErrorMessage)
            { StatusCode = (int?)result.HttpStatusCode };
}