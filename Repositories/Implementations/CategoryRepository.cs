using DataAccessLayer.Entities;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly LucySalesDataContext _context;

        public CategoryRepository()
        {
            _context = new LucySalesDataContext();
        }

        public List<Category> GetAllCategories()
            => _context.Categories.ToList();
    }
}
