### Queryabl.DataCalc (package)

|  Review  |
|:------------:|
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/4049b940d08b4bfeb84a3f42701b93e8)](https://app.codacy.com/gh/chrdek/linqpath_prerel?utm_source=github.com&utm_medium=referral&utm_content=chrdek/linqpath_prerel&utm_campaign=Badge_Grade) |
| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=chrdek_linqpath_prerel&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=chrdek_linqpath_prerel) |
| [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=chrdek_linqpath_prerel&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=chrdek_linqpath_prerel) |
| ![Nuget](https://img.shields.io/nuget/dt/LinqPath?logo=nuget) |
| [🌐 Global Status](https://status.nuget.org/) |

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

<br>

<br>

<hr>

__The IQueryable port alternative of LinqDataCalc.__

<br/>
<br/>
<br/>


Most parts of the re-written source are meant to provide a more fluent linq-like syntax alternative for some methods in the original LinqDataCalc with some examples.

<br/>

<div align="center">

Methods Ported to IQueryable syntax displayed in table below:

<br>

|  Method  | Implemented As | Usage |
|:------------:|:------------:|:------------:|
| AsNumericTuples()↗️ <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ↘️ | FilterNumerics()<br> FilterNumerics(predicate)  |  __example only__ of funct. filtered numerics, use 'where', 'select' clauses instead |
|   |   |   |
| OddOrEven()&nbsp;&nbsp; ➡️ | CollEvenLength()  | size-specific collection filter |


</div>


Online project located at [nuget gallery.](https://www.nuget.org/packages/LinqPath/)

