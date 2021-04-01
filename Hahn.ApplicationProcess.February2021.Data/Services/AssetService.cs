using Hahn.ApplicationProcess.February2021.Data.Repositories;
using Hahn.ApplicationProcess.February2021.Domain.DBContext;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.Services
{
    public class AssetService : BaseRepository<Asset>, IAssetService
    {

        public AssetService(ApplicationContext context) : base(context)
        {

        }


        public Asset GetAssetbyId(int assetId)
        {
            return FirstOrDefault(x => x.Id == assetId);
        }
        public int AddAsset(Asset  asset)
        {
             Add(asset);
            return asset.Id;
        }

        public int UpdateAsset(Asset asset)
        {
            Update(asset);
            return asset.Id;
        }

        public bool DeleteAsset(Asset asset)
        {
           Delete(asset);
           return true;
        }


    } 
}
