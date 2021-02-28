using AutoMapper;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain.Entity;
using System;
using System.Globalization;
using System.Linq;

namespace GeekLemon.Persistence.Dapper.SQLite.Map
{
    public class CallForSpeechTempTypeConverter : ITypeConverter<CallForSpeech, CallForSpeechTemp>
    {
        public CallForSpeechTemp Convert(CallForSpeech source, CallForSpeechTemp destination, ResolutionContext context)
        {
            CallForSpeechTemp sc = new CallForSpeechTemp();

            sc.CategoryId = source.Category.Id.Value;
            sc.Number = source.Number.Number;
            sc.Category_DisplayName = source.Category.DisplayName;
            sc.Category_Name = source.Category.Name;
            sc.Category_WhatWeAreLookingFor = source.Category.WhatWeAreLookingFor;
            sc.FinalDecision_Date = source.FinalDecision?.DecisionDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                        CultureInfo.InvariantCulture);
            sc.FinalDecision_DecisionBy = source.FinalDecision?.DecisionBy?.Value;
            sc.PreliminaryDecision_Date = source.PreliminaryDecision?.DecisionDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                        CultureInfo.InvariantCulture);
            sc.PreliminaryDecision_DecisionBy = source.PreliminaryDecision?.DecisionBy?.Value;
            sc.Registration_RegistrationDate = source.Registration.RegistrationDate.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                        CultureInfo.InvariantCulture);
            sc.Score_RejectExplanation = source.Score?.RejectExplanation;
            sc.Score_WarringExplanation = source.Score?.WarringExplanation;
            sc.Score_Score = (int)(source.Score?.Score ?? 0);

            if (source.Id != null)
                sc.Id = source.Id.Value;

            sc.Speaker_Adress_City = source.Speaker.Address.City;
            sc.Speaker_Adress_Country = source.Speaker.Address.Country;
            sc.Speaker_Adress_Street = source.Speaker.Address.Street;
            sc.Speaker_Adress_ZipCode = source.Speaker.Address.ZipCode;
            sc.Speaker_BIO = source.Speaker.Biography;
            sc.Speaker_Birthdate = source.Speaker.Birthdate.ToString("dd-MM-yyyy");
            sc.Speaker_Contact_Email = source.Speaker.Contact.Email;
            sc.Speaker_Name_First = source.Speaker.Name.First;
            sc.Speaker_Name_Last = source.Speaker.Name.Last;
            sc.Speaker_Contact_Phone = source.Speaker.Contact.Phone;
            sc.Speaker_Websites_Blog = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_Facebook = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_FanPageOnFacebook = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_GitHub = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_Instagram = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_LinkedIn = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_TikTok = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_Twitter = source.Speaker.SpeakerWebsites.Blog;
            sc.Speaker_Websites_Youtube = source.Speaker.SpeakerWebsites.Blog;
            sc.Speech_Description = source.Speech.Description;
            sc.Speech_ForWhichAudience = (int)source.Speech.ForWhichAudience;
            sc.Speech_Tags = string.Join(",", source.Speech.Tags);
            sc.Speech_TechnologyOrBussinessStory = (int)source.Speech.TechnologyOrBussinessStory;
            sc.Speech_Title = source.Speech.Description;
            sc.Version = source.Version;
            sc.UniqueId = source.UniqueId.Value.ToString();
            sc.Status = (int)source.Status;




            return sc;
        }
    }
}
