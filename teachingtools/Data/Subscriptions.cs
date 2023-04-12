using Azure.Identity;
using System.ComponentModel.DataAnnotations;

namespace teachingtools.Data
{
    public class Subscriptions
    {
        [Key]
        public string UserName { get; set; }
        public bool SubscriptionType { get; set; }
    }
}
