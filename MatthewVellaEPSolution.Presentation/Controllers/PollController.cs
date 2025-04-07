using Microsoft.AspNetCore.Mvc;
using MatthewVellaEPSolution.DataAccess;
using MatthewVellaEPSolution.Domain;

namespace MatthewVellaEPSolution.Presentation.Controllers
{
    [Route("Poll")]
    public class PollController : Controller
    {
        private readonly CommonPollRepository _pollRepository;

        public PollController(CommonPollRepository pollRepository)
        {
            _pollRepository = pollRepository;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            var polls = _pollRepository.GetPolls();
            return View(polls);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Create")]
        public IActionResult Create(Poll poll)
        {
            if (ModelState.IsValid)
            {
                poll.DateCreated = DateTime.Now;
                _pollRepository.CreatePoll(poll);
                return RedirectToAction("Index");
            }

            return View(poll);
        }

        [HttpGet("Vote/{id}")]
        public IActionResult Vote(int id)
        {
            var poll = _pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }

        [HttpPost("Vote/{id}")]
        public IActionResult Vote(int id, int optionNumber)
        {
            _pollRepository.Vote(id, optionNumber);
            return RedirectToAction("Results", new { id = id });
        }

        [HttpGet("Results/{id}")]
        public IActionResult Results(int id)
        {
            var poll = _pollRepository.GetPolls().FirstOrDefault(p => p.Id == id);
            if (poll == null)
            {
                return NotFound();
            }

            return View(poll);
        }
    }
}
