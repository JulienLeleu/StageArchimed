//-----------------------------------------------------------------------
// <copyright file="ResultSetObject.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Mashup.IO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using Provider;

    /// <summary>
    /// The result set object
    /// </summary>
    [DataContract(Name = "ResultSetObject")]
    [KnownType("GetModelsTypes")]
    public class ResultSetObject
    {
        /// <summary>
        /// List of raw data
        /// </summary>
        private Dictionary<string, object> jsonDatas;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ResultSetObject()
        {
            this.jsonDatas = new Dictionary<string, object>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResultSetObject"/> class
        /// </summary>
        /// <param name="jsonDatas">The raw data</param>
        public ResultSetObject(Dictionary<string, object> jsonDatas)
        {
            this.JsonDatas = jsonDatas;
        }

        /// <summary>
        /// Gets or sets raw data
        /// </summary>
        [DataMember(Name = "jsonDatas")]
        public Dictionary<string, object> JsonDatas
        {
            get
            {
                return this.jsonDatas;
            }

            set
            {
                this.jsonDatas = value;
            }
        }

        /// <summary>
        /// Gets the known types of Dictionary Object
        /// </summary>
        /// <returns>The Known types</returns>
        public static Type[] GetModelsTypes()
        {
            ProviderManager manager = ProviderManager.GetInstance();
            return manager.GetModelsTypes().ToArray();
        }

        public void Add(Dictionary<string, object> data)
        {
            jsonDatas = jsonDatas.Concat(data).ToDictionary(e => e.Key, e => e.Value);
        }

        /// <summary>
        /// Gets data as HTML
        /// </summary>
        /// <returns>string as HTML</returns>
        public string GetHtml()
        {
            return string.Empty;
        }
          
        /// <summary>
        /// Gets data as string representation
        /// </summary>
        /// <returns>string representation</returns>
        public override string ToString()
        {
            string str = "ResulSet : [\n";
            foreach (string key in this.jsonDatas.Keys)
            {
                str += "Provider : " + key + ", data : " + this.jsonDatas[key] + ",\n";
            }

            str += "\n]";
            return str;
        }
    }
}
