﻿using System;

namespace MatthewVellaEPSolution.Domain
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Option1Text { get; set; }
        public string Option2Text { get; set; }
        public string Option3Text { get; set; }

        public int Option1VotesCount { get; set; }
        public int Option2VotesCount { get; set; }
        public int Option3VotesCount { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
