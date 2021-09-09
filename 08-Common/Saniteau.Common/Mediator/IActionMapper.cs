using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public interface IActionMapper<TContractAction, TDomainAction>
    {
        TDomainAction Map(TContractAction action, IActionValidation<TContractAction> validation);
    }
}
