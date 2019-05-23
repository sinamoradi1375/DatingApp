using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DatingApp.Api.Helpers;
using System;

namespace DatingApp.Api.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Like> GetLike(int userID, int recipientID)
        {
            return await _context.Likes.FirstOrDefaultAsync(x => 
                x.LikerID == userID && x.LikeeID == recipientID);
        }

        public async Task<Photo> GetMainPhotoForUser(int userID) 
        {
            return await _context.Photos.Where(x => x.UserID == userID).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.ID == id);

            return photo;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(x => x.Photos).FirstOrDefaultAsync(x => x.ID == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.Include(x => x.Photos).OrderByDescending(x => x.LastActive).AsQueryable();

            users = users.Where(x => x.ID != userParams.UserID && x.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserID, userParams.Likers);
                users = users.Where(x => userLikers.Contains(x.ID));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserID, userParams.Likers);
                users = users.Where(x => userLikees.Contains(x.ID));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Now.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Now.AddYears(-userParams.MinAge);

                users = users.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created" : 
                        users = users.OrderByDescending(x => x.CreatedDate);
                        break;
                    default:
                        users = users.OrderByDescending(x => x.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        private async Task<IEnumerable<int>> GetUserLikes(int id, bool likers)
        {
            var user = await _context.Users
                .Include(x => x.Likers)
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(x => x.ID == id);
            
            if (likers)
            {
                return user.Likers.Where(x => x.LikeeID == id).Select(x => x.LikerID);
            }
            else
            {
                return user.Likees.Where(x => x.LikerID == id).Select(x => x.LikeeID);
            }
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}