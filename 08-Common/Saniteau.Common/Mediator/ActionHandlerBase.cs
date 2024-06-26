﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saniteau.Common.Mediator
{
    public abstract class ActionHandlerBase<TContractAction, TDomainAction> : IActionHandler<TContractAction> where TContractAction : IAction
    {
        private readonly IActionMapper<TContractAction, TDomainAction> _actionMapper;

        public ActionHandlerBase(IActionMapper<TContractAction, TDomainAction> actionMapper)
        {
            if(actionMapper == null) { throw new ArgumentNullException(nameof(actionMapper)); }

            _actionMapper = actionMapper;
        }

        public virtual void Handle(TContractAction action)
        {
            if (action == null) { throw BadRequestException.ActionIsNull(typeof(TContractAction)); }

            var validation = new ActionValidation<TContractAction>();
            var domainAction = _actionMapper.Map(action, validation);
            if (validation.HasErrors()) { throw BadRequestException.ActionHasErrors<TContractAction>(validation.GetErrors()); }

            Handle(domainAction!);//null forgiveness operator
        }

        public async virtual Task HandleAsync(TContractAction action)
        {
            if (action == null) { throw BadRequestException.ActionIsNull(typeof(TContractAction)); }

            var validation = new ActionValidation<TContractAction>();
            var domainAction = _actionMapper.Map(action, validation);
            if (validation.HasErrors()) { throw BadRequestException.ActionHasErrors<TContractAction>(validation.GetErrors()); }

            await HandleAsync(domainAction!);//null forgiveness operator
        }

        protected abstract void Handle(TDomainAction action);
        protected virtual async Task HandleAsync(TDomainAction action)
        {
            Handle(action);
        }

    }

    public abstract class ActionHandlerBase<TContractAction, TDomainAction, TDomainResult, TContractResult> : IActionHandler<TContractAction, TContractResult>
        where TContractAction : IAction<TContractResult>
    {
        private readonly IActionMapper<TContractAction, TDomainAction> _actionMapper;
        private readonly IMapper<TDomainResult, TContractResult> _resultMapper;

        public ActionHandlerBase(IActionMapper<TContractAction, TDomainAction> actionMapper, IMapper<TDomainResult, TContractResult> resultMapper)
        {
            if (actionMapper == null) { throw new ArgumentNullException(nameof(actionMapper)); }
            if (resultMapper == null) { throw new ArgumentNullException(nameof(resultMapper)); }

            _actionMapper = actionMapper;
            _resultMapper = resultMapper;
        }

        public TContractResult Handle(TContractAction action)
        {
            if (action == null) { throw BadRequestException.ActionIsNull(typeof(TContractAction)); }

            var validation = new ActionValidation<TContractAction>();
            TDomainAction domainAction = _actionMapper.Map(action, validation);
            if (validation.HasErrors()) { throw BadRequestException.ActionHasErrors<TContractAction>(validation.GetErrors()); }

            TDomainResult domainResult = Handle(domainAction!);
            TContractResult contractResult = _resultMapper.Map(domainResult);

            return contractResult;
        }
        public async Task<TContractResult> HandleAsync(TContractAction action)
        {
            if (action == null) { throw BadRequestException.ActionIsNull(typeof(TContractAction)); }

            var validation = new ActionValidation<TContractAction>();
            TDomainAction domainAction = _actionMapper.Map(action, validation);
            if (validation.HasErrors()) { throw BadRequestException.ActionHasErrors<TContractAction>(validation.GetErrors()); }

            TDomainResult domainResult = await HandleAsync(domainAction!);
            TContractResult contractResult = _resultMapper.Map(domainResult);

            return contractResult;
        }

        protected abstract TDomainResult Handle(TDomainAction action);
        protected virtual async Task<TDomainResult> HandleAsync(TDomainAction action)
        {
            return Handle(action);
        }
    }

    public abstract class ActionHandlerBase<TContractAction, TDomainAction, TContractResult> : IActionHandler<TContractAction, TContractResult>
        where TContractAction : IAction<TContractResult>
    {
        private readonly IActionMapper<TContractAction, TDomainAction> _actionMapper;

        public ActionHandlerBase(IActionMapper<TContractAction, TDomainAction> actionMapper)
        {
            if (actionMapper == null) { throw new ArgumentNullException(nameof(actionMapper)); }

            _actionMapper = actionMapper;
        }

        public TContractResult Handle(TContractAction action)
        {
            if (action == null) { throw BadRequestException.ActionIsNull(typeof(TContractAction)); }

            var validation = new ActionValidation<TContractAction>();
            TDomainAction domainAction = _actionMapper.Map(action, validation);
            if (validation.HasErrors()) { throw BadRequestException.ActionHasErrors<TContractAction>(validation.GetErrors()); }

            TContractResult contractResult = Handle(domainAction!);

            return contractResult;
        }

        public async Task<TContractResult> HandleAsync(TContractAction action)
        {
            if (action == null) { throw BadRequestException.ActionIsNull(typeof(TContractAction)); }

            var validation = new ActionValidation<TContractAction>();
            TDomainAction domainAction = _actionMapper.Map(action, validation);
            if (validation.HasErrors()) { throw BadRequestException.ActionHasErrors<TContractAction>(validation.GetErrors()); }

            TContractResult contractResult = await HandleAsync(domainAction!);

            return contractResult;
        }

        protected abstract TContractResult Handle(TDomainAction action);
        protected virtual async Task<TContractResult> HandleAsync(TDomainAction action)
        {
            return Handle(action);
        }

    }

}
