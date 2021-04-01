using FluentValidation.Results;
using Hahn.ApplicationProcess.February2021.Data.DTOs;
using Hahn.ApplicationProcess.February2021.Data.Services;
using Hahn.ApplicationProcess.February2021.Data.ViewModels;
using Hahn.ApplicationProcess.February2021.Domain.Interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using Hahn.ApplicationProcess.February2021.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : BaseController
    {
        private IUnitOfWork _unitOfWork;
        private ILogger _logger;
        private IExternalService _externalService;

        public OrganizationController(IUnitOfWork unitOfWork, ILogger<OrganizationController> logger, IExternalService externalService)
        {
            _unitOfWork = unitOfWork;
            _externalService = externalService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AssetViewModel model)
        {
            try
            {

                Asset asset = new Asset()
                {
                    AssetName = model.AssetName,
                    Broken = model.Broken,
                    CountryOfDepartment = model.CountryOfDepartment,
                    Department = model.Department,
                    EMailAdressOfDepartment = model.EMailAdressOfDepartment,
                    PurchaseDate = model.PurchaseDate
                };
                AssetValidator validator = new AssetValidator();
                ValidationResult results = validator.Validate(asset);
                if (results.IsValid)
                {
                    List<CountryDTO> countryDtos = await _externalService.GetCountryAsync(model.CountryOfDepartment);
                    if (countryDtos.Count > 0)
                    {
                        _unitOfWork.AssetService.AddAsset(asset);
                        _unitOfWork.SaveChanges();
                        _logger.LogInformation($"Asset with Id {asset.Id} have beeen created Successufully.");
                        return Ok(asset);
                    }
                    return BadRequest(new { message = "Invilid country Name. Please enter a valid county name." });
                }
                return BadRequest(new { message = results.ToString() });
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occure while creating asset.");
                return BadRequest(new { message = "Unknow server error" });
            }

        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                Asset asset = _unitOfWork.AssetService.GetAssetbyId(id);
                if (asset != null)
                {
                    return Ok(asset);
                }
                else
                {
                    _logger.LogInformation("Asset Id was not found in Get method.");
                    return NotFound(new { message = "Asset does not exist." });
                }
            }
            catch (Exception ex){

                _logger.LogWarning(ex, "Error occure while getting asset.");
                return BadRequest(new { message = "Unknow server error" });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(AssetViewModel model)
        {
            try
            {

                Asset asset = _unitOfWork.AssetService.GetAssetbyId(model.Id);
                if (asset != null)
                {
                    asset.AssetName = model.AssetName;
                    asset.Broken = model.Broken;
                    asset.CountryOfDepartment = model.CountryOfDepartment;
                    asset.Department = model.Department;
                    asset.EMailAdressOfDepartment = model.EMailAdressOfDepartment;
                    asset.PurchaseDate = model.PurchaseDate;

                    AssetValidator validator = new AssetValidator();
                    ValidationResult results = validator.Validate(asset);

                    if (results.IsValid)
                    {
                        List<CountryDTO> countryDtos = await _externalService.GetCountryAsync(model.CountryOfDepartment);
                        if (countryDtos.Count > 0)
                        {
                            _unitOfWork.AssetService.UpdateAsset(asset);
                            _unitOfWork.SaveChanges();
                            _logger.LogInformation($"Asset with Id {asset.Id} have beeen update Successufully.");
                            return Ok(asset);
                        }
                        return BadRequest(new { message = "Invilid country Name. Please enter a valid county name." });
                    }
                    return BadRequest(new { message = results.ToString() });
                }
                else
                {
                    _logger.LogInformation("Asset Id was not found in Put method.");
                    return NotFound(new { message = "Asset does not exist." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occure while updating asset.");
                return BadRequest(new { message = "Unknow server error" });
            }
        }



        [HttpDelete]
        public IActionResult Delete(int id)
        {
            ///Normally I don't do hard delete. I do delete by updating a flag. Unless when the data will not be important in the future.
            try
            {
                Asset asset = _unitOfWork.AssetService.GetAssetbyId(id);
                if (asset != null)
                {
                    bool isDeleted = _unitOfWork.AssetService.DeleteAsset(asset);
                    _unitOfWork.SaveChanges();
                    _logger.LogInformation($"Asset with Id {asset.Id} have beeen Deleted.");
                    return Ok(new { IsDeleted = isDeleted });
                }
                else
                {
                    _logger.LogInformation("Asset Id was not found in Delete method.");
                    return NotFound(new { message = "Asset does not exist." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error occure while deleting asset.");
                return BadRequest(new { message = "Error occure while deleting asset" });
            }
        }
    }
}
