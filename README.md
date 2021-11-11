# LaTeX Tools

Tools used with latex

![Test](https://github.com/Zehua-Chen/latextools/actions/workflows/test.yml/badge.svg)

## Features

- Automatically compile twice if `.aux` files changes
- Automatically run bibliography programs
- Automatically run glossary programs

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

### Architecture

- `src/LaTeXTools.Project`: manages project configuration
- `src/LaTeXTools.Build`: build projects
- `src/latextools`: the command line application

### Publish

1. Go to `src/latextools`, and run
   - `dotnet publish --self-contained -c Release -r osx-x64`
   - `dotnet publish --self-contained -c Release -r osx-arm64`
