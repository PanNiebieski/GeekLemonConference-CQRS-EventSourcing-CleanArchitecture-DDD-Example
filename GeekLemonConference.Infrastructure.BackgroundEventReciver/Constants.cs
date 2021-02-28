using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Infrastructure.BackgroundEventHandlersServer
{
    public class Constants
    {
        public const string QUEUE_JUDGE_CREATED = "judge_created";
        public const string QUEUE_JUDGE_UPDATED = "judge_updated";
        public const string QUEUE_JUDGE_DELETED = "judge_deleted";

        public const string QUEUE_CATEGORY_CREATED = "category_created";
        public const string QUEUE_CATEGORY_UPDATED = "category_updated";

        public const string QUEUE_CALLFORSPEECH_SUBMITC = "callforspeech_submit";
        public const string QUEUE_CALLFORSPEECH_REJECTC = "callforspeech_reject";
        public const string QUEUE_CALLFORSPEECH_PRELIMINARY_ACCEPT = "callforspeech_preminal_accept";
        public const string QUEUE_CALLFORSPEECH_EVALUATE = "callforspeech_evaluate";
        public const string QUEUE_CALLFORSPEECH_ACCEPT = "callforspeech_accept";
    }
}
