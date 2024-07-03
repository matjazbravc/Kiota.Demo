// <auto-generated/>
using Microsoft.Kiota.Abstractions.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
namespace OpenWeather.ApiClient.Demo.Models
{
    #pragma warning disable CS1591
    public class Sys : IAdditionalDataHolder, IParsable
    #pragma warning restore CS1591
    {
        /// <summary>Stores additional data not described in the OpenAPI description found when deserializing. Can be used for serialization as well.</summary>
        public IDictionary<string, object> AdditionalData { get; set; }
        /// <summary>Country code (GB, JP etc.)</summary>
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP3_1_OR_GREATER
#nullable enable
        public string? Country { get; set; }
#nullable restore
#else
        public string Country { get; set; }
#endif
        /// <summary>Internal parameter</summary>
        public int? Id { get; set; }
        /// <summary>Internal parameter</summary>
        public double? Message { get; set; }
        /// <summary>Sunrise time, unix, UTC</summary>
        public int? Sunrise { get; set; }
        /// <summary>Sunset time, unix, UTC</summary>
        public int? Sunset { get; set; }
        /// <summary>Internal parameter</summary>
        public int? Type { get; set; }
        /// <summary>
        /// Instantiates a new <see cref="OpenWeather.ApiClient.Demo.Models.Sys"/> and sets the default values.
        /// </summary>
        public Sys()
        {
            AdditionalData = new Dictionary<string, object>();
        }
        /// <summary>
        /// Creates a new instance of the appropriate class based on discriminator value
        /// </summary>
        /// <returns>A <see cref="OpenWeather.ApiClient.Demo.Models.Sys"/></returns>
        /// <param name="parseNode">The parse node to use to read the discriminator value and create the object</param>
        public static OpenWeather.ApiClient.Demo.Models.Sys CreateFromDiscriminatorValue(IParseNode parseNode)
        {
            _ = parseNode ?? throw new ArgumentNullException(nameof(parseNode));
            return new OpenWeather.ApiClient.Demo.Models.Sys();
        }
        /// <summary>
        /// The deserialization information for the current model
        /// </summary>
        /// <returns>A IDictionary&lt;string, Action&lt;IParseNode&gt;&gt;</returns>
        public virtual IDictionary<string, Action<IParseNode>> GetFieldDeserializers()
        {
            return new Dictionary<string, Action<IParseNode>>
            {
                { "country", n => { Country = n.GetStringValue(); } },
                { "id", n => { Id = n.GetIntValue(); } },
                { "message", n => { Message = n.GetDoubleValue(); } },
                { "sunrise", n => { Sunrise = n.GetIntValue(); } },
                { "sunset", n => { Sunset = n.GetIntValue(); } },
                { "type", n => { Type = n.GetIntValue(); } },
            };
        }
        /// <summary>
        /// Serializes information the current object
        /// </summary>
        /// <param name="writer">Serialization writer to use to serialize this model</param>
        public virtual void Serialize(ISerializationWriter writer)
        {
            _ = writer ?? throw new ArgumentNullException(nameof(writer));
            writer.WriteStringValue("country", Country);
            writer.WriteIntValue("id", Id);
            writer.WriteDoubleValue("message", Message);
            writer.WriteIntValue("sunrise", Sunrise);
            writer.WriteIntValue("sunset", Sunset);
            writer.WriteIntValue("type", Type);
            writer.WriteAdditionalData(AdditionalData);
        }
    }
}