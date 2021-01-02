using System;
using BenchmarkDotNet.Attributes;

namespace StringCreate
{
// |              Method |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
// |-------------------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
// |     StandardReplace | 244.3 ns | 3.03 ns | 2.53 ns | 0.1168 |     - |     - |     368 B |
// |        StringCreate | 127.9 ns | 2.63 ns | 2.92 ns | 0.0587 |     - |     - |     184 B |
// | StringCreateForEach | 126.7 ns | 2.41 ns | 2.25 ns | 0.0587 |     - |     - |     184 B |

    [MemoryDiagnoser]
    public class Replace
    {
        private const string S = "This is a string with an X in the middle and another right here: Y!";
        private const string X = "amazing word";
        private const string Y = "word";

        [Benchmark]
        public string StandardReplace()
        {
            return S.Replace("X", X).Replace("Y", Y);
        }

        [Benchmark]
        public string StringCreate()
        {
            return String.Create(
                S.Length - 2 + X.Length + Y.Length,
                state: (S,X,Y),
                (chars, data) =>
                {
                    var indexS = 0;
                    for (int index = 0; index < chars.Length;)
                    {
                        if (data.S[indexS] == 'X')
                        {
                            for (var indexX = 0; indexX < data.X.Length; indexX++)
                            {
                                chars[index] = data.X[indexX];
                                index++;
                            }
                        }
                        else if (data.S[indexS] == 'Y')
                        {
                            for (var indexY = 0; indexY < data.Y.Length; indexY++)
                            {
                                chars[index] = data.Y[indexY];
                                index++;
                            }
                        }
                        else
                        {
                            chars[index] = data.S[indexS];
                            index++;
                        }
                        indexS++;
                    }
                });
        }

        [Benchmark]
        public string StringCreateForEach()
        {
            return String.Create(
                S.Length - 2 + X.Length + Y.Length,
                (S,X,Y),
                (chars, data) =>
                {
                    var indexC = 0;
                    foreach (var s in data.S)
                    {
                        if (s == 'X')
                        {
                            foreach (var t in data.X)
                            {
                                chars[indexC] = t;
                                indexC++;
                            }
                        }
                        else if (s == 'Y')
                        {
                            foreach (var t in data.Y)
                            {
                                chars[indexC] = t;
                                indexC++;
                            }
                        }
                        else
                        {
                            chars[indexC] = s;
                            indexC++;
                        }
                    }
                });
        }
    }
}
