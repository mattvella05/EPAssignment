using MatthewVellaEPSolution.Domain;
using Microsoft.EntityFrameworkCore;

namespace MatthewVellaEPSolution.DataAccess
{
    public class PollRepository : CommonPollRepository
    {
        private readonly PollDbContext _context;

        public PollRepository(PollDbContext context)
        {
            _context = context;
        }

        // Adds a new poll to the database
        public void CreatePoll(Poll poll)
        {
            _context.Polls.Add(poll);
            _context.SaveChanges();
        }

        // Retrieves all polls from the database, sorted by date (most recent first)
        public List<Poll> GetPolls()
        {

            return _context.Polls
                .OrderByDescending(p => p.DateCreated)  
                .ToList();
        }

        // Allows a user to vote for an option in a poll
        public void Vote(int pollId, int optionNumber)
        {
            var poll = _context.Polls.Find(pollId);
            if (poll == null) return;

            // Increment the vote count based on the option selected
            switch (optionNumber)
            {
                case 1:
                    poll.Option1VotesCount++;
                    break;
                case 2:
                    poll.Option2VotesCount++;
                    break;
                case 3:
                    poll.Option3VotesCount++;
                    break;
            }

            _context.SaveChanges();
        }
    }
}
