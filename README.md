<p align="center">
  <kbd>    
    <img alt="Lamina Promo" src="https://github.com/user-attachments/assets/09a1d838-7571-4929-82a4-19c88c12889e" />
  </kbd>
</p>

<div align="center">
  
Lamina ✦ is a `WinUI 3 calculator` that is not only includes a Regular Calculator but also something called `"Scripties"`. She supports **Mensuration, Finance, Currency Conversion, Unit Conversions And More!**, making her a Very Extendable Option.
  
**Target OS:** **Windows 11** ONLY.  |  **Latest Stable Version:** **v11.26100.15.0**

**App Execution Aliases :** `lamina.exe` & `lmna.exe` 

*- TRUSTED SOURCES -*

<a href="https://github.com/Chill-Astro/Lamina-Calculator/releases/latest" target="_blank"><img src="https://img.shields.io/static/v1?label=%20&message=GitHub&color=FFFFFF&labelColor=000000&style=for-the-badge&logo=github&logoColor=FFFFFF" height="80" alt="GitHub"></a> 
<a href="https://sourceforge.net/projects/lamina-calculator/" target="_blank"><img src="https://img.shields.io/static/v1?label=%20&message=SourceForge&color=EE7034&labelColor=000000&style=for-the-badge&logo=sourceforge&logoColor=EE7034" height="80" alt="SourceForge"></a> 

</div>

> [!NOTE]
> Project [Trust My Msix!](https://github.com/Chill-Astro/Trust-My-Msix) can be used to Import the Self-Signed Certificate of the `.msix ` Version of Lamina ✦. A Copy is given in [Releases](https://github.com/Chill-Astro/Lamina/releases/latest) for Usage.

---
## TRAILER on @chill-astro-sfs :

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

## What are Scripties?

Scripties are High Performance GUI equivalents of Console Scripts, that are Reliable and Easy to Make.

---

## Key Features :

- Simple and Clean GUI. ✅
- Dozens of calculation options. ✅
- Fast and Error-Proof Calculations. ✅
- High Precision for decimals. ✅
- Modern UI with Fluid Animations and Transitions. ✅
- History Support for the Base Calculator UI. ✅
- Theme switching built in. ✅
- Backdrop switching betwwen Mica Alt, Mica and Acrylic! ✅
- Eggcelent Looking Splash Screen that hasn't been seen before. ✅
- Splash Screen can be toggled OFF if you are a Serious Mathematician or have 0 Attention Span. ✅
- Available in both Msix & Installer Variants. ✅

---

## Install Lamina ✦ from Winget :
      
      winget install Lamina

---

## Version Structure :

<div align="center">

<H2>

v`11`.`26100`.`15`.`0`

</H2>

</div>

- `11` -> Target OS ( She IS for Windows 11 )
- `26100` -> Release SDK Version ( Currently She uses 26100.xxxx Versions of Windows 11 SDK )
- `15` -> Release Index ( Here 15 stands for the 15th Release Of Course! )
- `0` -> Filler Number ( Package.appxmanifest doesn't allow me to edit this Number so it's there for NOTHING 💀 )

---

## Video Previews :

- New Onboarding Experience and Reveamped Settings Menu!!!!

https://github.com/user-attachments/assets/1a737f20-3497-4105-91b6-d23a1321b3ce

- BRAND NEW REVAMPED SCRIPTIES!!!!!

https://github.com/user-attachments/assets/69bcd36f-7746-40ea-ae45-64da65af73a8

---

## Installation : 

1.  Download the `.msix` and `.cer` files from the [latest release.](https://github.com/Chill-Astro/Lamina/releases/latest)
2.  Import the `.cer` file to the `Trusted Root Certificates` Store. ( First Run ONLY! )

    Alternatively use this Command after Downloading `TMM.exe` from [Releases](https://github.com/Chill-Astro/Lamina/releases/latest) :    

         .\tmm --i <Certificate>
    
    Also you can Paste the Path into the Terminal Prompt or Drag the `.cer` on TMM.exe!
    
4.  Install the `.msix` file.

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

Lamina ✦ is now Deployed and now she shall appear in the Start Menu. Enjoy! :)

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

## Art 

Since Lamina ✦ is Personified as `"She"` ( Because Computers are She ) and has many Anti-Corporate Design Choices,

so here's her Fictional Human Form! ヾ(^▽^*)))

<kbd>
<img alt="Art" width="400" src="https://github.com/user-attachments/assets/724831c5-0497-4a53-8be8-53e6a974412f" />
</kbd>

<br> <br>
Well I am no Artist, but I hope that you Have a Nice Time with Her!

---

## Pros ✅ :

- Considering my Age, My UI Design Ideas are Very Humane and Passionate-looking in this World of Corporate Slop.
- *Aesthetics* are *"Nailed it!"* in almost Every Part of the UI.
- An Onboarding Page in a Calculator is Unique ✨, afterall most Calculator Apps look Generic.
- She's Anti-Corporate in many parts like : *"Aww no History? Let's make Some!"* *"Oh Snap! (╯°□°）╯︵ ┻━┻ "* and *"Woohooo! ヾ(^▽^*)))"* too Make the User Experience less "It's just another Calculator".
- Tactile Key Press Animations in Base Calculator UI.
- Extra Power with Scripties.
- More Lamina ✦ UI talk you can generate in your Head after trying her out!

## Cons ❌ :

- Slow Updates :
    - Code is First Generated with `Gemini 3 Flash` ( after Brainstorming 🧠 ) and then the Generated Code is Rewritten to something not Cluttered with Comments.
    - This takes time as sometimes AI is Stupid and I can't do Ctrl + C and Ctrl + V every time or the App will be Burnt Garbage.
    - I am learning some of the More IMPORTANT Stuff this year and then Even More Next Year, so Understanding is Slow Paced.
- Lack of Scientific Calculator : Self Explanatory. Even if She's Gorgeous, she lacks the Advanced Stuff.
- Cursive ( For Certain People ) :
    - `Alex Brush` is an Excellent Font, but with the Rise of Social Media, people have mostly shifted to Typing and Block Letters or Goody Ahh Hybrid of Print and Cursive.
    - Certain UIs in this App such as `OnboardingPage`, `SettingsPage` and the **Title Bar** in `ShellPage` have used Cursive at least in Branding.
    - If Cursive looks like Alien Language to you, Sorry! :) The Usage of `Alex Brush` is **Branding Only**!

<kbd>
<img alt="image" src="https://github.com/user-attachments/assets/93ceb201-e3ff-4bd3-adae-1415efa5b0ae" />
</kbd>

- BUGS :
    - I am just a Teenage Boy with a Budget Laptop, and I have Studies. It's Impossible to Find and Fight all the Bugs and fix them Rapidly, when the Best I know is writing Logic JAVA for CLI ( Without BlueJ ).
    - Sometimes Bug Slip in or I couldn't find the Logical Error in AI Code while Rewriting, leading to ODD Issues.     

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

- She has previously Calculator has undergone Malware Attacks.

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
