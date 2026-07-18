
using System;
using System.Collections.Generic;


namespace CarePride.Domain.Entities
{
    public class ClassSubject
    {
        public int Id { get; set; }

        public int ClassId { get; set; }
        public int SubjectId { get; set; }

        // Navigation Properties
        public virtual Class Class { get; set; }
        public virtual Subject Subject { get; set; }
    }
}