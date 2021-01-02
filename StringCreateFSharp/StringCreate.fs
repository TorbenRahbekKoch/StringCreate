module StringCreate
open BenchmarkDotNet.Attributes
open System


// |          Method |     Mean |   Error |  StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
// |---------------- |---------:|--------:|--------:|-------:|------:|------:|----------:|
// | StandardReplace | 348.1 ns | 7.03 ns | 6.91 ns | 0.1173 |     - |     - |     368 B |
// | StringCreateFor | 210.8 ns | 4.18 ns | 4.29 ns | 0.0918 |     - |     - |     288 B | 

[<MemoryDiagnoser>]
type Replace() = 
    let S = "This is a string with an X in the middle and another right here: Y!";
    let X = "amazing word";
    let Y = "word";


    [<Benchmark>]
    member this.StandardReplace() = 
        S.Replace("X", X).Replace("Y", Y)

    [<Benchmark>]
    member public this.StringCreateFor() = 
        let data = {| S = S ; X = X; Y = Y |}

        String.Create(
            S.Length - 2 + X.Length + Y.Length, 
            data,
            fun (chars: Span<char>) (data: {| S: String; X: String; Y: String|}) -> 
                let mutable indexC = 0
                for s in data.S do
                    if s = 'X' then
                        for x in data.X do
                            chars.[indexC] <- x
                            indexC <- indexC + 1
                    else if s = 'Y' then
                        for y in data.Y do
                            chars.[indexC] <- y
                            indexC <- indexC + 1
                    else
                        chars.[indexC] <- s
                        indexC <- indexC + 1
                ()
        )
                

