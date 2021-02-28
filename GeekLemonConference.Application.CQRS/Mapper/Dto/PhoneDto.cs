using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Mapper.Dto
{
    public class PhoneDto
    {
        public string Type { get; set; }

        public int AreaCode { get; set; }

        public string Number { get; set; }
    }
}
