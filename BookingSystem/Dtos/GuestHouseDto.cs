﻿using BookingSystem.Entities;
using System.ComponentModel.DataAnnotations;
namespace BookingSystem.Dtos
{
    public class GuestHouseDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public List<RoomDto> Rooms { get; set; } = new List<RoomDto>();
    }
}
