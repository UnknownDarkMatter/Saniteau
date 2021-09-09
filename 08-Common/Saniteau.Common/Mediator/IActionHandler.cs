using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public interface IActionHandler<in TAction> where TAction:IAction
    {
        void Handle(TAction action);
    }
    public interface IActionHandler<in TAction, out TResult> where TAction : IAction<TResult>
    {
        TResult Handle(TAction action);
    }
}
