using AutoMapper;
using GeekLemon.Persistence.Dapper.SQLite.TempClasses;
using GeekLemonConference.Domain.Entities;
using GeekLemonConference.Domain.Entity;
using GeekLemonConference.Domain.ValueObjects;
using GeekLemonConference.Domain.ValueObjects.Ids;
using System;

namespace GeekLemon.Persistence.Dapper.SQLite.Map
{
    public class CallForSpeechTypeConverter : ITypeConverter<CallForSpeechTemp, CallForSpeech>
    {
        public CallForSpeech Convert(CallForSpeechTemp source, CallForSpeech destination, ResolutionContext context)
        {
            Category c = new Category(new CategoryId(source.CategoryId))
            {
                DisplayName = source.Category_DisplayName,
                Name = source.Category_DisplayName,
                WhatWeAreLookingFor = source.Category_WhatWeAreLookingFor,
            };

            SpeakerWebsites socialMedia = new SpeakerWebsites()
            {
                Blog = source.Speaker_Websites_Blog,
                Facebook = source.Speaker_Websites_Facebook,
                FanPageOnFacebook = source.Speaker_Websites_FanPageOnFacebook,
                GitHub = source.Speaker_Websites_GitHub,
                Instagram = source.Speaker_Websites_Instagram,
                LinkedIN = source.Speaker_Websites_LinkedIn,
                TikTok = source.Speaker_Websites_TikTok,
                Twitter = source.Speaker_Websites_Twitter,
                YouTube = source.Speaker_Websites_Youtube
            };

            Address address = new Address
                (
                    source.Speaker_Adress_Country,
                    source.Speaker_Adress_ZipCode,
                    source.Speaker_Adress_Street,
                    source.Speaker_Adress_City
                );

            Name name = new Name(
                source.Speaker_Name_First,
                source.Speaker_Name_Last);

            DateTime dateTime;
            bool C = DateTime.TryParse(source.Speaker_Birthdate, out dateTime);


            Contact cc = new Contact(source.Speaker_Contact_Email,
                source.Speaker_Contact_Phone);
            Speaker speaker = new Speaker(name, dateTime, address, socialMedia
                , source.Speaker_BIO, cc);


            CallForSpeechNumber callForSpeechNumber = new CallForSpeechNumber
                (source.Number);

            Speech speech = new Speech(source.Speech_Title, source.Speech_Description,
                source.Speech_Tags.Split(","),
                (ForWhichAudience)source.Speech_ForWhichAudience,
                (TechnologyOrBussinessStory)source.Speech_TechnologyOrBussinessStory);

            CallForSpeechStatus status = (CallForSpeechStatus)source.Status;


            CallForSpeechMachineScore callForSpeechMachineScore =
                (CallForSpeechMachineScore)source.Score_Score;



            CallForSpeechScoringResult res =
                new CallForSpeechScoringResult(callForSpeechMachineScore,
                source.Score_RejectExplanation, source.Score_WarringExplanation);

            DateTime dateTime2 = DateTime.Parse(source.Registration_RegistrationDate);
            Registration registration = new Registration(dateTime2);

            Decision decisionPreminal = null;
            try
            {
                DateTime dateTime3 = DateTime.Parse(source.PreliminaryDecision_Date);
                decisionPreminal =
                    new Decision(dateTime3, new JudgeId(source.PreliminaryDecision_DecisionBy.Value));

            }
            catch (Exception)
            {

            }

            Decision decisionFinal = null;
            try
            {
                DateTime dateTime4 = DateTime.Parse(source.FinalDecision_Date);
                decisionFinal =
                    new Decision(dateTime4, new JudgeId(source.FinalDecision_DecisionBy.Value));

            }
            catch (Exception)
            {

            }

            CallForSpeech sc = new CallForSpeech(callForSpeechNumber, status, speaker, speech, c,
                res, registration,
                decisionPreminal, decisionFinal, new CallForSpeechId(source.Id));

            if (source.UniqueId != "")
                sc.UniqueId = new CallForSpeechUniqueId(Guid.Parse(source.UniqueId));

            sc.Version = source.Version;

            return sc;


        }
    }
}
