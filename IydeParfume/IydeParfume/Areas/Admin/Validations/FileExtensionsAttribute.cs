using System.ComponentModel.DataAnnotations;

namespace IydeParfume.Areas.Admin.Validations
{
    public class FileExtensionsAttribute: ValidationAttribute
    {
        private readonly string[] _fileExtensions;
        public FileExtensionsAttribute(string[] fileExtensions)
        {
            _fileExtensions = fileExtensions;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_fileExtensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }
        public string GetErrorMessage()
        {
            return $"This file extension is wrong";
        }
    }
}
