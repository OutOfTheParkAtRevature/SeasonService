using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Model.Tests
{
    public class UnitTest1
    {

        public class ModelTests
        {
            /// <summary>
            /// Checks the data annotations of Models to make sure they aren't being violated
            /// </summary>
            /// <param name="model"></param>
            /// <returns></returns>
            private IList<ValidationResult> ValidateModel(object model)
            {
                var result = new List<ValidationResult>();
                var validationContext = new ValidationContext(model);
                Validator.TryValidateObject(model, validationContext, result, true);
                // if (model is IValidatableObject) (model as IValidatableObject).Validate(validationContext);

                return result;
            }

            /// <summary>
            /// Makes sure Game Model works with valid data
            /// </summary>
            [Fact]
        public void ValidateGame()
        {
            var game = new Game()
            {
                GameID = 1,
                SeasonID = 2,
                HomeTeamID = 1,
                AwayTeamID = 2,
                GameDate = DateTime.UtcNow,
                WinningTeam = 1,
                HomeScore = 24,
                AwayScore = 12,
                HomeStatID = 5,
                AwayStatID = 6
            };

            var results = ValidateModel(game);
            Assert.True(results.Count == 0);
        }
    }
}
