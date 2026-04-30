# 🚀 VMware Simple Launcher

**[🌍Русская версия ](https://github.com/EgorchikF/VMWare-Launcher/blob/main/README_ru.md)**

A minimalistic, silent background launcher for **VMware Workstation** and **VMware Player**.

### ✨ Features
- Automatically starts all required VMware services
- Launches VMware Workstation / Player
- Monitors the main VMware process in the background
- When VMware is closed:
  - Gracefully kills remaining VMware processes
  - Disables all VMware services (sets to `Disabled`)
- Completely silent — no GUI, no console window
- Written in **C# .NET 8**

### Why use it?
VMware services keep consuming RAM and CPU even when not in use. This launcher lets you keep them disabled by default and start them only when needed.

### 📋 How to use
1. Build the project in **Release** mode
2. Place the `.exe` in any folder
3. Enjoy clean and fast VMware launching!

### 🛠 Requirements
- Windows 10 / 11 (64-bit)
- VMware Workstation Pro (16.x and higher)
- .NET 8.0 Desktop Runtime
