using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{

    /// <summary>
    /// Electronic/online order with IpAddress of the buyer
    /// </summary>
    public class EOrder : Order
    {
        public string IpAddress { get; set; }
    }
}
