using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ResourceSkillMatching.Models;
using ResourceSkillMatching.Services;

namespace ResourceSkillMatching.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceSkillMatchingController : ControllerBase
    {
        private readonly IResourceSkillMatchingService _resourceSkillMatchingService;
        public ResourceSkillMatchingController(IResourceSkillMatchingService resourceSkillMatchingService)
        {
            _resourceSkillMatchingService = resourceSkillMatchingService;
        }

        /// <summary>
        ///  Resource Skill Matching Data
        /// </summary>
        /// <returns>Get Resource Skill Matching Data</returns>
        /// <param name="projectID"></param>
        /// <response code="200">Returns Resource Skill Matching Data</response>
        /// <response code="500">If there are any errors</response>
        [HttpGet("GetResources/{projectID}")]
        public IEnumerable<ResourceSkillMatchingModel> GetResources(string projectID)
        {
            return _resourceSkillMatchingService.GetResources(projectID);
        }
    }
}
