<p align="center">
  <kbd>
    <img width="1920" height="1080" alt="Lamina Promo" src="https://github.com/user-attachments/assets/284ba1ee-c554-4ef0-b0ac-679cedca0e0a" /><p align="center">  
  </kbd>
</p>

<div align="center">
  
Lamina ✦ is a **Windows 11** native **WinUI 3 calculator** application that is **Elevated with Powerful Scripted Actions**. This utility combines a modern, clean interface with the ability to perform complex calculations across **Mensuration, Finance, and Unit Conversions**, making it significantly more powerful than standard calculators.
  
**Target OS:** **Windows 11** ONLY.  |  **Latest Stable Version:** **v11.26100.12.0**

**App Execution Aliases**

**Msix Release:** `lamina.exe` & `lmna.exe` 

**Installer Release:** `lamina.exe`

</div>

---

## NOTICE :

I am currently unable to update and maintain this project due to my upcoming ICSE Exams. Don't worry the project isn't abandoned. It will be updated after ICSE 2026 Examinations are over. Also there are plans to publish this app to Microsoft Store. I am excited to show this to you!

---

## Scripted Actions Included [ as of v11.26100.12.0 ] :

- Base Calculator UI.
- Scientific Calculator UI [ "Possibility" for v11.26100.13.0 ]
- Convertors :
  * Unit Convertor
  * Currency Convertor
  * Base Convertor
- Mensuration :
  * Heron's Formula
  * Perimeter Calculator
  * Area Calculator
  * Volume Calculator
  * Total Surface Area
  * Curved Surface Area
  * Diagonal Calculator
- Algebra :
  * Quadratic Equation Solver  
- Finance :
  * Simple Interest
  * Compound Interest
  * Discounted Price Calculator
- Extras :
  * Age Calculator
  * Date Difference Calculator
  * Factorial Calculator
  * Approximation
  * Prime No. Checker
  * Right Triangle Checker

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

## Install Lamina [ Installer ] from Winget :
      
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
  ---------------------[ OR ]---------------------
</p>

-  Download the `Lamina-Setup.exe` to install the app easily.

---

## Building from Source [ Screenshots are from Visual Studio 2022 ]  :

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

Lamina is now Deployed and now it shall appear in the Start Menu.

![image](https://github.com/user-attachments/assets/99678fdb-c955-4818-a7bf-5d58fdfa1cfd)

---

## Adding Currency Convertor :

Lamina ✦ uses [ExchangeRate-API](https://app.exchangerate-api.com) for Currency conversion. An API key must be mannualy added in the region indicated.

- Open `appsettings.json` : Paste the Code into here.

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

## ⚠️ IMPORTANT NOTICE ⚠️

Please be aware: There are fraudulent repositories on GitHub that are cloning this project's name and using AI-generated readmes, but they contain **completely random and unrelated files in each release**. These are NOT official versions of this project.

**ALWAYS ensure you are downloading or cloning this project ONLY from its official and legitimate source:**
`https://github.com/Chill-Astro/Lamina`

I am trying my best to report these people.

---
## ⚠️ Smoking Gun for Danger :

<details>
<summary><b>View Details</b></summary>
  
**If your download contains any of the following, DELETE IT IMMEDIATELY:**

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
