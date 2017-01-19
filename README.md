[![Build Status](https://travis-ci.org/Geaz/coreArgs.svg?branch=master)](https://travis-ci.org/Geaz/coreArgs)
[![MIT licensed](https://img.shields.io/badge/license-MIT-blue.svg)](https://raw.githubusercontent.com/Geaz/coreArgs/master/LICENSE)
[![Gitter](https://badges.gitter.im/geaz84/coreArgs.svg)](https://gitter.im/geaz84/coreArgs?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

(Notes to me: Feature ready (just help text missing) - tests all positive - write more tests - some refactorings and documentation writing necessary - nuget creation outstanding)

# coreArgs
**coreArgs** is a command line arguments parser for *.NET Standard*.
It was created in need of a command line parser which is able to parse arguments into
a predifined class and any remaining arguments into a dynamic object.

This behaviour was needed for [coreDox](http://github.com/geaz/coreDox).

## Installation

Use NuGet to install the [coreArgs NuGet (not available - wait until first version)](http://google.de).

## Usage

Create a class with your needed options.
```csharp
using coreArgs;

public class Options
{
    [Option("longoption", "This LongOption is required", true)]
    public string RequiredLongStringOption { get; set; }

    [Option('s', "shortoption", "This ShortOption is not required")]
    public string ShortStringOption { get; set; }

    [Option('d', "decimal", "Decimal option")]
    public decimal DecimalOption { get; set; }

    [Option('b', "bool", "Bool option")]
    public bool BoolOption { get; set; }

    [Option('l', "list", "List option")]
    public List<string> ListOption { get; set; }

    // If defined, coreArgs will drop all not defined options,
    // into this dynamic object. Internally this property is set
    // to a custom dynamic object (UndefinedArgs). This object returns *null* for access
    // on not defined properties, instead of throwing an exception like the 
    // .NET dynamic object.
    [RemainingOptions]
    public dynamic RemainingOptions { get; set; }

    // If defined, coreArgs will drop all arguments,
    // which are no options into this property.
    [BinOptions]
    public List<string> BinOptions { get; set; }
}
```

And parse it with the **coreArgs** parser.
```csharp
using coreArgs;
using coreArgs.Extensions;

public class ParseTest
{
    //Arguments: whatsThat andThat --longoption test -b --noOption "This was not defined in option class"
    static int Main(string[] args)
    { 
        var result = ArgsParser.Parse<Options>(args);
        if(result.Errors.Count > 0)
        {
            // Do something in case of errors
            // for example show the help text
            Console.Write(ArgsParser.GetHelpText());
        }
        else
        {
            var arguments = result.Arguments;

            Console.WriteLine(arguments.LongOption);
            Console.WriteLine(arguments.BoolOption);
            // ...

            Console.WriteLine(arguments.RemaingOptions.noOption);
            // Output: "This was not defined in option class"

            Console.WriteLine(arguments.RemaingOptions.notDefined);
            // Output: ""
            
            Console.WriteLine(string.Join(", ", arguments.BinOptions));
            // Output: "whatsThat, andThat"
        }        
    }
}
```

