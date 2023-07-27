using System.Dynamic;
using ETLBox.DataFlow;
using SqlKata.Compilers;

namespace Nox.Integration.Abstractions;

public interface IIntegrationSource
{
    string Name { get; }
    string Type { get; }
    Compiler SqlCompiler { get; }
    IDataFlowExecutableSource<ExpandoObject> DataFlowSource();
}