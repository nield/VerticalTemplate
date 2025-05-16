using FluentValidation;

namespace VerticalTemplate.Api.Features.ToDos.V1.CreateToDo;

internal sealed class Request
{
    public required string Title { get; set; }
    public List<string> Tags { get; set; } = [];

    internal sealed class Validator : Validator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Title).NotEmpty();
        }
    }
}

internal sealed class Response
{
    public required long Id { get; set; }
}
