BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "Categories" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"UniqueId"	TEXT NOT NULL,
	"Version"	INTEGER NOT NULL,
	"DisplayName"	TEXT,
	"Name"	TEXT,
	"WhatWeAreLookingFor"	TEXT,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "CallForSpeakes" (
	"Id"	INTEGER NOT NULL UNIQUE,
	"UniqueId"	TEXT NOT NULL,
	"Version"	INTEGER NOT NULL,
	"Number"	TEXT NOT NULL,
	"Status"	INTEGER NOT NULL,
	"PreliminaryDecision_DecisionBy"	NUMERIC,
	"PreliminaryDecision_Date"	TEXT,
	"FinalDecision_DecisionBy"	INTEGER,
	"FinalDecision_Date"	TEXT,
	"Speaker_Name_First"	TEXT NOT NULL,
	"Speaker_Name_Last"	TEXT NOT NULL,
	"Speaker_Adress_Country"	TEXT NOT NULL,
	"Speaker_Adress_ZipCode"	TEXT NOT NULL,
	"Speaker_Adress_City"	TEXT NOT NULL,
	"Speaker_Adress_Street"	TEXT NOT NULL,
	"Speaker_Websites_Facebook"	TEXT,
	"Speaker_Websites_Twitter"	TEXT,
	"Speaker_Websites_Instagram"	TEXT,
	"Speaker_Websites_LinkedIn"	TEXT,
	"Speaker_Websites_TikTok"	TEXT,
	"Speaker_Websites_Youtube"	REAL,
	"Speaker_Websites_FanPageOnFacebook"	REAL,
	"Speaker_Websites_GitHub"	TEXT,
	"Speaker_Websites_Blog"	TEXT,
	"Speaker_BIO"	TEXT NOT NULL,
	"Speaker_Contact_Phone"	TEXT,
	"Speaker_Contact_Email"	TEXT NOT NULL,
	"Speaker_Birthdate"	TEXT,
	"Speech_Title"	TEXT NOT NULL,
	"Speech_Description"	TEXT NOT NULL,
	"Speech_Tags"	TEXT NOT NULL,
	"Speech_ForWhichAudience"	INTEGER NOT NULL,
	"Speech_TechnologyOrBussinessStory"	INTEGER NOT NULL,
	"Registration_RegistrationDate"	TEXT,
	"CategoryId"	INTEGER NOT NULL,
	"Score_Score"	INTEGER,
	"Score_RejectExplanation"	TEXT,
	"Score_WarringExplanation"	TEXT,
	CONSTRAINT "FK_JudgeID_CallForSpeech_PreminaryDecision_DecysionBy" FOREIGN KEY("PreliminaryDecision_DecisionBy") REFERENCES "Judges",
	CONSTRAINT "FK_CategoryID_CallForSpeakes" FOREIGN KEY("CategoryId") REFERENCES "Categories",
	CONSTRAINT "FK_JudgeID_CallForSpeech_FinalDecision_DecysionBy" FOREIGN KEY("FinalDecision_DecisionBy") REFERENCES "Judges",
	CONSTRAINT "PK_CallForSpeakes_PK" PRIMARY KEY("Id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "Judges" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"UniqueId"	TEXT NOT NULL,
	"Version"	INTEGER NOT NULL,
	"Login"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	"BirthDate"	NUMERIC NOT NULL,
	"Name_First"	TEXT NOT NULL,
	"Name_Last"	TEXT NOT NULL,
	"CategoryID"	INTEGER NOT NULL,
	CONSTRAINT "FK_CategoryId_Judges" FOREIGN KEY("CategoryID") REFERENCES "Categories",
	CONSTRAINT "PK_Judges_KEY" PRIMARY KEY("ID" AUTOINCREMENT)
);
CREATE UNIQUE INDEX IF NOT EXISTS "CategoryID_Index" ON "Categories" (
	"ID"	DESC
);
CREATE UNIQUE INDEX IF NOT EXISTS "JudgesID_Index" ON "Judges" (
	"ID"	DESC
);
COMMIT;
