using Application.DTO_s;
using FluentValidation;

namespace Application.Validations.Comment;

public class Update : AbstractValidator<CommentUpdateInfo>
{
    public Update()
    {
        // Validate Content
        RuleFor(car => car.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(200).WithMessage("Content must not exceed 200 characters.");

        // Validate TaskId
        RuleFor(car => car.TaskId)
            .NotEmpty().WithMessage("TaskId is required.");
        
        // Validate UserId
        RuleFor(car => car.UserId)
            .NotEmpty().WithMessage("UserId is required.");


    }
}