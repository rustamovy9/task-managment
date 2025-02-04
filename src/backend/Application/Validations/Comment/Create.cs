using Application.DTO_s;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Validations.Comment;

public class Create : AbstractValidator<CommentCreateInfo>
{
    public Create()
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