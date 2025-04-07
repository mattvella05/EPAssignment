using MatthewVellaEPSolution.Domain;

namespace MatthewVellaEPSolution.DataAccess
{
    public interface CommonPollRepository
    {
        void CreatePoll(Poll poll);
        List<Poll> GetPolls();
        void Vote(int pollId, int optionNumber);
    }
}
