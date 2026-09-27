#define MyAppName "Lamina ✦"
#define MyAppVersion "11.28000.19.0"
#define MyAppPublisher "Chill-Astro Software"
#define MyAppURL "https://github.com/Chill-Astro/Lamina-Calculator"

; Dynamically build the filenames using MyAppVersion
#define MyAppMsixBundle "Lamina_" + MyAppVersion + "_x64_arm64.msixbundle"
#define MyAppCertName "Lamina_" + MyAppVersion + "_x64_arm64.cer"

[Setup]
AppId={{633C1E5F-90A3-492B-933F-84ECEE95A462}
AppVerName={#MyAppName} Installer Helper
AppName={#MyAppName}
AppVersion={#MyAppVersion}
; Allow the installer to run on x64 and ARM64
ArchitecturesAllowed=x64compatible arm64
ArchitecturesInstallIn64BitMode=x64compatible arm64
DefaultDirName={autopf}\Chill-Astro\Lamina
LicenseFile=LICENSE.txt
PrivilegesRequired=admin
SetupIconFile=C:\Users\Master\Chill-Astro\Lamina-Calculator\Installer.ico
UninstallDisplayIcon=C:\Users\Master\Chill-Astro\Lamina-Calculator\Installer.ico
WizardStyle=modern dynamic windows11
OutputBaseFilename=Setup
DisableWelcomePage=no
SolidCompression=yes

[Files]
; Include both MSIX files in the installer package
Source: "C:\Users\Master\Chill-Astro\Lamina-Calculator\Lamina\Installer\*"; DestDir: "{app}"; Flags: ignoreversion

[Run]
; 1. Install the Certificate (Universal)
Filename: "certutil.exe"; \
    Parameters: "-addstore -f ""Root"" ""{app}\{#MyAppCertName}"""; \
    StatusMsg: "Installing Security Certificate..."; \
    Flags: runhidden

; 2. Install the MSIX Bundle (Windows automatically matches x64 or ARM64)
Filename: "powershell.exe"; \
    Parameters: "-ExecutionPolicy Bypass -Command ""Add-AppxPackage -Path '{app}\{#MyAppMsixBundle}'"""; \
    StatusMsg: "Installing Lamina ✦ ..."; \
    Flags: runhidden

[UninstallRun]
Filename: "powershell.exe"; \
    Parameters: "-ExecutionPolicy Bypass -Command ""Get-AppxPackage -Name '*Lamina*' | Remove-AppxPackage"""; \
    Flags: runhidden