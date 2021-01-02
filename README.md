# StringCreate

Comparing the String.Create clever method in C# and F# with BenchmarkDotNet

Using String.Create in F# allocates more memory than in C#.

Results for F#

|          Method |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
| StandardReplace | 348.1 ns | 7.03 ns | 6.91 ns | 0.1173 |     - |     - |     368 B |
| StringCreateFor | 210.8 ns | 4.18 ns | 4.29 ns | 0.0918 |     - |     - |     288 B | 

And C#

|              Method |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
|     StandardReplace | 244.3 ns | 3.03 ns | 2.53 ns | 0.1168 |     - |     - |     368 B |
|        StringCreate | 127.9 ns | 2.63 ns | 2.92 ns | 0.0587 |     - |     - |     184 B |
| StringCreateForEach | 126.7 ns | 2.41 ns | 2.25 ns | 0.0587 |     - |     - |     184 B |


Using String.Create with F# allocates 288bytes. In C# it is only 184bytes.


