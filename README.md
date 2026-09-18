# Student Management System

A C# WinForms school-management application with student/teacher administration, academic records, reporting, **automatic teaching assignment**, and **automatic timetable generation**.

## Highlights

Two modules make this project more than a standard CRUD application:

### Automatic teaching assignment

A heuristic assignment service matches teachers to class/subject requirements while considering specialization, homeroom priority, and teaching-load balance.

### Automatic timetable generation

A Tabu Search scheduler generates timetables under hard and soft constraints.

**Hard constraints** include avoiding teacher, class, and room conflicts in the same time slot.

**Soft constraints** include subject distribution, schedule compactness, and teacher-load balance.

The workflow supports previewing generated solutions before they are accepted.

## Core features

- Student and parent records
- Teacher and specialization management
- Classes and subjects
- Teaching assignment
- Automatic timetable generation
- Grades and academic classification
- Reports and statistics
- Excel / PDF export workflows

## Tech stack

| Area | Technology |
| --- | --- |
| Application | C# WinForms |
| Runtime | .NET Framework 4.8 |
| Database | MySQL 8 / MariaDB-compatible setup |
| Architecture | DAO / BUS / GUI layers |
| Optimization | Heuristic assignment, Tabu Search |

## Scheduling model

A timetable solution is generated over configured school days and periods. The scheduler uses a configurable iteration budget, tabu tenure, no-improvement stopping condition, and weighted soft constraints.

Relevant implementation and documentation live in the scheduling service and the technical notes under the project directory.

## Repository structure

```text
Student-Management-System_CSharp_SGU2025/
├── Student-Management-System_CSharp_SGU2025.sln
├── Student-Management-System_CSharp_SGU2025/
│   ├── DAO/
│   ├── BUS/
│   ├── GUI/
│   ├── Services/
│   ├── ConnectDatabase/
│   └── docs/
├── HUONG_DAN_THEM_ANH_HOC_SINH.md
└── README.md
```

## Local setup

### Requirements

- Windows 10/11
- Visual Studio with .NET Framework 4.8 support
- MySQL 8+ or compatible MariaDB setup

### Database

Create the local database, then import the schema from the repository's `ConnectDatabase` directory.

Do not hard-code production credentials into source code. Configure the local development connection for your own environment.

### Build

Open:

```text
Student-Management-System_CSharp_SGU2025.sln
```

in Visual Studio, restore dependencies, and run the WinForms application.

## Engineering notes

The repository includes documentation for the assignment/timetable workflow and smoke testing. The scheduling layer is designed so generated assignments can be validated and reviewed before replacing the authoritative timetable.

## Status

This repository represents an academic software project and is kept as a portfolio example of desktop application development, relational-data workflows, layered architecture, and combinatorial scheduling.


## Local configuration

Database credentials are not stored in source code. Configure the local MySQL connection with environment variables:

```text
STUDENT_DB_PASSWORD
STUDENT_DB_USER
STUDENT_DB_HOST
STUDENT_DB_NAME
STUDENT_DB_PORT
```

The host, database name, user, and port have local-development defaults; the password defaults to empty. Gmail settings in `App.config` are placeholders only—use local, uncommitted credentials for real SMTP access.
