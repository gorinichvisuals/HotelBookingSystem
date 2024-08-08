namespace Shared.Services.AppResult;

public class ApplicationResult : IEquatable<ApplicationResult>
{
    public bool IsSucceed { get; }
    public string ErrorMessage { get; }
    public int StatusCode { get; }

    protected ApplicationResult(bool isSuccess, string errorMessage, int statusCode)
    {
        IsSucceed = isSuccess;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
    }

    public static ApplicationResult<TResponse> Success<TResponse>(int statusCode, TResponse response)
    {
        return ApplicationResult<TResponse>.Success(statusCode, response);
    }

    public static ApplicationResult Success(int statusCode)
    {
        return new ApplicationResult(true, null!, statusCode);
    }

    public static ApplicationResult Fail(int statusCode, string errorMessage)
    {
        return new ApplicationResult(false, errorMessage, statusCode);
    }

    public override bool Equals(object? obj)
    {
        if (obj is ApplicationResult e)
        {
            return e.IsSucceed.Equals(IsSucceed);
        }

        return ReferenceEquals(this, obj);
    }

    public bool Equals(ApplicationResult? other)
    {
        if (other is null)
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return IsSucceed == other.IsSucceed && ErrorMessage == other.ErrorMessage;
    }

    public override int GetHashCode()
    {
        return (int)(IsSucceed.GetHashCode() ^ ErrorMessage?.GetHashCode()!);
    }

    public static bool operator ==(ApplicationResult left, ApplicationResult right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(ApplicationResult left, ApplicationResult right)
    {
        return !Equals(left, right);
    }

    public virtual object? GetResponse()
    {
        return null;
    }
}

public sealed class ApplicationResult<TResult> : ApplicationResult
{
    public TResult Result { get; }

    private ApplicationResult(bool isSuccess, string errorMessage, TResult result, int statusCode) : base(isSuccess, errorMessage, statusCode)
    {
        Result = result;
    }

    public static ApplicationResult<TResult> Success(int statusCode, TResult response)
    {
        return new ApplicationResult<TResult>(true, null!, response, statusCode);
    }

    public static new ApplicationResult<TResult> Fail(int statusCode, string errorMessage)
    {
        return new ApplicationResult<TResult>(false, errorMessage, default!, statusCode);
    }
}
