﻿namespace GCop.MSharp.FixProvider.Design
{
    using Core;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CodeActions;
    using Microsoft.CodeAnalysis.CodeFixes;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;
    using System;
    using System.Collections.Immutable;
    using System.Composition;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    [ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(StringEqualsCodeFixProvider)), Shared]
    public class StringEqualsCodeFixProvider : GCopCodeFixProvider
    {
        private string Title => "Use ==";
        public override ImmutableArray<string> FixableDiagnosticIds => ImmutableArray.Create("GCop157");

        protected override void RegisterCodeFix()
        {
            try
            {
                var token = Root.FindToken(DiagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<InvocationExpressionSyntax>().FirstOrDefault();
                if (token == null) return;

                Context.RegisterCodeFix(CodeAction.Create(Title, action => UseEqualEqual(Context.Document, token, action), Title), Diagnostic);
            }
            catch (NullReferenceException)
            {
                //No matter to handle NullReferenceException
            }
        }

        private async Task<Document> UseEqualEqual(Document document, InvocationExpressionSyntax invocation, CancellationToken cancellationToken)
        {
            BinaryExpressionSyntax newBinaryExpression = null;
            try
            {
                newBinaryExpression = SyntaxFactory.BinaryExpression(
                    SyntaxKind.EqualsExpression,
                    SyntaxFactory.ParseExpression(invocation.Expression.GetIdentifier()),
                    SyntaxFactory.Token(SyntaxKind.EqualsEqualsToken),
                    SyntaxFactory.ParseExpression(invocation.ArgumentList.Arguments.ToString()));
            }
            catch
            {
                //No logging needed
            }

            var root = await document.GetSyntaxRootAsync(cancellationToken);
            var newRoot = root.ReplaceNode(invocation, newBinaryExpression);
            return document.WithSyntaxRoot(newRoot);
        }
    }
}