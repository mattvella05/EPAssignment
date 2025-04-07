using System.Text.Json;
using MatthewVellaEPSolution.Domain;

namespace MatthewVellaEPSolution.DataAccess
{
    public class PollFileRepository
    {
        private readonly string _filePath = "polls.json";

        // CreatePoll - Adds a poll to the JSON file
        public void CreatePoll(Poll poll)
        {
            var polls = GetPolls().ToList();
            poll.Id = polls.Count > 0 ? polls.Max(p => p.Id) + 1 : 1;
            poll.DateCreated = DateTime.Now;
            polls.Add(poll);
            SavePollsToFile(polls);
        }

        // GetPolls - Reads all polls from the JSON file
        public IEnumerable<Poll> GetPolls()
        {
            if (!File.Exists(_filePath))
                return new List<Poll>();

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Poll>>(json) ?? new List<Poll>();
        }

        private void SavePollsToFile(List<Poll> polls)
        {
            string json = JsonSerializer.Serialize(polls, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }
    }
}
