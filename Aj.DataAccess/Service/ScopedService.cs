using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Service
{
    public class ScopedService : IScopedService
    {
        private readonly Guid Id;
        public ScopedService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
