using DataBaseProject.Data;
using DataBaseProject.Entities;
using System;
using System.Collections.Generic;

namespace DataBaseProject.Business
{
    public class DataService
    {
        #region Private Properties

        private string FilePath { get; set; }

        #endregion

        public DataService(string filePath)
        {
            FilePath = filePath;
        }

        #region Public Methods

        public void Save(IDataObject data)
        {
            if (data.Id == null) data.Id = Guid.NewGuid().ToString();
            Serializer.SerializeData(FilePath, data);
        }

        public ICollection<IDataObject> GetAllData()
        {
            return Serializer.DeserializeCollection(FilePath);
        }

        public IDataObject FindById(string id)
        {
            var searchedData = Serializer.DeserializeDataById(FilePath, id);
            if (searchedData == null) throw new NullReferenceException($"Unable to find data with Id <{id}>.");
            return searchedData;
        }

        public IDataObject Edit(string id, IDataObject newData)
        {
            var oldData = FindById(id);
            newData.Id = oldData.Id;
            Serializer.ModifyData(FilePath, newData);
            return newData;
        }

        public void DeleteDataById(string id)
        {
            Serializer.DeleteData(FilePath, id);
        }

        #endregion
    }
}
