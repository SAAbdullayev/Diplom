﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomLayihe.AppCode.Extensions
{
    public static partial class Extension
    {
        //public static HtmlString GetCategoriesRaw(this List<Category> categories)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("<ul class=\"widget-body filter-items search-ul\">");

        //    foreach (var category in categories.Where(c => c.ParentId == null))
        //    {
        //        AppendCategory(category, sb);

        //    }
        //    sb.Append("</ul>");

        //    return new HtmlString(sb.ToString());
        //}

        //static void AppendCategory(Category category, StringBuilder sb)
        //{
        //    bool hasChild = category.Children.Any();

        //    sb.Append($"<li {(hasChild ? "class=with-ul" : "")}>" +
        //        $"<a href=\"#\">{category.Name}");

        //    if (hasChild)
        //        sb.Append("<i class=\"fas fa-chevron-down\"></i>");

        //    sb.Append("</a>");

        //    if (hasChild)
        //    {
        //        sb.Append("<ul style=\"display: none; \">");

        //        foreach (var item in category.Children)
        //        {
        //            AppendCategory(item, sb);
        //        }


        //        sb.Append("</ul>");
        //    }

        //    sb.Append("</li>");
        //}

        //public static IEnumerable<Category> GetAllChildren(this Category category)
        //{
        //    if (category.ParentId != null)
        //        yield return category;

        //    if (category.Children != null)
        //    {
        //        foreach (var item in category.Children.SelectMany(c => c.GetAllChildren()))
        //        {
        //            yield return item;
        //        }
        //    }
        //}
    }
}
