using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Service
{
    public class TrnsientService : ITransientService
    {
        private readonly Guid Id;
        public TrnsientService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
