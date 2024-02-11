
using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace DormitoryAPI.Services
{
    public class ProblemService
    {
        private EF_DormitoryDb _context;
        public ProblemService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Problem> GetAllProblems()
        {
            List<Problem> response = new List<Problem>();
            var dataList = _context.Problem.ToList();
            dataList.ForEach(row => response.Add(new Problem()
            {
                idProblem = row.idProblem,
                idUser = row.idUser,
                idRoom = row.idRoom,
                category = row.category,
                title = row.title,
                details = row.details,
                timesTamp = row.timesTamp
            }));
            return response;
        }

        public async Task<Problem> PostProblem(Problem res)
        {
            var _problem = new Problem();

            _problem.idProblem = Guid.NewGuid().ToString();
            _problem.idUser = res.idUser;
            _problem.idRoom = res.idRoom;
            _problem.category = res.category;
            _problem.title = res.title;
            _problem.details = res.details;
            _problem.timesTamp = DateTimeOffset.UtcNow;

            await _context.Problem.AddAsync(_problem);
            await _context.SaveChangesAsync();
            
            return _problem;
        }

        public async Task<Problem> CreateProblem(CreateProblem res)
        {

            var user = await _context.User.FirstOrDefaultAsync(user => user.Id == res.idUser);

            var _problem = new Problem();

            if (user != null)
            {
                _problem.idProblem = Guid.NewGuid().ToString();
                _problem.idRoom = user.IdRoom;
                _problem.idUser = res.idUser;
                _problem.category = res.category;
                _problem.title = res.title;
                _problem.details = res.details;
                _problem.timesTamp = DateTimeOffset.UtcNow;

                await _context.Problem.AddAsync(_problem);
                await _context.SaveChangesAsync();

                return _problem;
            }
            
            return null;
            
        }

        public async Task<List<Problem>> GetAllProblemsByIdRoom(string idRoom)
        {
            var _problemsList = await _context.Problem.Where(p => p.idRoom == idRoom).ToListAsync();
    
            return _problemsList;
        }
    }
    
}