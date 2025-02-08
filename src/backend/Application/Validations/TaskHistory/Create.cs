using Application.DTO_s;
using FluentValidation;

namespace Application.Validations.TaskHistory;

public class Create : AbstractValidator<TaskHistoryCreateInfo>
{
    public Create()
    {
        RuleFor(t => t.ChangeDescription)
            .NotEmpty().WithMessage("Change description is required.")
            .MaximumLength(500).WithMessage("Change description must be at most 500 characters.");
        
        RuleFor(t => t.ChangedAt)
            .LessThanOrEqualTo(DateTimeOffset.UtcNow).WithMessage("Change date cannot be in the future.");
        
        RuleFor(t => t.TaskId)
            .GreaterThan(0).WithMessage("Task ID must be greater than 0.");
        
        RuleFor(t => t.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
    }
}