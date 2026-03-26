<p align="center">
  <kbd>
    <img width="1920" height="1080" alt="Lamina Promo" src="https://github.com/user-attachments/assets/5c483dca-ca62-4a1a-b836-df5a05e355d0" />
  </kbd>
</p>

<div align="center">
  
Lamina ✦ is a **Windows 11** native **WinUI 3 calculator** application that is **Elevated with Powerful Scripted Actions**. This utility combines a modern, clean interface with the ability to perform complex calculations across **Mensuration, Finance, and Unit Conversions**, making it significantly more powerful than standard calculators.
  
**Target OS:** **Windows 11** ONLY.  |  **Latest Stable Version:** **v11.26100.13.0**

**App Execution Aliases**

**Msix Release:** `lamina.exe` & `lmna.exe` 

**Installer Release:** `lamina.exe`

</div>

---

## PLANS :

- A Simple Scientific Calculator Addon in Main UI

<img width="480" height="500" alt="Lamina" src="https://github.com/user-attachments/assets/816e7a2d-0f90-48f0-b3ef-dc260077c5f7" />

- New Settings Page (inspired from FOSS-Root-Checker)

<img width="480" height="500" alt="Lamina Settings(1)" src="https://github.com/user-attachments/assets/fd326b4b-bc68-49a5-9748-580f8e10637d" />

- New Scripted Actions Design (WIP)

<img width="480" height="500" alt="Upcoming Revamp" src="https://github.com/user-attachments/assets/8fb2bfeb-1ff1-48d9-bc32-5f2c98f58eee" />

Please note that certain buttons here such as "Copy" will exist as icons.

---

## Scripted Actions Planned ( for v11.26100.14.0 ) :

- Base Calculator UI + Scientific Calculator.
- Date Calculator
- Programming
- Convertors :
  * Unit Convertor
  * Currency Convertor
- Mensuration :
  * Heron's Formula
  * Perimeter Calculator
      * Equilateral Triangle
      * Isosceles Triangle
      * Square / Rhombus
      * Rectangle / Parallelogram
      * Circle
      * Semi-circle
  * Area Calculator
      * Equilateral Triangle
      * Isosceles Triangle
      * Standard Triangle
      * Square
      * Rectangle / Parallelogram
      * Rhombus
      * Circle
      * Semi-circle
      * Room
  * Volume Calculator
      * Cube
      * Cuboid
      * Cylinder
      * Cone
      * Sphere
  * Total Surface Area
      * Cube
      * Cuboid
      * Cylinder
      * Cone
      * Sphere
  * Curved Surface Area      
      * Cylinder
      * Cone
      * Sphere
  * Diagonal Calculator
      * Square
      * Rectangle
      * Cube
      * Cuboid
- Algebra :
  * Quadratic Equation Solver  
- Finance :
  * Financial Calculator
       * Simple Interest
       * Compund Interest
       * Recurring Deposit
       * Fixed Deposit

---

## Key Features :

- Simple and Clean GUI. ✅
- Dozens of calculation options. ✅
- Fast and Error-Proof Calculations. ✅
- High Precision for decimals. ✅
- Modern UI with Fluid Animations and Transitions. ✅
- History Support for the Base Calculator UI. ✅
- Theme switching built in. ✅
- Backdrop switching built in. [ B/w Mica Alt and Acrylic ] ✅
- Available in both Msix & Installer Variants. ✅

---

## Install Lamina ( Installer ) from Winget :
      
      winget install Lamina.unp

---

## Video Previews :

- History Preview [ TO BE UPDATED ] :

https://github.com/user-attachments/assets/624ade09-6050-4a4e-965d-7ef7938f4c1e

- Scripted Actions Preview [ TO BE UPDATED ] :

https://github.com/user-attachments/assets/870b4683-0e88-4f46-b4e2-9220e19e9a86

---

## Installation : 

1.  Download the `.msix` and `.cer` files from the [latest release.](https://github.com/Chill-Astro/Lamina/releases/latest)
2.  Import the `.cer` file to the `Trusted Root Certificates` Store. [ ONLY ON FIRST RUN! ].
3.  Install the `.msix` file.

<p align="center">
  --------------------- OR ---------------------
</p>

-  Download the `Lamina-Setup.exe` to install the app easily.

---

## Building from Source [ TO BE UPDATED ]  :

- Install Visual Studio 2026 with **WinUI Application Development** and **.NET Desktop Development** workloads. [ Visual Studio 2022 can also be used ].
  - Windows 11.
  - [XAML Styler](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler) is recommended for contributing.
  - .NET 10.0 Runtime LTS is must.
  - Get the latest Windows 11 SDK [26100.xxxx].
  - Community Edition is sufficient for contributing and testing. Pro and Enterprise Editions can also be used.
  - Github Copilot and Live Share can be skipped for Storage Saving.
 
![image](https://github.com/user-attachments/assets/0a18b87a-de85-40f9-80bc-ef2575dc221c)

- Get the Code :
  
      git clone https://github.com/Chill-Astro/Lamina.git

- Open [Lamina.sln](/Lamina.sln) in Visual Studio.
- Hit Deploy as shown in Screenshot. [Building is Automatically Done while Deploying.]

![image](https://github.com/user-attachments/assets/d343c12f-03c0-4e52-95d8-925c5262f304)

Lamina ✦ is now Deployed and now it shall appear in the Start Menu.

![image](https://github.com/user-attachments/assets/99678fdb-c955-4818-a7bf-5d58fdfa1cfd)

---

## Adding Currency Convertor :

Lamina ✦ uses [ExchangeRate-API](https://app.exchangerate-api.com) for Currency conversion. An API key must be manually added in the region indicated.

- Add `appsettings.json` under Lamina/ : Paste the Code into here.

        {
          "LocalSettingsOptions": {
            "ApplicationDataFolder": "Calculator/ApplicationData",
            "LocalSettingsFile": "LocalSettings.json"
          },
          "CurrencyApiKey": "Enter API Key Here"
        }

- Buid and Run the Application as shown above.

---

## Icon Sources and Credits :

- [Icons8](https://icons8.com) : For all the Mensuration and Quadratic Equation Solver Menu Logos, 
- [SVG REPO](https://www.svgrepo.com/) : For Calculator Menu Logo, Unit Convertor, Heron's Formula, and most of the icons.
- [Icomoon](https://icomoon.io/) : For the Base Calculator Icon and Produce the `.ttf` file for the Icons.
- [Microsoft Calculator](https://github.com/microsoft/calculator) : For Square Root and Cube Root Button Icons. Also this inspired me to make this app.
- [Advanced Installer Free](https://www.advancedinstaller.com) : For Creating an Installer for it's Complex Structure.
- [ExchangeRate-API](https://app.exchangerate-api.com) : For Currency Conversion. [ Free Plan, so Currency Conversion is Limited ]
- [Wallpaper by Anni Roenkae at Pexels](https://www.pexels.com/photo/abstract-painting-3276035/) : Wallpaper used for Promo Art.

---

## HALL OF FAME 👍 : 

// Will add Forked Repos which are genuinely good. 🤩 I will list everything Good about them.

---

## HALL OF NEUTRALITY 😐 :

// Will add Inactive Forks. Uh yeah that's it atleast it's Forking not Cloning! 😅

---

## HALL OF SHAME 👎 :

// Includes Clones who are working against the MIT Licence and Distributing Malware. All Flaws are mentioned. 😑

- Lamina previously Calculator has undergone Malware Attacks.

---

## ⚠️ IMPORTANT NOTICE ⚠️

Please be aware: There are fraudulent repositories on GitHub that are cloning this project's name and using AI-generated readmes, but they contain **completely random and unrelated files in each release**. These are NOT official versions of this project.

**ALWAYS ensure you are downloading or cloning this project ONLY from its official and legitimate source:**
`https://github.com/Chill-Astro/Lamina-Calculator`

I am trying my best to report these people.

---

## ⚠️ Smoking Gun for Danger :

> [!CAUTION]
> **MALWARE ALERT:** If your downloaded folder looks like the images below, **DO NOT OPEN** any files. Format the drive or delete the folder immediately. Official releases are ONLY `.msix` files or an Inno Setup `.exe`.

<details>
<summary><b>View Details</b></summary>
  
* **Suspicious Windows Executables:** Files ending in `.exe`, `.bat`, or `.dll` (e.g., `luau.exe`, `StartApp.bat`).
* **Compressed Archives:** This project is distributed as an **MSIX**, never as a `.zip` or `.7z` containing Windows binaries.
* **Hidden Scripts:** Text files like `asm.txt` used to execute malicious code on your PC.
* The Following Folder Structure is used by Malware (Shown in a VM) :

![Screenshot_2026-03-01-18-52-39-337_com clone android dual space](https://github.com/user-attachments/assets/be691c9f-7def-4e8b-982c-c7ca2e9a067d)

![Screenshot_2026-03-01-18-53-09-759_com clone android dual space](https://github.com/user-attachments/assets/1c75031d-95be-4716-9347-b762e3dad5b8)

</details>

---


## Note from Developer :

Appreciate my effort? Why not leave a Star ⭐ ! Also if forked, please credit me for my effort and thanks if you do! :)

---
