﻿using ScheduleGenerator.Server.Models;
using System.Threading.Tasks;

namespace ScheduleGenerator.Server.Repositories
{
    public class DbRepository : IDbRepository
    {
        private readonly ScheduleGeneratorContext _context;

        public DbRepository(ScheduleGeneratorContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
