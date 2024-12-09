namespace HigherLevelEducation.Core
{
    public abstract class QuestionsApi
    {
        public record CreateQuestionsModel(int ClassLevel, Subject Subject, Topic Topic, DifficultyLevel Difficulty, int Limit);

        private static readonly Random RandomInstance = new Random();

        public static List<string> CreateQuestions(CreateQuestionsModel model)
        {
            var questions = new List<string>();
            var calculationSign = GetCalculationSign(model.Topic);
            var numberCriteria = GetNumberCriteriaBasedOnTopic(model.ClassLevel, model.Topic);

            for (var i = 0; i < model.Limit; i++)
            {
                var question = GenerateQuestion(numberCriteria, model.Difficulty, calculationSign);
                questions.Add(question);
            }

            return questions;
        }

        private static string GenerateQuestion(NumberCriteria numberCriteria, DifficultyLevel difficulty, string calculationSign)
        {
            var firstRandomNumber = GenerateRandomNumber(numberCriteria.FirstNumber, numberCriteria.IncludeDecimals, difficulty);
            var secondRandomNumber = GenerateRandomNumber(numberCriteria.SecondNumber, numberCriteria.IncludeDecimals, difficulty);

            var firstNumberToShow = Math.Max(firstRandomNumber, secondRandomNumber);
            var secondNumber = firstNumberToShow == firstRandomNumber ? secondRandomNumber : firstRandomNumber;

            return $"{firstNumberToShow},{calculationSign},{secondNumber}";
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
                    6 => new NumberCriteria(1000, 9999, false),
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

        private static double GenerateRandomNumber(int max, bool? includeDecimal, DifficultyLevel difficulty)
        {
            var min = difficulty switch
            {
                DifficultyLevel.Easy => 1,
                DifficultyLevel.Medium => max / 2,
                DifficultyLevel.Hard => (int)(max * 0.75),
                _ => throw new ArgumentException("Invalid difficulty level")
            };

            var number = RandomInstance.Next(min, max);

            if (includeDecimal == true && RandomInstance.Next(2) == 0)
            {
                return number / 10.0;
            }

            return number;
        }

        public record NumberCriteria(int FirstNumber, int SecondNumber, bool? IncludeDecimals = false);
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
        Addition,
        Subtraction,
        Multiplication,
        Division,
        Fractions,
        DecimalsAndPercentages,
        PlaceValue,
        NumberTheory
    }
}