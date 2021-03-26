using FluentValidation;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Validators
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(x => x.AssetName).MaximumLength(5);
            RuleFor(x => x.Department).IsInEnum();
            RuleFor(x => x.EMailAdressOfDepartment).EmailAddress();
            RuleFor(x => x.Broken).NotNull();
            RuleFor(x => x.PurchaseDate).GreaterThanOrEqualTo(DateTime.UtcNow.AddYears(-1));
        }
    
    }
}
