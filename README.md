<p align="center">
  <kbd>    
    <img alt="Lamina Promo" src="https://github.com/user-attachments/assets/09a1d838-7571-4929-82a4-19c88c12889e" />
  </kbd>
</p>

<div align="center">
  
Lamina ✦ is an **Open-source WinUI 3 Calculator** for **Windows 11** AND **Windows 10** featuring a **Standard Calculator**, An **NCalc-Based Advanced Calculator**, **Persistent History**, **Customization**, and having support **Built-in** & **User-made**  `"Scripties"`.
  
**Target OS:** **Windows 11** & **Windows 10** |  **Latest Stable Version:** **v11.28000.20.0**

**App Execution Aliases :** `lamina.exe` & `lmna.exe` 

**Codename** : `LMNA`

*- TRUSTED SOURCES -*

| Download Channels | Get the App |
| :--- | :--- |
| **GitHub** | <a href="https://github.com/Chill-Astro/Lamina-Calculator/releases/latest" target="_blank"><img src="https://img.shields.io/static/v1?label=%20&message=GitHub&color=FFFFFF&labelColor=000000&style=for-the-badge&logo=github&logoColor=FFFFFF" height="40" alt="GitHub"></a> |
| **Uptodown** | <a href='https://lamina.en.uptodown.com/windows' title='Download Lamina - A FOSS and Memorible Calculator!' ><img src='https://stc.utdstc.com/img/mediakit/download-gio-big.png' height="40" alt='Download Lamina - A FOSS and Memorible Calculator!'></a> |

</div>

## What are Scripties?

Scripties are High Performance GUI equivalents of Console Scripts, that are Reliable and Easy to Make. 

Categories :

- Built-In Scripties - Made in XAML & C# and Compiled. ( Static )
- User Made Scripties - Made in JSON and loaded by my Interpreter `Dynamo`. ( Dynamic )

---

## How do I Install Scripties?

- Go to [Lamina ✦ Modules Repo](https://github.com/Chill-Astro/Lamina-Modules-Repo).
- Download your Desired Mod, a `.lamina` File.
- Go to Menu > Press `+ Add` and Add the File from the Popup!

---

## Features :

- Simple and Clean GUI. ✅
- Dozens of calculation options. ✅
- Programmable using JSON-based Logic. ✅
- Fast and Error-Proof Calculations. ✅
- High Precision for decimals. ✅
- Modern UI with Fluid Animations and Transitions. ✅
- History Support for Basic and Advanced Calculator. ✅
- Theme switching built in. ✅
- Backdrop switching between Mica Alt, Mica and Acrylic! ( WINDOWS 11 ONLY ) ✅
- Eggcelent Looking Splash Screen that can be toggled OFF. ✅
- FULL Power of NCalc in Advanced Calculator. ✅
- Expression Editor for Advanced Calculations ( Yes your OWN MATH! ). ✅
- Proper History Sidebar for Large Window Sizes and Full Screen. ✅
- Persistent History that survives App Restarts. ✅
- Recall Expression Button for Advanced Calculator. ✅
- Available in both Msixbundle & Installer Variants. ✅
- Windows 10 Icons if using Windows 10. ✅
- Acrylic is Used by Default on Windows 10 and "Backdrop Setting" is Removed! ✅

---

## Anti-Features :

- Slow Updates ❌
- Use of AI Assistance as I am learning UI and WinUI 3 with AI. ❌
- Use of `Alex Brush` in Branding which may look like Alien Language for certain People. ❌
- BUGS may Slip in because AI is Stupid, and without a Manual Audit, it's basically `AI SLOP`. ❌
    
---

## Install Lamina ✦ from Winget :
      
      winget install Lamina

---

## Installation : 

> [!NOTE]
> Project [Trust My Msix!](https://github.com/Chill-Astro/Trust-My-Msix) can be used to Import the Self-Signed Certificate of the `.msix ` Version of Lamina ✦. A Copy is given in [Releases](https://github.com/Chill-Astro/Lamina/releases/latest) for Usage.

1.  Download the `.msixbundle` and `.cer` files from the [latest release](https://github.com/Chill-Astro/Lamina/releases/latest).
2.  Import the `.cer` file to the `Trusted Root Certificates` Store. ( First Run ONLY! )

    Alternatively use this Command after Downloading `TMM.exe` from [Releases](https://github.com/Chill-Astro/Lamina/releases/latest) :    

         .\tmm --i <Certificate>
    
    Also you can Paste the Path into the Terminal Prompt or Drag the `.cer` on TMM.exe!
    
4.  Install the `.msixbundle` file.

<p align="center">
  --------------------- OR ---------------------
</p>

-  Download the `Setup.exe` that does the Importing and Installation FOR YOU!

---

## Version Structure :

<div align="center">

<H2>

v`11`.`28000`.`20`.`0`

</H2>

</div>

- `11` -> Target OS is Windows 11
- `28000` -> Release SDK Version ( 28000.xxxx Versions of Windows 11 SDK )
- `20` -> Release Index
- `0` -> Filler Number I couldn't Edit. 💀

---

## DETAILS ON DYNAMO :

Dynamo Scriptie Loader (DSL) is an `Interpreter` for `JSON` Code for creating a Dynamic UI for User-Made Math. 

Features :

- Dynamically Generates a UI with the Required Fields and Formulae Entered by the User.
- ONE PAGE, MULTIPLE POSSIBILITIES. Yes, `DyNamoPage.xaml` is the Backbone of Any Scriptie Mod.
- Integrated Manager `DyNamoManagerPage.xaml` that Interprets the Mods and feeds this data into `DyNamoPage.xaml` to Load the UI.
- <details> <summary><b>Very Clean & Simple Structure</b></summary>
        
        {
          "Metadata": {
            "Name": "", // This is What your Module Shows Up!
            "Author": "", // Write your Username.
            "Version": "", // This is the Version of your Scriptie.
            "Description": "", // Details
            "Repo": "" // Link ( If FOSS )
          },
          "UI": {
            "Formula": "", // Example W = F.S cos (theta)
            "Inputs": [
              { "Header": "Input Label", "Placeholder": "0.0", "Key": "var_name" } // Add as many as you like.
            ]
          },
          "Logic": {
            "Output": "NCalc math string using [var_name]", // Your Output. Example : "`Work Done = ` + F*S*Cos(Theta)"
            "Error": "Message if math fails" // Your Error.
          }
        }
  
</details> 
      
- Thus, a `Massive Time Saver` and `Low Barrier to Entry` as one can make modules in Notepad.
- Since NCalc is NCalc and not a Compiler, the Risk of Malicious Mods is VERY LOW.

---

## Contributing Mods :

- Fork [Lamina ✦ Modules Template](https://github.com/Chill-Astro/Lamina-Modules-Template).
- Sample Code is Given. Modify as you Need.
- I will add you to the Lamina ✦ Modules Repo

---

## Building from Source :

<details>
  <summary>Very Long Stuff for Developers 😴</summary>

- Install Visual Studio 2026 with **WinUI Application Development** and **.NET Desktop Development** workloads.
  - Windows 11 or Windows 10.
  - [XAML Styler](https://marketplace.visualstudio.com/items?itemName=TeamXavalon.XAMLStyler) is recommended for contributing.
  - .NET 10.0 Runtime LTS is must.
  - Get the latest Windows 11 SDK (28000.xxxx)
  - Community Edition is sufficient for contributing and testing. Pro and Enterprise Editions can also be used.
  - Github Copilot and Live Share can be skipped for Storage Saving.
  
<img width="1280" height="720" alt="image" src="https://github.com/user-attachments/assets/1f2705e2-de7d-41d5-8dbd-8138d1812730" />

- Get the Code :
  
      git clone https://github.com/Chill-Astro/Lamina-Calculator.git

- Open [Lamina.sln](/Lamina.sln) in Visual Studio.
- All the Code is Inside [Lamina/](/Lamina)
- Hit Deploy as shown in Screenshot. ( Building is Automatically Done while Deploying )

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/69bff8d4-5724-4854-b558-62484ddb6bea" />

Lamina ✦ is now Deployed and now she shall appear in the Start Menu. Enjoy! :)

<img width="1920" height="1080" alt="image" src="https://github.com/user-attachments/assets/65cc1ddb-da33-472c-941b-3a3e55c5073e" />

</details>

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

## Mascot Art 

Since Lamina ✦ is Personified as `"She"` ( Because Computers are She ),

so here's a cute Mascot! ヾ(^▽^*)))

NOTE : THIS ISN'T CODE RELATED!

<details><summary><b>View Art</b></summary>

<kbd>
<img width="1854" height="1854" alt="Lamina" src="https://github.com/user-attachments/assets/fe911f2f-f30a-4f0f-b10d-7434896a3cca" />
</kbd>

</details>

---

## TRAILER on @delta-v-sfs :

<div align="center">
  <kbd>
  <a href="https://www.youtube.com/watch?v=AWq2MTEJa0I">
    <img src="https://img.youtube.com/vi/AWq2MTEJa0I/maxresdefault.jpg" alt="Watch the Lamina Trailer"style="border-radius:10px;">
    <br>
    <b><h3>▶ Click to Watch the Official Trailer</h3></b>
  </a>
</kbd>
</div>

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

## Note from Developer :

A Star ⭐ will be Appreciated to Support this Project! :)

---
