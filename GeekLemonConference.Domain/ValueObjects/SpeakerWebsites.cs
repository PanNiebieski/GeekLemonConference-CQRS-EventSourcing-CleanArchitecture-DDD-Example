using GeekLemonConference.Domain.Ddd;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekLemonConference.Domain.ValueObjects
{
    public class SpeakerWebsites : ValueObject<SpeakerWebsites>
    {
        public string Facebook { get; init; }
        public string LinkedIN { get; init; }
        public string Twitter { get; init; }
        public string Instagram { get; init; }
        public string TikTok { get; init; }
        public string YouTube { get; init; }
        public string FanPageOnFacebook { get; init; }

        public string GitHub { get; init; }
        public string Blog { get; init; }

        public SpeakerWebsites(string facebook = null, string linkedIN = null,
            string twitter = null, string instagram = null, string tikTok = null,
            string gitHub = null, string blog = null)
        {
            Facebook = facebook;
            LinkedIN = linkedIN;
            Twitter = twitter;
            Instagram = instagram;
            TikTok = tikTok;
            GitHub = gitHub;
            Blog = blog;
        }

        public SpeakerWebsites()
        {

        }

        public bool HaveSocialMedia()
        {
            if (!string.IsNullOrWhiteSpace(Facebook)
                || !string.IsNullOrWhiteSpace(LinkedIN)
                || !string.IsNullOrWhiteSpace(Twitter)
                || !string.IsNullOrWhiteSpace(Instagram)
                || !string.IsNullOrWhiteSpace(TikTok)
                || !string.IsNullOrWhiteSpace(YouTube)
                || !string.IsNullOrWhiteSpace(FanPageOnFacebook))
                return true;

            return false;
        }

        public bool HaveGitHub()
        {
            if (!string.IsNullOrWhiteSpace(GitHub))
                return true;

            return false;
        }

        public bool HaveBlog()
        {
            if (!string.IsNullOrWhiteSpace(Blog))
                return true;

            return false;
        }


        protected override IEnumerable<object> GetAttributesToIncludeInEqualityCheck()
        {
            yield return Facebook;
            yield return LinkedIN;
            yield return Twitter;
            yield return Instagram;
            yield return TikTok;
            yield return GitHub;
            yield return Blog;
        }
    }
}
