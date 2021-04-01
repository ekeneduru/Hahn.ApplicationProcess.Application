using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAssetService AssetService { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
