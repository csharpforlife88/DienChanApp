using System;
namespace DienChanApp.ViewModels
{
    public class CustomerViewModel:BaseViewModel
    {
        private int _customerId;
        public int CustomerId
        {
            get { return _customerId; }
            set { SetProperty(ref _customerId, value); }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set{SetProperty(ref _firstName, value);}
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        public string FullName {
            get { return FirstName + " " + LastName; }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private string _address1;
        public string Address1
        {
            get { return _address1; }
            set { SetProperty(ref _address1, value); }
        }

        private string _address2;
        public string Address2
        {
            get { return _address2; }
            set { SetProperty(ref _address2, value); }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set { SetProperty(ref _state, value); }
        }

        private string _zip;
        public string Zip
        {
            get { return _zip; }
            set { SetProperty(ref _zip, value); }
        }

        private string _country;
        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }
    }
}
