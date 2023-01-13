namespace JSR.Utilities
{
    /// <summary>
    /// Options for using <see cref="PropertyUtilities.GetProperties"/>.
    /// </summary>
    public struct GetPropertiesOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetPropertiesOptions"/> struct.
        /// </summary>
        public GetPropertiesOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPropertiesOptions"/> struct.
        /// </summary>
        /// <param name="defaultValue">Specify whether or not to get all properties by default.</param>
        public GetPropertiesOptions(bool defaultValue) : this(defaultValue, defaultValue, defaultValue, defaultValue, defaultValue, defaultValue, defaultValue)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GetPropertiesOptions"/> struct.
        /// </summary>
        /// <param name="readWriteProperties">Get readwrite properties.</param>
        /// <param name="readOnlyProperties">Get readonly properties.</param>
        /// <param name="writeOnlyProperties">Get writeonly properties.</param>
        /// <param name="valueProperties">Get value type properties.</param>
        /// <param name="classProperties">Get class type properties.</param>
        /// <param name="interfaceProperties">Get interface type properties.</param>
        /// <param name="listProperties">Get list type properties.</param>
        public GetPropertiesOptions(bool readWriteProperties, bool readOnlyProperties, bool writeOnlyProperties, bool valueProperties, bool classProperties, bool interfaceProperties, bool listProperties)
        {
            ReadWriteProperties = readWriteProperties;
            ReadOnlyProperties = readOnlyProperties;
            WriteOnlyProperties = writeOnlyProperties;
            ValueProperties = valueProperties;
            ClassProperties = classProperties;
            InterfaceProperties = interfaceProperties;
            ListProperties = listProperties;
        }

        /// <summary>
        /// Gets or sets a value indicating whether to get readwrite properties.
        /// </summary>
        public bool ReadWriteProperties { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to get readonly properties.
        /// </summary>
        public bool ReadOnlyProperties { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to get writeonly properties.
        /// </summary>
        public bool WriteOnlyProperties { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to get value type properties.
        /// </summary>
        public bool ValueProperties { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to get class type properties.
        /// </summary>
        public bool ClassProperties { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to get interface type properties.
        /// </summary>
        public bool InterfaceProperties { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether to get list type properties.
        /// </summary>
        public bool ListProperties { get; set; } = false;
    }
}
