using System;
namespace DienChanApp.ViewModels
{
    public class UserViewModel: BaseViewModel
    {
        private int _id;
        public int ID 
        { 
            get { return _id; }
            set{OnPropertyChanged();}
        }

        private string _name;
        public string Name 
        { 
            get { return _name; } 
            set {OnPropertyChanged();}
        }

        private string _email;
        public string Email 
        { 
            get { return _email; } 
            set {OnPropertyChanged();}
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set {OnPropertyChanged(); }
        }

        private string _company;
        public string Company
        {
            get { return _company; }
            set{OnPropertyChanged();}
        }
    }

    public class Address 
    {
        public string street { get; set; }
        public string suite { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
    }
}
