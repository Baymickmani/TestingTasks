using ResourceSkillMatching.Models;
using System.Collections.Generic;

namespace ResourceSkillMatching.Services
{
    public interface IResourceSkillMatchingService
    {
        List<ResourceSkillMatchingModel> GetResources(string projectID);
    }
}
