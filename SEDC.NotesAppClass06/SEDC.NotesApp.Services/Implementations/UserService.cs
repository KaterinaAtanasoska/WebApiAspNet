using SEDC.NotesApp.DataAccess;
using SEDC.NotesApp.Domain.Models;
using SEDC.NotesApp.Mappers;
using SEDC.NotesApp.Models;
using SEDC.NotesApp.Services.Interfaces;
using SEDC.NotesApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.NotesApp.Services.Implementations
{
    public class UserService : IUserService
    {   
        private IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;                       
        }
        public void AddUser(UserModel userModel)
        {
            ValidateUserModel(userModel);

            User user = userModel.ToUser();            
            _userRepository.Add(user);

        }

        public void DeleteUser(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException(id);
            }
            _userRepository.Delete(userDb);
        }

        public List<UserModel> GetAllUsers()
        {
            List<User> usersDb = _userRepository.GetAll();
            List<UserModel> userModels = new List<UserModel>();
            foreach (User user in usersDb)
            {
               userModels.Add(user.ToUserModel());
            }

            return userModels;
        }

        public UserModel GetUserById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {             
                throw new NotFoundException($"User with id {id} was not found!");               
            }
            return userDb.ToUserModel();
        }

        public void UpdateUser(UserModel userModel)
        {
            User userDb = _userRepository.GetById(userModel.Id);
            if (userDb == null)
            {
                throw new NotFoundException(userModel.Id);
            }

            userDb.FirstName = userModel.FirstName;
            userDb.LastName = userModel.LastName;
            userDb.Address = userModel.Address;
            userDb.Username = userModel.Username;
            
            _userRepository.Update(userDb);
        }

        private void ValidateUserModel(UserModel userModel)
        {            
            if (string.IsNullOrEmpty(userModel.Username))
            {
                throw new UserException("Required username");
            }
            if (userModel.FirstName.Length > 50)
            {
                throw new UserException("FirstName can not contain more than 50 characters");
            }
            if (userModel.LastName.Length > 30)
            {
                throw new UserException("LastName can not contain more than 50 characters");
            }
            if (userModel.Address.Length > 150)
            {
                throw new UserException("Address can not contain more than 50 characters");
            }                        
        }
    }
}
