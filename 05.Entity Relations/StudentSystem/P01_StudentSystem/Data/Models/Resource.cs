namespace P01_StudentSystem.Data.Models
{
    using P01_StudentSystem.Data.Models.Enums;

    public class Resource
    {
        public Resource(int resourceId, string name, string url, ResourceType resourceType, int courseId)
        {
            ResourceId = resourceId;
            Name = name;
            Url = url;
            ResourceType = resourceType;
            CourseId = courseId;
        }

        public int ResourceId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public ResourceType ResourceType { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}