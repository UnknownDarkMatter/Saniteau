using Saniteau.Facturation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saniteau.Facturation.Application.Mappers
{
    public class ApiResponseMapper<TResult>
    {
        public static Contract.Model.ApiResponse<TResult> Map(Domain.ApiResponse<TResult> apiResponse)
        {
            var result = new Contract.Model.ApiResponse<TResult>();
            result.Result = apiResponse.Result;
            result.IsError = apiResponse.IsError;
            result.ErrorMessage = apiResponse.ErrorMessage;
            return result;
        }
    }
}
