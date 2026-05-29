# ![Logo](Resources/uygulamamin_ikonu.ico)
# SB File Encryptor & Decryptor

A professional-grade file security tool developed with C# and Windows Forms, designed to provide robust encryption and secure protection for confidential files and sensitive data.

![C#](https://img.shields.io/badge/Language-C%23-blue)
![Platform](https://img.shields.io/badge/Platform-Windows-lightgrey)
![License](https://img.shields.io/badge/License-Dual_License-brightgreen)

---

## 🚀 Features of the Application

* 🔑 **Password Protection** – Protect your files with a password of your choice.
* 🔒 **Versatile Security** – Secure Office documents, archives, media files, and more.
* 🗂️ **Wide File Support** – Encrypt photos, videos, PDFs, ZIP/RAR archives, and virtually any file type.
* 👤 **User-Friendly Interface** – Modern and responsive design focused on simplicity.
* ⚡ **Fast & Efficient** – Optimized for performance and stability, even with large files.
* 🕵️ **Privacy-Focused** – Prevent unauthorized access to sensitive and confidential data.
* 📊 **Real-Time Progress Tracking** – Monitor encryption and decryption progress live.
* ⏹️ **Operation Cancellation** – Safely cancel long-running operations whenever needed.
* 🔔 **Smart Notifications** – Built-in Snackbar notification system for clear user feedback.
* 👁️ **Password Visibility Toggle** – Easily verify passwords before processing.
* 📱 **DPI-Aware Design** – Sharp and responsive interface across different monitor resolutions and scaling factors.

---

## 🔐 Security Features

* **AES-256-CBC Encryption** for strong file protection.
* **PBKDF2-SHA256 Key Derivation** with **100,000 iterations**.
* **Unique 32-byte Random Salt** generated for every encrypted file.
* **Cryptographically Secure Random Number Generation (CSPRNG)**.
* **Encrypted File Signature Validation** to identify files encrypted by this application.
* **Password Validation Protection** to detect incorrect passwords.
* **Corrupted File Detection** to prevent invalid decryption attempts.
* **Local-Only Processing** – No cloud services, uploads, telemetry, or external communication.

---

## ⚙️ Under the Hood (Technical Specs)

This application is built with security, performance, and reliability in mind:

* **AES-256 Encryption** – Industry-standard AES-256-CBC encryption.
* **PBKDF2-SHA256 Key Derivation** – 100,000 iterations with unique 32-byte random salts.
* **Secure Random Generation** – Cryptographically secure salt generation.
* **File Signature Verification** – Detects invalid or unsupported files before decryption.
* **Corruption & Password Validation** – Detects corrupted files and incorrect passwords.
* **Asynchronous Processing** – Uses `async/await` to keep the UI responsive.
* **Large File Support** – Stream-based processing prevents loading entire files into memory.
* **1 MB Buffered I/O** – Optimized file streaming for large file performance.
* **Operation Cancellation** – Encryption and decryption can be canceled safely.
* **Automatic Cleanup** – Removes partially generated output files after cancellation or failure.
* **DLL Integrity Verification** – SHA-256 hash validation detects modified or tampered dependency files.
* **Single Instance Protection** – Prevents multiple instances of the application from running simultaneously.
* **Disk Space Validation** – Verifies available storage space before processing.
* **Locked File Detection** – Prevents operations on files currently used by other applications.
* **Input/Output Collision Protection** – Prevents accidental overwriting of source files.
* **Modern UX Components** – Animated progress indicators, Snackbar notifications, password visibility toggle, character counters, and smooth UI transitions.
* **DPI-Aware Interface** – Fully responsive and crisp UI on different display scaling settings.

---

## 💻 System Requirements

### Minimum

* Windows 10
* 4 GB RAM
* Dual-Core CPU (Intel Core i3 / AMD Ryzen 3 or equivalent)

### Recommended

* Windows 10 / Windows 11
* 8+ GB RAM
* Quad-Core CPU
* SSD Storage

---

## 🛠️ Installation & Usage

### Clone the Repository

```bash
git clone https://github.com/suleymanbeyhan28/sb-file-encryptor-and-decryptor.git
```

### Build and Run

1. Open the solution (`.sln`) using **Visual Studio 2022** or newer.
2. Ensure the required .NET Framework/runtime is installed.
3. Restore dependencies if necessary.
4. Build the solution.
5. Press **F5** to run the application.

---

## 📺 Video Demonstration

See the application in action by watching the full walkthrough and demonstration video:

[![Watch the Demo](https://img.youtube.com/vi/iy-5eJQGd1I/maxresdefault.jpg)](https://www.youtube.com/watch?v=iy-5eJQGd1I)

*Click the preview image above to watch the video.*

---

## 💾 Data Policy

This application operates **entirely offline**.

It does **not collect, transmit, analyze, or store** any personal information on external servers.

All encryption and decryption operations occur locally on your device. Your files never leave your computer and are never shared with third parties.

---

## 🤝 Contributing

Contributions are highly encouraged.

Whether you'd like to:

* Improve performance
* Fix bugs
* Enhance the user interface
* Improve security
* Add new features

Feel free to contribute.

### Contribution Workflow

1. Fork the repository.
2. Create a feature branch:

```bash
git checkout -b feature/AmazingFeature
```

3. Commit your changes:

```bash
git commit -m "Add AmazingFeature"
```

4. Push to your branch:

```bash
git push origin feature/AmazingFeature
```

5. Open a Pull Request.

---

## 📜 License & Usage

**SB File Encryptor & Decryptor v1.0.0**

Copyright © 2026 Süleyman BEYHAN. All rights reserved.

### Personal Use

The software is free for personal, non-commercial use.

### Commercial Use

For any commercial, corporate, educational, governmental, research, or business-related use, obtaining a commercial license is mandatory.

### Open-Source Contributions

Forks and Pull Requests are welcome for improving the project.

However:

* Modified versions may not be redistributed as standalone products.
* Modified versions may not be sold or monetized.
* The original copyright notice must remain intact.

For complete terms, please read the full [LICENSE](LICENSE) file.

---

## 📩 Contact & Commercial Licensing

For commercial licensing inquiries, feature requests, bug reports, or feedback:

### 🌐 Website / Blog

[https://suleymanbprojects.blogspot.com/](https://suleymanbprojects.blogspot.com/)

### 📧 Email

sbprojects.requests@gmail.com

*Typical response time: 24–48 hours.*

---

## ⭐ Support the Project

If you found this project useful, please consider giving it a star on GitHub.

Your support helps the project grow and motivates future development.

⭐ **Star the repository if you like it!** ⭐