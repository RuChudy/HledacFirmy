﻿using System.Text;
using System.Xml.Linq;

namespace Hledac.Domain.Rss;


/// <summary>Feed object which maps to 'channel' property on Feed.Serialize()</summary>
public class Feed
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public Uri? Link { get; set; }
    public string? Title { get; set; }
    public string? Copyright { get; set; }

    /// <summary>
    /// ISO-639 language codes.
    /// </summary>
    /// <example>en</example>
    /// <remarks>https://www.loc.gov/standards/iso639-2/php/code_list.php</remarks>
    public string Language { get; set; } = "en";

    public ICollection<Item> Items { get; set; } = new List<Item>();

    public static Feed FromXDocument(XDocument xdoc)
    {
        ArgumentNullException.ThrowIfNull(xdoc);

        XElement xroot = xdoc.Root ?? throw new ArgumentNullException(nameof(xdoc.Root));
        XNamespace ns = xroot.GetDefaultNamespace();

        XElement xchanel = xdoc.Root.Elements()
            .Where(e => e.Name.Equals(ns + "channel"))
            .SingleOrDefault() ?? throw new ArgumentNullException("rss-channel");

        // pomocne
        Uri? StringToUri(string? uriString) => string.IsNullOrWhiteSpace(uriString) ? null : new Uri(uriString, UriKind.Absolute);
        DateTime? StringToDate(string? uriString) => DateTime.TryParse(uriString, out DateTime result) ? result : null;

        List<Item> feeds = xchanel.Elements(ns + "item").Select(e => new Item
        {
            Guid = (string?)e.Element(ns + "guid"),
            Link = StringToUri((string?)e.Element(ns + "link")),
            PublishDate = StringToDate((string?)e.Element(ns + "pubDate")),

            Title = (string?)e.Element(ns + "title"),
            Body = (string?)e.Element(ns + "description"),
            Categories = e.Elements(ns + "category").Select(e => (string)e).ToList()
        })
        .ToList();

        return new Feed
        {
            Link = StringToUri((string?)xchanel.Element(ns + "link")),
            Title = (string?)xchanel.Element(ns + "title"),
            Language = ((string?)xchanel.Element(ns + "language")) ?? "en",
            Copyright = (string?)xchanel.Element(ns + "copyright"),
            Description = (string?)xchanel.Element(ns + "description"),
            Items = feeds
        };
    }


    /// <summary>Produces well-formatted rss-compatible xml string.</summary>
    public string Serialize()
    {
        var contentNamespaceUrl = "http://purl.org/rss/1.0/modules/content/";

        XNamespace nsAtom = "http://www.w3.org/2005/Atom";
        var doc = new XDocument(new XElement("rss"));
        if (doc?.Root == null)
            throw new ArgumentNullException(nameof(doc));

        doc.Root.Add(
                new XAttribute("version", "2.0"),
                new XAttribute(XNamespace.Xmlns + "atom", "http://www.w3.org/2005/Atom"));

        //namespace for Facebook's xmlns:content full article content area
        doc.Root.Add(new XAttribute(XNamespace.Xmlns + "content", contentNamespaceUrl));

        var channel = new XElement("channel");
        // ignore if Link is not specified to prevent a NullReferenceException
        if (Link != null)
            channel.Add(
                new XElement(nsAtom + "link",
                new XAttribute("rel", "self"),
                new XAttribute("type", "application/rss+xml"),
                new XAttribute("href", Link.AbsoluteUri)));

        channel.Add(new XElement("title", Title));
        if (Link != null) channel.Add(new XElement("link", Link.AbsoluteUri));
        channel.Add(new XElement("description", Description));
        // copyright is not a requirement
        if (!string.IsNullOrEmpty(Copyright)) channel.Add(new XElement("copyright", Copyright));

        channel.Add(new XElement("language", Language));

        doc.Root.Add(channel);

        foreach (var item in Items)
        {
            var itemElement = new XElement("item");

            itemElement.Add(new XElement("title", item.Title));

            if (item.Link != null) itemElement.Add(new XElement("link", item.Link.AbsoluteUri));

            itemElement.Add(new XElement("description", item.Body));

            if (item.Author != null) itemElement.Add(new XElement("author", $"{item.Author.Email} ({item.Author.Name})"));

            foreach (var c in item.Categories) itemElement.Add(new XElement("category", c));

            if (item.Comments != null) itemElement.Add(new XElement("comments", item.Comments.AbsoluteUri));

            if (!string.IsNullOrWhiteSpace(item.Permalink)) itemElement.Add(new XElement("guid", item.Permalink));

            var dateFmt = item.PublishDate?.ToString("r");
            if (item.PublishDate != DateTime.MinValue) itemElement.Add(new XElement("pubDate", dateFmt));

            if (item.Enclosures != null && item.Enclosures.Any())
            {
                foreach (var enclosure in item.Enclosures)
                {
                    var enclosureElement = new XElement("enclosure");
                    if (enclosure?.Length > 0)
                    {
                        enclosureElement.Add(new XAttribute("length", enclosure.Length));
                    }

                    if (enclosure?.Url != null)
                    {
                        enclosureElement.Add(new XAttribute("url", enclosure.Url.AbsoluteUri));
                    }

                    if (!string.IsNullOrWhiteSpace(enclosure?.MimeType))
                    {
                        enclosureElement.Add(new XAttribute("type", enclosure.MimeType.Trim()));
                    }

                    if (enclosure?.Values?.AllKeys != null)
                    {
                        foreach (var key in enclosure.Values.AllKeys)
                        {
                            if (key != null)
                            {
                                var values = enclosure?.Values?[key];
                                if (values != null)
                                {
                                    enclosureElement.Add(new XAttribute(key, values));
                                }
                            }
                        }
                    }
                    itemElement.Add(enclosureElement);
                }
            }

            if (!string.IsNullOrWhiteSpace(item.FullHtmlContent))
            {
                //add content:encoded element, CData escaped html
                var ns = XNamespace.Get(contentNamespaceUrl);
                var html = new XElement(ns + "encoded", new XCData(item.FullHtmlContent));
                itemElement.Add(html);
                html.ReplaceNodes(new XCData(item.FullHtmlContent));
            }

            channel.Add(itemElement);
        }

        return doc.ToString();
    }
}