using System.Diagnostics.Contracts;

namespace PortfolioAPI.Models
{
    public class Education
    {
        public int EducationId { get; set; }
        public string SchoolName { get; set; }
        public string Course { get; set; }
        public string Subjects { get; set; }
        public string Year { get; set; }        
    }
}
