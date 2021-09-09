using Saniteau.Common;
using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Contract.Commands
{
    public class CréePayeCommand : IAction
    {
        public Date DatePaye { get; private set; }
        public int IdDelegant { get; private set; }

        public CréePayeCommand(Date datePaye, int idDelegant)
        {
            DatePaye = datePaye;
            IdDelegant = idDelegant;
        }

    }
}
