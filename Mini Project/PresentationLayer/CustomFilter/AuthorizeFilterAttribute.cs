﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PresentationLayer.CustomFilter
{
    public class AuthorizeFilterAttribute:AuthorizeAttribute
    {
        public AuthorizeFilterAttribute()
        {
            View = "AuthorizationFailed";
        }

        public string View { get; set; }

        /// <summary>
        /// Check for Authorization
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            IsUserAuthorized(filterContext);
        }

        /// <summary>
        /// Method to check if the user is Authorized or not
        /// if yes continue to perform the action else redirect to error page
        /// </summary>
        /// <param name="filterContext"></param>
        private void IsUserAuthorized(AuthorizationContext filterContext)
        {
            // If the Result returns null then the user is Authorized 
            if (filterContext.Result == null)
                return;

            //If the user is Un-Authorized then Navigate to Auth Failed View 
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                // var result = new ViewResult { ViewName = View };
                var vr = new ViewResult();
                vr.ViewName = View;

                ViewDataDictionary dict = new ViewDataDictionary();
                dict.Add("Message", "Sorry you are not Authorized to Perform this Action");

                vr.ViewData = dict;

                var result = vr;

                filterContext.Result = result;
            }
            else
            {
                var viewForNotLoggedIn = new ViewResult();
                viewForNotLoggedIn.ViewName = "LoginToContinue";

                ViewDataDictionary dict2 = new ViewDataDictionary();
                dict2.Add("Message", "Please Login to continue");

                viewForNotLoggedIn.ViewData = dict2;

                var result2 = viewForNotLoggedIn;

                filterContext.Result = result2;
            }
        }



    }
}