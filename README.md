# ![Sin.Net](./Images/Sin.Net.Logo.small.png "Sin.Net") Sin.Net

The **Sin.Net** Project contains a list of reusable assemblies
to build clean software architectures for any software purpose.

## Downloadable on NuGet

* [Sin.Net.Domain](https://www.nuget.org/packages/Sin.Net.Domain/ "Sin.Net.Domain")
* [Sin.Net.Persistence](https://www.nuget.org/packages/Sin.Net.Persistence/ "Sin.Net.Persistence")
* [Sin.Net.Logging](https://www.nuget.org/packages/Sin.Net.Logging/ "Sin.Net.Logging")

## Descriptions

### Sin.Net.Domain

Implements basic abstractions and interfaces that
can be used for all types of .Net applications.

The abstractions cover the following definitions:

 * A general persistence layer, following the strategy pattern.
 * Logging, see optional Implementation **Sin.Net.Logging** for more information.
 * A repository pattern for managing collections and attached fields.

### Sin.Net.Logging

Implements logging as part of the persistence layer.
This assembly can be seen as reference code or
for usage in addition to the **Sin.Net.Domain** assembly.