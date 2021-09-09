using System;

namespace Saniteau.Common.Mediator
{
    public interface IActionHandlerMediator
    {
        void Handle<TAction>(TAction action) where TAction : IAction;

        TActionResult Handle<TAction, TActionResult>(TAction action) where TAction : IAction<TActionResult>;
    }
}
