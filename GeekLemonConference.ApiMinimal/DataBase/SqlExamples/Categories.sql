INSERT INTO Categories (DisplayName, Name,WhatWeAreLookingFor)
VALUES ("test", "test", "test");

UPDATE Categories
SET Name = "t", DisplayName = "t", WhatWeAreLookingFor = "t"
WHERE Id=15;

DELETE FROM Categories WHERE Id=15;

UPDATE CallForSpeakes
SET PreliminaryDecision_DecisionBy = "t", PreliminaryDecision_Date = "t",
Status = 1
WHERE Id=15;

