using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsDataAPI.Model
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Age is required")]
        public int Age { get; set; }
        [Required(ErrorMessage ="DOB is required")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string Course { get; set; }

    }
}
