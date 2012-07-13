namespace Cavity.Data
{
    using System;

    public sealed class JsonSerializationSettings
    {
        private string _itemsName;

        public JsonSerializationSettings()
        {
            ItemsName = string.Empty;
        }

        public string ItemsName
        {
            get
            {
                return _itemsName;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                _itemsName = value;
            }
        }
    }
}