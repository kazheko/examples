using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Examples.AuctionApi.MediaTypeFormatters.SirenMediaTypeFormatter.Maps
{
    internal class Entity
    {
        private readonly string _class;
        private readonly List<JObject> _entities;
        private readonly List<JObject> _properties;
        private readonly List<JObject> _actions;
        private readonly List<JObject> _links;

        public Entity(string entity, string id)
        {
            _class = entity;
            _properties = new List<JObject>();
            _entities = new List<JObject>();
            _actions = new List<JObject>();
            _links = new List<JObject>
            {
                new JObject(new JObject{ { "rel", new JArray {"self"}}, {"href", Links.Api.Build(entity, id)}})
            };
        }

        public JObject Build()
        {
            return new JObject
            {
                {"class", _class},
                new JProperty("properties", new JArray(_properties)),
                new JProperty("entities", new JArray(_entities)),
                new JProperty("actions", new JArray(_actions)),
                new JProperty("links", new JArray(_links))
            };
        }

        public Entity EmbeddedRepresentation(string entity, string id, IDictionary<string, string> properties)
        {
            _entities.Add(new JObject
            {
                {"class", new JValue(entity)},
                {"rel", Links.Doc.Build(entity)},
                new JProperty("properties", new JArray(properties.Select(x => new JObject(new JProperty(x.Key, x.Value))))),
                new JProperty("links", new JArray(new JObject{ { "rel", new JArray {"self"}}, {"href", Links.Api.Build(entity, id)}}))
            });
            return this;
        }

        public Entity EmbeddedLink(string entity, string id)
        {
            _entities.Add(new JObject
            {
                {"class", new JValue(entity)},
                {"rel", Links.Doc.Build(entity)},
                {"href", Links.Api.Build(entity, id)}
            });

            return this;
        }

        public Entity Properties(string field, string value)
        {
            _properties.Add(new JObject { {field, new JValue(value)}});
            return this;
        }

        public Entity Action(string entity, string name, string method, IDictionary<string,string> fields)
        {
            _actions.Add(new JObject
            {
                {"name", new JValue(name)},
                {"method", new JValue(method)},
                {"href", new JValue(Links.Api.Build(entity,null))},
                {"type", "application/json"},
                new JProperty("fields", new JArray(fields.Select(x=>
                {
                    var item = new JObject {{"name", x.Key}};
                    if (x.Value != null)
                    {
                        item.Add("value",x.Value);
                    }
                    return item;
                }))),
            });
            
            return this;
        }
    }

    internal static class Links
    {
        public static class Doc
        {
            public static string Build(string entity)
            {
                return $"http://localhost:7000/home/{entity}";
            }
        }

        public static class Api
        {
            public static string Build(string entity, string id)
            {
                id = id ?? string.Empty;
                return $"http://localhost:7000/{entity}/{id}";
            }
        }

    }
}
