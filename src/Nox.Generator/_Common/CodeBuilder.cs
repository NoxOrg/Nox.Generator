﻿using System.Text;

namespace Nox.Generator;

internal class CodeBuilder
{
    private const int _spacing = 4;

    private StringBuilder _stringBuider = new ();

    private int _indentation = 0;

    public void AppendLine(string text = "")
    {
        _stringBuider.Append(new string(' ', _spacing * _indentation));
        _stringBuider.AppendLine(text);
    }

    public void AppendIndented(string text = "")
    {
        _stringBuider.Append(new string(' ', _spacing * _indentation));
        _stringBuider.Append(text);
    }

    public void Append(string text = "")
    {
        _stringBuider.Append(text);
    }

    public void StartBlock()
    {
        AppendLine($"{{");
        Indent();
    }

    public void EndBlock()
    {
        UnIndent();
        AppendLine($"}}");
    }

    public override string ToString()
    {
        return _stringBuider.ToString();
    }

    public int Indent()
    {
        _indentation++;

        return _spacing * _indentation;
    }

    public int UnIndent()
    {
        _indentation--;

        return _spacing * _indentation;
    }

    public int CurrentIndentSpacing()
    {
        return _spacing * _indentation;
    }
}
