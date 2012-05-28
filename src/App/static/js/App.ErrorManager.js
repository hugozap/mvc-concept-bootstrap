/*
Obassi 2011
Hugo Zapata
General puropose error presenter
*/
var App = App || {};
(function () {
    
    App.ErrorManager =
    {
        showError: function (message) {
            var msg = [];
            msg[msg.length] = '<div class="alert-message block-message error">'
            msg[msg.length] = message || "Operación exitosa";
            msg[msg.length] = "</div>";

             $.sticky(msg.join(""),
                    {
                 
                        "autoclose":4000
                    }
             );
        }
    }

    App.showError = App.ErrorManager.showError;
    App.notifySuccess = function(message)
    {
        var msg = [];
        msg[msg.length] = '<div class="alert-message block-message success">'
        msg[msg.length] = message || "Operación exitosa";
        msg[msg.length] = "</div>";

         $.sticky(msg.join(""),
                {
                 
                    "autoclose":2000
                }
            );
    }
})();