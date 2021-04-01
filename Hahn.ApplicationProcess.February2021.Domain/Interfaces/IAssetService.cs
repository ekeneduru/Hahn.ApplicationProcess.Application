using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Interfaces
{
    public interface IAssetService
    {
        Asset GetAssetbyId(int assetId);
        int AddAsset(Asset asset);
        int UpdateAsset(Asset asset);
        bool DeleteAsset(Asset asset);
    }
}
