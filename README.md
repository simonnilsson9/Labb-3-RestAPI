# Labb 3 API
API:

Hämta alla personer i systemet : https://localhost:7022/api/Person

Hämta alla intressen som är kopplade till en specifik person: https://localhost:7022/api/Person/Interests?id=1
Hämtar med personId 1.

Hämta alla länkar som är kopplade till en specifik person: https://localhost:7022/api/Person/Links?id=2 
Hämtar person med id 2.

Koppla en person till ett nytt intresse: https://localhost:7022/api/Interest/AddPerson?interestId=2&personId=3
Kopplar via interest, där man tar interestID och personID.

Lägga in nya länkar för en specifik person och ett specifikt intresse: https://localhost:7022/api/Link?title=Bundesliga&url=https%3A%2F%2Fwww.bundesliga.com%2Fen%2Fbundesliga&personId=2&interestId=4
