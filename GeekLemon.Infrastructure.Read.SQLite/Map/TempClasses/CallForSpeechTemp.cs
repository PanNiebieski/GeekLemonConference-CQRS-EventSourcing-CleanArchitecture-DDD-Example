using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekLemon.Persistence.Dapper.SQLite.TempClasses
{
    public class CallForSpeechTemp
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public int Version { get; set; }

        public string Number { get; set; }
        public int Status { get; set; }

        public string PreliminaryDecision_Date { get; set; }
        public int? PreliminaryDecision_DecisionBy { get; set; }

        public string FinalDecision_Date { get; set; }
        public int? FinalDecision_DecisionBy { get; set; }

        public string Speaker_Name_First { get; set; }
        public string Speaker_Name_Last { get; set; }
        public string Speaker_Adress_Country { get; set; }
        public string Speaker_Adress_ZipCode { get; set; }
        public string Speaker_Adress_City { get; set; }
        public string Speaker_Adress_Street { get; set; }
        public string Speaker_Websites_Facebook { get; set; }
        public string Speaker_Websites_Twitter { get; set; }
        public string Speaker_Websites_Instagram { get; set; }
        public string Speaker_Websites_LinkedIn { get; set; }
        public string Speaker_Websites_TikTok { get; set; }
        public string Speaker_Websites_Youtube { get; set; }
        public string Speaker_Websites_FanPageOnFacebook { get; set; }
        public string Speaker_Websites_GitHub { get; set; }
        public string Speaker_Websites_Blog { get; set; }
        public string Speaker_BIO { get; set; }
        public string Speaker_Contact_Phone { get; set; }
        public string Speaker_Contact_Email { get; set; }
        public string Speaker_Birthdate { get; set; }

        public string Speech_Title { get; set; }
        public string Speech_Description { get; set; }
        public string Speech_Tags { get; set; }
        public int Speech_ForWhichAudience { get; set; }
        public int Speech_TechnologyOrBussinessStory { get; set; }
        public string Registration_RegistrationDate { get; set; }
        public int CategoryId { get; set; }
        public int Score_Score { get; set; }

        public string Score_RejectExplanation { get; set; }
        public string Score_WarringExplanation { get; set; }

        public string Category_DisplayName { get; set; }
        public string Category_WhatWeAreLookingFor { get; set; }
        public string Category_Name { get; set; }

    }
}
