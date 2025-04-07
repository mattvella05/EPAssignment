using MatthewVellaEPSolution.DataAccess;
using MatthewVellaEPSolution.Domain;
using System.Text.Json;

public class PollFileRepository : CommonPollRepository
{
    private readonly string _filePath = "polls.json";

    public void CreatePoll(Poll poll)
    {
        var polls = GetPolls().ToList();
        poll.Id = polls.Count > 0 ? polls.Max(p => p.Id) + 1 : 1;
        poll.DateCreated = DateTime.Now;
        polls.Add(poll);
        SavePollsToFile(polls);
    }

    public List<Poll> GetPolls()
    {
        if (!File.Exists(_filePath))
            return new List<Poll>();

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
    }

    public void Vote(int pollId, int optionNumber)
    {
        var polls = GetPolls().ToList();
        var poll = polls.FirstOrDefault(p => p.Id == pollId);
        if (poll == null) return;

        switch (optionNumber)
        {
            case 1: poll.Option1VotesCount++; break;
            case 2: poll.Option2VotesCount++; break;
            case 3: poll.Option3VotesCount++; break;
        }

        SavePollsToFile(polls);
    }

    private void SavePollsToFile(List<Poll> polls)
    {
        string json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}
