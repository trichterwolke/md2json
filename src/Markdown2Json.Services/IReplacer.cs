﻿namespace Markdown2Json.Services
{
    public interface IReplacer
    {
        string ReplaceHeadlines(string text);
        string RemoveGrave(string text);
    }
}