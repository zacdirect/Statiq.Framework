﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Statiq.CodeAnalysis.Scripting;
using Statiq.Common;

namespace Statiq.CodeAnalysis
{
    /// <summary>
    /// Evaluates C# code.
    /// </summary>
    /// <remarks>The current context and document are in-scope as <c>Context</c> and <c>Document</c> respectively and can be used from within the script.</remarks>
    /// <example>
    /// <code>
    /// &lt;?# Eval ?>&lt;?# return 1 + 2; ?>&lt;?#/ Eval ?>
    /// </code>
    /// </example>
    public class EvalShortcode : Shortcode
    {
        public override async Task<IDocument> ExecuteAsync(KeyValuePair<string, string>[] args, string content, IDocument document, IExecutionContext context)
        {
            byte[] assembly = ScriptHelper.Compile(content, document, context);
            object value = await ScriptHelper.EvaluateAsync(assembly, document, context);
            return context.CreateDocument(await context.GetContentProviderAsync(value.ToString()));
        }
    }
}
