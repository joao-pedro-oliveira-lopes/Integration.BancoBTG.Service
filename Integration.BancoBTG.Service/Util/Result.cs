namespace Integration.BancoBTG.Service.Util
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string Error { get; }
        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, string error)
        {
            if (isSuccess && !string.IsNullOrEmpty(error))
                throw new InvalidOperationException();
            if (!isSuccess && string.IsNullOrEmpty(error))
                throw new InvalidOperationException();

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success()
        {
            return new Result(true, String.Empty);
        }

        public static Result Failure(string error)
        {
            return new Result(false, error);
        }
    }

    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal Result(bool isSuccess, string error, T value)
            : base(isSuccess, error)
        {
            _value = value;
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, String.Empty, value);
        }

        public static new Result<T> Failure(string error)
        {
            return new Result<T>(false, error, default);
        }
    }
}
