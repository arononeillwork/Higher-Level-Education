namespace HigherLevelEducation.Core.Responses;

public static class Errors
{
    public static Error MissingValue(string property)
    {
        return new Error("missing_data", $"{property} is required");
    }
}