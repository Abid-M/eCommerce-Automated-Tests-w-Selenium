/* Author: Abid Miah */
namespace uk.co.nfocus.ecommerceproject.Utils
{
    public class Customer
    {
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Postcode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }

    // No Constructor Needed. 
    // With Constructors test fails due to: "BoDi.ObjectContainerException : Primitive types or structs cannot be resolved"
}
