using Saniteau.Common;
using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Commands
{
    public class GetAllFacturationsCommand : IAction<List<Model.Facturation>>
    {
    }
}
