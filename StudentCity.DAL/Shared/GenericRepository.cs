using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace StudentCity.DAL.Shared
{

    public class GenericRepository<T> : IRepository<T> where T : BaseModel
    {
        private readonly IOptions<DbSettings> _settings;
        public readonly string CollectionName;
        private readonly IMongoCollection<T> _collection;

        public GenericRepository(IOptions<DbSettings> settings, string collectionName)
        {
            _settings = settings;
            CollectionName = collectionName;
            var database = _settings.Value.Database;
            _collection = database.GetCollection<T>(collectionName);

        }
        public IQueryable<T> GetAll()
        {
            return _settings.Value.Database.GetCollection<T>(CollectionName).AsQueryable().Where(x => x.IsDeleted == false);
        }

        public List<T> GetAll(int pageIndex, int pageSize)
        {
            //var result = _collection.Find(new BsonDocument())
            //    .Skip(pageIndex)
            //    .Limit(pageSize)
            //    .ToList();

            //return result;
            return null;
        }

        public async Task<object> GetBy(Expression<Func<T, bool>> exp)
        {
            var table = _settings.Value.Database.GetCollection<T>(CollectionName);
            IFindFluent<T, T> findFluent = table.Find(Builders<T>.Filter.Not(Builders<T>.Filter.Where(exp)));
            return await findFluent.ToListAsync();
        }

        public T InsertOrUpdate(Expression<Func<T, bool>> exp, dynamic entity)
        {
            // _set = 
            var table = _settings.Value.Database.GetCollection<T>(CollectionName);
            var filter = Builders<T>.Filter.Where(exp);
            var objeDefault = table.Find(filter).FirstOrDefault();
            if (objeDefault != null)
            {

                //   dynamic newObject = data as IDictionary<string, object>;
                var propList = TypeDescriptor.GetProperties(entity.GetType());
                var expando = new ExpandoObject();
                var newObject = (IDictionary<string, object>)expando;

                foreach (PropertyDescriptor property in propList)
                {
                    if (property.Name == "_id")
                    {
                        continue;
                    }
                    newObject.Add(property.Name, property.GetValue(entity));
                }
                //table.FindOneAndReplace(filter, newObject,
                //    new FindOneAndReplaceOptions<T, T>()
                //    {
                //        IsUpsert = true
                //        //ReturnDocument = ReturnDocument.After
                //    });
                try
                {
                    dynamic eoDynamic = newObject;
                    var updatedObject = JsonConvert.SerializeObject(eoDynamic);

                    updatedObject = JsonConvert.DeserializeObject<T>(updatedObject);
                    table.FindOneAndReplace<T>(filter, updatedObject);
                    return entity;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            else
            {
                table.InsertOne(entity);
            }
            return entity;
        }

        public T GetById(int iD)
        {
            var table = _settings.Value.Database.GetCollection<T>(CollectionName);
            var item = table.AsQueryable().FirstOrDefault(x => x.Id == iD && x.IsDeleted == false);
            return item;
        }

        public T Add(T insertEntity)
        {
            insertEntity.Id = GetAutoId();
            _settings.Value.Database.GetCollection<T>(CollectionName).InsertOne(insertEntity);
            return insertEntity;
        }

        public IEnumerable<T> AddAll(IEnumerable<T> insertListEntity)
        {
            var listEntity = insertListEntity as IList<T> ?? insertListEntity.ToList();
            _settings.Value.Database.GetCollection<T>(CollectionName).InsertMany(listEntity);
            return listEntity;
        }

        public bool Update(T updateEntity)
        {
             
            var baseModel = (BaseModel)updateEntity;
            var filter = Builders<T>.Filter.Eq("_id", baseModel.Id);
            var update = Builders<T>.Update;

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var key = propertyInfo.Name;
                var value = updateEntity.GetType().GetProperty(key).GetValue(updateEntity);

             //   update.AddToSet(key, value);
                update.Set(key, value);

            }
        
            var result = _collection.UpdateOne(filter, update.ToBsonDocument(), new UpdateOptions());

            return result.ModifiedCount != 0;
        }

        public bool Delete(int id)
        {
            var table = _settings.Value.Database.GetCollection<T>(CollectionName);
            var item = table.AsQueryable().FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.IsDeleted = true;
                return Update(item);
            }
            else
            {
                return false;
            }
        }
        public bool DeleteObj(int id)
        {

            var collection = _settings.Value.Database.GetCollection<T>(CollectionName); 
            var res = collection.DeleteOne(x => x.Id == id);
            return res.IsAcknowledged;

        }

        public bool Exists(Guid entityId)
        {
            var filter = Builders<T>.Filter.Eq("_id", entityId);
            return _collection.Find(filter).Any();
        }

        public bool Exists(Expression<Func<T, bool>> exp)
        {
            return _collection.Find(exp).Any();
        }

        private int GetAutoId()
        {
            var document = _settings.Value.Database.GetCollection<T>(CollectionName).Find(new BsonDocument())
                .SortByDescending(x => x.Id).FirstOrDefault();
            if (document != null)
            {
                return document.Id + 1;
            }
            else
            {
                return 1;
            }
        }
    }
}
