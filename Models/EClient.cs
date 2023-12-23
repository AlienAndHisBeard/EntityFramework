using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework.Models
{
    /// <summary>
    /// Electronic user/client with additional IpAddress for online orders
    /// </summary>
    public class EClient : Client
    {
        public string IpAddress { get; set; }
    }
}
