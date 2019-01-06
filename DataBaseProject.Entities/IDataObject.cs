using System;

namespace DataBaseProject.Entities
{
    public interface IDataObject
    {
        string FileName { get; set; }
        string Id { get; set; }
    }
}
