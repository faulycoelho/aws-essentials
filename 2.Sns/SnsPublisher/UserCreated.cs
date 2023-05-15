﻿namespace SnsPublisher
{
    internal class UserCreated
    {
        public required int Id { get; init; }

        public required string Name { get; init; }

        public required string Surname { get; init; }

        public required string Email { get; init; }

        public required DateTime DateOfBirth { get; init; }
    }
}
