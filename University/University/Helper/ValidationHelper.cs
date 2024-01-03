using Microsoft.AspNetCore.Mvc;

namespace University.Helper;

public static class ValidationHelper
{
    public static ActionResult HandleValidationErrors(ControllerBase controller)
    {
        var errors = controller.ModelState
            .Where(x => x.Value.Errors.Any())
            .Select(x => new
            {
                Property = x.Key,
                ErrorMessage = x.Value.Errors.First().ErrorMessage
            });

        var problemDetails = new ProblemDetails
        {
            Status = 400,
            Title = "Validation error(s) occurred.",
            Detail = "One or more validation errors occurred.",
            Extensions = { ["errors"] = errors }
        };

        return controller.BadRequest(problemDetails);
    }
}


