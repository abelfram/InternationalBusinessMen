using Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.RepositoryContracts
{
    public interface IRatesRepository
    {
        public List<RateDomainEntity> GetAll();
    }
}
