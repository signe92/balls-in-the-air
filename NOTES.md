#Dag 1

##Hvad var svært? 
- Der var to forskellige Vector2, og det gjordet at VS automatisk tilføjet "using System.Numerics" 
- kom til at kalde feltet for Positions med s i stedet for uden, skulle omdøbes så det blev kaldt det samme. 

##Hvad har jeg lært?
- Update ændrer verden så logikken, Draw viser den
- Delta time (dt) gør at spillet ko7rer lige hurtigt på alle computere
- Det kører i en rækkefølge med konstruktør, initialize, LoadContent og derefter kører Update, Draw, Update, Draw osv igen og igen indtil spillet lukker. Så en loop. 


##Hvad overraskede mig?
- Jeg havde ikke tænkt over at et spil er bare en løkke der kører Update og Draw 60 gange i sekundet, men det giver mening når man får det i hænderne selv. 
- Blev overasket over at tyngdekraften er bare to linjer kode, hvor det stiger lidt hver frame og farten flytter bolden

#Dag 2

##Hvad var svært?
- Highscoren blev gemt, men ikke vist i menuen, så det tog nogle forsøg at få det til at virke

##Hvad har jeg lært?
- HighscoreStore og BallSpawner har hvert sit ansvar så resten af spillet ikke behøver vide, hvordan de virker? 

##Hvad overraskede mig?
- Hvorfor gemmes data i ProgramData i stedet for i projektmappen? Og hvorfor må spil og programmer ikke gemme data direkte i deres egen projektmappe?

#Day 3

##Hvad var svært?
- Menuen overlappede, y koordinater skal gå opad, og hver tekstlinje fylder 8 pixels

##Hvad har jeg lært?
- MeasureString til at centrere tekst
- Matrix-tranformationer: rækkefølgen betyder noget, først flyt og derefter forstør

##Hvad overraskede mig?
- Lille rystelse gjorde spillet meget sjovere, selvom reglerne var det samme
