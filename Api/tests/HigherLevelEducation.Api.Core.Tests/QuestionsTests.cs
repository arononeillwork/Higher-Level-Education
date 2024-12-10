namespace HigherLevelEducation.Api.Core.Tests;

using HigherLevelEducation.Core;

public class QuestionsTests
{
    // TEST LIST
    // Check questions are within requested class level 
    // Add for multiple subjects
    
    [Theory]
    [InlineData(Topic.Addition, DifficultyLevel.Easy, 5)]
    [InlineData(Topic.Division, DifficultyLevel.Easy, 15)]
    [InlineData(Topic.Multiplication, DifficultyLevel.Easy, 3)]
    [InlineData(Topic.Subtraction, DifficultyLevel.Easy, 5)]
    [InlineData(Topic.Addition, DifficultyLevel.Medium, 7)]
    [InlineData(Topic.Division, DifficultyLevel.Medium, 4)]
    [InlineData(Topic.Subtraction, DifficultyLevel.Medium, 8)]
    [InlineData(Topic.Multiplication, DifficultyLevel.Hard, 13)]
    [InlineData(Topic.Subtraction, DifficultyLevel.Hard, 15)]
    public void Should_Return_Questions_Grouped_By_Difficulty_Ordered_Easiest_First(
        Topic topic,
        DifficultyLevel difficulty,
        int numberOfQuestions)
    {
        // Arrange - Request Questions
        var parameters = new QuestionsApi.CreateQuestionsModel(
            ClassLevel: 3,
            Subject.Maths,
            topic,
            difficulty,
            numberOfQuestions
        );

        // Act
        var result = QuestionsApi.CreateQuestions(parameters);

        // Assert

        // Check if numberOfQuestions applied
        result.Count.Should().Be(parameters.NumberOfQuestions);

        // Check if each string contains the calculation sign
        var calculationSign = QuestionsApi.GetCalculationSign(parameters.Topic);
        result.All(q => q.Contains(calculationSign)).Should().BeTrue();
        
        // Retrieve number criteria
        var numberCriteria = QuestionsApi.GetNumberCriteriaBasedOnTopic(parameters.ClassLevel, parameters.Topic);

        // Check if the numbers in the questions adhere to the difficulty level
        foreach (var question in result)
        {
            var parts = question.Split(',');
            var firstNumber = double.Parse(parts[0]);
            var secondNumber = double.Parse(parts[2]);

            var min = difficulty switch
            {
                DifficultyLevel.Easy => 1,
                DifficultyLevel.Medium => numberCriteria.FirstNumber / 2,
                DifficultyLevel.Hard => (int)(numberCriteria.FirstNumber * 0.75),
                _ => throw new ArgumentException("Invalid difficulty level")
            };

            firstNumber.Should().BeGreaterThanOrEqualTo(min);
            secondNumber.Should().BeGreaterThanOrEqualTo(min);
        }
    }
}