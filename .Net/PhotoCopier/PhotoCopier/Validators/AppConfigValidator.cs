using System.ComponentModel.DataAnnotations;
using PhotoCopier.Models;

namespace PhotoCopier.Validators;

public static class AppConfigValidator
{
    public static (bool, string) IsValid(AppConfig config)
    {
        var context = new ValidationContext(config, serviceProvider: null, items: null);
        var validationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(config, context, validationResults, true);
        
        return (isValid, string.Join(',', validationResults));
    }
}