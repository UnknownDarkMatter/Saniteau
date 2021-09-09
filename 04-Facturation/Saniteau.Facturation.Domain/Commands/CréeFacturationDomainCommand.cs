using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Domain.Commands
{
    public class CréeFacturationDomainCommand
    {
        public DateTime DateFacturation { get; }

        public CréeFacturationDomainCommand(DateTime dateFacturation)
        {
            DateFacturation = dateFacturation;
        }
    }
}
