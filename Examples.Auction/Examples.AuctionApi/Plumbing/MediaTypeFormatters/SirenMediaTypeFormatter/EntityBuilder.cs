using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Examples.AuctionApi.Plumbing.MediaTypeFormatters.SirenMediaTypeFormatter
{
    internal class EntityBuilder
    {
        private readonly string _class;
        private readonly ICollection<JObject> _entities;
        private readonly JObject _properties;
        private readonly ICollection<JObject> _actions;
        private readonly ICollection<JObject> _links;

        public EntityBuilder(string entity, string id = null)
        {
            _class = entity;
            _properties = new JObject();
            _entities = new Collection<JObject>();
            _actions = new Collection<JObject>();
            _links = new Collection<JObject>
            {
                new JObject(new JObject{ { "rel", new JArray {"self"}}, {"href", Links.Api.Build(entity, id)}})
            };
        }

        public JObject Build()
        {
            return new JObject
            {
                new JProperty("class", new JArray(_class)),
                new JProperty("properties", new JObject(_properties)),
                new JProperty("entities", new JArray(_entities)),
                new JProperty("actions", new JArray(_actions)),
                new JProperty("links", new JArray(_links))
            };
        }

        public EntityBuilder EmbeddedRepresentation(string entity, string id, IDictionary<string, string> properties)
        {
            var obj = new JObject
            {
                new JProperty("class", new JArray(entity)),
                new JProperty("rel", new JArray(Links.Doc.Build(entity))),
                new JProperty("properties", new JObject(properties.Select(x => new JProperty(x.Key, x.Value))))
            };

            if(!string.IsNullOrEmpty(id))
                obj.Add(new JProperty("links", new JArray(new JObject { { "rel", new JArray { "self" } }, { "href", Links.Api.Build(entity, id) } })));

            _entities.Add(obj);

            return this;
        }

        public EntityBuilder EmbeddedLink(string entity, string id)
        {
            _entities.Add(new JObject
            {
                new JProperty("class", new JArray(entity)),
                new JProperty("rel", new JArray(Links.Doc.Build(entity))),
                {"href", Links.Api.Build(entity, id)}
            });

            return this;
        }

        public EntityBuilder Properties(string field, string value)
        {
            _properties.Add(new JProperty(field, value));
            return this;
        }

        public EntityBuilder Action(string entity, string name, string method, IDictionary<string,string> fields)
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
