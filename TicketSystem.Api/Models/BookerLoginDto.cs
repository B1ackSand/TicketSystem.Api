﻿using Routine.Api.Entities;

namespace TicketSystem.Api.Models
{
    public class BookerLoginDto
    {
        public string PhoneNum { get; set; }
        public string BookerPwd { get; set; }
        public string CardId { get; set; }
    }
}
