using FluentValidation.TestHelper;
using ModularMonolithTemplate.Api.Modules.Reminder.Endpoints.V1.Todos;

namespace ModularMonolithTemplate.Api.Tests.Modules.Reminder.Endpoints.V1.ToDos.CreateToDo;

public class ValidatorTests
{
    private readonly Create.Validator _validator = new();

    [Fact]
    public async Task Given_EmptyTitle_Should_Fail()
    {
        var sut = await _validator.TestValidateAsync(new Create.Request
        {
            Title = ""
        });

        sut.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
