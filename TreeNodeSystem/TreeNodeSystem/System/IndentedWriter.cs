using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeNodeSystem.System
{
    public class IndentedWriter
    {
        private string indent = string.Empty;
        private StringBuilder builder = new StringBuilder();

        public virtual string indentToken { get; } = "    ";

        public void PushIndent()
        {
            lock (indent)
            {
                indent += indentToken;
            }
        }

        public void PopIndent()
        {
            lock (indent)
            {
                if (indent.Length >= indentToken.Length)
                    indent = indent.Substring(0, indent.Length - indentToken.Length);
            }
        }

        public void AppendIndentedString(string toPrepare)
        {
            builder.AppendFormat("// {0}{1}", indent, toPrepare);
        }

        public void AppendRawString(string toAppend)
        {
            builder.Append(toAppend);
        }

        public void Clear()
        {
            builder.Clear();
            indent = string.Empty;
        }

        public override string ToString()
        {
            return builder.ToString();
        }
    }
}
