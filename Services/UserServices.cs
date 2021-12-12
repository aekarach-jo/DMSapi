using MongoDB.Driver;
using System.Collections.Generic;
using System;
using System.Linq;
using DMSapi.Models;

namespace DMSapi.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _User;
        public UserService(DatabaseSetting settings)
        {
            var Client = new MongoClient(settings.ConnectionString);
            var database = Client.GetDatabase(settings.DatabaseName);
            var filter = Builders<User>.Filter.Where(user => user.Status == "Open");
            _User = database.GetCollection<User>(settings.UserCollection);
        }

       public List<User> GetUserAll() => _User.Find(user => user.Status == "Open").ToList();
       public List<User> GetUserAlls() => _User.Find(user => true).ToList();
       public User GetUserById(string id) => _User.Find<User>(user => user.UserId == id).FirstOrDefault();
        
       public User CreateUser(User user){
           _User.InsertOne(user);
           return user;
       } 
       public void EditUser(string id, User userIn) => _User.ReplaceOne(user => user.UserId == id, userIn);
       public string DeleteUser(string id, User userIn) {
            _User.ReplaceOne(user => user.UserId == id, userIn);
            return "Delete Succeess";
            }
    }
}