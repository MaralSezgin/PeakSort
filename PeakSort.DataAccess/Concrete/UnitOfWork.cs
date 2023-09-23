using PeakSort.DataAccess.Abstract;
using PeakSort.DataAccess.Concrete.EntityFramework.Contexts;
using PeakSort.DataAccess.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeakSort.DataAccess.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PeaksortContext _context;

        private EfAboutRepository _aboutRepository;
        private EfCategoryRepository _categoryRepository;
        private EfContactRepository _contactRepository;
        private EfProductRepository _productRepository;
        private EfProjectRepository _projectRepository;
        private EfReferenceRepository _referenceRepository;
        private EfRoleRepository _roleRepository;
        private EfUserRepository _userRepository;
        public UnitOfWork(PeaksortContext context)
        {
            _context = context;
        }
        public IAboutRepository Abouts => _aboutRepository ?? new EfAboutRepository(_context);

        public ICategoryRepository Categorys => _categoryRepository ?? new EfCategoryRepository(_context);

        public IContactRepository Contacts => _contactRepository ?? new EfContactRepository(_context);

        public IProductRepository Products => _productRepository ?? new EfProductRepository(_context);

        public IProjectRepository Projects => _projectRepository ?? new EfProjectRepository(_context);

        public IReferenceRepository References => _referenceRepository ?? new EfReferenceRepository(_context);

        public IRoleRepository Roles => _roleRepository ?? new EfRoleRepository(_context);

        public IUserRepository Users => _userRepository  ?? new EfUserRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
