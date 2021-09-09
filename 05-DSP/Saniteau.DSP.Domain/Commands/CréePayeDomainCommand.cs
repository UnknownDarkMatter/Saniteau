using Saniteau.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.DSP.Domain.Commands
{
    public class CréePayeDomainCommand
    {
        public Date DatePaye { get; private set; }
        public IdDelegant IdDelegant { get; private set; }

        public CréePayeDomainCommand(Date datePaye, IdDelegant idDelegant)
        {
            if (datePaye is null) { throw new ArgumentNullException(nameof(datePaye)); }
            if (idDelegant is null) { throw new ArgumentNullException(nameof(idDelegant)); }

            DatePaye = datePaye;
            IdDelegant = idDelegant;
        }
    }
}
