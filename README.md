# LaTeX Tools

Tools used with latex

## Get Started

1. `dotnet new --name project`
2. Enter folder `project` (`cd project`)
3. Invoke in command line
   ```
   latextools build
   ```
4. The PDF should be at `bin/index.pdf`

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
