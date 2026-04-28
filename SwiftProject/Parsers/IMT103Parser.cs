using SwiftProject.Models;

namespace SwiftProject.Parsers
{
    public interface IMT103Parser
    {
        MT103 Parse(string mT103);
    }
}
