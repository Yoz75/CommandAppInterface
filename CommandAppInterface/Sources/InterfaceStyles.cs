using System;

namespace CAI;

[Flags] public enum InterfaceStyles
{
    None = 0,
    WriteCommandPrompt = 1,
    WelcomeMessage = 2,
    WelcomeAndCommandPrompt = WriteCommandPrompt | WelcomeMessage
}
