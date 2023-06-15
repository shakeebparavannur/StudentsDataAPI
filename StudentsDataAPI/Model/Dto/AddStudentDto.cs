using System.ComponentModel.DataAnnotations;

namespace StudentsDataAPI.Model.Dto
{
    public class AddStudentDto:IValidatableObject
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Date of Birth is required")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage ="Course is a required field")]
        public string Course { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DOB > DateTime.Now)
            {
                yield return new ValidationResult("Date of Birth cannot be in the future.", new[] { nameof(DOB) });
            }
        }
    }
}
