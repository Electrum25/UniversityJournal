namespace UniversityJournal.Core.Common
{
    public class Result<T>
    {
        public T Value { get; private set; } = default!;
        public bool IsSuccess { get; private set; }
        public string Error { get; private set; } = string.Empty;

        public static Result<T> Success(T value) => new Result<T> { Value = value, IsSuccess = true };
        public static Result<T> Failure(string error) => new Result<T> { Error = error, IsSuccess = false };
    }
}