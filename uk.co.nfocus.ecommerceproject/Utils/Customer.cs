/* Author: Abid Miah */
namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class Customer
    {
        internal string _fName;
        internal string _lName;
        internal string _address;
        internal string _city;
        internal string _postcode;
        internal string _phone;
        internal string _email;

        public Customer(string fName, string lName, string address, string city, string postcode, string phone, string email)
        {
            this._fName = fName;
            this._lName = lName;
            this._address = address;
            this._city = city;
            this._postcode = postcode;
            this._phone = phone;
            this._email = email;
        }
    }
}
