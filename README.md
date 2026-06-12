<div align="center">
<img src="Resources/uygulamamin_ikonu.ico" width="120" alt="SB File Encryptor Logo" />

# SB File Encryptor & Decryptor

Secure Local File Encryption & Decryption for Windows

Protect documents, archives, photos, videos, backups, and sensitive files using strong AES-256 encryption while keeping everything entirely under your control.

<br>

<div align="center">

<img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=sharp&logoColor=white" />  
<img src="https://img.shields.io/badge/.NET-WinForms-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />  
<img src="https://img.shields.io/badge/Platform-Windows-0078D6?style=for-the-badge&logo=windows&logoColor=white" />  
<img src="https://img.shields.io/badge/License-Source_Available-2ea44f?style=for-the-badge&logo=gitbook&logoColor=white" />

</div>

</div>

<br>

> 🚀 **[Development Milestone: Help sustain my work and unlock the full SB Ecosystem as open-source. Click to see the 1 BTC Roadmap.](#unlock-ecosystem)**

---

## 📥 Downloads

Pre-built executable versions are available in GitHub Releases.

👉 [Latest Release](https://github.com/suleymanbeyhan28/sb-file-encryptor-and-decryptor/releases)

---

## 🖼️ Application Preview

<p align="center">
  <img src="Resources/App-Screenshot.png" width="90%" alt="Application Preview" />
</p>

<p align="center">
  <b> Modern WinForms UI with custom controls and enhanced UX </b>
</p>

---

## 🎬 Video Demonstration

<p align="center">
  <a href="https://www.youtube.com/watch?v=iy-5eJQGd1I">
    <img src="https://img.youtube.com/vi/iy-5eJQGd1I/maxresdefault.jpg" width="90%" alt="Video Demo" />
  </a>
</p>

<p align="center">
  <b> Click the image to watch the full demonstration</b>
</p>

---

## 🔒 Why SB File Encryptor & Decryptor?

SB File Encryptor & Decryptor is a Windows desktop application designed for users who want complete local control over their data.

Unlike cloud-based encryption services, all operations occur directly on your computer.

The interface is designed to be simple and intuitive, allowing you to focus on encrypting or decrypting files without navigating complex settings or unnecessary options.

No technical expertise is required, making the application accessible to users of all experience levels.

### Key Benefits

* 🔐 **AES-256 Encryption** – For strong file protection.
* 🔑 **Access Control** – Password-based access.
* 📁 **Universal Compatibility** – Compatible with virtually any file type.
* ⚡ **Efficient Processing** – Fast with real-time progress monitoring.
* 🚫 **Privacy First** – No cloud services, telemetry, or analytics.
* 🔒 **Complete Control** – Fully offline operation.

Whether you're protecting personal documents, backups, archives, business files, or confidential information, the application emphasizes security, privacy, reliability, and ease of use.

---

## 🚀 Features

### File Protection

* 📁 **Format Support** – Encrypt and decrypt virtually any file format.
* 🔑 **Secure Access** – Password-protected access to encrypted files.
* 🛡️ **Data Security** – Protection against unauthorized access.

### User Experience

* 👤 **Modern Interface** – Modern Windows Forms interface.
* 👁️ **Password Visibility** – Toggle to view password.
* 📊 **Live Progress** – Real-time progress tracking.
* 🔔 **Smart Notifications** – Snackbar notification system.
* 📱 **DPI-Aware** – Sharp design across displays.
* ✅ **Clear Feedback** – Clear validation and error handling.

### Reliability

* 🔄 **Large File Support** – Stream-based processing for large files.
* ⏹️ **Operation Cancellation** – Safely cancel running tasks.
* 🧹 **Automatic Cleanup** – Cleanup after failures or cancellation.
* 🔒 **Locked File Detection** – Avoids file conflicts.
* 💾 **Storage Check** – Disk space validation.
* 🛡️ **Collision Protection** – Input/output collision protection.

---

## 🔐 Security Architecture

Security is the primary focus of this project.

### Encryption

* **AES-256-CBC Encryption**
* **Unique IV** generated for every encryption operation.
* **Cryptographically Secure Random Number Generation (CSPRNG)**

### Key Derivation

* **PBKDF2-SHA256**
* **100,000 Iterations**
* **Unique 32-byte Random Salt** for every encrypted file.

### Validation & Protection

* **Custom Encrypted File Signature Validation** ("SB_EncryptedFile").
* **Password Validation Protection** to detect incorrect passwords.
* **Corrupted File Detection**
* **Unsupported File Detection**

> [!IMPORTANT]
> A unique random IV is generated for every encryption operation and stored alongside the encrypted file to prevent pattern leakage and strengthen cryptographic security.

---

## 💾 Privacy & Data Policy

This application operates **entirely offline**.

The application does not:

* Collect user data
* Track activity
* Upload files
* Send analytics
* Communicate with external servers

All encryption and decryption operations occur locally on your device.

Your files remain under your control and are never uploaded or transmitted to third parties.

---

## ⚙️ Technical Highlights

* **Async/await architecture**
* **Stream-based file processing**
* **1 MB buffered I/O**
* **Responsive UI during long operations**
* **Automatic cleanup mechanisms**
* **Secure file validation workflow**

---

## 🎨 UI Components & Custom Controls

### 🧩 SBCustomControls.dll

A lightweight custom UI library used throughout the application.

**Features:**

* Modern rounded buttons
* Hover and click animations
* DPI-aware rendering
* Consistent application styling
* Lightweight reusable controls

> 📦 **Location:** `Lib/SBCustomControls.dll`

### 🔔 Snackbar Notification System

A modern notification system used instead of traditional modal message boxes.

**Features:**

* Success notifications
* Error notifications
* Warning notifications
* Information notifications
* Auto-dismiss support
* Smooth animations

> 📦 **Location:** `UI/Snackbar.cs`

---

## 🖥️ System Requirements

### Minimum

* Windows 10
* 4 GB RAM
* Dual-Core CPU

### Recommended

* Windows 10 / Windows 11
* 8+ GB RAM
* Quad-Core CPU
* SSD Storage

---

## 🛠️ Installation

### Clone the Repository

```bash
git clone https://github.com/suleymanbeyhan28/sb-file-encryptor-and-decryptor.git
```

### Build and Run

1. Open the solution in **Visual Studio 2022** or newer.
2. Restore dependencies if required.
3. Build the project.
4. Run the application.

---

## 💼 Commercial Licensing

This project is distributed under a custom Source-Available License.

The source code is provided for transparency, review, educational purposes, and community contributions.

### Free Personal Use

Individuals may use the software free of charge for personal and non-commercial purposes.

### Commercial Use

A commercial license is required for:

* Businesses
* Companies
* Organizations
* Government agencies
* Educational institutions
* Research organizations
* Revenue-generating activities
* Professional environments

If your organization uses this software to protect business-related data, a commercial license is required.

**Commercial License Inquiries:**
📧 [sbprojects.requests@gmail.com](mailto:sbprojects.requests@gmail.com)

---

<a id="unlock-ecosystem"></a>
## ❤️ Unlock the Open-Source Ecosystem

### 🎯 Open-Source Milestone: The Complete SB Note Management & Developer Ecosystem

Over the past 1.5+ years, I have developed a secure, production-ready note management ecosystem: **SB Notepad** (Windows) and **SB Note Viewer PRO** (Android). 

If community donations reach a total of **1 BTC**, I will release the complete codebase for both projects as open-source, completely free of any licenses or restrictions, and I will also convert my existing **SB File Encryptor & Decryptor** repository to an open-source license.

👉 **[Review the operational logic and UI screens of SB Notepad on my blog.](https://suleymanbprojects.blogspot.com/2026/04/sb-notepad.html)**

### Why This Architecture Matters

Building a comprehensive architecture from scratch—especially a note management system centered on cryptography and high-security data storage—demands intense mental focus and a rigorous R&D process to manage edge cases and establish an impenetrable foundation. My goal with this ecosystem is twofold: to provide developers with a professional reference architecture that saves them from the exhaustion of "reinventing the wheel," and to offer a robust, production-ready infrastructure for investors and users who prioritize digital security, privacy, and data protection. With this pristine, modular, and optimized architecture, I am offering a reliable foundation that you can integrate directly into your own projects or workflows.

**With this ecosystem, you will get:**

*   **Production-Ready Cryptography:** A strict, fully functional, and auditable dual-file encryption architecture (`.sbnote`/`.sbnotekey`).
*   **The Mobile Frontier & Premium Access (SB Note Viewer PRO):** This Android application is not just a viewer; it acts as a hardware-based security key for the ecosystem's high-security NFC layers.
*   **NFC Signature Generation:** Saving highly secure `+NFC` and `JUST NFC` notes on the desktop requires a unique signature. This signature can only be generated from the SB Note Viewer PRO app. You create the NFC-supported signature on your mobile device and enter it into the desktop application during the saving process.
*   **Exclusive Viewing Restrictions:** While the desktop SB Notepad can save all note variants, it is strictly restricted to viewing only the "Normal Note" variant. Decrypting and reading the secured NFC variant notes—along with accessing other premium features—is strictly exclusive to the SB Note Viewer PRO mobile application, creating an unbreakable cross-device security layer.
*   **Modular Architecture & Internal Classes:** Monolithic structures are avoided. Both applications are broken down into logical, platform-specific, and reusable source-code classes. These classes, which you can copy directly into your own projects, include a custom **Emoji Cleaner** and a dynamic **Snackbar** class for the Windows Forms project (the latter is currently active in the SB File Encryptor & Decryptor repo you are examining), as well as the custom SBPopup notification system for the .NET MAUI project. Furthermore, the codebase includes numerous reusable helper methods designed to accelerate development across both WinForms and .NET MAUI architectures, ready to be directly imported into your own projects.
*   **The Windows Forms Developer Toolkit (6 Proprietary DLLs):** Beyond the raw source code, this milestone unlocks 6 standalone DLLs that I engineered specifically to overcome major hurdles in the Windows Forms development process. You can easily adapt these into your own projects.
    *   **SBFormOptimizer.dll:** Provides critical performance and memory optimizations for heavy forms with too many controls.
    *   **SBCustomControls.dll:** A modern, stylish, and highly customizable UI package (`SBButton`, `SBPicturebox`, `SBRadioButton`) designed to escape classic WinForms aesthetics.
    *   **SBSec1.dll, SBSec2.dll, SBSec3.dll:** 3 distinct cryptography libraries specially written considering different security operations, scalability, and performance needs.
    *   **SBSettingsOperations.dll:** A powerful configuration manager designed to seamlessly export and import application settings.

### Reliability & Transparency

You might ask: "What is the proof that the projects will be published when the goal is reached?"

My past as a developer is my reference. My current **SB File Encryptor & Decryptor** repo demonstrates my standards regarding code quality, documentation, and sustainability. I have regularly updated and supported my open-source work without any financial expectations. This milestone is a continuation of this stable workflow I have established.

### 🛡️ The "What If" Guarantee: Exclusive Repository Access

A valid and natural question you might have is: *"What if the 1 BTC target is never reached? Does my contribution go to waste?"*

**Absolutely not.** I want to ensure that your trust and financial support yield a guaranteed return, regardless of the campaign's ultimate outcome. 

When you email me your details to register for the Hall of Fame, you are simultaneously added to my exclusive backer list. Everyone on this list—regardless of the donation amount or whether the 1 BTC goal is ever met—will be granted **special, private access to my future closed-source repositories and exclusive developer tools.** 

By supporting this milestone today, you are not just funding a campaign; you are securing a lifetime ticket to my private development ecosystem.

### Transparency: Why Bitcoin?

Given my current geographical and economic circumstances, Bitcoin is the most accessible and reasonable channel for me to receive international support. If I had the legal and financial infrastructure to commercialize this ecosystem on a global scale, I would have followed that path. 

Therefore, under these circumstances, my options were quite limited. From those available, I chose GitHub as my starting point—aiming to both contribute to the developer community and achieve my personal life goals. I planned a roadmap to publish my current projects and put it into action. I embarked on this journey with the sincere dream that the community might support my efforts in whatever capacity they deem appropriate.

### My Commitment and Beyond the Code

This ecosystem is **100% complete and tested**. This is a value-driven agreement: By supporting this milestone, you are not just funding a project; you are investing in a robust codebase and professional components. From the moment the target amount is reached, I commit to publishing all source codes and custom DLLs within **2 months**. Additionally, at that same time, my existing repository, **SB File Encryptor & Decryptor**, will also be converted to an open-source license.

**Furthermore, when the 1 BTC goal is reached, I will not only publish my projects. I will share a comprehensive retrospective review series on my blog.**

These writings will not be in the nature of "advice." On the contrary, to provide a vision of what a large-scale and solo development process actually demands, I will convey the realities I experienced with complete transparency. I will deeply address the following:

*   My architectural planning processes and the paths I followed.
*   How I solved the complex technical problems I encountered along the way.
*   My clear experiences regarding efficient time management.
*   How to cope with health conditions such as severe screen fatigue, dry eyes, and sleep problems that I faced during the process of developing these softwares, and how these problems can be minimized. I will present my experiences, which I think can add vision to you, or at least give you an idea even if it doesn't add vision, in the clearest and most understandable way.

---

### 💎 Support the Milestone

If you see the value in this architecture and want to make this comprehensive open-source release a reality, you can contribute below.

---

**📊 Campaign Progress**  
<br>
`[░░░░░░░░░░]` **0%** *(Current: 0 BTC / Target: 1 BTC)*  
*Note: The progress bar is updated weekly. (Last Update: June 11, 2026)*

> [!WARNING]
> **Important Technical Note:** To ensure successful delivery of your contribution, please make sure to select the **'Bitcoin' (BTC) network** when initiating the transfer. Only transfers made via the original Bitcoin network are supported.

**₿ Bitcoin (BTC) Wallet Address:**

```text
bc1qraj6jdvnz0wyge42mrtc7jr72n74cttzqukcw6
```

---

### 🏆 Supporter Hall of Fame & VIP Access

Bitcoin transactions are pseudonymous. To be featured in this Hall of Fame **and to secure your access to my future private repositories**, please email me (beyhansuleyman27@gmail.com) **BEFORE** making your donation. 

State your GitHub username, your company link (optional), and the exact amount you are about to send (e.g., 0.0512 BTC). Once the amount reflects in the wallet, you will be officially registered for both the Hall of Fame and the exclusive VIP access list.

1. 🥇 ...
2. 🥈 ...
3. 🥉 ...

---

🌐 **My Blog:** [suleymanbprojects.blogspot.com](https://suleymanbprojects.blogspot.com/)

---

## 🤝 Contributions

Bug reports, suggestions, and pull requests are welcome.

Areas where contributions may be helpful:

* Performance improvements
* Security reviews
* UI enhancements
* Bug fixes
* Documentation improvements

### Contribution Workflow

```bash
git checkout -b feature/AmazingFeature
git commit -m "Add AmazingFeature"
git push origin feature/AmazingFeature
```

Then open a Pull Request.

All accepted contributions may be incorporated into future versions of the project at the author's discretion.

---

## 📜 License

**SB File Encryptor & Decryptor v1.0.0**

Copyright © 2026 Süleyman BEYHAN. All rights reserved.

The software is distributed under a custom Source-Available License.

Please read the full [LICENSE](LICENSE) file for complete terms and conditions.

---

## 💬 Feedback

Suggestions, bug reports, and improvement ideas are always appreciated.

**Feedback Form:**
👉 [https://forms.gle/1r5Ho11SU1vEXY9e9](https://forms.gle/1r5Ho11SU1vEXY9e9)

---

## 📩 Contact

**🌐 Website / Blog**
[https://suleymanbprojects.blogspot.com/](https://suleymanbprojects.blogspot.com/)

**📧 Email**
[sbprojects.requests@gmail.com](mailto:sbprojects.requests@gmail.com)

---

## ⭐ Support the Project

If you explore or use this project, consider:

* ⭐ Starring the repository
* 🐛 Reporting issues
* 💡 Suggesting improvements
* 🔄 Sharing the project

Your support helps improve visibility, encourages development, and makes the project easier for others to discover.

Thank you for your support.
