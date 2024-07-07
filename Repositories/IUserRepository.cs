using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    Task<User> CreateUser(User user);
    Task<bool> DeleteUser(int userId);
    Task<bool> ValidateUser(string userName, string userPassword);
}