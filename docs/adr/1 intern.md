# 1: Tegne i 320 x 180 og skaler 4 gange op

# kontekst
- Jeg ville gerne have at spillet havde det der skarpe pixel look fra 90'erne. Samtidig ville jeg gerne holde tallene og koordinaterne simpel, så det var nemmere at arbejde med. 

# beslutning
- jeg valgte at tegne hele spillet i en mindre opløsning på 320 x 180 og derefter skalere det 4 gange op til 1280 x 720. Jeg bruger så SamplerState.PointClamp, så pixels forbliver skarpe og ikke bliver slørede, når billedet bliver større.

# alternativer 
- Jeg kunne også have tegnet direkte i 1280 x 720. Det ville give mere plads at arbejde med, men det ville gøre pixels-stilen sværere at bevare, og alle koordinater ville være 4 gange større. 

# konsekvenser 
- Jeg kunne også tilpasse fonten til pixel-stilen. her fandt jeg ud af, at Monogame regner fontstørrelse i punkter og ikke direkte i pixels. Derfor var teksten først for stor, men med en størrelse på 6 endte den på 8 pixels. 
- En ulempe er at spillet er lavet til 16:9 format, så hvis vinduet har et andet format ville der komme sorte kanter. 