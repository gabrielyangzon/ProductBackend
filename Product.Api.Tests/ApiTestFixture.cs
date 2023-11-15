using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Product.Api.Tests
{
    public class ApiTestFixture : WebApplicationFactory<Program>
    {
    }
}
