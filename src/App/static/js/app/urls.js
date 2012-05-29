/* A central place to store the URL's of your application.
   This makes it easy to locate and change them.
   put the relative path to your actions here.

*/

var App = App || {};
var Urls = {
    loadTaskLists: "App/TaskLists",
    loadTaskDetails: "App/TaskDetails",
    saveTaskList: "App/SaveList",
    saveTaskDetails: "App/SaveTask"
};

// returns the absolute path given a relative one
App.getUrl = function (relative) {

        return App.basePath + relative;

    }

    $(function () {
        //Get the base path from the body element data attribut data-baseurl
        App.basePath = $('body').data('baseurl');

    });