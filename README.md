# Labb-5

Ni ska skapa en WPF-applikation. Använd GitHub för versionshantering.
Laborationen ska utföras i grupper om två.

Idén är göra ett program för att administrera användare i ett system. Det ska finnas en lista med användare och en mindre lista med administratörer. Man ska kunna lägga till, ändra och ta bort användare. Man ska kunna välja ut användare och flytta över dem till en lista med administratörer.

Ni får använda följande WPF-element: Button, Label, TextBox och ListBox.

Användare ska beskrivas med en klass vid namn User som har publika properties för namn och e-postadress.
Skapa och ändra användare
Använd två TextBox i kombination med en Button för att skapa användare. Man fyller i namn och e-postadress och klickar på en Button, för att skapa ett User-objekt som läggs till i en ListBox med vanliga användare.

Det ska finnas en Button för att ändra en vald användare. Återanvänd TextBox-elementen men gör en ny Button. När man klickar på den ska den användare i ListBox som är vald uppdateras med nya värden. Om ingen användare är vald ska knappen vara disabled och inte gå att klicka på.

Det ska finnas en Button för att ta bort en vald användare. Om man klickar på den så ska den valda användaren tas bort från ListBox. Om ingen användare är vald ska Button vara disabled precis som för föregående Button.
Listorna
Båda ListBox-elementen ska innehålla User-objekt. Men de ska bara skriva ut användarens namn i listan. Om något element är valt i den första eller den andra listan så ska den fulla informationen om User-objektet visas i en Label, på ett användarvänligt sätt.
Flytta användare
Det ska finnas två Button-element för att ändra användares status. Om man klickar på den ena så ska den valda användaren göras till administratör: dvs tas bort från första ListBox och läggas in i andra ListBox. Om man klickar på den andra så ska den valda administratören göras till vanlig användare. Om användare/administratör inte är vald så ska motsvarande Button inte gå att klicka på.
Bedömning
Laborationen kommer att rättas med visuell inspektion. VG utfärdas om det går att använda applikationen enligt beskrivningen ovan. G fås vid mindre avvikelser, exempelvis om det tummas på användarvänligheten eller visning av information. VG om laborationen uppfyller specen, G vid sen inlämning.
Inlämning via ithsdistans
Uppgiften lämnas in på ithsdistans
Eftersom det är en gruppuppgift så lämnar en partner in koden
Ladda upp hela projektet som en kommentar, skriv github länken i ett fritextfält. 
Den partner som inte lämnar in kod skall istället lämna in en kommentar. Till exempel
“Jobbar i grupp med Pontus Lindgren”
.
