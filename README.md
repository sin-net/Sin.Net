# ![Sin.Net](./Images/Sin.Net.Logo.small.png "Sin.Net") Sin.Net

[![Build Status](https://dev.azure.com/adriansinger87/adriansinger87/_apis/build/status/adriansinger87.Sin.Net?branchName=master)](https://dev.azure.com/adriansinger87/adriansinger87/_build/latest?definitionId=1&branchName=master)


The **Sin.Net** Project contains a list of reusable assemblies
to build clean software architectures for any software purpose.

## Downloadable on NuGet
* [![Nuget](https://img.shields.io/nuget/v/Sin.Net.Domain.svg?label=Sin.Net.Domain)](https://www.nuget.org/packages/Sin.Net.Domain/)
* [![Nuget](https://img.shields.io/nuget/v/Sin.Net.Persistence.svg?label=Sin.Net.Persistence)](https://www.nuget.org/packages/Sin.Net.Persistence/)
* [![Nuget](https://img.shields.io/nuget/v/Sin.Net.Logging.svg?label=Sin.Net.Logging)](https://www.nuget.org/packages/Sin.Net.Logging/)
* [![Nuget](https://img.shields.io/nuget/v/Sin.Net.Infrastructure.svg?label=Sin.Net.Infrastructure)](https://www.nuget.org/packages/Sin.Net.Infrastructure/)

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
