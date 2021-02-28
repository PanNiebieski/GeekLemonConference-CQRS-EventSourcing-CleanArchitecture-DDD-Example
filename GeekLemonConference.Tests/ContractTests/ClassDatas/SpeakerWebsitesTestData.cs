using GeekLemonConference.Domain.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GeekLemonConference.Tests.ContractTests.ClassDatas
{
    public class SpeakerWebsitesTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { new SpeakerWebsites() { Instagram = "http://instagram.pl" } };
            yield return new object[] { new SpeakerWebsites() { LinkedIN = "http://linkediIn.pl" } };
            yield return new object[] { new SpeakerWebsites() { TikTok = "http://TikTok.pl" } };
            yield return new object[] { new SpeakerWebsites() { Twitter = "http://TWITTER.PL" } };
            yield return new object[] { new SpeakerWebsites() { YouTube = "http://YOUTUBE.PL" } };
            yield return new object[] { new SpeakerWebsites() { FanPageOnFacebook = "http://FACEBOOK.PL" } };
            yield return new object[] { new SpeakerWebsites() { Facebook = "http://FACEBOOK.PL" } };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


    }
}
