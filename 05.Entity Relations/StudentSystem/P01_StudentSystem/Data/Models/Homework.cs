namespace P01_StudentSystem.Data.Models
{
    using P01_StudentSystem.Data.Models.Enums;
    using System;

    public class Homework
    {
        public Homework(int homeworkId, string content, ContentType contentType, DateTime submissionTime, int studentId, int courseId)
        {
            HomeworkId = homeworkId;
            Content = content;
            ContentType = contentType;
            SubmissionTime = submissionTime;
            StudentId = studentId;
            CourseId = courseId;
        }

        public int HomeworkId { get; set; }

        public string Content { get; set; }

        public ContentType ContentType { get; set; }

        public DateTime SubmissionTime { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}