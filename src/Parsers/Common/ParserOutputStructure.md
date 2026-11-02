# Parser Output Structure

This document defines the parser-independent structure returned in a `ParseResult`. A parser may use any parsing library internally, but its output should use these shared types and meanings.

## Result

`ParseResult` contains one root element and the diagnostics produced while parsing:

| Property | Type | Meaning |
|---|---|---|
| `Root` | `ParsedElement` | Root of the source file's element tree. Use `ElementKind.CompilationUnit` for a source file. |
| `Diagnostics` | `List<ParseDiagnostic>` | Parse errors, warnings, and informational messages in the common diagnostic format. |

## Common element fields

Every node has the following properties:

| Property | Type | Meaning |
|---|---|---|
| `Kind` | `ElementKind` | Parser-independent category of this node. |
| `Span` | `SourceSpan?` | Optional range in the original source. Offsets are zero-based UTF-16 code units; the end is exclusive. |
| `Text` | `string` | Source text represented by this node. It may be empty when the parser cannot provide it. |
| `Children` | `List<ParsedElement>` | Child nodes in source/structural order. |

A consumer should use `Kind` and the common properties rather than depend on the concrete parser's syntax-node types.

## Element-specific data

Use the common `ParsedElement` for kinds that have no additional normalized data. Specialized element types add only the fields listed here:

| Element type | `Kind` | Additional properties |
|---|---|---|
| `DeclarationElement` | A declaration kind, such as `Namespace`, `Class`, `Struct`, `Interface`, `Record`, or `Enum` | `Name` |
| `CallableElement` | `Callable` | `Name`, `Modifiers`, optional `ReturnType` |
| `ParameterElement` | `Parameter` | `Name`, `Type`, `IsNullable` |
| `ImportElement` | `Import` | `ImportedName`, optional `Alias` |
| `IdentifierElement` | `Identifier` | `Name` |
| `LiteralElement` | `Literal` | `Value` |
| `OperatorElement` | `Assignment`, `BinaryExpression`, `PrefixUnaryExpression`, `PostfixUnaryExpression`, or `UnaryExpression` | `Operator` |
| `IsExpressionElement` | `IsExpression` | `Pattern` |

Properties that do not apply should not be added as empty placeholders to the base class. For example, an invocation with no additional normalized fields is a `ParsedElement` whose `Kind` is `Invocation`.

## Tree relationships

`Children` represent the structural contents of a node, not an additional copy of every token or trivia item. Parsers should preserve useful semantic structure consistently:

- A compilation unit contains its top-level imports, namespaces, and declarations.
- A namespace contains its nested namespaces and declarations.
- A type declaration contains its members and, when represented, its inheritance information.
- A callable contains its parameter list, followed by its body or expression body when present.
- An invocation contains the invoked expression and its argument-list node.
- A member access contains the receiver expression and the member-name node.
- An argument contains its expression.
- Other constructs contain the meaningful child nodes in source order.

If a parser encounters syntax it cannot classify, it should still preserve the source range, text, and any useful children using `ElementKind.Unknown`, rather than silently dropping that part of the tree.

## Example

```text
CompilationUnit
├── Import
│   └── ImportedName = "System"
└── Class
    ├── Name = "Greeter"
    └── Callable
        ├── Name = "SayHello"
        ├── ReturnType = "void"
        ├── ParameterList
        │   └── Parameter
        │       ├── Name = "name"
        │       └── Type = "string"
        └── Block
            └── ExpressionStatement
                └── Invocation
                    ├── MemberAccess
                    └── ArgumentList
```

The diagram omits common fields (`Span`, `Text`) and any child nodes that are not relevant to the example.

## Current implementation notes

The C# parser uses Roslyn internally and maps its nodes to this model. Its default mapping recursively includes syntax-node children, with special child selection for callables, invocations, member accesses, arguments, and constant patterns. The common output contract above is the target for all parsers; it does not require other parsers to reproduce Roslyn's syntax tree shape.

The fields and relationships supported by each parser can grow over time. When adding a normalized field or changing a relationship, update this contract and tests for the parser output.
