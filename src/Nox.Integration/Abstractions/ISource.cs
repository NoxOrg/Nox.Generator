using System.Dynamic;
using ETLBox.Connection;
using ETLBox.DataFlow;
using SqlKata.Compilers;

namespace Nox.Integration.Abstractions;

public interface ISource
{
    string Name { get; }
    string Type { get; }
    Compiler SqlCompiler { get; }
    IDataFlowExecutableSource<ExpandoObject> DataFlowSource();
    void ApplyMergeInfo(IntegrationMergeStates lastMergeInfo, string[] dateTimeStampColumns, string[] targetColumns);
}