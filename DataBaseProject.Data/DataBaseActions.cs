using DataBaseProject.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataBaseProject.Data
{
    public static class DataBaseActions
    {
        #region Private Fields

        private static BinaryFormatter binaryFormatter = new BinaryFormatter();

        #endregion

        #region Public Methods

        public static void Save(IDataObject data)
        {
            try
            {
                var filePath = Environment.CurrentDirectory + "/DataBaseFiles/" + data.FileName;
                SerializeData(filePath, data);
            }
            catch(Exception ex)
            {
                throw ex;//test
            }
        }

        public static ICollection<object> GetAllData(object data)
        {
            var dataObject = (IDataObject)data;
            var filePath = Environment.CurrentDirectory + "/DataBaseFiles/" + dataObject.FileName;

            return DeserializeCollection(filePath);
        }

        #endregion

        #region Private Methods

        private static void SerializeData(string filePath, object data)
        {
            if (data == null) throw new Exception("Cannot store a null value.");
            try
            {
                if (!File.Exists(filePath)) File.Create(filePath);
                var fileStream = new FileStream(filePath, FileMode.Append);
                binaryFormatter.Serialize(fileStream, data);
                fileStream.Close();
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
        }

        private static ICollection<object> DeserializeCollection(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return new List<object>();
                var fileStream = new FileStream(filePath, FileMode.Open);
                var dataCollection = new List<object>();
                while (fileStream.Position < fileStream.Length)
                {
                    dataCollection.Add(binaryFormatter.Deserialize(fileStream));
                }
                fileStream.Close();
                return dataCollection;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static object DeserializeData(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return null;
                var fileStream = new FileStream(Environment.CurrentDirectory + "/DataBaseFiles/" + filePath, FileMode.Open);
                var dataCollection = binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return dataCollection;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
