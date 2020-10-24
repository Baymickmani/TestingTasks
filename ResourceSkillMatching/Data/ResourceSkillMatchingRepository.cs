using ResourceSkillMatching.Data.Interfaces;
using ResourceSkillMatching.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace ResourceSkillMatching.Data
{
    public class ResourceSkillMatchingRepository : IResourceSkillMatchingRepository
    {
        private readonly DbContext _dbContext;
        public ResourceSkillMatchingRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<ResourceSkillMatchingModel> GetResources(string projectID)
        {
            var query = @"SELECT PMAS.ProjectName,
	                               TMAS.TaskName,
	                               TMAS.StartDate AS [TaskStartDate],
	                               TMAS.EndDate AS [TaskEndDate],
	                               SMAS.SkillName,
	                               RMAS.ResourceName,
	                               RMAS.StartDate AS [ResourceAvailableStartDate] ,
	                               RMAS.EndDate AS [ResourceAvailableEndDate]
	                               FROM ProjectMaster PMAS
	                               INNER JOIN TaskMaster TMAS ON PMAS.ProjectID = TMAS.ProjectID
	                               INNER JOIN SkillMaster SMAS ON SMAS.SkillID = TMAS.SkillID
	                               INNER JOIN ResourceMaster RMAS ON RMAS.SkillID = SMAS.SkillID  
	                               WHERE PMAS.ProjectID = @ProjectID
	                               AND ((RMAS.StartDate <= TMAS.StartDate AND RMAS.EndDate >= TMAS.EndDate) OR 
				                            ((RMAS.StartDate BETWEEN TMAS.StartDate AND TMAS.EndDate) AND (RMAS.EndDate BETWEEN TMAS.StartDate AND TMAS.EndDate)))
	                               ORDER BY TaskName";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("ProjectID", projectID)
            };

           return _dbContext.Query<ResourceSkillMatchingModel>(query.ToString(), parameters.ToArray(), CommandType.Text);
        }
    }
}
