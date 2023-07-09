using Microsoft.Build.Framework;

namespace EndPoint.Site.Models
{
    public class CheckOutAddress
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Street { get; set; }

        public string Unit { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string County { get; set; }
        public string PostCode { get; set; }
        [Required]
        public string Mobile { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
