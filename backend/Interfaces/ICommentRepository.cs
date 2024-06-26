using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();

        Task<Comment?> GetCommentByIdAsync(int id);

        Task<Comment> CreateCommentAsync(Comment comment);

        Task<Comment?> DeleteAsync(int id);

        Task<Comment?> UpdateAsync(int id, Comment commentModel);
    }
}