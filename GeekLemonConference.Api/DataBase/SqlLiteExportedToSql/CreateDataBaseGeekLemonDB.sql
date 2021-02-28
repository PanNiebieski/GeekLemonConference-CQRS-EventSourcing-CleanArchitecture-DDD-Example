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
CREATE TABLE IF NOT EXISTS "Judges" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"UniqueId"	TEXT NOT NULL,
	"Version"	INTEGER NOT NULL,
	"Login"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	"BirthDate"	NUMERIC NOT NULL,
	"Name_First"	TEXT NOT NULL,
	"Name_Last"	TEXT NOT NULL,
	"Email_ForeConference"	TEXT,
	"Email_ForSpeakers"	TEXT,
	"Phone_ForSpekers"	TEXT,
	"Phone_ForConference"	TEXT,
	"CategoryID"	INTEGER NOT NULL,
	CONSTRAINT "FK_CategoryId_Judges" FOREIGN KEY("CategoryID") REFERENCES "Categories",
	CONSTRAINT "PK_Judges_KEY" PRIMARY KEY("ID" AUTOINCREMENT)
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
	CONSTRAINT "FK_CategoryID_CallForSpeakes" FOREIGN KEY("CategoryId") REFERENCES "Categories",
	CONSTRAINT "FK_JudgeID_CallForSpeech_PreminaryDecision_DecysionBy" FOREIGN KEY("PreliminaryDecision_DecisionBy") REFERENCES "Judges",
	CONSTRAINT "FK_JudgeID_CallForSpeech_FinalDecision_DecysionBy" FOREIGN KEY("FinalDecision_DecisionBy") REFERENCES "Judges",
	CONSTRAINT "PK_CallForSpeakes_PK" PRIMARY KEY("Id" AUTOINCREMENT)
);
INSERT INTO "Categories" VALUES (1,'c9f0802c-751d-11eb-9439-0242ac130002',1,'.NET','DotNet','Szukamy war-stories, czyli czegoś co osoba mówiąca przeżyła, brała czynny udział i wyszła na lub pod tarczą. Czyli życiowych problemów i rozwiązań. To taki nasz konik. W drugiej kolejności patrzymy na ciekawe przekrojowe tematy, które poruszają wzorce, style architektoniczne i inne ciekawe aspekty programowania w .net które mają głębszy sens a do tego nie przemijają z wiatrem. Następnie ciekaw narzędzia, które mogą nam pomóc i wesprzeć w codziennej pracy – na przykład testy konwencji za pomocą specjalnego narzędzia do dotnet które to umożliwia robić. Na końcu zaś biblioteki i frameworki.');
INSERT INTO "Categories" VALUES (2,'d427cdb6-751d-11eb-9439-0242ac130002',1,'Architektury Aplikacji','Architecture','Szukamy tematów, w których od razu wiadomo czego konkretnie uczestnik się dowie i co musi już wiedzieć.
Jeżeli masz temat ogólny i przekrojowy, to napisz jakie decyzje będziemy mogli lepiej podejmować dzięki tej prezentacji.
Jeżeli masz temat narzędziowy, to napisz jaki problem albo jaki cel
konkretnie możemy zaadresować po tej prezentacji.');
INSERT INTO "Categories" VALUES (3,'dc94f492-751d-11eb-9439-0242ac130002',1,'Soft Skills','SoftSkills','Kompetencje miękkie i budowanie relacji w biznesie inaczej. Ostatni rok pokazał, że żyjemy w bardzo niepewnych czasach. Potrzebujemy zatem nowych umiejętności, innowacyjnych rozwiązań i świeżego podejścia w tym jak wykonujemy swoją pracę. W tym roku stawiamy na kompetencje miękkie i budowanie relacji w biznesie po rebeliancku. Szukamy nowych, odważnych i eksperymentalnych metod współpracy między ludźmi oraz z klientami. Nawet jeśli użyłeś jakiejś metody tylko raz, ale prezentuje zupełnie nowy sposób myślenia, to jesteśmy nią zainteresowani.
Zaskocz nas i innych uczestników konferencji 4Developers 2021 i dołącz do rebelii!');
INSERT INTO "Categories" VALUES (4,'e04ac88c-751d-11eb-9439-0242ac130002',1,'Testerska','Tester','Zapraszamy każdą osobę, która chce się podzielić swoim doświadczeniem/projektem/porażkami/sukcesami/przemyśleniami w temacie testowania oprogramowania. Od początkującego do zaawansowanego. W pełni wspomożemy w przygotowaniach do poprowadzenia prelekcji.');
INSERT INTO "Categories" VALUES (5,'e411058a-751d-11eb-9439-0242ac130002',1,'Cloud','Cloud','Tematy na których będziemy się skupiać podczas tegorocznej edycji:
Chmura publiczna
Chmura hybrydowa
Multicloud
IaaS
IoT / Edge computing
Przetwarzanie danych
Security
Serverless');
INSERT INTO "Categories" VALUES (6,'e7ae72d6-751d-11eb-9439-0242ac130002',1,'PHP','PHP','Moja propozycja na motyw przyszłorocznej ścieżki PHP to „lepsze jutro w Twoim projekcie”. Rozumiem przez to praktyczne sposoby na wyjście z obecnych problemów w istniejących projektach od ludzi, którym to się udało. Wiele osób utknęło w projektach, gdzie nic się nie zmienia, bo nikt nie ma wystarczającej wiedzy ani doświadczenia (ani odwagi), żeby ruszyć coś, co działa od lat. Takie osoby przychodzą na konferencje, widzą te wszystkie nowe lśniące narzędzia, ale nie mają jak zasypać tego wąwozu pomiędzy własnymi projektami a „nowym wspaniałym światem”. Dzięki takiemu podejściu możemy dowolny temat pokazać od strony drogi, jaką przeszły projekty prelegentów. Zamiast opowiadać po raz kolejny o danym narzędziu, niech prelegent pokaże jaką drogę sam z nim przeszedł, im bardziej „mięsiście”, tym lepiej. Oczywiście zachowamy balans, tj. na pewno pojawią się propozycje o PHP8, testowaniu, frameworkach, etc., ale byłoby interesujące, gdyby prelegenci pokazali temat z ww. perspektywy.');
INSERT INTO "Categories" VALUES (7,'ed879386-751d-11eb-9439-0242ac130002',1,'Java','Java','Szukamy tematów, które związane są z całym ekosystemem JVM. Mogą dotyczyć przykładowo, nowości w języku, ciekawostek, wydajności, frameworków czy dobrych praktyk. Z racji tego, że 4Developers łączy ze sobą wiele różnych technologii, dobrze by było, żeby tematy były potencjalnie interesujące dla szerokiej grupy odbiorców, ale nie oznacza to, że tematy niszowe są z góry przekreślone. Po prostu musimy zachować odpowiedni balans podczas układania agendy.');
INSERT INTO "Categories" VALUES (8,'ed8795ca-751d-11eb-9439-0242ac130002',1,'JavaScript','JavaScript','Zapraszamy do zgłaszania wystąpień związanych dowolnie z tematyką JavaScript – zarówno tych bliskich przeglądarce, jak i związanych z backendem oraz node.js! W tym i nadchodzącym roku nowymi gorącymi tematami zdają się być: WebAssembly, Micro-frontends, JS w chmurze, (Dev)Ops z Node.js. Również królujące dotychczas tematy jak TypeScript oraz GraphQL wciąż mocno grzeją i nie zanosi się by przestały ;). Zachęcamy do zgłaszania prelekcji dotyczących zarówno wymienionych, jak i dowolnych innych tematów.
');
INSERT INTO "Categories" VALUES (9,'ed8799ee-751d-11eb-9439-0242ac130002',1,'Frontend / UI','Frontend','Jeśli chodzi o nową ścieżkę Frontend/UI chętnie posłucham o design systemach, dostępności, metodach na usprawnienie pracy na linii design – developerzy, wtyczkach do Figmy/XD, kosmicznych zastosowaniach zmiennych w CSS – generalnie o wszystkim co wiąże się z frontendem w przeglądarce, a niekoniecznie jest kolejnym frameworkiem JS Jeśli nie masz pewności czy Twoja prezentacja powinna być skierowana bardziej na ścieżkę Frontend czy na JavaScript, śmiało zgłoś na którąkolwiek z nich – postaram się dobrać odpowiednio do reszty i przekazać informację zwrotną.');
INSERT INTO "Categories" VALUES (10,'f9a1d49c-751d-11eb-9439-0242ac130002',1,'DevOps','DevOps','Podczas tegorocznego CFP na ścieżkę DevOps, wypatrujemy zgłoszeń z zakresu:
Cloud Native, Mikroserwisy, Kubernetes, Containers, Serverless
Zarządzanie konfiguracją, infrastruktura jako kod, automatyzacja
Kultura DevOps
SRE
Obserwowalność, pomiar, monitoring
Analiza DevOps, kwantyfikacja produktywności, pomiar ryzyka
To może być przydatne przy tworzeniu zgłoszenia CFP:
Bądź zwięzły, ale też treściwy, konkretny i szczegółowy
Odpowiedz sobie na pytanie: co dokładnie słuchacz wyniesie z Twojej prezentacji?
Wysyłanie kilku zgłoszeń jest mile widziane!
Nie przyjmujemy “vendor talks”!');
INSERT INTO "Categories" VALUES (11,'f9a1d712-751d-11eb-9439-0242ac130002',1,'Mobile','Mobile','Ścieżka mobilna ma odzwierciedlać multidyscyplinarność całej konferencji 4developers, bo chcemy aby developerzy mogli uczyć się od siebie nawzajem, również cross platformowo a często również korzystać z doświadczeń niemobilnych. Dlatego składamy agendę jak najszerzej, każdy język, każda platforma, każde doświadczenie się liczy.');
INSERT INTO "Categories" VALUES (12,'0ad0921c-751e-11eb-9439-0242ac130002',1,'Machine Learning','Machine Learning','Szukamy sesji opisujących projekty z zakresu AI/ML/DL w których braliście udział — zarówno tych zakończonych sukcesem, jak i tych mniej udanych. Interesują nas też sesje poświęcone używanym przez Was na co dzień narzędziom, platformom i biblioteką uczenia maszynowego, szczególnie jeśli możecie podzielić się radami dotyczącymi ich efektywnego użycia. Nie ograniczamy zakresu tematycznego i poziomu trudności zgłaszanych sesji.');
INSERT INTO "Categories" VALUES (13,'0349f236-751e-11eb-9439-0242ac130002',1,'Python','Python',NULL);
INSERT INTO "Categories" VALUES (14,'0349f600-751e-11eb-9439-0242ac130002',1,'aa','ddd','ccc');
INSERT INTO "Judges" VALUES (1,'2e26df1e-751e-11eb-9439-0242ac130002',1,'Bartek','Bartek','30-12-1980','Bartosz','Gutkowski','Gutkowski@wp.pl','Gutkowski@geeklemonconference.pl','(+48)555 555 555','(+48)777 555 777',1);
INSERT INTO "Judges" VALUES (2,'2e26e158-751e-11eb-9439-0242ac130002',1,'Jakub','Jakub','22-11-1982','Jakub','Stapp','Stapp@wp.pl','Stapp@geeklemonconference.pl','(+48)555 555 555','(+48)777 555 777',1);
INSERT INTO "Judges" VALUES (3,'2e26e3d8-751e-11eb-9439-0242ac130002',1,'Anita','Anita','13-01-1987','Anita','Bartyzel','Bartyzel@geeklemonconference.pl','Bartyzel@wp.pl','(+48)555 555 555','(+48)777 555 777',2);
INSERT INTO "Judges" VALUES (4,'2e26e554-751e-11eb-9439-0242ac130002',1,'Piotr','Piotr','23-05-1981','Jakub','Wicherski','Wicherski@geeklemonconference.pl','Wicherski@wp.pl','(+48)555 555 555','(+48)777 555 777',2);
INSERT INTO "Judges" VALUES (5,'2e26eaae-751e-11eb-9439-0242ac130002',1,'Damian','Damian','23-09-1985','Damian','Sobótka','Sobotka@geeklemonconference.pl','Sobotka@wp.pl','(+48)555 555 555','(+48)777 555 777',3);
INSERT INTO "Judges" VALUES (6,'3da8dfd2-751e-11eb-9439-0242ac130002',1,'Tomasz','Tomasz','23-09-1989','Tomasz','Pilimon','Pilimon@geeklemonconference.pl','Pilimon@wp.pl','(+48)555 555 555','(+48)777 555 777',3);
INSERT INTO "Judges" VALUES (7,'3da8e5ea-751e-11eb-9439-0242ac130002',1,'Michal','Michal','23-09-1989','Michał','Libudzic','Libudzic@geeklemonconference.pl','Libudzic@wp.pl','(+48)555 555 555','(+48)777 555 777',4);
INSERT INTO "Judges" VALUES (8,'43af19a0-751e-11eb-9439-0242ac130002',1,'Pawel','Pawel','23-09-1989','Paweł','Mazurek','Mazurek@geeklemonconference.pl','Mazurek@wp.pl','(+48)555 555 555','(+48)777 555 777',4);
INSERT INTO "Judges" VALUES (9,'43af20bc-751e-11eb-9439-0242ac130002',1,'Grzegorz','Grzegorz','23-09-1989','Grzegorz','Przygudzki','Przygudzki@geeklemonconference.pl','Przygudzki@wp.pl','(+48)555 555 555','(+48)777 555 777',5);
INSERT INTO "Judges" VALUES (10,'4899c488-751e-11eb-9439-0242ac130002',1,'Krzysztof','Krzysztof','23-09-1989','Krzysztof','Piwowarek','Piwowarek@geeklemonconference.pl','Piwowarek@wp.pl','(+48)555 555 555','(+48)777 555 777',5);
INSERT INTO "Judges" VALUES (11,'4899c924-751e-11eb-9439-0242ac130002',1,'Maciek','Maciek','23-09-1990','Maciek','Zub','Zub@geeklemonconference.pl','Zub@wp.pl','(+48)555 555 555','(+48)777 555 777',6);
INSERT INTO "Judges" VALUES (12,'4899ca0a-751e-11eb-9439-0242ac130002',1,'Hubert','Hubert','15-06-1980','Hubert','Korsan','Korsan@geeklemonconference.pl','Korsan@wp.pl','(+48)555 555 555','(+48)777 555 777',6);
INSERT INTO "Judges" VALUES (13,'5086986a-751e-11eb-9439-0242ac130002',1,'GregoryA','GregoryB','20-02-2021','Gregory','Zakrowiski','Zakrowiski@wp.pl','Zakrowiski@geeklemonconference.pl','(51)222 555 555','(51)222 555 777',7);
INSERT INTO "Judges" VALUES (14,'716a6f7d-ebe7-4d3e-8fd8-44c12aaca95c',1,'GregoryC','Gregoryf','20-02-1988','GregoryAAA','ZakrowiskiAA','ZakrowiskiA@wp.pl','ZakrowiskiA@geeklemonconference.pl','(421)111 555 555','(451)111 555 777',1);
INSERT INTO "CallForSpeakes" VALUES (1,'1fb57aee-751e-11eb-9439-0242ac130002',1,'4687654',0,NULL,NULL,NULL,NULL,'Cezary','Walenciuk','Poland','00-222','White Underwodds','Balskowa','https://www.facebook.com/cezary.walenciuk','https://twitter.com/walenciukc','https://www.instagram.com/cezarywalenciuk/','https://www.linkedin.com/in/cezary-walenciuk-35615644/','https://www.tiktok.com/@shanselman?','https://www.youtube.com/channel/UCaryk7_lKRI1EldZ6saVjBQ','https://www.facebook.com/JakProgramowac?fref=nf','https://github.com/PanNiebieski','https://cezarywalenciuk.pl/','Człowiek, który czuje, że jego misją jest nauczanie innych i otwieranie ich dróg na lepsze jutro. Prowadzi bloga o przemawianiu i programowaniu. Ma także swój kanał YouTubie, na którym są webinary na wszelki tematy. Prowadzący fanpage "JakProgramować" na Facebooku. Aktywny na Twitterze pod nazwą "@waleniukC" oraz na Instagramie "cezary.walenciuk" gdzie wkleja pytania rekrutacyjne z różnych obszarów programowania.
Przemawia od 5 lat w Toastmasters oraz na konferencjach programistycznych.
Założyciel klubu mówców Ninja Speakers w Warszawie, który funkcjonował przez 2 lata.  Od 8 lat pracuje jako programista .NET i siedzi w tak zwanej Webówce. Zwycięzca konkursu na mowę inspiracyjną po angielsku w 2016 roku na terenie Polski Wschodniej. W 2018 roku wygrał konkurs na opowiadanie historii w Bydgoszczy.','(+48)777 555 777','ce@gmail.com','1981-01-02','JSON Web Token i Samuraje z ASP.NET CORE, Swagger UI i Blazor','Uwierzytelnianie i autoryzacja przy pomocy JSON Web Tokenów (JWT) jest bardzo prosta. Chcielibyśmy stworzyć REST API dla naszej strony Samurajów, którą napiszemy w Blazor. Zobaczmy, jak te tokeny podróżują z REST API do aplikacji SPA napisanej nie w Angularze, nie w React, a w C#. Oto moc WebAssemlby mój samuraju. Dodatkowo nie chcemy się bawić w testowanie API przez Postman. Skorzystajmy z Swagger UI, który stworzy dla naszego REST API odpowiednią dokumentację i taką stronę testową, która tak też obsłuży  Uwierzytelnianie i autoryzacja przez Json Web Tokeny. Wstawaj Samuraju mamy aplikację do zabezpieczenia.','.NET,C#,ASP.NET CORE, JSON WEB TOKENS, BLAZOR, Swagger UI',1,0,'02-02-2021',1,NULL,NULL,NULL);
INSERT INTO "CallForSpeakes" VALUES (2,'1fb57fc6-751e-11eb-9439-0242ac130002',1,'9989754',3,2,'poniedziałek, 22 lutego 2021',2,'poniedziałek, 22 lutego 2021','Cezary','Walenciuk','Poland','00-222','White Underwodds','Balskowa','https://www.facebook.com/cezary.walenciuk','https://twitter.com/walenciukc','https://www.instagram.com/cezarywalenciuk/','https://www.linkedin.com/in/cezary-walenciuk-35615644/','https://www.tiktok.com/@shanselman?','https://www.youtube.com/channel/UCaryk7_lKRI1EldZ6saVjBQ','https://www.facebook.com/JakProgramowac?fref=nf','https://github.com/PanNiebieski','https://cezarywalenciuk.pl/','Człowiek, który czuje, że jego misją jest nauczanie innych i otwieranie ich dróg na lepsze jutro. Prowadzi bloga o przemawianiu i programowaniu. Ma także swój kanał YouTubie, na którym są webinary na wszelki tematy. Prowadzący fanpage "JakProgramować" na Facebooku. Aktywny na Twitterze pod nazwą "@waleniukC" oraz na Instagramie "cezary.walenciuk" gdzie wkleja pytania rekrutacyjne z różnych obszarów programowania.
Przemawia od 5 lat w Toastmasters oraz na konferencjach programistycznych.
Założyciel klubu mówców Ninja Speakers w Warszawie, który funkcjonował przez 2 lata.  Od 8 lat pracuje jako programista .NET i siedzi w tak zwanej Webówce. Zwycięzca konkursu na mowę inspiracyjną po angielsku w 2016 roku na terenie Polski Wschodniej. W 2018 roku wygrał konkurs na opowiadanie historii w Bydgoszczy.','(+48)555777111','ce@gmail.com','11-12-1988','JSON Web Token i Samuraje z ASP.NET CORE, Swagger UI i Blazor','Uwierzytelnianie i autoryzacja przy pomocy JSON Web Tokenów (JWT) jest bardzo prosta. Chcielibyśmy stworzyć REST API dla naszej strony Samurajów, którą napiszemy w Blazor. Zobaczmy, jak te tokeny podróżują z REST API do aplikacji SPA napisanej nie w Angularze, nie w React, a w C#. Oto moc WebAssemlby mój samuraju. Dodatkowo nie chcemy się bawić w testowanie API przez Postman. Skorzystajmy z Swagger UI, który stworzy dla naszego REST API odpowiednią dokumentację i taką stronę testową, która tak też obsłuży  Uwierzytelnianie i autoryzacja przez Json Web Tokeny. Wstawaj Samuraju mamy aplikację do zabezpieczenia.','.NET,C#,ASP.NET CORE, JSON WEB TOKENS, BLAZOR, Swagger UI',1,0,'02-02-2021',1,3,'','');
CREATE UNIQUE INDEX IF NOT EXISTS "CategoryID_Index" ON "Categories" (
	"ID"	DESC
);
CREATE UNIQUE INDEX IF NOT EXISTS "JudgesID_Index" ON "Judges" (
	"ID"	DESC
);
COMMIT;
