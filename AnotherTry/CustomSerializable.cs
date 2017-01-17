using Xunit.Abstractions;

namespace AnotherTry
{
    class CustomSerializable : IXunitSerializable
    {
        public CustomSerializable() { }  // Needed for deserializer

        public CustomSerializable(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }

        public void Deserialize(IXunitSerializationInfo info)
        {
            Value = info.GetValue<int>("Value");
        }

        public void Serialize(IXunitSerializationInfo info)
        {
            info.AddValue("Value", Value);
        }

        public override string ToString()
        {
            return "Value = " + Value;
        }
    }
}