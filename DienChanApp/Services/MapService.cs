using System;
using DienChanApp.Models;
using DienChanApp.ViewModels;

namespace DienChanApp.Services
{
    public static class MapService
    {
        public static User MapUserViewModelToModel(UserViewModel vm)
        {
            return new User
            {
                id = vm.ID,
                name = vm.Name,
                phone = vm.Phone,
                email = vm.Email,
                company = vm.Company
            };
        }

        public static UserViewModel MapUserModelToViewModel(User m)
        {
            return new UserViewModel
            {
                ID = m.id,
                Name = m.name,
                Phone = m.phone,
                Email = m.email,
                Company = m.company
            };
        }
    }
}
