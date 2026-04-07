<p align="center">
  <kbd>
    <img width="1920" height="1080" alt="Lamina Promo" src="https://github.com/user-attachments/assets/5c483dca-ca62-4a1a-b836-df5a05e355d0" />
  </kbd>
</p>

<div align="center">
  
Lamina ✦ is a **Windows 11** native **WinUI 3 calculator** application that is **Elevated with Powerful Scripted Actions**. This utility combines a modern, clean interface with the ability to perform complex calculations across **Mensuration, Finance, and Unit Conversions**, making it significantly more powerful than standard calculators.
  
**Target OS:** **Windows 11** ONLY.  |  **Latest Stable Version:** **v11.26100.14.0**

**App Execution Aliases :** `lamina.exe` & `lmna.exe` 

</div>

> [!NOTE]
> Lamina ✦ uses Project [Trust My Msix!](https://github.com/Chill-Astro/Trust-My-Msix) for Installing the `.msix` using an Inno Setup Installer Wrapper to Inject its certificate to `Trusted Root Certification Authourities Store`. A copy is given in [Releases](https://github.com/Chill-Astro/Lamina/releases/latest) for Usage.

---

## PLANS :

- New UI for Scripted Actions ( Coming Soon ) :
<kbd>
<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/92b8217e-18b9-4fd5-bb94-5b4988a884db" />
</kbd>

- Scientific Calculator Addon in Main UI ( Coming Later ) :

<kbd>
<img width="480" height="500" alt="Lamina" src="https://github.com/user-attachments/assets/816e7a2d-0f90-48f0-b3ef-dc260077c5f7" />
</kbd>

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

## Install Lamina ✦ from Winget :
      
      winget install Lamina.unp

---

## Video Previews :

- New Onboarding Experience and Reveamped Settings Menu!!!!

https://github.com/user-attachments/assets/1a737f20-3497-4105-91b6-d23a1321b3ce

- Scripted Actions Preview [ TO BE UPDATED IN NEXT RELEASE ] :

https://github.com/user-attachments/assets/870b4683-0e88-4f46-b4e2-9220e19e9a86

---

## Scripted Actions :

> [!NOTE]
> A Scientific Calculator WILL BE ADDED onto the Main Calculator UI in v11.26100.15.0

- Base Calculator UI
- Scientific Calculator ( v11.26100.15.0 )
- Date Calculator
- Convertors :
  * Base Converter
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
       * Compound Interest
       * Recurring Deposit

---

## Installation : 

1.  Download the `.msix` and `.cer` files from the [latest release.](https://github.com/Chill-Astro/Lamina/releases/latest)
2.  Import the `.cer` file to the `Trusted Root Certificates` Store. ( First Run ONLY! )

    Alternatively use this Command after Downloading `TMM.exe` from [Releases](https://github.com/Chill-Astro/Lamina/releases/latest) :

         .\tmm --i <Certificate>
    
3.  Install the `.msix` file.

<p align="center">
  --------------------- OR ---------------------
</p>

-  Download the `Setup.exe` that does the Importing and Installation FOR YOU!

---

## Building from Source :

- Install Visual Studio 2026 with **WinUI Application Development** and **.NET Desktop Development** workloads.
  - Windows 11.
  - [XAML Styler](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler) is recommended for contributing.
  - .NET 10.0 Runtime LTS is must.
  - Get the latest Windows 11 SDK [26100.xxxx].
  - Community Edition is sufficient for contributing and testing. Pro and Enterprise Editions can also be used.
  - Github Copilot and Live Share can be skipped for Storage Saving.
 
<img width="1280" height="720" alt="image" src="https://github.com/user-attachments/assets/1f2705e2-de7d-41d5-8dbd-8138d1812730" />

- Get the Code :
  
      git clone https://github.com/Chill-Astro/Lamina-Calculator.git

- Open [Lamina.sln](/Lamina.sln) in Visual Studio.
- Hit Deploy as shown in Screenshot. ( Building is Automatically Done while Deploying ). 

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/69bff8d4-5724-4854-b558-62484ddb6bea" />

Lamina ✦ is now Deployed and now it shall appear in the Start Menu. Enjoy! :)

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/65cc1ddb-da33-472c-941b-3a3e55c5073e" />

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
- [Inno Setup by JRSoftware](https://jrsoftware.org/isinfo.php) : Literally the Installer is possible THANKS to them!
- [Microsoft Calculator](https://github.com/microsoft/calculator) : For Square Root and Cube Root Button Icons. Also this inspired me to make this app.
- [ExchangeRate-API](https://app.exchangerate-api.com) : For Currency Conversion. ( Free Plan, so Currency Conversion is Limited! -_- )
- [@Lisa on Pexels](https://www.pexels.com/photo/pink-flowers-photograph-1083822/) : For Wallpaper for Promo Art.

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
