using kiso.Models;
using Microsoft.AspNetCore.Mvc;

namespace kiso.Controllers
{

    [ApiController]
    [Route("pay/[controller]")]
    public class ApiController : Controller
    {
        private readonly KisoLightContext _dbContext;

        public ApiController(KisoLightContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetCities")]
        public IActionResult GetCities()
        {
            var cities = _dbContext.Cities.ToList();
            return Ok(cities);
        }

        [HttpGet("GetDistricts/{cityId}")]
        public IActionResult GetDistricts(int cityId)
        {
            var districts = _dbContext.Districts
                .Where(d => d.CityId == cityId)
                .ToList();

            return Ok(districts);
        }
        [HttpGet("GetWard/{districtId}")]
        public IActionResult GetWard(int districtId)
        {
            var ward = _dbContext.Wards
                .Where(w => w.DistrictId == districtId)
                .ToList();
            return Ok(ward);
        }
    }
}
