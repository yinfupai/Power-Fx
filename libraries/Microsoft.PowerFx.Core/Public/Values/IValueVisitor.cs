using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.PowerFx
{
    public interface IValueVisitor
    {
        void Visit(BlankValue value);
        void Visit(NumberValue value);
        void Visit(BooleanValue value);
        void Visit(StringValue value);
        void Visit(ErrorValue value);
        void Visit(RecordValue value);
        void Visit(TableValue value);
        void Visit(TimeValue value);
        void Visit(DateValue value);
    }
}
