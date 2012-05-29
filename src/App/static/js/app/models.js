/* Put your models here */
var App = App || {};

/**
 * Task item. represents and item in the TODO list
 */
App.Task = function (defaults) {

    var self = this; //using self avoids scope problems with this keyworkd
    self.id = ko.observable(defaults.id || App.emptyGuid);
    self.name = ko.observable(defaults.name || '');
    self.validate = function () {
        return self.name() != null && self.name().length>0;
    }
}

/**
* Task list.  a collection of items
*/
App.TaskList = function (defaults) {
    var self = this;
    self.id = ko.observable(defaults.id || App.emptyGuid);
    self.name = ko.observable(defaults.name || '');
    self.taskItems = ko.observableArray();
}

/**
 * The main application model
 */
App.Model = function () {
    var self = this;
    self.myLists = ko.observableArray();
    self.selectedList = ko.observable();
    self.selectedTask = ko.observable();
    //parameters for list creation
    self.taskListToAdd = ko.observable();
    //parameters for task creation
    self.taskToAdd = ko.observable();
}