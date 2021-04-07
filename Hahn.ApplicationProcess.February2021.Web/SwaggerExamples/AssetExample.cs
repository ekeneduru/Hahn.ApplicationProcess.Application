using Hahn.ApplicationProcess.February2021.Data.ViewModels;

using Swashbuckle.Examples;
//using Swashbuckle.AspNetCore.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Web.SwaggerExamples
{
    public class AssetExample: IExamplesProvider
    {
        public object GetExamples()
        {
            return new AssetViewModel
            {
                AssetName = "Television in the ware house",
                Broken = true,
                CountryOfDepartment = "Nigeria",
                Department =1,
                EMailAdressOfDepartment = "ekene@ekene.com",
                PurchaseDate = "2021-04-06T18:48:18.148Z",

            };

        }
    }
}
