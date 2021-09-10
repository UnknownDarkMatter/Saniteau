using Saniteau.Common.Mediator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saniteau.Facturation.Contract.Commands
{
    public class GetFacturationCommand : IAction<Model.Facturation>
    {
        public int IdAbonne { get; set; }
        public int IdFacturation { get; set; }

        public GetFacturationCommand(int idFacturation, int idAbonne)
        {
            IdFacturation = idFacturation;
            IdAbonne = idAbonne;
        }
    }   
}
