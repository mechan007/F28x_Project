<<<<<<< HEAD
<p align="left">
  <a href="README.cs.md">🇨🇿 Čeština</a> | 
  <a href="README.md">🇬🇧 English</a>
</p>
<p align="center">
  <img src="Img/Header_transparent144.png" alt="Header and logo">
</p>



## Overview

**F28X Toolset** is a desktop application for **displaying measured values ​​from a FLUKE 289 or 287 multimeter on a PC monitor in real time**.
It connects via **"USB–IR" cable** to **emulated COM port** and allows:

- **live reading of values** from the multimeter
- **plotting the waveform into a graph**
- switching between **continuous** and **sliding** graphs
- **selecting the interface language** (EN, CS, DE, PL, others are gradually added)
=======
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42

<p align="left">
  <a href="README.cs.md">🇨🇿 Čeština</a> | 
  <a href="README.md">🇬🇧 English</a>
</p>
<p align="center">
  <img src="Img/Header_transparent144.png" alt="Header and logo">
</p>



## Main functions

<<<<<<< HEAD
- **Real time:**
The values ​​from the multimeter are displayed on the PC in real time, including numerical display and graph.
=======
**F28X Toolset** je desktopová aplikace pro **zobrazení měřených hodnot z multimetru FLUKE 289 nebo 287 na monitoru PC v reálném čase**.  
Připojuje se pomocí **"USB–IR" kabelu** na **emulovaný COM port** a umožňuje:
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42

- **Graph:**
The application plots the course of the measured quantity over time and allows you to switch the mode:
- **Continuous graph** – classic “scope” style, where the course is plotted from the beginning
- **Scrolling graph** – a window in time that moves along with the measurement

- **Connection via COM port:**
The connection is made via **"USB–IR" cable** to **emulated COM port**.

- **Multilingual interface:**
Currently available languages:
- English
- Czech
- German
- Polish
Other languages ​​will be added gradually.

---

## User interface

### Basic mode

In basic mode you see:

<<<<<<< HEAD
- **current measured value** (e.g. VDC)
- **time graph**
- **information about the connected port** (e.g. COM3)
- **basic menu**: File, Settings, etc.
=======
- **Připojení přes COM port:**  
  Připojení probíhá přes **"USB–IR" kabel** na **emulovaný COM port**.

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
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140456.png" alt="F28X Toolset – základní rozhraní" width="300">
</p>

<<<<<<< HEAD
Interface layout:
=======
Rozvržení rozhraní:
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42

- **top** – current value from the multimeter
- **middle** – progress graph
- **bottom bar** – information about the device (e.g. FLUKE 287, S/N, version)

---

### Setting the graph and displayed values

In the **Settings** menu, you can change:

- **Port** – select COM port
- **Language** – switch the interface to another language
- **Displayed values** – basic / advanced values ​​
- **Graph** – switch between **Sliding** and **Continuous**

---

<<<<<<< HEAD
- **Sliding** graph moves forward the timeline which always has 60sec.
- optimal horizontal resolution of the graph
- only 60 seconds of time data.

- **Continuous** graph has a fixed timeline at point 0 and time is added
- Overall time view
- with increasing time the horizontal resolution of the graph decreases
=======
### Nastavení grafu a zobrazených hodnot

V menu **Nastavení** lze měnit:

- **Port** – výběr COM portu  
- **Jazyk** – přepnutí rozhraní do jiného jazyka  
- **Zobrazené hodnoty** – základní / pokročilé hodnoty  
- **Graf** – přepnutí mezi **Posuvný** a **Průběžný**

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140627.png" alt="F28X Toolset – graf a menu" width="300">
</p>

- **Posuvný** graf posouvá vpřed časovuo osu která má vždy 60sec.
  - optimální horizontální rozlišení grafu
  - jen 60ti sekundový časový údaj.
  
- **Průběžný** graf má pevnou časovou osu v bodě 0 a dále se přičítá čas  
  - Celkový časový pohled
  - s přibývajícím časem se snižuje horizontální rozlišení grafu
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42
---

## Advanced interface (WIP)

The application also contains **advanced interface**, which is still under development and is not available in the current version.
Once completed, it will offer additional features:

<<<<<<< HEAD
- **the ability to go back in the graph up to 30 minutes**
- **logging values ​​to `.CSV`** for further analysis
- **displaying secondary values ​​from the multimeter**
- option to **automatically connect** to the multimeter after startup
- other advanced features for detailed measurement analysis
=======
- **možnost vracet se v grafu až o 30 minut zpět**
- **logování hodnot do `.CSV`** pro další analýzu
- **zobrazení sekundárních hodnot z multimetru**
- volba **automatického připojení** k multimetru po spuštění
- další pokročilé funkce pro detailní analýzu měření
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42

It will be possible to switch to advanced mode directly from the menu (e.g. **Displayed values ​​→ Advanced**).

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140555.png" alt="F28X Toolset – pokročilé rozhraní (ve vývoji)" width="300">
</p>

> ⚠ **Note:** The advanced interface is currently under development and is not available in this version.

---

## Supported languages

- **English (EN)**
- **Czech (CS)**
- **German (DE)**
- **Polish (PL)**
- **Other languages** will be added gradually.

---

## Connection and requirements

<<<<<<< HEAD
- **Multimeter** compatible with the given protocol (e.g. FLUKE 287)
- **USB–RS232 converter**
- **Emulated COM port** (e.g. COM3)
- OS: Windows 11, 10(> 1607 build), lower series are not supported
=======
- **Multimetr** kompatibilní s daným protokolem (např. FLUKE 287)  
- **USB–RS232 převodník**  
- **Emulovaný COM port** (např. COM3)  
- OS: Windows 11, 10(> 1607 build), nižší řady nejsou podporovány
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42

---

## Project status

The project is actively developed.
The basic interface is functional, **advanced interface** is still **under development**.

---

## License

<<<<<<< HEAD
This software is protected by a proprietary license that can be found [HERE](License.txt).

**This license is valid without exceptions unless the author explicitly states otherwise.**
=======
Tento software je chráněn proprietární licencí kterou najdete [ZDE](License.txt).

**Tato licence je platná bez vyjímek pokud autor výslovně neurčí jinak.**
>>>>>>> e2914fa4dfc0dee735e281064581d17581407e42
