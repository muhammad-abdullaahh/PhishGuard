using System;
using System.ComponentModel.DataAnnotations;

namespace PhishGuard.Models
{
    public class Link
    {
        [Key]
        public int LinkID { get; set; }

        [Required]
        [StringLength(500)]
        public string URL { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }

        public DateTime CheckedDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string Source { get; set; }
    }
}