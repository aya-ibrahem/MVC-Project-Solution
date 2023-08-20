using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class
    {
        private MVCDb_Context _dbontext;
        public GenericRepository(MVCDb_Context dbontext)
        {
            _dbontext = dbontext;
        }
        public async Task<int> Add(T item)
        {
            await _dbontext.Set<T>().AddAsync(item);
            return await _dbontext.SaveChangesAsync();
        }
        public async Task<int> Delete(T item)
        {
            _dbontext.Set<T>().Remove(item);
            return  await _dbontext.SaveChangesAsync();
        }
        public async Task<int> Update(T item)
        {
             _dbontext.Set<T>().Update(item);
            return await _dbontext.SaveChangesAsync();
        }
        public async Task<T> Get(int id)
        //=> _dbontext.Set<T>().Where(D => D.Id == id).FirstOrDefault();
         => await _dbontext.Set<T>().FindAsync(id);
        public async Task<IEnumerable<T>> GetAll()
        => await _dbontext.Set<T>().ToListAsync();

    
    
}
}
