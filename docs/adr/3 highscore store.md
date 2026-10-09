# 3: Highscore i egen klasse 

# kontekst
- Jeg ville gerne have at spillet kunne huske highscoren ligesom man har i de fleste spil. Derfor skulle jeg finde en måde at gemme scoren på så den ikke forsvinder også når spillet lukkes

# beslutning
- Lavede derfor en seperat klasse der hedder HighscoreStore, som står for at gemme og hente highscoren med metoderne Load() og Save(). Jeg gemmer scoren i en tekstfil i brugerens programdata-mappe. Hvis filen mangler eller er ødelagt, starter highscoren på 0 i stedet for, at spillet crasher. 

# alternativer
- Jeg kunne have lagt det ind i Game1, men så ville logikken og håndtering af filerne blive blandet sammen. Kunne så have gemt filen i projektmappen, fordi den så ville være nemmere at finde. Men programmer bør som udgangspunkt ikke gemme data direkte i deres egen mappe. 

# konsekvenser 
- Valgte at adskille lagringen fra resten af spillet, sa8 Game1 ikke behøver at vide, hvordan highscoren bliver gemt. Hvis jeg senere vil gemme den i skyen i stedet, kan jeg nøjes med at ændre HighscoreStore. Og da highscoren på et tidspunkt ikke blev vist i menuen, kunne jeg nemmere finde fejlen, fordi jeg allerade havde adskilt lagringen fra visningen. Jeg kunne derfor undersøge, om problemet lå i visningen frem for selve lagringen. 