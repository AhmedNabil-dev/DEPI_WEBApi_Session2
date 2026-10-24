namespace DEPI_WebApi_Session2.Results
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public bool IsFailure => !IsSuccess;

        public string Error { get; set; } = string.Empty;

        public Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrEmpty(error))
                throw new InvalidOperationException();
            if (!isSuccess && string.IsNullOrEmpty(error))
                throw new InvalidOperationException();
            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Fail(string error) => new Result(false, error);
        public static Result success() => new Result(true, string.Empty);


    }
}
