using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Windows.Data;
using System.ComponentModel;

namespace MediaLibrary.Core
{
    public delegate void EditButtonHandler(object sender, EventArgs e);

    public enum ExitMode { Autosave, AskForSave, Exit }

    public enum SearchPosition { StartsWith, EndsWith, Contains, Equals }

    public class SearchHelper
    {
        public static SearchPosition FindSearchPosition(int i)
        {
            switch (i)
            {
                case 1: return SearchPosition.EndsWith;
                case 2: return SearchPosition.Contains;
                case 3: return SearchPosition.Equals;
                default: return SearchPosition.StartsWith;
            }
        }

        public static bool ContainsText(string filter, string text, SearchPosition position = SearchPosition.StartsWith)
        {
            if (String.IsNullOrEmpty(filter))
                return true;

            if (String.IsNullOrEmpty(text))
                return String.IsNullOrEmpty(filter);

            if ((position == SearchPosition.StartsWith && text.ToLowerInvariant().StartsWith(filter.ToLowerInvariant()))
                || (position == SearchPosition.EndsWith && text.ToLowerInvariant().EndsWith(filter.ToLowerInvariant()))
                || (position == SearchPosition.Contains && text.ToLowerInvariant().Contains(filter.ToLowerInvariant()))
                || (position == SearchPosition.Equals && text.ToLowerInvariant().Equals(filter.ToLowerInvariant()))
            )
                return true;

            return false;
        }

        public static bool IsNewerThen(DateTime? filter, DateTime compare)
        {
            if (filter == null)
                return true;

            return filter.Value <= compare;
        }

        public static bool TestThreeStates(bool? filter, bool value)
        {
            if (filter == null)
                return true;

            if (filter == value)
                return true;

            return false;
        }

        public static bool TestThreeStatesTrue(bool? filter, bool value)
        {
            if (filter == null)
                return true;

            if (filter.Value && !value)
                return false;

            return true;
        }

        public static bool ContainsItems(string filter, string text, char separator)
        {
            if (String.IsNullOrEmpty(filter))
                return true;

            if (String.IsNullOrEmpty(text))
                return String.IsNullOrEmpty(filter);

            string[] categories = filter.Split(separator);
            foreach (string item in categories)
            {
                if (text.StartsWith(item.Trim()))
                    return true;
            }

            return false;
        }

        public static bool ContainsItems(string filter, string[] text, char separator)
        {
            if (String.IsNullOrEmpty(filter))
                return true;

            if (text == null)
                return String.IsNullOrEmpty(filter);

            foreach (string item in text)
            {
                if (ContainsItems(filter, item, separator))
                    return true;
            }
            return false;
        }
    }

    public class CompareHelper
    {
        public static Database Compare(Database source, Database target)
        {
            Database result = new Database();
            result.Name = source.Name + " -> " + target.Name;

            foreach (Movie item in source.Movies)
            {
                Movie movie = target.Movies.FindByName(item.Name);
                if (movie == null)
                    result.Movies.Add(item);
            }

            return result;
        }

        public static void FillProperties(Database source, Database target)
        {
            foreach (Movie sourceMovie in source.Movies)
            {
                Movie targetMovie = target.Movies.FindByName(sourceMovie.Name);
                if (targetMovie != null)
                {
                    targetMovie.FillProperties(sourceMovie);

                    if (!String.IsNullOrEmpty(targetMovie.Category) && !target.Movies.Categories.Contains(targetMovie.Category))
                        target.Movies.Categories.Add(targetMovie.Category);

                    if (!String.IsNullOrEmpty(targetMovie.Country) && !target.Movies.Countries.Contains(targetMovie.Country))
                        target.Movies.Countries.Add(targetMovie.Country);

                    if (targetMovie.Actors != null)
                    {
                        foreach (string actor in targetMovie.Actors)
                        {
                            if (!target.Movies.Actors.Contains(actor))
                                target.Movies.Actors.Add(actor);
                        }
                    }
                }
            }
        }
    }

    public static class StringHelper
    {
        public static string ConcatWith(this string[] array, char separator)
        {
            if (array == null)
                return null;

            string result = "";
            foreach (string val in array)
            {
                if (result != "")
                    result += separator;
                result += val;
            }

            return result;
        }
    }

}
