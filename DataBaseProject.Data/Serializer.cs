using DataBaseProject.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataBaseProject.Data
{
    public static class Serializer
    {
        #region Private Fields

        private static BinaryFormatter binaryFormatter = new BinaryFormatter();

        #endregion

        public static void SerializeCollection(string filePath, ICollection<IDataObject> dataCollection)
        {
            FileStream fileStream = StreamManager.CreateFileStream(filePath, FileMode.Append);
            try
            {
                foreach (var data in dataCollection)
                {
                    binaryFormatter.Serialize(fileStream, data);
                }
            }
            catch (SerializationException exception)
            {
                throw exception;
            }
            finally
            {
                StreamManager.CloseFileStream(fileStream);
            }
        }

        public static void SerializeData(string filePath, IDataObject data)
        {
            if (data == null) throw new Exception("Cannot store a null value.");
            FileStream fileStream = StreamManager.CreateFileStream(filePath, FileMode.Append);
            try
            {
                binaryFormatter.Serialize(fileStream, data);
            }
            catch (SerializationException exception)
            {
                throw exception;
            }
            finally
            {
                StreamManager.CloseFileStream(fileStream);
            }
        }

        public static ICollection<IDataObject> DeserializeCollection(string filePath)
        {
            FileStream fileStream = StreamManager.CreateFileStream(filePath, FileMode.OpenOrCreate);
            try
            {
                if (!File.Exists(filePath)) return new List<IDataObject>();
                var dataCollection = new List<IDataObject>();
                while (fileStream.Position < fileStream.Length)
                {
                    dataCollection.Add(binaryFormatter.Deserialize(fileStream) as IDataObject);
                }
                return dataCollection;
            }
            catch (SerializationException exception)
            {
                throw exception;
            }
            finally
            {
                StreamManager.CloseFileStream(fileStream);
            }
        }

        public static IDataObject DeserializeDataById(string filePath, string id)
        {
            FileStream fileStream = StreamManager.CreateFileStream(filePath, FileMode.OpenOrCreate);
            try
            {
                IDataObject searchedData = null;
                while (fileStream.Position < fileStream.Length && searchedData == null)
                {
                    var data = binaryFormatter.Deserialize(fileStream) as IDataObject;
                    if (data.Id == id) searchedData = data;
                }
                return searchedData;
            }
            catch (SerializationException exception)
            {
                throw exception;
            }
            finally
            {
                StreamManager.CloseFileStream(fileStream);
            }
        }

        public static void ModifyData(string filePath, IDataObject newData)
        {
            var dataDeleted = DeleteData(filePath, newData.Id);
            if (!dataDeleted) throw new NullReferenceException($"Cannot replace data with id <{newData.Id}>");
            SerializeData(filePath, newData);
        }

        public static bool DeleteData(string filePath, string id)
        {
            var dataCollection = DeserializeCollection(filePath);
            var dataToRemove = dataCollection.FirstOrDefault(data =>
            {
                return data.Id == id;
            });
            bool dataDeleted = dataCollection.Remove(dataToRemove);
            if (dataDeleted == false) throw new Exception($"Unable to remove data with Id <{id}>.");
            ReplaceFile(filePath, dataCollection);
            return dataDeleted;
        }

        #region Private Methods

        private static void ReplaceFile(string filePath, ICollection<IDataObject> newCollection)
        {
            if (!File.Exists(filePath)) throw new FileNotFoundException("Unable to find correct file.");
            File.Delete(filePath);
            SerializeCollection(filePath, newCollection);
        }

        #endregion
    }
}
