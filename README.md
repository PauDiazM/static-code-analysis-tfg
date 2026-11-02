# Static Code Analysis - TFG

Static code analysis tool developed as part of a Bachelor's Thesis (TFG) in Computer Science at Universitat Autònoma de Barcelona.

## Overview

The project aims to design and implement a multi-language static code analysis tool capable of extracting and representing structural and dependency information from source code.

The tool is intended to support the analysis of different programming languages through a common abstraction layer.

## Project Status

🚧 Work in progress.

The architecture and supported programming languages are currently being defined.

## Parser Output

Parsers expose a shared `ParseResult` containing a `ParsedElement` tree and normalized parse diagnostics. Parser-specific libraries are used only inside their parser implementations; downstream analysis should consume the shared model rather than syntax nodes from a particular parser.

The C# parser uses Roslyn internally and maps its syntax tree to the common model. The console application currently prints that model as a development aid.

## Repository Structure

```text
docs/       Project documentation and TFG tracking
src/        Source code
tests/      Automated tests
examples/   Example source code used for analysis
```

## Goals

* Support multiple programming languages.
* Provide a common abstraction for language-specific code structures.
* Extract structural and dependency information.
* Represent relationships between elements of a program.
* Enable further analysis such as dependency and impact analysis.
* Provide a basis for visualizing the extracted information via graphs.

## Author

Pau Díaz Martínez

Bachelor's Degree in Computer Science
Universitat Autònoma de Barcelona
