using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aj.DataAccess.Service
{
    public class SingletoneService : ISingletoneService
    {
        private readonly Guid Id;
        public SingletoneService()
        {
            Id = Guid.NewGuid();
        }
        public string GetGuid()
        {
            return Id.ToString();
        }
    }
}
