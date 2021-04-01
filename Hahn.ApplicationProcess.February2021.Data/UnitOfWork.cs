using Hahn.ApplicationProcess.February2021.Domain.DBContext;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public IAssetService AssetService { get; }

      

        public UnitOfWork(ApplicationContext context,
            IAssetService assetService)
        {
           _context = context;
            AssetService = assetService;
           
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
