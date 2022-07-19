using System.Net;

namespace BTT.Api.Infrastructure;

public class ApiResult
{
    public bool IsSuccess { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Message { get; set; }

    public ApiResult(bool isSuccess, HttpStatusCode statusCode, string? message = null)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Message = message;
    }
}

public class ApiResult<TData> : ApiResult where TData : class
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TData Data { get; set; }

    public ApiResult(bool isSuccess, HttpStatusCode statusCode, TData data, string? message = null)
        : base(isSuccess, statusCode, message)
    {
        Data = data;
    }
}