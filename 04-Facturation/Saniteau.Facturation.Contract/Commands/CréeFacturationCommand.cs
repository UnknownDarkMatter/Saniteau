using Saniteau.Common;
using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Commands
{
    public class CréeFacturationCommand : IAction<List<Model.Facturation>>
    {
        public DateTime DateFacturation { get; set; }
        public CréeFacturationCommand(DateTime dateFacturation)
        {
            DateFacturation = dateFacturation;
        }
    }
}
