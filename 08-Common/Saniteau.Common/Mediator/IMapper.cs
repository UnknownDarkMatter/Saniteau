using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Common.Mediator
{
    public interface IMapper<in TInput, out TOutput>
    {
        TOutput Map(TInput input);
        IEnumerable<TOutput> Map(IEnumerable<TInput> input);
    }
}
