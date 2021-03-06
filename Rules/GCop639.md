﻿# GCop 639

> *"It should be written as if `foo?.Bar != null`"*

## Rule description

The `?.` operator lets you access members and elements only when the receiver is not-null, returning null result otherwise. It is exactly what we except when checking an object is null or not like the samples below, while `?.` is more meaningful and readable.

## Example

```csharp
if (foo != null && foo.Bar != null)
{
    ...
}
```

*should be* 🡻

```csharp
if (foo?.Bar != null)
{
    ...
}
```