
namespace CAI.Examples;

class ExceptionExample : IExample
{
    public void Run()
    {
        throw new System.Exception("Something went really wrong!!");
    }
}
