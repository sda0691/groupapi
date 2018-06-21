//var app = angular.module("myApp", ['angular.filter']);


var app = angular.module("myApp", ["ngRoute","ngMask", 'angular.filter'])

.filter('sumOfValue', function () {
    return function (groups) {

        var taxTotals = 0;
        var isCancelOrder = false;
        for (i = 0; i < groups.length; i++) {
            if (groups[i].OrderStatus == 3)
                isCancelOrder = true;
            taxTotals = taxTotals + (parseFloat(groups[i].Price, 10) + parseFloat(groups[i].Tax, 10) + parseFloat(groups[i].ShippingHandlingFee, 10));
            //groupedOrder.Price + groupedOrder.Tax + groupedOrder.ShippingHandlingFee
        };
        if (isCancelOrder)
            return 0;
        else
            return parseFloat(taxTotals, 10);
    };
})
//.filter('ShowCancelBtnCall', function () {
//    return function (groups) {
//        var keepgoing = true;
//        debugger;
//        for (i = 0; i < groups.length; i++) {
//            debugger;
//            if (keepgoing) {
//                if  (groups[i].OrderStatus == 3 ||
//                    //(groups[i].TrackingStatus == 2 || groups[i].TrackingStatus == 3 || groups[i].TrackingStatus == 4 || groups[i].TrackingStatus == 0) ||
//                    (groups[i].Courier != 0))
//                {
//                    keepgoing = false;
//                    return false;
//                }
//                //groupedOrder.Price + groupedOrder.Tax + groupedOrder.ShippingHandlingFee
//            };
//        };
//        //return parseFloat(taxTotals,10);
//        return true;
//    };
//})
//.filter('ShowCancelBtn', function () {
//    return function (groups) {
//        var keepgoing = true;

//        for (i = 0; i < groups.length; i++) {
//            if (keepgoing) {
//                if  (groups[i].OrderStatus == 3 ||
//                    (groups[i].TrackingStatus == 2 || groups[i].TrackingStatus == 3 || groups[i].TrackingStatus == 4 || groups[i].TrackingStatus == 0) ||
//                    (groups[i].Courier != 0))
//                {
//                    keepgoing = false;
//                    return false;
//                }
//                //groupedOrder.Price + groupedOrder.Tax + groupedOrder.ShippingHandlingFee
//            };
//        };
//        //return parseFloat(taxTotals,10);
//        return true;
//    };
//})
//.filter('ShowRefundBtn', function () {
//    return function (groups) {
//        for (i = 0; i < groups.length; i++) {
//            var gTotal = groups[i].Price + groups[i].Tax + groups[i].ShippingHandlingFee;
//            if (groups[i].BillStatus == 3 || groups[i].BillStatus == 4  || groups[i].PaymentType != 1 || gTotal <= 0)
//                return false;
//            //groupedOrder.Price + groupedOrder.Tax + groupedOrder.ShippingHandlingFee
//        };
//        //return parseFloat(taxTotals,10);
//        return true;
//    };
//})
//.filter('ShowRefundBtnCall', function () {
//    return function (groups) {
//        for (i = 0; i < groups.length; i++) {
//            var gTotal = groups[i].Price + groups[i].Tax + groups[i].ShippingHandlingFee;
//            if (groups[i].TransactionType == 2 || groups[i].TransactionType == 4 || groups[i].PaymentType != 1 || gTotal <= 0)
//                return false;
//            //groupedOrder.Price + groupedOrder.Tax + groupedOrder.ShippingHandlingFee
//        };
//        //return parseFloat(taxTotals,10);
//        return true;
//    };
//})


.filter('showPrice', function () {
    return function (sku, groups, price) {
        var keepgoing = true;
        for (i = 0; i < groups.length; i++) {
            if (keepgoing) {
                
                if (groups[i].SKU == sku){
                    keepgoing = false;
                    return 'free';
                }
            }
        };
        return price.toFixed(2);
    };
})
//.filter('showQuantity', function () {
//    return function (inventoryStatus, quantity) {
//        debugger;
//        var keepgoing = true;
//        if inventoryStatus = 1 return quantity
//        else if inventoryStatus
//        for (i = 0; i < groups.length; i++) {
//            if (keepgoing) {

//                if (groups[i].SKU == sku) {
//                    keepgoing = false;
//                    return 'free';
//                }
//            }
//        };
//        return price.toFixed(2);
//    };
//})

app.service('ProvinceService', function () {
    this.context = {
        province: "ON",
        postalcode: "",
        // need when you place entitlement order
        entitlement: []
    };
});

app.factory("ShareData", function () {
    return {
        cmisid: 0,
        customerprovince: "ON",
        csid: "0",
        MainAddress: {
            CMIS_CustomerID: 0, CS_CustomerID: "0",
            HouseNum: "", StreetName: "", StreetSuffix: "", PostDirection: "",
            Unit: "", City: "", Province: "", PostalCode: "",
            CS_FirstName: "", CS_LastName: "", CS_Phone: ""
        },
        TimeStamp: "0",
        EmailSent: 0,
        DataResult: [],
        UserName: ""
    }
});


app.directive('convertToNumber', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            ngModel.$parsers.push(function (val) {
                //saves integer to model null as null
                return val == null ? null : parseInt(val, 10);
            });
            ngModel.$formatters.push(function (val) {
                //return string for formatter and null as null
                return val == null ? null : '' + val;
            });
        }
    };
});

app.directive("ngConfirmClick", [
  function () {
      return {
          priority: -1,
          restrict: "A",
          link: function (scope, element, attrs) {
              element.bind("click", function (e) {
                  var message;
                  message = attrs.ngConfirmClick;
                  if (message && !confirm(message)) {
                      e.stopImmediatePropagation();
                      e.preventDefault();
                  }
              });
          }
      };
  }
]);
app.config(['$routeProvider', function ($routeProvider, $locationProvider) {
    //alert("ttt");
    //debugger;
    $routeProvider
    .when("/", {
        templateUrl: "/ypcsr/customer/customersearch",
        controller: "SearchController"
    })
    .when("/customersearch", {
        templateUrl: "/ypcsr/customer/customersearch",
        controller: "SearchController"
    })
    .when("/customersearchnew", {
    templateUrl: "/ypcsr/customer/customersearchnew",
    controller: "SearchController"
    })
    .when("/customerdetail", {
        templateUrl: "/ypcsr/customer/customerdetail"
    })
    .when("/customerdetailnew/:idProfile", {
        templateUrl: "/ypcsr/customer/customerdetailnew"
    })
    .when("/customerdetail/:idProfile", {
        templateUrl: "/ypcsr/customer/customerdetail"
    })
    .when("/resultdetail", {
        templateUrl: "/ypcsr/customer/resultdetail"
    })
    .when("/login", {
        templateUrl: "/ypcsr/customer/login"
    })
}]);

app.config(function ($provide) {

    $provide.decorator("$exceptionHandler", ['$delegate', '$injector', 'LoggerService', function ($delegate, $injector, LoggerService) {
        return function (exception, cause) {
            var cusmessage = "Error happened, please contact CMIS help for detail.";
            debugger;
            //log error at server side
            LoggerService.Enabled = false;
            if( LoggerService.Enabled )
                LoggerService.InsertLog(exception,cause);
            else
                cusmessage = exception.message + "<br>" + exception.stack;

            //display error message at page
            myApp.ErrorNotification(cusmessage);
            $('.modal-backdrop').remove();  //fix
            myApp.hidePleaseWait();


            $delegate(exception, cause); //will comment it later
        };
    }]);
});


//This function is called every time a new page is called 
//and it updates the page title.
app.run(['$location', '$rootScope', 'AuthService', function ($location, $rootScope, AuthService) {

    //$rootScope.$on('$locationChangeStart', function (event, current, previous) {
    $rootScope.$on('$routeChangeStart', function (event, current, previous) {
       
        debugger;
        
        if (current != "/" && current.$$route.originalPath != "/login") {
            $rootScope.IsAuthorized = false;
            AuthService.Authorize().then(function (pl) {
                //success
                $rootScope.IsAuthorized = true;
            }, function (errorPl) {
                debugger;
                event.preventDefault();
                myApp.ErrorNotification("Unauthorized", 401);//errorPl.data, errorPl.status);
            });
        }else
            $rootScope.IsAuthorized = true;

    });
}]);

// jQuery (jquery should be included before angular in layout page)
var myApp;
myApp = myApp || (function () {
    var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog" data-backdrop="static" data-keyboard="false" role="dialog" tabindex="-1"><div class="modal-dialog"><div class="modal-content"><div class="modal-header"><h4>Processing...</h4></div><div class="modal-body"><div class="progress progress-striped active"><div class="progress-bar" style="width: 100%;"></div></div></div></div></div></div>');
    //var pleaseWaitDiv = $('<div class="modal fade" id="pleaseWaitDialog3" data-backdrop="static" data-keyboard="false" role="dialog"><div class="modal-dialog"><div class="modal-content"><img src="/YPGMVC/images/loading_icon.gif" width="300" /></div></div></div>');
    this.DisplayNotification = function (type, message) {
        debugger;
        if (type == "" || type == "error") type = "danger";

        var htmlmsg = "<div class='alert alert-" + type + " alert-dismissable'><button type='button' class='close' data-dismiss='alert' aria-hidden='true'>&times;</button>" + message + "</div>";
        var divmsg = $("#divGlobalMessage");
        if (divmsg == undefined)
            alert(type + ":" + message);
        else
            divmsg.prepend(htmlmsg);
    };

    return {
        showPleaseWait: function () {
            pleaseWaitDiv.modal();
        },
        hidePleaseWait: function () {
            $('.modal-backdrop').remove();
            pleaseWaitDiv.modal('hide');
        },
        //Display sucess notification message on main page. message should be string value
        SuccessNotification: function (message) {
            DisplayNotification("success", message);
        },

        //Display Error Notification. obj could be object or string. 
        ErrorNotification: function (obj, status) {
            //redirect to login if unauthorized
            debugger;
            var msgstr = "";
            if (obj == null) obj = "NULL";
            if ( typeof obj == "object") {
                if (obj.hasOwnProperty("Message"))
                    msgstr = obj.Message;
                else
                    msgstr = JSON.stringify(obj);
            }
            else
                msgstr = obj;
             
            if (status == 401) {
                myApp.hidePleaseWait();
                SecurityManager.logout();
                var baseurl = location.protocol + '//' + location.hostname + (location.port ? ':' + location.port : '')
                location.href = baseurl+"/login.asp";
                //location.href = baseurl + "/ypcsr/customer#!/login";
            }
            else
                DisplayNotification("error", msgstr.replace("\r\n", "<br/>"));
        },
        WarningNotification: function (message) {
            DisplayNotification("warning", message.replace("\r\n", "<br/>"));
        },
        ClearAllNotifications() {
            var divmsg = $("#divGlobalMessage");
            if (divmsg != undefined)
                divmsg.empty();
        }
    };

})();


var SecurityManager = {
    // this is refreshed when page is loaded. Any activity inside SPA will not read below.(from salt to ip)
    salt: 'rz8LuOtFBXphj9WQfvFh', // Generated at https://www.random.org/strings
    username: sessionStorage['SecurityManager.username'],
    key: sessionStorage['SecurityManager.key'],
    time: sessionStorage['SecurityManager.time'],
    ip: null,
    generate: function (username, password) {
        debugger;
        if (username && password) {
            // If the user is providing credentials, then create a new key.
            SecurityManager.logout();
        }
        // Set the username.
        SecurityManager.username = SecurityManager.username || username;
        // Set the key to a hash of the user's password + salt.
        SecurityManager.key = SecurityManager.key || CryptoJS.enc.Base64.stringify(CryptoJS.HmacSHA256([password, SecurityManager.salt].join(':'), SecurityManager.salt));
        // Set the client IP address.
        SecurityManager.ip = SecurityManager.ip || SecurityManager.getIp();
        // Set the previous timestamp
        SecurityManager.time = SecurityManager.time;

        // Get the (C# compatible) ticks to use as a timestamp. http://stackoverflow.com/a/7968483/2596404
        var ticks = ((new Date().getTime() * 10000) + 621355968000000000);

        // Persist key pieces.
        if (SecurityManager.username) {
            sessionStorage['SecurityManager.username'] = SecurityManager.username;
            sessionStorage['SecurityManager.key'] = SecurityManager.key;
            sessionStorage['SecurityManager.time'] = ticks;
        }
        if (SecurityManager.time == null)
            SecurityManager.time = ticks;

        // Construct the hash body by concatenating the username, ip, and userAgent.
        //var message = [SecurityManager.username, SecurityManager.ip, navigator.userAgent.replace(/ \.NET.+;/, ''), ticks].join(':');  // original script
        var message = [SecurityManager.username, SecurityManager.ip, navigator.userAgent.replace(/ \.NET.+;/, ''), SecurityManager.time].join(':');
        // Hash the body, using the key.
        var hash = CryptoJS.HmacSHA256(message, SecurityManager.key);
        // Base64-encode the hash to get the resulting token.
        var token = CryptoJS.enc.Base64.stringify(hash);
        // Include the username and timestamp on the end of the token, so the server can validate.
        //var tokenId = [SecurityManager.username, ticks].join(':');
        var tokenId = [SecurityManager.username, SecurityManager.time].join(':');
        // Base64-encode the final resulting token.
        var tokenStr = CryptoJS.enc.Utf8.parse([token, tokenId].join(':'));
        // Set new activity time 
        SecurityManager.time = ticks;
        //SecurityManager.time = ticks;
        return CryptoJS.enc.Base64.stringify(tokenStr);
    },
    logout: function () {
        SecurityManager.ip = null;
        sessionStorage.removeItem('SecurityManager.username');
        SecurityManager.username = null;
        sessionStorage.removeItem('SecurityManager.key');
        SecurityManager.key = null;
        sessionStorage.removeItem('SecurityManager.time');
        SecurityManager.time = null;
    },
    getIp: function () {
        var result = '';
        $.ajax({
            url: '/ypcsr/customer/GetIP',
            method: 'GET',
            async: false,
            success: function (ip) {
                result = ip;
            }
        });
        return result;
    }
};
