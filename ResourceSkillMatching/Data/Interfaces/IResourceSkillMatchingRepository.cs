using ResourceSkillMatching.Models;
using System.Collections.Generic;

namespace ResourceSkillMatching.Data.Interfaces
{
    public interface IResourceSkillMatchingRepository
    {
        IEnumerable<ResourceSkillMatchingModel> GetResources(string projectID);
    }
}
