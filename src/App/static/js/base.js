/** Common rules **/
$(function(){
   setupConsoleAndLogging();
   setupTables();
});

function setupTables()
{
    $("table.dataGrid tr").live("click", function (e) {
        $(this).siblings(".selected").removeClass("selected");
        $(this).addClass("selected");
        return true;
    });

   //Configurar alto de grids fijo (plugin table scroll
   //size1: grids principales
//   $('table.size1').tableScroll({ height: 500 });
//   //size2:grids secundarios
//   $('table.size2').tableScroll({ height: 300 }); 
    
}

/*Make console.log universal*/
function setupConsoleAndLogging()
{

// usage: log('inside coolFunc', this, arguments);
// paulirish.com/2009/log-a-lightweight-wrapper-for-consolelog/
    window.log = function () {
        log.history = log.history || [];   // store logs to an array for reference
        log.history.push(arguments);
        arguments.callee = arguments.callee.caller;
        if (this.console) console.log(Array.prototype.slice.call(arguments));
    };
// make it safe to use console.log always
    (function (b) { function c() { } for (var d = "assert,count,debug,dir,dirxml,error,exception,group,groupCollapsed,groupEnd,info,log,markTimeline,profile,profileEnd,time,timeEnd,trace,warn".split(","), a; a = d.pop(); ) b[a] = b[a] || c })(window.console = window.console || {});

}