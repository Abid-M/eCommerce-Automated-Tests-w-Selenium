/* Author: Abid Miah */
namespace uk.co.nfocus.ecommerceproject.Utils
{
    internal class Customer
    {
        internal string _fName { get; set; }
        internal string _lName { get; set; }
        internal string _address { get; set; }
        internal string _city { get; set; }
        internal string _postcode { get; set; }
        internal string _phone { get; set; }
        internal string _email { get; set; }

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
