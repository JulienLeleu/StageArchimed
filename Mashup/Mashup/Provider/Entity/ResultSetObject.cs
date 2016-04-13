//-----------------------------------------------------------------------
// <copyright file="Identifier.cs" company="Archimed">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Mashup.Entity
{
    public class ResultSetObject
    {
        private List<string> rawsDatas;

        public ResultSetObject(List<string> rawsDatas)
        {
            this.RawsDatas = rawsDatas;
        }

        public List<string> RawsDatas
        {
            get
            {
                return rawsDatas;
            }

            set
            {
                rawsDatas = value;
            }
        }

        public string getJSON()
        {
            JArray array = new JArray();
            foreach (string str in rawsDatas)
            {
                if (str != null && !str.Equals(""))
                {
                    try
                    {
                        array.Add(JObject.Parse(str));
                    }
                    catch (Exception e)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine(e);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            return array.ToString();
        }

        public string getHTML()
        {
            return "";
        }

        override
        public string ToString()
        {
            string str = "ResulSet : [\n";
            foreach (string data in rawsDatas)
            {
                str+= data + "\n";
            }
            str += "]";
            return str;
        }
    }
}
