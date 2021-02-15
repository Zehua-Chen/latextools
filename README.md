# LaTeX Tools

Tools used with latex

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

```
latextools new -n <folder>
```

Create a new project

```
latextools build
```

Build the project and then rebuild it if needed

```
latextools clean
```

Clean the build folder

```
latextools generate
```

Generate a Makefile for this project

```
latextools open
```

Open the pdf file

## Documentation

- [Project](docs/Project.md)

## Development

### Architecture

- `src/LaTeXTools.Project`: manages project configuration
- `src/LaTeXTools.Build`: build projects
- `src/latextools`: the command line application

### Publish

1. Go to `src/latextools`, and run
   - `dotnet publish -c Release -r osx-x64`
