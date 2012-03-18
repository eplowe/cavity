namespace Cavity.Transactions
{
    using System;

    public struct EnlistmentIdentity
    {
        public EnlistmentIdentity(Guid resourceManager)
            : this()
        {
            ResourceManager = resourceManager;
            Instance = Guid.NewGuid();
        }

        public Guid Instance { get; private set; }

        public Guid ResourceManager { get; private set; }

        public static bool operator ==(EnlistmentIdentity operand1, 
                                       EnlistmentIdentity operand2)
        {
            return operand1.Equals(operand2);
        }

        public static bool operator !=(EnlistmentIdentity operand1, 
                                       EnlistmentIdentity operand2)
        {
            return !operand1.Equals(operand2);
        }

        public override bool Equals(object obj)
        {
            if (obj is EnlistmentIdentity)
            {
                var other = (EnlistmentIdentity)obj;
                return ResourceManager.Equals(other.ResourceManager) && Instance.Equals(other.Instance);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ResourceManager.GetHashCode() ^ Instance.GetHashCode();
        }
    }
}