namespace HigherLevelEducation.Core;

public abstract class QuestionsApi
{
    public record CreateQuestionsModel(int ClassLevel, Subject Subject, Topic Topic, DifficultyLevel Difficulty, int Limit);
    
    public static List<string> CreateQuestions(CreateQuestionsModel model)
    {
        var (topic, difficulty, classLevel, numberOfQuestions) = (model.Topic, model.Difficulty, model.ClassLevel, model.Limit);

        var questions = new List<string>();
        var calculationSign = GetCalculationSign(topic);
        var random = new Random();

        var numberCriteria = GetNumberCriteriaBasedOnTopic(classLevel, topic);

        for (var i = 0; i < numberOfQuestions; i++)
        {
            var firstRandomNumber = GenerateRandomNumber(random, numberCriteria.FirstNumber, numberCriteria.IncludeDecimals, difficulty);
            var secondRandomNumber = GenerateRandomNumber(random, numberCriteria.SecondNumber, numberCriteria.IncludeDecimals, difficulty);

            var firstNumberToShow = Math.Max(firstRandomNumber, secondRandomNumber);
            var secondNumber = firstNumberToShow == firstRandomNumber ? secondRandomNumber : firstRandomNumber;

            var question = $"{firstNumberToShow},{calculationSign},{secondNumber}";

            questions.Add(question);
        }

        return questions;
    }

    public static string GetCalculationSign(Topic topic)
    {
        return topic switch
        {
            Topic.Multiplication => "x",
            Topic.Division => "÷",
            Topic.Addition => "+",
            Topic.Subtraction => "-",
            _ => throw new ArgumentException("Invalid topic")
        };
    }

    public static NumberCriteria GetNumberCriteriaBasedOnTopic(int classLevel, Topic topic)
    {
        return topic switch
        {
            Topic.Addition or Topic.Subtraction => classLevel switch
            {
                6 => new NumberCriteria(1000, 9999, IncludeDecimals: false),
                5 => new NumberCriteria(100, 999, true),
                4 => new NumberCriteria(1000, 9999, false),
                3 => new NumberCriteria(100, 999, false),
                _ => throw new ArgumentException("Invalid class level")
            },
            Topic.Multiplication => classLevel switch
            {
                6 => new NumberCriteria(1, 1000, true),
                5 => new NumberCriteria(1, 100, true),
                4 => new NumberCriteria(10, 999, false),
                3 => new NumberCriteria(1, 99, false),
                _ => throw new ArgumentException("Invalid class level")
            },
            Topic.Division => classLevel switch
            {
                6 => new NumberCriteria(1000, 9999, true),
                5 => new NumberCriteria(100, 999, true),
                4 => new NumberCriteria(100, 999, false),
                3 => new NumberCriteria(10, 99, false),
                _ => throw new ArgumentException("Invalid class level")
            },
            _ => throw new ArgumentException("Invalid topic"),
        };
    }

    private static double GenerateRandomNumber(Random random, int max, bool? includeDecimal, DifficultyLevel difficulty)
    {
        var min = difficulty switch
        {
            DifficultyLevel.Easy => 1,
            DifficultyLevel.Medium => max / 2,
            DifficultyLevel.Hard => (int)(max * 0.75),
            _ => throw new ArgumentException("Invalid difficulty level")
        };
        
        var number = random.Next(min, max);

        if (includeDecimal == true && random.Next(2) == 0)
        {
            return number / 10.0;
        }

        return number;
    }

    public record NumberCriteria(
        int FirstNumber,
        int SecondNumber,
        bool? IncludeDecimals = false);
}

public enum DifficultyLevel
{
    Easy,
    Medium,
    Hard
}

public enum Subject
{
    Maths
}

public enum Topic
{
    // Topics within the Operations Strand
    Addition,
    Subtraction,
    Multiplication,
    Division,

    Fractions,
    DecimalsAndPercentages, // 5th & 6th only   decimals only for 3rd 4th

    PlaceValue,
    NumberTheory, // 5th & 6th only 

    //// Units for Algebra
    //ExtendingPatterns,
    //NumberPatternsAndSequences,
    //NumberSentences,
    //RulesAndProperties,
    //Variables,
    //Equations,

    //// Units for Shape and Space
    //SpatialAwareness,
    //TwoDShapes,
    //ThreeDShapes,
    //Symmetry,
    //LinesAndAngles,
    //Transformation,

    //// Units for Measures
    //Length,
    //Area,
    //Weight,
    //Capacity,
    //Time,
    //Money,
    //Temperature,

    //// Units for Data
    //SortingAndClassifying,
    //RepresentingAndInterpretingData,
    //Chance
}