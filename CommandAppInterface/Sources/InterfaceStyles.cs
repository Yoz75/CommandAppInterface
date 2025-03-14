using System;

namespace CAI;

[Flags] public enum InterfaceStyles
{
    None = 0,
    WriteCommandPrompt = 1,
    CopyrightMessage,
    WelcomeMessage = 4,
    CAIName = 8,
    WelcomeAndCommandPrompt = WriteCommandPrompt | WelcomeMessage,
    Full = WriteCommandPrompt | WelcomeMessage | CopyrightMessage | CAIName
}
