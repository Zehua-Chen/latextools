# LaTeX Tools

Various tools to help you with LaTeX projects

![LaTeX](https://img.shields.io/badge/latex-%23008080.svg?style=for-the-badge&logo=latex&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)

![Test](https://github.com/Zehua-Chen/latextools/actions/workflows/test.yml/badge.svg)

- [LaTeX Tools](#latex-tools)
  - [Installation](#installation)
  - [Get Started](#get-started)
  - [Commands](#commands)
  - [Documentation](#documentation)
  - [Development](#development)
    - [Features](#features)
    - [Architecture](#architecture)
    - [Publish](#publish)

## Installation

1. `brew tap zehua-chen/tools`
2. `brew install latextools`

## Get Started

1. `latextools new --name project`
2. Enter folder `project` (`cd project`)
3. Invoke in command line
   ```
   latextools build
   ```
4. `latextools open` would open the pdf file

## Commands

- Create a new project
  ```
  latextools new -n <folder>
  ```
- Build the project and then rebuild it if needed
  ```
  latextools build
  ```
- Clean the build folder
  ```
  latextools clean
  ```
- Generate a Makefile for this project
  ```
  latextools generate
  ```
- Open the pdf file
  ```
  latextools open
  ```

## Documentation

- [Project](docs/Project.md)

## Development

### Features

- [x] Automatically compile twice if `.aux` files changes
- [x] Automatically run bibliography programs
- [x] Automatically run glossary programs
- [ ] Nested projects

### Architecture

- `src/LaTeXTools.Project`: manages project configuration
- `src/LaTeXTools.Build`: build projects
- `src/latextools`: the command line application

### Publish

1. Go to `src/latextools`, and run
   - `dotnet publish --self-contained -c Release -r osx-x64`
   - `dotnet publish --self-contained -c Release -r osx-arm64`
