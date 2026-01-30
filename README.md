# Moonlight lib
Moonlight lib is a set of C# net9.0 utils which can ease the making of console based applications by giving different classes for specific use cases, it's based in
C# console for multiplatform purposes which allows for an easy use outside of the windows dependant console project, it contains:

## Menu management:
A series of classes which allow the making of an menu through a dictionary rather by manually implementing a do-while loop and hardcoded switch-case options, the Menus namespace
has all of the needed classes for a simple menu system to be implemented, including:
- Menu
- Submenu
- DescriptiveMenu
- DescriptiveSubmenu
Where regardless of the chosen menu all follow the same standard of dictionary based options, submenus having the particularity of having a "return to last menu" message while
the descriptive variants of each hold an auxiliar dictionary to give each option a description

## Ask helpers:
A series of functions which allow you to acquire data from a user through loops and verification, they force the user to type the requested data until it's viable for the desired
value type, they allow the programmer to set a message or run with a default explicit "write a [type]" text

## Console helpers:
3 simple functions which allow for easy console output, allowing printing multiple lines of text and also giving a background with the option a foreground

## Fractions
A new number type which allows the representation of fractions through a pair of decimals: a numerator and a denominator. Fractions have been made based on C# internal numeric
types and hence can be mostly used as a regular numeric data type, including parsing and try parsing. Fractions can be parsed through numbers ("10", "20", etc.) or through the
common way to write fractions ("1/2","20/4", etc.). Common properties include:
- Upper
- Lower
- Value
Where upper is the upper part of the fraction, lower the lower one and value being the result of solving the division of both parts.

# Remarks
This code was made through Visual Studio Code, this project is entirely a library and hence lacks any Main methods
