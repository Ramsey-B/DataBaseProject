using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseProject.Entities
{
    [Serializable]
    public class Student : IDataObject
    {
        public string FileName { get; set; } = "student_data.txt";
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Grade { get; set; }
    }
}
