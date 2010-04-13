using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Divan;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LoveSeat
{
    public class CouchFunctionDefinition : ICanJson
    {
        public string Function { get; set; }
        public string Name { get; set; }

        public void ReadJson(Newtonsoft.Json.Linq.JObject obj)
        {
            throw new NotImplementedException();
        }

        public void WriteJson(Newtonsoft.Json.JsonWriter writer)
        {
            writer.WritePropertyName(Name);
            writer.WriteValue(Function);
        }
    }


    public class GenericDesignDocument: CouchDesignDocument
    {
        public IList<CouchFunctionDefinition> Shows { get; set; }
        public IList<CouchFunctionDefinition> Lists { get; set; }

        public GenericDesignDocument()
        {
            Shows = new List<CouchFunctionDefinition>();
            Lists = new List<CouchFunctionDefinition>();
        }

        public GenericDesignDocument(string name, ICouchDatabase db) : base(name, db)
        {
            Shows = new List<CouchFunctionDefinition>();
            Lists = new List<CouchFunctionDefinition>();
        }

        public override void ReadJson(JObject obj)
        {
            base.ReadJson(obj);

            if (obj["shows"] as JObject != null)
                foreach (var prop in ((JObject)obj["shows"]).Properties())
                    Shows.Add(new CouchFunctionDefinition { Name = prop.Name, Function = prop.Value<string>() });

            if (obj["lists"] as JObject != null)
                foreach (var prop in ((JObject)obj["lists"]).Properties())
                    Shows.Add(new CouchFunctionDefinition { Name = prop.Name, Function = prop.Value<string>() });
        }

        private void writeList(IList<CouchFunctionDefinition> list, string name, JsonWriter writer)
        {
            if (list == null || list.Count == 0)
                return;

            writer.WritePropertyName(name);
            writer.WriteStartObject();
            foreach (var f in list)
            {
                writer.WritePropertyName(f.Name);
                writer.WriteValue(f.Function);
            }
            writer.WriteEndObject();
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer)
        {
            base.WriteJson(writer);

            writeList(Shows, "shows", writer);
            writeList(Lists, "lists", writer);
        }
    }
}
