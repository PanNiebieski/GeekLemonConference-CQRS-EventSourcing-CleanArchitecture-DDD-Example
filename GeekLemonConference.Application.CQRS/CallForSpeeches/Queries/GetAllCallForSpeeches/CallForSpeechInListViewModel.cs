using GeekLemonConference.Application.CQRS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemonConference.Application.CQRS.Queries.CallForSpeeches.GetAllCallForSpeeches
{
    public class CallForSpeechInListViewModel
    {
        public int Id { get; set; }
        public SpeechDto Speech { get; set; }
        public CategoryDto Category { get; set; }
        public string Status { get; set; }
        public int Version { get; set; }

        public Guid UniqueId { get; set; }
    }
}
