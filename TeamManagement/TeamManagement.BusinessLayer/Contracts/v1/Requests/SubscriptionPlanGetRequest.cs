using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TeamManagement.BusinessLayer.Contracts.v1.Requests
{
    public class SubscriptionPlanGetRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public int ProjectsQuantity { get; set; }
        public int TeamsQuantity { get; set; }
        public string Description { get; set; }
        //public SubscriptionGetRequest Subscription { get; set; }
    }
}
