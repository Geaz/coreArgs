[![Build Status](https://travis-ci.org/Geaz/coreArgs.svg?branch=master)](https://travis-ci.org/Geaz/coreArgs)

# coreArgs
**coreArgs** is a command line arguments parser for *.NET Standard*.
It was created in need of a command line parser which is able to parse arguments into
a predifined class and any remaining arguments into a dynamic object.

This behaviour was needed for [coreDox](http://github.com/geaz/coreDox).

## Usage

Create a class with your needed options.
```
using coreArgs;

public class Options
{
    [Option("longoption", "This LongOption is required", true)]
    public string RequiredLongStringOption { get; set; }

    [Option('s', "This ShortOption is not required")]
    public string ShortStringOption { get; set; }

    [Option('d', "decimal", "Decimal option")]
    public decimal DecimalOption { get; set; }

    [Option('b', "bool", "Bool option")]
    public bool BoolOption { get; set; }

    [Option('l', "list", "List option")]
    public List<string> ListOption { get; set; }

    // If defined, coreArgs will drop all not defined options,
    // into this dynamic object.
    [RemainingOptions]
    public dynamic RemaingOptions { get; set; }

    // If defined, coreArgs will drop all arguments,
    // which are no options into this property.
    [BinOptions]
    public List<string> BinOtpions { get; set; }
}
```

And parse it with the **coreArgs** parser.
```
using coreArgs;

public class ParseTest
{
    //testProg whatsThat andThat --longoption test -b --notdefined "This was not defined in option class"
    static int Main(string[] args)
    { 
        var options = Parser.DefaultParser.Parse<Options>(args);
        Console.WriteLine(options.LongOption);
        Console.WriteLine(options.BoolOption);
        // ...

        if(options.RemaingOptions.notdefined)
        {
            Console.WriteLine(options.RemaingOptions.notdefined);
            // Output: "This was not defined in option class"
        }
        
        Console.WriteLine(string.Join(", ", options.BinOptions));
        // Output: "whatsThat, andThat"
    }
}
```

