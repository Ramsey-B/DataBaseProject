using System;

namespace DataBaseProject.Entities
{
    [Serializable]
    public class Student : IDataObject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public int Grade { get; set; }
    }
}
