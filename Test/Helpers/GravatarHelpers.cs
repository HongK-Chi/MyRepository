using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Test.Helpers
{
    public static class GravatarHelpers
    {
        public static string Gravatar(this HtmlHelper html,
                                      string email,
                                      int? size = null,
                                      GravatarRating rating = GravatarRating.Default,
                                      GravatarDefaultImage defaultImage =
                                      GravatarDefaultImage.MysteryMan,
                                      object htmlAttributes = null)
        {
            string url = GravatarUrl(email, size, rating, defaultImage);

            var tag = new TagBuilder("img");
            tag.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            tag.Attributes.Add("src", url.ToString());

            if (size != null)
            {
                tag.Attributes.Add("width", size.ToString());
                tag.Attributes.Add("height", size.ToString());
            }

            return tag.ToString();
        }

        public static string GravatarUrl(string email, int? size = null,
                                         GravatarRating rating = GravatarRating.Default,
                                         GravatarDefaultImage defaultImage =
                                         GravatarDefaultImage.MysteryMan)
        {
            var url = new StringBuilder("//www.gravatar.com/avatar/", 90);
            url.Append(GetEmailHash(email));

            var isFirst = true;
            Action<string, string> addParam = (p, v) =>
            {
                url.Append(isFirst ? '?' : '&');
                isFirst = false;
                url.Append(p);
                url.Append('=');
                url.Append(v);
            };

            if (size != null)
            {
                if (size < 1 || size > 512)
                    throw new ArgumentOutOfRangeException("size", size,
                       "Must be null or between 1 and 512, inclusive.");
                addParam("s", size.Value.ToString());
            }

            if (rating != GravatarRating.Default)
                addParam("r", rating.ToString().ToLower());

            if (defaultImage != GravatarDefaultImage.Default)
            {
                if (defaultImage == GravatarDefaultImage.Http404)
                    addParam("d", "404");
                else if (defaultImage == GravatarDefaultImage.Identicon)
                    addParam("d", "identicon");
                if (defaultImage == GravatarDefaultImage.MonsterId)
                    addParam("d", "monsterid");
                if (defaultImage == GravatarDefaultImage.MysteryMan)
                    addParam("d", "mm");
                if (defaultImage == GravatarDefaultImage.Wavatar)
                    addParam("d", "wavatar");
            }

            return url.ToString();
        }

        private static string GetEmailHash(string email)
        {
            if (email == null)
                return new string('0', 32);

            email = email.Trim().ToLower();

            var emailBytes = Encoding.ASCII.GetBytes(email);
            var hashBytes = new MD5CryptoServiceProvider().ComputeHash(emailBytes);

            var hash = new StringBuilder();
            foreach (var b in hashBytes)
                hash.Append(b.ToString("x2"));
            return hash.ToString();
        }
    }

    public enum GravatarRating
    {
        Default,
        G,
        Pg,
        R,
        X
    }

    public enum GravatarDefaultImage
    {
        Default,
        Http404,
        MysteryMan,
        Identicon,
        MonsterId,
        Wavatar
    }
}
