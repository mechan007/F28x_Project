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

---

## Main functions

- **Real time:**
Values ​​from the multimeter are displayed on the PC in real time, including numerical display and graph.

- **Graph:**
The application plots the measured quantity's course over time and allows you to switch modes:
- **Continuous graph** – classic “scope” style, where the course is plotted from the beginning
- **Scrolling graph** – a window in time that moves along with the measurement

- **Connection via COM port:**
Connection is via **"USB–IR" cable** to **emulated COM port**.

- **Multilingual interface:**
Currently available languages:
- English
- Czech
- German
- Polish
- More languages ​​will be added gradually.

---

## User interface

### Basic mode

In basic mode you see:

- **measured value** (e.g. VDC)
- **Graph over time**
- **information about the connected port** (e.g. COM3)
- **basic menu**: File, Settings, etc.

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140456.png" alt="F28X Toolset – základní rozhraní" width="300">
</p>

Interface layout:

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

<p align="center">
  <img src="Img/Screenshot/Snímek obrazovky 2026-06-01 140627.png" alt="F28X Toolset – graf a menu" width="300">
</p>

- **Sliding** graph moves forward the timeline which always has 60sec.
- optimal horizontal resolution of the graph
- only 60 seconds of time data.

- **Continuous** graph has a fixed timeline at point 0 and time is added
- Overall time view
- with increasing time the horizontal resolution of the graph decreases
---

## Advanced interface (WIP)

The application also contains **advanced interface**, which is still under development and is not available in the current version.
Once completed, it will offer additional features:

- **the ability to go back in the graph up to 30 minutes**
- **logging values ​​to `.CSV`** for further analysis
- **displaying secondary values ​​from the multimeter**
- option to **automatically connect** to the multimeter after startup
- other advanced features for detailed measurement analysis

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

## Connections and requirements

- **Multimeter** compatible with the given protocol (IR → RS232)
- **USB-RS232 converter**
- **Emulated COM port** (e.g. COM3)
- OS: Windows 11, 10(> 1607 build), lower series are not supported

---

## Project status

The project is actively being developed.
The basic interface is functional, the **advanced interface** is still **under development**.

---

## License

This software is protected by a proprietary license that you can find [HERE](License.txt).

**This license is valid without exceptions unless the author expressly states otherwise.**
