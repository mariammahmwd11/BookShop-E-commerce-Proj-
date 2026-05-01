using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookSAW.BL.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        public string Bio { get; set; }
        public IFormFile? Photo { get; set; }

        public string? ImageURl { get; set; }
        public int? BookCount { get; set; }
    }
}
