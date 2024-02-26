using DormitoryAPI.EFcore;
using DormitoryAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DormitoryAPI.Services
{
    public class CommentService
    {
        private EF_DormitoryDb _context;
        public CommentService(EF_DormitoryDb context)
        {
            _context = context;
        }

        public List<Comment> Get()
        {
            List<Comment> response = new List<Comment>();
            var dataList = _context.Comment.ToList();
            dataList.ForEach(row => response.Add(new Comment()
            {
                idComment = row.idComment,
                idCommunity = row.idCommunity,
                idUser = row.idUser,
                details = row.details,
                timesTamp = row.timesTamp
            }));
            return response;
        }

        public async Task<Comment> Post(Comment comment)
        {
            var _comment = new Comment();

            _comment.idComment = Guid.NewGuid().ToString();
            _comment.idCommunity = comment.idCommunity;
            _comment.idUser = comment.idUser;
            _comment.details = comment.details;
            _comment.timesTamp = DateTimeOffset.UtcNow;

            await _context.Comment.AddAsync(_comment);
            await _context.SaveChangesAsync();
            
            return _comment;
        }

        public async Task<bool> CreateComment(CreateComment res)
        {
            var _comment = new Comment();

            _comment.idComment = Guid.NewGuid().ToString();
            _comment.idCommunity = res.idCommunity;
            _comment.idUser = res.idUser;
            _comment.details = res.details;
            _comment.timesTamp = DateTimeOffset.UtcNow;

            await _context.Comment.AddAsync(_comment);
            await _context.SaveChangesAsync();
            
            return true;
        }
        public async Task<List<GetComment>> GetAllComment(string idCommunity)
        {
            var _commentAll = new List<GetComment>();
            var commentAll = await _context.Comment.Where(c => c.idCommunity == idCommunity).ToListAsync();
            
            foreach(var comment in commentAll)
            {

                var user = await _context.User.FirstOrDefaultAsync(u => u.Id == comment.idUser);
                var getComment = new GetComment{
                    idComment = comment.idComment,
                    idCommunity = comment.idCommunity,
                    idUser = comment.idUser,
                    fullName = user.name + " " + user.lastname,
                    details = comment.details,
                    timesTamp = comment.timesTamp,
                };
                _commentAll.Add(getComment);
            }

            return _commentAll;
        }

    }   
    
}
