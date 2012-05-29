
var App = App || {};
// #constantes
App.defaultPageSize = 20;
App.emptyGuid = "00000000-0000-0000-0000-000000000000";
/*String utilities*/
String.prototype.startsWith = function (str){
    return this.slice(0, str.length) == str;
};

function AssertException(message) { this.message = message; }
AssertException.prototype.toString = function () {
  return 'AssertException: ' + this.message;
}

function assert(exp, message) {
  if (!exp) {
    throw new AssertException(message);
  }
}

/*Editable entities can save and restore state
* Use this as base class (using inheritsFrom)*/
App.Editable = function()
{
    //the saved values updated the lasttime saveState was called
    this.oldValues = {};
    //name of the fields that were saved
    this.savedFields = [];
    //Flag to indicate if there is a saving process running (useful to display progress dialogs)
    this.saving = ko.observable(false);
    //true if entity is in edit mode
    this.editing = ko.observable(false);
}
App.Editable.prototype = {
    /*Save the entity selected fields in a temporal map, that will be restored
    when the method restore is called
    @param fields Array of property names to save and restore

     */
    saveState:function(fields)
    {
        this.savedFields = fields;
        fields = fields || {};
        for(var i=0;i<fields.length;i++)
        {
            var field =fields[i];
            if(field!= null)
            {
                var value = ko.utils.unwrapObservable(this[field]);
                this.oldValues[field] = value;
            }
        }
    },
    /*Restore the properties saved*/
    restore:function()
    {
        for(var i=0;i<this.savedFields.length;i++)
        {
            var field = this.savedFields[i];
            if(field!=null)
            {
                if(ko.isObservable(this[field]))
                {
                    this[field](this.oldValues[field]);
                }
                else
                {
                    this[field] = this.oldValues[field];
                }
            }
        }
    }
}
App.utilities = {
   //Returns a copy of the object converting observables to plain properties and removing functions
    //This is useful before sending objetcs to ajax, as jquery ajax tries to execute the object functions.
   flat:function(obj)
   {
       var copy = jQuery.extend(true,{},ko.mapping.toJS(obj));
        for(var v in copy)
        {
            if(copy[v] instanceof Function && v!="constructor")
            {
                delete copy[v];
            }
            else if(copy[v] instanceof Date)
            {
                //si es fecha no hacer nada
            }
            //Recursively flat child objects (except arrays)
            else if (copy[v] instanceof Object && typeof copy[v].length != "number")
            {
                //Recursively flat child properties
                copy[v] = App.utilities.flat(copy[v]);
            }
        }
       return copy;
   },
    emptyGuid:"00000000-0000-0000-0000-000000000000"
};

/*ASP.NET MVC date serialization fix:
  ASPNET sends dates as /Date(1224043200000)/,
  modify all ajax calls to take this into account
 */

(function () {
    var DATE_START = "/Date(";
    var DATE_START_LENGTH = DATE_START.length;

    function isDateString(x) {
        return typeof x === "string" && x.startsWith(DATE_START);
    }

    function deserializeDateString(dateString) {
        var date = new Date(parseInt(dateString.substr(DATE_START_LENGTH)));
        return date;
    }

    function convertJSONDates(key, value) {
      if (isDateString(value)) {
        return deserializeDateString(value);
      }
      return value;
    }

    window.jQuery.ajaxSetup({
      converters: {
        "text json": function(data) {
          return window.JSON.parse(data, convertJSONDates);
        }
      }
    });
}());



/*Utility to allow inheritance in javascript*/
Function.prototype.inheritsFrom = function (parentClassOrObject) {
    if (parentClassOrObject.constructor == Function) {
        //Normal Inheritance 
        this.prototype = new parentClassOrObject;
        this.prototype.constructor = this;
        this.prototype.parent = parentClassOrObject.prototype;
    }
    else {
        //Pure Virtual Inheritance 
        this.prototype = parentClassOrObject;
        this.prototype.constructor = this;
        this.prototype.parent = parentClassOrObject;
    }
    return this;
} 

// Read a page's GET URL variables and return them as an associative array.
//Author: http://snipplr.com/users/Roshambo/
function getUrlVars()
{
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for(var i = 0; i < hashes.length; i++)
    {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

 App.showDeckCard = function(selector)
 {
    var elem =  $(selector);
    elem.siblings().hide();
    elem.show();

 }

 /*
   Clase utilitaria usada cuando se quieran mostrar
   items con una etiqueta y un id
 */
 App.simpleItem = function(id,label)
 {
    this.id = id;
    this.label = label;
 }

 App.Error = function(msg)
 {
    this.message = msg;
}


/**
 * utility that makes a request (JSON) and fills and observable array.
 * It's expected that the server returns a OperationResult json object
 * @param url the url of the endpoint
 * @array the ko.observableArray instance
 * @type of the items in the array (the function must have a constructor that accepts defaults)
 */
App.loadObservableArray = function (url, array, type) {
    if (!array || typeof (array.removeAll) != 'function') {
        throw new Error('App.loadObservableArray: array not defined or not ko.observableArray');
    }
    if (!type) {
        throw new Error('App.loadObservableArray: type not defined');
    }

    $.getJSON(App.getUrl(Urls.loadTaskLists),
            function (result) {
                //result is a serialized OperationResult server objectt
                array.removeAll();
                for (var i = 0; i < result.data.length; i++) {
                    var item = new type(result.data[i]);
                    array.push(item);
                }
            });
        }


