# Sin.Net.Persistence

## Description

Implements commonly used persistence functionality. 
A derivation of the `PersistenceController` class in your client code is supported and
follows the strategy pattern with a fluent API.

The main functionalities are:
* **Binary** Im- and Export with the .NET `BinaryFormatter`.
* **CSV** Im and Export with `DataTable` objects.
  An adaption of client-side data structures to their `DataTable` representation
  is done by injected adapter objects that follow the adapter pattern
* **Json** Im and Export based on the `Newtonsoft.Json` library.

The setup of the importer and exporter instances is realized with settings objects
 
## Dependencies

 * Sin.Net.Domain
 * Newtonsoft.Json