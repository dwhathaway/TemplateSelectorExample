using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TemplateSelectorExample.Models
{
    [DataContract]
    public class LocationQueryResult
    {
        [DataMember(Name = "predictions")]
        public List<Place> Places { get; set; } = new List<Place>();
        [DataMember(Name = "status")]
        public string Status { get; set; } = string.Empty;
    }

    [DataContract]
    public class Place
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "place_id")]
        public string PlaceId { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "structured_formatting")]
        public StructuredFormatting StructuredFormatting { get; set; }

        [DataMember(Name = "terms")]
        public List<Term> Terms { get; set; }
    }

    [DataContract]
    public class StructuredFormatting
    {
        [DataMember(Name = "main_text")]
        public string MainText { get; set; }
    }

    [DataContract]
    public class Term
    {
        [DataMember(Name = "offset")]
        public int Offset { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }

    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public List<string> types { get; set; }
    }

    public class Geopoint
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Bounds
    {
        public Geopoint northeast { get; set; }
        public Geopoint southwest { get; set; }
    }

    public class Viewport
    {
        public Geopoint northeast { get; set; }
        public Geopoint southwest { get; set; }
    }

    public class Geometry
	{
        public Bounds bounds { get; set; }
        public Geopoint location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public List<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
		public List<string> types { get; set; }
		public string icon { get; set; }
		public string id { get; set; }
		public string name { get; set; }
		public OpeningHours opening_hours { get; set; }
		public List<Photo> photos { get; set; }
		public int price_level { get; set; }
		public double rating { get; set; }
		public string reference { get; set; }
		public string scope { get; set; }
		public string vicinity { get; set; }
    }

    public class GeocodeResult
    {
        public List<Result> results { get; set; }
        public string status { get; set; }
    }

	public class OpeningHours
	{
		public bool open_now { get; set; }
		public List<object> weekday_text { get; set; }
	}

	public class Photo
	{
		public int height { get; set; }
		public List<string> html_attributions { get; set; }
		public string photo_reference { get; set; }
		public int width { get; set; }
	}

	public class SearchResults
	{
		public List<object> html_attributions { get; set; }
		public string next_page_token { get; set; }
		public List<Result> results { get; set; }
		public string status { get; set; }
	}
}
