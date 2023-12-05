### Queryabl.DataCalc (package)

|  Review  |
|:------------:|
[![Codacy Badge](https://app.codacy.com/project/badge/Grade/9450c471020d498a9f0e10616855fcf6)](https://app.codacy.com/gh/chrdek/QueryablDataCalc/dashboard?utm_source=gh&utm_medium=referral&utm_content=&utm_campaign=Badge_grade) |
| [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=chrdek_QueryablDataCalc&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=chrdek_QueryablDataCalc) |
| [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=chrdek_QueryablDataCalc&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=chrdek_QueryablDataCalc) |
| ![Nuget](https://img.shields.io/nuget/dt/Queryabl.DataCalc?logo=nuget) |
| [🌐 Global Status](https://status.nuget.org/) |

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;


<hr>

__The IQueryable port alternative of LinqDataCalc.__

<br/>
<br/>

Most parts of the re-written source are meant to provide a more fluent linq-like syntax alternative for some methods in the original LinqDataCalc with some examples.

<br/>

<div align="center">

Previous/New methods Ported to IQueryable syntax displayed in table below:

<br>

|  Method  | Implemented As | Usage |
|:------------:|:------------:|:------------:|
| AsNumberTuples() &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ↗️ <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ↘️ | <br> FilterNumerics()<br> FilterNumerics(predicate)  |  __example only__ of funct. filtered numerics, use 'Where', 'Select' clauses instead |
|   |   |   |
| OddOrEven()&nbsp;&nbsp; ➡️ | CollEvenLength()  | size-specific collection filters |
|   |   |   |
| HammingDist() ➡️ | WhereDist()  | hamming-distance collection search  using 'Where' clause |
|   |   |   |
| New Methods&nbsp;↗️ <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ↘️ | FreqOccur()<br> FreqObjsOccur()  | filtering based on object frequency |
|   |   |   |
| ToIntMatrix()&nbsp;&nbsp;&nbsp;&nbsp;↗️ <br> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ↘️ | SelectIntMatrix()<br> SelectIntMatrix() | Large Integer string-to-matrix representations |

</div>

<br>

Online project located at [nuget gallery.](#)

