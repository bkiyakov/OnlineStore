using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.API.Models
{
    public class OrderConfirmInputModel
    {
        [JsonProperty("ShipmentDate")]
        [Required]
        public DateTime ShipmentDate { get; set; }
    }
}
