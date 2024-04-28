using System;
using System.Collections.Generic;
using System.Web.Http;
using Assignment_1.Models;

namespace Assignment_1.Controllers
{
    public class CountryController : ApiController
    {
        private static List<Country> countryList = new List<Country>
        {
            new Country { id = 1, CountryName = "INDIA", Capital = "NEW DELHI" },
            new Country { id = 2, CountryName = "FRANCE", Capital = "PARIS" },
            new Country { id = 3, CountryName = "AUSTRALIA", Capital = "CANBERRA" },
            new Country { id = 4, CountryName = "MEXICO", Capital = "MEXICO CITY" }
        };

        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return countryList;
        }

        [HttpGet]
        public IHttpActionResult GetCountry(int id)
        {
            var country = countryList.Find(c => c.id == id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }
        [HttpPost]
        public IHttpActionResult PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            countryList.Add(country); 

            return CreatedAtRoute("DefaultApi", new { id = country.id }, country);
        }
        [HttpPut]
        public IHttpActionResult PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCountry = countryList.Find(c => c.id == id);
            if (existingCountry == null)
            {
                return NotFound();
            }

            existingCountry.CountryName = country.CountryName;
            existingCountry.Capital = country.Capital;

            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
        [HttpDelete]
        public IHttpActionResult DeleteCountry(int id)
        {
            var countryToRemove = countryList.Find(c => c.id == id);
            if (countryToRemove == null)
            {
                return NotFound();
            }

            countryList.Remove(countryToRemove);

            return Ok(countryToRemove);
        }

    }
}
