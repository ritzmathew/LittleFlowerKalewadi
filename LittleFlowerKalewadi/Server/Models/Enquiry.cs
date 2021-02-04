using System;
using System.ComponentModel.DataAnnotations;

namespace LittleFlowerKalewadi.Server.Models
{

    public class Enquiry
    {
        [Required]
        public string ApplicantName { get; set; }
        [Required]
        public DateTime ApplicantDOB { get; set; }
        [Required]
        public string[] ParentNames { get; set; }
        public string Source { get; set; }
        [Required]
        public string EMail { get; set; }
        public Guid UniqueId { get; set; }
    }
}
