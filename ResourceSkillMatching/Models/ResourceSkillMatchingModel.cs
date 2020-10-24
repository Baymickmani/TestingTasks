namespace ResourceSkillMatching.Models
{
    public class ResourceSkillMatchingModel
    {
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string TaskStartDate { get; set; }
        public string TaskEndDate { get; set; }
        public string SkillName { get; set; }
        public string ResourceName { get; set; }
        public string ResourceAvailableStartDate { get; set; }
        public string ResourceAvailableEndDate { get; set; }
    }
}
