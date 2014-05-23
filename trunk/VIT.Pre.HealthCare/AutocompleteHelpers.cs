namespace VIT.Pre.HealthCare
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// The autocomplete helpers.
    /// </summary>
    public static class AutocompleteHelpers
    {
        #region Constants

        /// <summary>
        /// The auto complete action key.
        /// </summary>
        private const string AutoCompleteActionListKey = "AutoCompleteActionList";


        /// <summary>
        /// The auto complete action key.
        /// </summary>
        private const string AutoCompleteActionGetTextByValueKey = "AutoCompleteActionGetTextBuValue";

        /// <summary>
        /// The auto complete controller key.
        /// </summary>
        private const string AutoCompleteControllerKey = "AutoCompleteController";


        /// <summary>
        /// The auto complete action key.
        /// </summary>
        private const string AutoCompleteAreaKey = "AutoCompleteAreaKey";

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The get auto complete url.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetAutoCompleteActionListUrl(this HtmlHelper html, ModelMetadata metadata)
        {
            string controller = metadata.AdditionalValues.GetString(AutoCompleteControllerKey);
            string action = metadata.AdditionalValues.GetString(AutoCompleteActionListKey);
            string area = metadata.AdditionalValues.GetString(AutoCompleteAreaKey);
            var route = new RouteValueDictionary { { "area", area } };

            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                return null;
            }

            return UrlHelper.GenerateUrl(
                null, 
                action,
                controller,
                route,
                html.RouteCollection,
                html.ViewContext.RequestContext, 
                true);
        }

        /// <summary>
        /// The get auto complete url.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetAutoCompleteActionGetTextByValueUrl(this HtmlHelper html, ModelMetadata metadata)
        {
            string controller = metadata.AdditionalValues.GetString(AutoCompleteControllerKey);
            string action = metadata.AdditionalValues.GetString(AutoCompleteActionGetTextByValueKey);
            string area = metadata.AdditionalValues.GetString(AutoCompleteAreaKey);
            var route = new RouteValueDictionary { { "area", area } };
            if (string.IsNullOrEmpty(controller) || string.IsNullOrEmpty(action))
            {
                return null;
            }

            return UrlHelper.GenerateUrl(
                null,
                action,
                controller,
                route,
                html.RouteCollection,
                html.ViewContext.RequestContext,
                true);
        }

        /// <summary>
        /// The set auto complete.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="actionList">
        /// The action list.
        /// </param>
        /// <param name="actionGetTextByValue">
        /// The action get text by value.
        /// </param>
        /// <param name="area">
        /// The area.
        /// </param>
        public static void SetAutoComplete(this ModelMetadata metadata, string controller, string actionList, string actionGetTextByValue, string area)
        {
            metadata.AdditionalValues[AutoCompleteControllerKey] = controller;
            metadata.AdditionalValues[AutoCompleteActionListKey] = actionList;
            metadata.AdditionalValues[AutoCompleteActionGetTextByValueKey] = actionGetTextByValue;
            metadata.AdditionalValues[AutoCompleteAreaKey] = area;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The get string.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetString(this IDictionary<string, object> dictionary, string key)
        {
            object value;
            dictionary.TryGetValue(key, out value);
            return (string)value;
        }

        #endregion
    }
}