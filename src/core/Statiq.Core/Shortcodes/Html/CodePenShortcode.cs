﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Statiq.Common;

namespace Statiq.Core
{
    /// <summary>
    /// Embeds a CodePen pen.
    /// </summary>
    /// <remarks>
    /// You need the path of the pen (essentially everything after the domain in the URL):
    /// <code>
    /// https://codepen.io/edanny/pen/JXwgdK
    /// </code>
    /// </remarks>
    /// <example>
    /// <code>
    /// &lt;?# CodePen edanny/pen/JXwgdK /?&gt;
    /// </code>
    /// </example>
    /// <parameter>The path of the pen.</parameter>
    public class CodePenShortcode : EmbedShortcode
    {
        public override async Task<IDocument> ExecuteAsync(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context) =>
            await ExecuteAsync("https://codepen.io/api/oembed", $"https://codepen.io/{args.SingleValue()}", new[] { "format=json" }, context);
    }
}
