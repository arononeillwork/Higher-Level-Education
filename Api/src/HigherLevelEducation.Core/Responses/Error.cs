namespace HigherLevelEducation.Core.Responses;

public record Error(string Code, string Description)
{
    public static Error None => new(Code: string.Empty, Description: string.Empty);
}