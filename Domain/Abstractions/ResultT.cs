namespace Domain.Abstractions
{
    public class ResultT<T> : Result
    {
        public T? Value { get; }

        protected ResultT(T? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            Value = value;
        }

        public static ResultT<T> Success(T value) => new(value, true, Error.None);

        public static new ResultT<T> Failure(Error error) => new(default, false, error);

        public static implicit operator ResultT<T>(Error error) => Failure(error);
    }
}