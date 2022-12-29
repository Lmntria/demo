using System.ComponentModel.DataAnnotations;

namespace QuarterApp.Attributes.ValidationAttributes
{
    public class AllowedFileTypeAttribute:ValidationAttribute
    {
        private string[] _fileTypes;
        public AllowedFileTypeAttribute(params string[] fileTypes)
        {
            _fileTypes = fileTypes;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                if (!_fileTypes.Contains(file.ContentType))
                    return new ValidationResult("File Content type must be one of these: " + String.Join(",", _fileTypes));
            }
            return ValidationResult.Success;
        }

    }
}
