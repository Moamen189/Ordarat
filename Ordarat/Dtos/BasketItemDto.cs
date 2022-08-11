﻿using System.ComponentModel.DataAnnotations;

namespace Ordarat.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]

        public string ProductName { get; set; }
        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage ="Price Must be Greater than Zero!!")]
        public decimal Price { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity Must be at Least one item!!")]

        public int Quantity { get; set; }
        [Required]

        public string PictureUrl { get; set; }
        [Required]

        public string Brand { get; set; }
    }
}