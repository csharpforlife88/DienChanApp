using System;
using DienChanApp.Models;

namespace DienChanApp.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        private int _id;
        public int ID 
        { 
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _userName;
        public string UserName 
        { 
            get { return _userName; } 
            set {SetProperty(ref _userName, value);}
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private int _permissionId;
        public int PermissionId
        {
            get { return _permissionId; }
            set { SetProperty(ref _permissionId, value); }
        }

        private UserPermission _permission;
        public UserPermission Permission
        {
            get { return _permission; }
            set { SetProperty(ref _permission, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _email;
        public string Email 
        { 
            get { return _email; } 
            set { SetProperty(ref _email, value); }
        }

        private DateTime _createdDate;
        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { SetProperty(ref _createdDate, value); }
        }
    }
}
