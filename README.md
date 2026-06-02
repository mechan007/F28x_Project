<table style="border-collapse: collapse; border: none;">
  <tr>
    <td style="border: none; vertical-align: middle;">
      <img src="Img/icon_transparent.png" width="96">
    </td>
    <td style="border: none; vertical-align: middle; padding-left: 12px;">
      <h1 style="margin: 0;">F28X Toolset</h1>
    </td>
  </tr>
</table>

---

## Přehled

**F28X Toolset** je desktopová aplikace pro **zobrazení měřených hodnot z multimetru na monitoru PC v reálném čase**.  
Připojuje se pomocí **USB–RS232 převodníku** na **emulovaný COM port** a umožňuje:

- **živé čtení hodnot** z multimetru
- **vykreslování průběhu do grafu**
- přepínání mezi **průběžným** a **posuvným** grafem
- **volbu jazyka rozhraní** (EN, CS, DE, PL, další postupně přibývají)

---

## Hlavní funkce

- **Reálný čas:**  
  Hodnoty z multimetru jsou zobrazovány na PC v reálném čase, včetně číselného zobrazení i grafu.

- **Graf průběhu:**  
  Aplikace vykresluje průběh měřené veličiny v čase a umožňuje přepínat režim:
  - **Průběžný graf** – klasický “scope” styl, kde se průběh vykresluje od začátku
  - **Posuvný graf** – okno v čase, které se posouvá spolu s měřením

- **Připojení přes COM port:**  
  Připojení probíhá přes **USB–RS232 kabel** na **emulovaný COM port** (např. COM3).

- **Vícejazyčné rozhraní:**  
  Aktuálně dostupné jazyky:
  - Angličtina
  - Čeština
  - Němčina
  - Polština  
  Další jazyky budou postupně přidávány.

---

## Uživatelské rozhraní

### Základní režim

V základním režimu vidíš:

- **aktuální měřenou hodnotu** (např. VDC)
- **graf průběhu v čase**
- **informaci o připojeném portu** (např. COM3)
- **základní menu**: Soubor, Nastavení, atd.

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140456.png" alt="F28X Toolset – základní rozhraní" width="300">
</p>

Text pod screenshotem můžeš použít k vysvětlení, kde co je:

- **horní část** – aktuální hodnota z multimetru  
- **střed** – graf průběhu  
- **spodní lišta** – informace o přístroji (např. FLUKE 287, S/N, verze)

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140627.png" alt="F28X Toolset – graf a menu" width="300">
</p>

### Nastavení grafu a zobrazených hodnot

V menu **Nastavení** lze měnit:

- **Port** – výběr COM portu  
- **Jazyk** – přepnutí rozhraní do jiného jazyka  
- **Zobrazené hodnoty** – základní / pokročilé hodnoty  
- **Graf** – přepnutí mezi **Posuvný** a **Průběžný**

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140539.png" alt="F28X Toolset – nastavení portu, jazyka a grafu" width="300">
</p>

---

## Pokročilé rozhraní (WIP)

Aplikace obsahuje také **pokročilé rozhraní**, které je zatím ve vývoji a v aktuální verzi není dostupné.  
Po dokončení bude nabízet navíc:

- **možnost vracet se v grafu až o 30 minut zpět**
- **logování hodnot do `.CSV`** pro další analýzu
- **zobrazení sekundárních hodnot multimetru**
- **automatické připojení** k multimetru po spuštění
- další pokročilé funkce pro detailní analýzu měření

Do pokročilého režimu se bude možné přepnout přímo z menu (např. **Zobrazené hodnoty → Pokročilé**).

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140555.png" alt="F28X Toolset – pokročilé rozhraní (ve vývoji)" width="300">
</p>

> ⚠ **Poznámka:** Pokročilé rozhraní je aktuálně ve vývoji a v této verzi není dostupné.

---

## Podporované jazyky

- **Angličtina (EN)**
- **Čeština (CS)**
- **Němčina (DE)**
- **Polština (PL)**
- **Další jazyky** budou postupně přidávány.

---

## Připojení a požadavky

- **Multimetr** kompatibilní s daným protokolem (např. FLUKE 287)  
- **USB–RS232 převodník**  
- **Emulovaný COM port** (např. COM3)  
- OS: Windows (doplníš konkrétní verze)

---

## Stav projektu

Projekt je aktivně vyvíjen.  
Základní rozhraní je funkční, **pokročilé rozhraní** je zatím **ve vývoji**.

---

## Licence

(Doplníš např. MIT / GPL / jinou licenci)
