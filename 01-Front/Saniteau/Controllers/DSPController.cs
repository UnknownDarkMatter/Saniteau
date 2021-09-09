using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Saniteau.Models;

namespace Saniteau.Controllers
{
    [Authorize(Policy = Constants.JWT.ApiAccessPolicyName)]
    public class DSPController : Controller
    {
        public DSPController()
        {
        }
    }
}
