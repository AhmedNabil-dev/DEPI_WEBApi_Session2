namespace DEPI_WebApi_Session2.Results
{
    public class GenericResult<T> : Result
    {
        public T? Value { get; set; }
        public GenericResult(bool isSuccess, string error, T? value) : base(isSuccess, error)
          => Value = value;
        
        public static GenericResult<T> Fail(string error) => new GenericResult<T>(false, error, default);
        public static GenericResult<T> Success(T value) => new GenericResult<T>(true, string.Empty, value);
    }
}
