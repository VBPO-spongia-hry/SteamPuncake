# Spongia-2021
Hlavný repozitár pre tohtoročnú špongiu.
Hlavné pravidlá, ktorých sa treba držať:
- Aby bol vo všetkom poriadok, tak platí pravidlo **čo nie je tu na GitHube to *NIE JE***.
- Do branchu **main** nikdy nezasahujem. *Do branchu **main** budem mergovať dev vždy keď niečo dokončíme. Branch main sa totiž automaticky builduje vždy keď tam niekto čosi uploadne. Ak tam niečo dáte tak to budem musieť dlho opravovať, lebo to už nepôjde mergnúť automaticky a okrem toho zbytočne pustíte build hry.*
### Je v hre nejaký bug, alebo niekomu chceš zadať čo má spraviť?
Klikni [sem](https://github.com/VBPO-spongia-hry/Spongia-2021/issues/new) a vytvor nový issue

### Chceš pridať nejaké súbory?
Tu je na to postup:

##### Mám stiahnutý a funkčný git
Týmto uploadnem celý svoj lokálny stav na sem na web
```bash
git commit -am "Pridane nejake obrazky"
git push origin dev
```
###### Setup
otvorím git bash a spustím nasledovné príkazy (stiahne mi to celý repozitár do počítača a môžem niečo začať robiť v Unity😎)
```bash
git clone https://github.com/VBPO-spongia-hry/Spongia-2021.git
git checkout dev
```
###### Git nejak papuľuje pri pushovaní / pullovaní
**uistím sa, že som dal dobré meno a heslo**
```bash
git config --global user.name "Moje meno na githube"
git config --global user.email "Moj mail"
```
Papuľuje stále? 
[Tu](https://stackoverflow.com/questions/68775869/support-for-password-authentication-was-removed-please-use-a-personal-access-to) je odpoveď aj návod ako ho opraviť. 
Ak nechceš aby sa git vkuse pýtal na heslo, tak si ho vieš uložiť
```bash
git config --global credential.helper store
```

##### Nemám git a nechcem ho ani vidieť
toto je fakt neodporúčaný postup, fakt sa oplatí stiahnuť git aj unity 😁
1. Uistím sa, že som na branchi **dev**. 
*Ak to uploadnete na main spôsobí to veľa problémov a budem sa hnevať.*
2. Pomocou webu uploadnem svoje obrázky / hudbu, ...
3. Spravím issue, že nech to niekto importuje do hry.

### Chceš si robiť zoznam obrázkov / hudby, čo treba spraviť?
Nájdeš ho [tu](https://github.com/orgs/VBPO-spongia-hry/projects/1). Funguje to tu podobne ako v trelle, len sa s tým robí jednoduchšie. 
