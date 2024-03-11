using System.ComponentModel.DataAnnotations;

namespace LinkedinClone.Validations
{
    public class FileTypeValidation: ValidationAttribute
    {
        private readonly string[] _fileTypes;
        public FileTypeValidation()
        {
            _fileTypes = new[] { ".pdf", ".docx", ".doc" };
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            IFormFile formFile = value as IFormFile;


            string extension = Path.GetExtension(formFile.FileName);

            if (value == null)
            {
                return ValidationResult.Success;
            }


            if(formFile == null)
            {
                return ValidationResult.Success;
            }

            if(_fileTypes.Contains(extension))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Only Pdf and docx are allowed");
            }

        }
    }
}
