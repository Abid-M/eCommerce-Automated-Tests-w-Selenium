/* Author: Abid Miah */
namespace uk.co.nfocus.ecommerceproject.Utils
{
    public class Customer
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }

    // No Constructor Needed. As using Step Argument Conversion (CreateInstance)
    // With Constructors test fails due to: "BoDi.ObjectContainerException : Primitive types or structs cannot be resolved".
}
