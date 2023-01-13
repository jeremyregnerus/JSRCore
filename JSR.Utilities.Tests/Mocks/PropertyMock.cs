using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace JSR.Utilities.Tests.Mocks
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit test")]
    public class PropertyMock
    {
        public ChildClassMock ClassReadWrite { get; set; }

        public ChildClassMock ClassReadOnly { get => ClassReadWrite; }

        public ChildClassMock ClassWriteOnly { set => ClassReadWrite = value; }

        public object ObjectReadWrite { get; set; }

        public object ObjectReadOnly { get; private set; }

        public object ObjectWriteOnly { private get; set; }

        public INotifyPropertyChanged InterfaceReadWrite { get; set; }

        public INotifyPropertyChanged InterfaceReadOnly { get => InterfaceReadWrite; }

        public INotifyPropertyChanged InterfaceWriteOnly { set => InterfaceReadWrite = value; }

        public List<string> ListReadWrite { get; set; }

        public List<string> ListReadOnly { get; private set; }

        public List<string> ListWriteOnly { private get; set; }

        public string StringReadWrite { get; set; }

        public string StringReadOnly { get => StringReadWrite; }

        public string StringWriteOnly { set => StringReadWrite = value; }

        public EnumMock EnumReadWrite { get; set; }

        public EnumMock EnumReadOnly { get; private set; }

        public EnumMock EnumWriteOnly { private get; set; }

        public StructMock StructReadWrite { get; set; }

        public StructMock StructReadOnly { get => StructReadWrite; }

        public StructMock StructWriteOnly { set => StructReadWrite = value; }

        public char CharReadWrite { get; set; }

        public char CharReadOnly { get; private set; }

        public char CharWriteOnly { private get; set; }

        public (int Val1, int Val2) TupleReadWrite { get; set; }

        public (int Val1, int Val2) TupleReadOnly { get => TupleReadWrite; }

        public (int Val1, int Val2) TupleWriteOnly { set => TupleReadWrite = value; }

        public bool BoolReadWrite { get; set; }

        public bool BoolReadOnly { get; private set; }

        public bool BoolWriteOnly { private get; set; }

        public decimal DecimalReadWrite { get; set; }

        public decimal DecimalReadOnly { get => DecimalReadWrite; }

        public decimal DecimalWriteOnly { set => DecimalReadWrite = value; }

        public double DoubleReadWrite { get; set; }

        public double DoubleReadOnly { get; private set; }

        public double DoubleWriteOnly { private get; set; }

        public int IntReadWrite { get; set; }

        public int IntReadOnly { get => IntReadWrite; }

        public int IntWriteOnly { set => IntReadWrite = value; }
    }
}
