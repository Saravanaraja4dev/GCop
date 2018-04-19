﻿# GCop 534

> *"Use yourMemberInfo.Defines< TYPE > instead."*

## Rule description

...

## Example

```csharp
var result = Attribute.IsDefined(myElement, typeof(EnableLogAttribute));
```

*should be* 🡻

```csharp
var result = myElement.Defines<EnableLogAttribute>;
```