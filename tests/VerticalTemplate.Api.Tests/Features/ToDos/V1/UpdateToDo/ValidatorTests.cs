using FluentValidation.TestHelper;
using VerticalTemplate.Api.Features.ToDos.V1.UpdateToDo;

namespace VerticalTemplate.Api.Tests.Features.ToDos.V1.UpdateToDo;

public class ValidatorTests
{
    private readonly Request.Validator _validator = new();

    [Fact]
    public async Task Given_EmptyTitle_Should_Fail()
    {
        var sut = await _validator.TestValidateAsync(new Request
        {
            Title = ""
        });

        sut.ShouldHaveValidationErrorFor(x => x.Title);
    }
}
