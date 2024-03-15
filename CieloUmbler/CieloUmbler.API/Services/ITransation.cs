using CieloUmbler.API.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CieloUmbler.API.Services
{
    public interface ITransation
    {
        Task<Dictionary<string, object>> QueryCardBin(string cardBin);
        Task<Dictionary<string, object>> TokenCard(BaseEntity entity);
        Task<Dictionary<string, object>> AuthenticateCard(BaseEntity entity);
        Task<Dictionary<string, object>> Capture(string tokenCapture);
        Task<Dictionary<string, object>> CancelProcess();

    }
}
