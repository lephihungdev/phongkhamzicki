namespace VIT.Pre.HealthCare
{
    using System;
    using System.Web.Mvc;

    /// <summary>
    /// The auto complete attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoCompleteAttribute : Attribute, IMetadataAware
    {
        #region Fields

        /// <summary>
        /// The _action.
        /// </summary>
        private readonly string actionList;

        /// <summary>
        /// The _action.
        /// </summary>
        private readonly string actionGetTextByValue;

        /// <summary>
        /// The _controller.
        /// </summary>
        private readonly string controller;

        /// <summary>
        /// Gets or sets the area.
        /// </summary>
        private readonly string area;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteAttribute"/> class.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="actionList">
        /// The actionList.
        /// </param>
        public AutoCompleteAttribute(string controller, string actionList)
        {
            this.controller = controller;
            this.actionList = actionList;
            this.area = string.Empty;
            this.actionGetTextByValue = string.Empty;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteAttribute"/> class.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="actionList">
        /// The actionList.
        /// </param>
        /// <param name="actionGetTextByValue">
        /// The action Get Text By Value.
        /// </param>
        public AutoCompleteAttribute(string controller, string actionList, string actionGetTextByValue)
        {
            this.controller = controller;
            this.actionList = actionList;
            this.area = string.Empty;
            this.actionGetTextByValue = actionGetTextByValue;
        }



        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteAttribute"/> class.
        /// </summary>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="actionList">
        /// The actionList.
        /// </param>
        /// <param name="area">
        /// The area.
        /// </param>
        /// <param name="actionGetTextByValue">
        /// The action Get Text By Value.
        /// </param>
        public AutoCompleteAttribute(string controller, string actionList, string area, string actionGetTextByValue)
        {
            this.controller = controller;
            this.area = area;
            this.actionList = actionList;
            this.actionGetTextByValue = actionGetTextByValue;
        }
        
        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The on metadata created.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetAutoComplete(this.controller, this.actionList, this.actionGetTextByValue, this.area);
        }

        #endregion
    }
}