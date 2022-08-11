using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ordarat.Dtos
{
    public class CustomerBasketDto
    {
        [Required]

        public string Id { get; set; }

        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();
    }
}
