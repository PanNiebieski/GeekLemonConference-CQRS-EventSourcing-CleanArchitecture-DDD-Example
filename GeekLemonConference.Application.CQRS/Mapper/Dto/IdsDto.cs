using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Mapper.Dto
{
    public class IdsDto
    {
        public int CreatedId { get; set; }
        public Guid UniqueId { get; set; }
        public string Status { get; set; }
    }
}
