namespace Cavity
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("command.derived")]
    public sealed class DerivedCommand : Command
    {
        public DerivedCommand()
        {
        }

        public DerivedCommand(bool unidirectional)
            : base(unidirectional)
        {
        }

        public override bool Act()
        {
            throw new NotSupportedException();
        }

        public override bool Revert()
        {
            throw new NotSupportedException();
        }
    }
}