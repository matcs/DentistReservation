namespace DentistReservation.Domain.Abstractions;

public class Result<TValue, TError>
{
    private readonly bool _isSuccess;
    
    public TValue? Value { get; }
    
    public TError? Error { get; }

    private Result(TValue value)
    {
        _isSuccess = true;
        Value = value;
    }

    private Result(TError error)
    {
        _isSuccess = false;
        Error = error;
    }

    public static implicit operator Result<TValue, TError>(TValue value) => new(value);

    public static implicit operator Result<TValue, TError>(TError error) => new(error);
    
    public bool HasError => !_isSuccess;
}