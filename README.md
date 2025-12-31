# Arithmetica

Arithmetica is a Xamarin.Forms arithmetic practice app that runs on Android and iOS. It provides a timed quiz experience where players solve randomly generated addition, subtraction, multiplication, and division problems, then records each session locally for later review or export.

## Features

- **Configurable timed quiz** – Choose which operations to include, set operand ranges for addition/subtraction and multiplication/division separately, pick a duration (30–150 seconds), and start/stop a timed round at any moment.
- **Player tracking and scoring** – Enter a player name before starting, answer questions to earn points, and see the current score and elapsed time update live during the round.
- **Saved history with details** – Each completed session is saved with the player name, score, duration, and problem settings. The History tab lists all sessions and lets you tap into a detailed view.
- **Clear or export results** – Clear all saved sessions or export them to a CSV file (written to the Android Downloads folder) for sharing or analysis.

## Project structure

- `Arithmetica.sln` – Solution file that ties the projects together.
- `Testing/Testing` – Shared .NET Standard 2.0 project that contains the UI (Shell pages, view models, and resources), quiz logic, and data access.
- `Testing/Testing.Android` – Android head project for packaging and device-specific assets.
- `Testing/Testing.iOS` – iOS head project for building on macOS toolchains.
- `com.companyname.testing.apk` – Prebuilt Android package output from a prior build.

## Getting started

1. Install Visual Studio 2022 (or Visual Studio for Mac) with the **Mobile development with .NET (Xamarin)** workload. Ensure you have the Android SDK/emulator or access to a Mac for iOS builds.
2. Clone the repository and open `Arithmetica.sln` in Visual Studio.
3. Restore NuGet packages when prompted; the shared project relies on Xamarin.Forms, Xamarin.Essentials, sqlite-net-pcl, and CsvHelper.
4. Select either the **Testing.Android** or **Testing.iOS** startup project, choose a device/emulator target, and run/debug from Visual Studio.
5. On Android, grant storage permissions if you plan to use CSV export (files are written to the device Downloads directory).

## Using the app

1. Open the **Arithmetic Game** tab.
2. Enter a player name, choose the operations to practice, set operand ranges, and pick a duration.
3. Press **(Re)Start** to begin the round. Correct answers advance to the next question and increase the score; the timer stops automatically when the duration elapses.
4. Optionally press **Stop** to pause a round early.
5. Switch to the **History** tab to review saved sessions, clear all history, export it to CSV, or tap an entry for full details.

## Data and storage

- Session data is persisted locally in a SQLite database located at the app data directory (`ArithmeticaSQLite.db3`).
- The History tab reads from the database, and the **Export** action writes a CSV file named `aritmetica_<timestamp>.csv` to the Android Downloads folder for easy sharing.

## Notes and next steps

- This README is a first draft; expand it with platform-specific build caveats, screenshots, or troubleshooting tips as the project evolves.
- Consider adding automated build steps (e.g., `dotnet` or MSBuild commands) and device permission guidance once platform pipelines are defined.
