using Application.DTO_s;
using FluentValidation;

namespace Application.Validations.Task;

public class Update : AbstractValidator<TaskCreateInfo>
{
    public Update()
    {
        RuleFor(t => t.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title must be at most 100 characters.");
        
        RuleFor(t => t.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");
        
        RuleFor(t => t.Status)
            .IsInEnum().WithMessage("Invalid status value.");
        
        RuleFor(t => t.Priority)
            .IsInEnum().WithMessage("Invalid priority value.");
        
        RuleFor(t => t.DeadLine)
            .GreaterThan(DateTimeOffset.UtcNow).WithMessage("Deadline must be in the future.");
        
        RuleFor(t => t.AssignedToUserId)
            .GreaterThan(0).WithMessage("Assigned user ID must be greater than 0.");
    }
}