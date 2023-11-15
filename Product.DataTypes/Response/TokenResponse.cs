using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.DataTypes.Response
{
    public class TokenResponse
    {
        public bool IsAutheticate { get; set; }
        public string? Token { get; set; }
        public string? Error { get; set; }
    }
}
