
namespace CAI
{
    /// <summary>
    /// Something that has name and description
    /// </summary>
    interface IDescribable
    {
        public string GetName();

        public string GetDescription();

        public string GetUsage();
    }
}
