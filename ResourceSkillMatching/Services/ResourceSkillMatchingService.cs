using System;
using System.Collections.Generic;
using ResourceSkillMatching.Data.Interfaces;
using ResourceSkillMatching.Models;
using System.Linq;

namespace ResourceSkillMatching.Services
{
    public class ResourceSkillMatchingService : IResourceSkillMatchingService
    {
        private readonly IResourceSkillMatchingRepository _resourceSkillMatchingRepository;

        public ResourceSkillMatchingService(IResourceSkillMatchingRepository resourceSkillMatchingRepository)
        {
            _resourceSkillMatchingRepository = resourceSkillMatchingRepository;
        }

        public List<ResourceSkillMatchingModel> GetResources(string projectID)
        {
            if (String.IsNullOrEmpty(projectID))
                throw new Exception("Project ID is Mandatory");
            try
            {
               var data = _resourceSkillMatchingRepository.GetResources(projectID)?.ToList();
                if (data.Count > 0)
                    return data;
                else
                    throw new Exception("No Project ID is present or No Resources present for specified Project ID to allocate");
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
