﻿namespace AuthAPI.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set; }
    }
}
