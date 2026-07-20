using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace CarePride.Domain.Entities
{
    public class Class
    {
        // we dont need to do validation and property setting now.
        public string? ClassId { get; set; }
        public string? TeacherId { get; set; }
        public string? EnrollmentId { get; set; }
        public string? ClassName { get; set; }
        public string? GradeLevel { get; set; }
        public bool IsActive{ get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
