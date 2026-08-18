# Planering — VR-sportspel i Unity

> **Obligatorisk.** Fyll i hela dokumentet och lämna in det till läraren för godkännande **innan** ni börjar koda.
> Ta bort exempeltexten i kursivt och skriv era egna svar.

---

## 1. Gruppen

| Namn | Klass | Huvudansvar |
|---|---|---|
| Villemo | 240s | |
| Raina | 240s | |
| Ludvig | 240s | |
| Alan | 240s | |

**Projektnamn:**

**GitHub-repo (länk):**

---

## 2. Spelidé

**Vilken sport har ni valt?**

Badminton

**Beskriv spelet i 3–5 meningar. Vad gör spelaren? Vad är målet?**

*Spelaren står på en badmintonplan vid ett nät. Man plockar upp racketen med handkontrollen och kastar badminton bollen upp i luften och slår till den över nätet. En AI bot spelar tillbaka och Spelet räknar antal poäng. Efter 11 rutor visas slutresultatet.*

**Varför är den här sporten lämplig i VR?**
Lätt att träna på sporten, kontrollerna påminner om ett racket.

**Vad gör ert spel roligt? (Vad är "kroken"?)**
Kontrollen vibererar när man träffar bollen, ljudeffekter, ger effekten av att man faktiskt står i en badminton hall och spelar.

---

## 3. Skisser

Rita spelplanen, var spelaren står, vad hen ser och hur UI:t ser ut. Handritat och fotograferat går bra — lägg bilderna i mappen `Dokumentation/` och länka till dem här.

- [ ] Skiss: översikt av spelplanen
- [ ] Skiss: vad spelaren ser i headsetet (förstapersonsvy)
- [ ] Skiss: startmeny och poängtavla

*Länkar till bilder:*

---

## 4. Spelmekanik

**Vad kan spelaren göra med händerna?**

*Greppa racket, greppa boll, kasta boll.*

**Vilka fysikobjekt finns och hur beter de sig?**

| Objekt | Rigidbody? | Collider | Beteende |
|---|---|---|---|
| *bollen* | *Ja* | *Sphere* | *Påverkas av gravitation, flyger vid träff* |
| *Racket* | *Ja* | *Capsule* | *gravitation, vibererar vid kontakt* |
| | | | |

**Hur räknas poäng? Skriv reglerna tydligt.**
Mål: Slå bollen över nätet så att den landar inom motståndarens planhalva utan att de kan slå tillbaka den.
Poäng: Du får poäng vid varje vunnen duell (oberoende av vem som servade). Upp till 11 rundor.
Fejl/Fel: Det blir motståndarens poäng om bollen hamnar utanför planen, under nätet, i nätet, eller om du vidrör nätet med rack eller kropp.
Serve:
Linjer & Planen:
Singel: Lång och smal plan gäller (de yttre sidolinjerna räknas som ut).

**Hur vinner eller förlorar man?**
Den med mest poäng vinner

**Hur startas spelet om?**
Man väljer med hjälp av restart knapp

---

## 5. Kravlista

Dela upp funktionerna. **Måste** ska vara klart för godkänt — börja alltid med dem.

### Måste (grundkrav)
- [ ] VR-spelare (XR Origin) med fungerande head tracking
- [ ] Spelaren kan greppa och släppa minst ett föremål
- [ ] Träffdetektion som fungerar
- [ ] Poängsystem
- [ ] Vinst-/förlustvillkor
- [ ] Startmeny i World Space
- [ ] Game over-skärm med resultat
- [ ] Ljudeffekter
- [ ] Omstart av spelet
- [ ] Byggbar .apk
- [ ] Nåt aom slår tillbaka bollen och kan missa 
- [ ]

### Om vi hinner (extra)
- [ ] Haptisk feedback
- [ ] Bakgrundsmusik / publikljud
- [ ] Mode där man kan slå hur länge som helst
- [ ] Räkna highscore
- [ ] Svårighetsgrader
- [ ] 
- [ ]

---

## 6. Arbetsfördelning

| Område | Ansvarig | Beskrivning |
|---|---|---|
| VR-uppsättning (XR Origin, interaktion) | Villemo | Koppla headset, inställningar Unity|
| Spelmekanik och fysik | Ludvig | Rörelser, fysik |
| Poängsystem och regler | Alan | |
| 3D-modeller och miljö | Raina | |
| UI och menyer | Villemo  | Startmeny, game over|
| Ljud | Alan | bakgrundsljud, ljudffekter|
| Testning och bygge | Raina | |
| Integrera | Ludvig | Samla ihop allt i slutet |

**Hur samarbetar ni i Git?** *(branches, vem äger scenen, hur ofta ni pushar)*
Alla jobbar på olika branches, Ludvig äger scenen, pushar när man kommit till nå som bör sparas. 

---

## 7. Tidsplan

| Vecka | Mål — vad ska vara klart? | Ansvarig |
|---|---|---|
| 1 | *Unity + VR installerat, tutorial genomförd, planering godkänd* | Alla |
| 2 | | |
| 3 | | |
| 4 | | |
| 5 | | |
| 6 | | |

**Deadline för första spelbara version (prototyp):**
8/9 morgon

**Deadline för färdigt bygge:**
11 september 
---

## 8. Assets

Vad bygger ni själva och vad hämtar ni? Ange alltid källa och licens.

| Asset | Egen eller hämtad? | Källa / licens |
|---|---|---|
| | | |
| | | |
| | | |

---

## 9. Risker

Vad kan gå fel och vad gör ni då?

| Risk | Plan B |
|---|---|
| *Det är för svårt att träffa bollen* | Öka hitoxen |
| *11 matcher blir för mycket* | *Säkn till lämpligt antal* |
| Folk är sjuka och har inte pushat sin kod| Hör av oss till varandra |

---

## 10. Lärarens godkännande

- [ ] Planeringen är godkänd

**Datum:**  **Signatur:**

**Kommentarer från läraren:**
