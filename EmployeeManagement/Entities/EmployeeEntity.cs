using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entities
{
    public class EmployeeEntity
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public int Age { get; set; }
		public string Department { get; set; } = string.Empty;
	}
}

