using CleanArchitecture.Domain.Abstractions;

namespace CleanArchitecture.Domain.Reviews;

public record Rating
{
    public static readonly Error Invalid = new("Rating.Invalid", "El rating es inválido");

    public int Value { get; init; }

    //constructor
    private Rating(int value) => Value = value;

    public static Result<Rating> Create(int value)
    {
        if (value < 1 || value > 5)
        {
            return Result.Failure<Rating>(Invalid);
        }

        return new Rating(value);
    }
}