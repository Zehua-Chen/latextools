# LaTeX Tools

Tools used with latex

## Get Started

1. Create a folder named `paper`
2. Create a file named `index.tex`
   ```tex
   \documentclass{article}

   \begin{document}
     Hello world!
   \end{document}
   ```
3. Create a project file `latexproject.json`
   ```json
   {
     "entry": "index.tex",
     "bin": "bin"
   }
   ```
4. Invoke in command line
   ```
   latextools build
   ```
5. The PDF should be at `bin/index.pdf`

## Commands

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
