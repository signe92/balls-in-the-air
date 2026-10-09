# 2: Spillets tilstande med enum og switch

# kontekst 
- Det har tre forskellige skærme lige nu som er selve spillet og game over. De skal hver især have deres egen logik, både når spillet opdateres, og når tingene skal tegnes på skærmen. 

# beslutning
- Jeg valgte at bruge en GameState-enum til at hodle styr på hvilken skærm spilleren er på. Derefter brugte jeg en swicth i både Update og Draw til at bestemme hvad der skulle ske. Jeg har lavet seperate metoder til de forskellige skærme så jeg nemt kunne finde rundt i koden. 

# alternativer
- Jeg kunne også ahve lavet seperate klasser til hver skærm og det ville gøre det mere fleksibelt fandt jeg ud af senere men ville for mig være unødvendigt meget struktur til det her spil med kun tre skærme.

# konsekvenser 
- Hvis spillet får mange flere skærme kan Game1 blive for stor og sværere at holde styr på. I så fald ville jeg overveje at give hver skærm sin egen klasse, det er noget jeg kan prøve til næste gang. 