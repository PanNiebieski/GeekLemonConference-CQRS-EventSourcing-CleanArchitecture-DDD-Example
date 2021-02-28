using System;

namespace GeekLemonConference.Application.CQRS.Dto
{
    public class CreateJudgeRequest
    {
        //public Guid UniqueId { get; set; }
        //public int Id { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public NameDto Name { get; set; }

        public int CategoryId { get; set; }

        //public List<EmailDto> Emails { get; set; }
        public DateTime Birthdate { get; set; }
        //public List<PhoneDto> Phones { get; set; }
    }
}
