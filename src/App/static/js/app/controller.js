var App = App || {}; //namespace declaration.

(function () {
    //private space

    $(function () {
        setupUI();
        setupKnockout();
        loadInitialData();
    });

    /**
    * setup ui widgets
    */
    function setupUI() {


    }

    /**
    * apply knockout bindings (see knockout help for more info)
    */
    function setupKnockout() {
        App.myModel = new App.Model();
        ko.applyBindings(App.myModel);
    }

    /**
    * Load initial application data
    */
    function loadInitialData()
    {
        loadTaskLists();
    }

    function loadTaskLists(){
        //make the asynch request and fill the specified observable array
        App.loadObservableArray(
            App.getUrl(Urls.loadTaskLists),
            App.myModel.myLists,
            App.TaskList
        );

    }

   App.selectList = function(listitem){
        alert('list selected:'+listitem.id());
   };

//inmediatly execute the function
})();