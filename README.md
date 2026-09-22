# AI Project Doctor 🩺

AI Project Doctor is a Unity Editor Tool designed to detect common project issues, provide diagnostics, and apply automated fixes directly inside the Unity Editor.

## Features

* Detects common Unity project issues
* Rigidbody validation
* Collider validation
* Tag validation
* Audio configuration checks
* Health score based project analysis
* Automated issue fixing
* Automatic re-scan after fixes
* Editor-based workflow
* Offline AI-style diagnosis and recommendations

## How It Works

**Detect → Diagnose → Fix → Re-scan**

1. Select a GameObject or analyze the project.
2. AI Project Doctor scans for common configuration issues.
3. The tool displays detected issues and recommendations.
4. Apply the available fixes directly from the Editor Window.
5. Re-scan the project to verify the result.

## Tech Stack

* Unity
* C#
* Unity Editor Scripting
* EditorWindow
* Object Validation
* Automated Project Diagnostics

## Project Structure

```text
Assets/
└── Editor/
    ├── DoctorIssue.cs
    ├── DoctorRule.cs
    ├── ProjectDoctorAI.cs
    └── ProjectDoctorWindow.cs
```

## Key Engineering Concepts

* Rule-based validation
* Separation of detection and fixing logic
* Unity Editor tooling
* C# object-oriented programming
* Automated validation workflow
* Modular editor architecture

## Purpose

This project was built as a technical portfolio project to demonstrate Unity/C# programming, editor scripting, debugging, validation systems, and tool development.

## Demo

Demo video/screenshots can be added here.

## Author

**Himani Yadav**

Game Developer | Unity | C#
