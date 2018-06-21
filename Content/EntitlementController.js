
//app.controller("EntitlementController",['ShareData', function ($scope, $location, addrService, $timeout, ShareData, ProvinceService, EntitlementService) {
app.controller("EntitlementController", ['$scope', 'ShareData', 'EntitlementService', '$rootScope', 'ProvinceService', 'AddressService', function ($scope, ShareData, EntitlementService, $rootScope, ProvinceService, AddressService) {

    $scope.EntitlementArray = [];

    // this works with new customer only
    $scope.aProvinceService = ProvinceService;
    $scope.$watch('aProvinceService.context.postalcode', function (newValue, oldValue, scope) {
        //address update postal code when customer is a new customer (not call address customer and not main address customer)
        if (newValue != undefined && newValue != "" && newValue != oldValue && newValue.length === 6 && ShareData.cmisid == 0) {
        //if (newValue != undefined && newValue != "" && newValue != oldValue ) {
            myApp.showPleaseWait();
            //EntitlementService.GetEntitlementMappingByPostalCode(ShareData.cmisid, ShareData.csid, newValue);
            ChangeCustomerEntitlement(newValue);
            myApp.hidePleaseWait();
        }
    }, true
    );
    $scope.cmisid = ShareData.cmisid;
    $scope.csid = ShareData.csid;
    $scope.Quantity=1;
    $scope.OptIn = false;
    $scope.OptOut = false;
    $scope.ConfirmationDate = '';
    var RouteNumber = '';
    var DeliveryStatus = '';
    var LastDeliveredDate = '';
    var DeliveryType = '';

    // this works with existing customer
    $scope.$on('ShowEntitlementQty', function (e) {
        // from CustomerEntitlement object
        $scope.customerEntitlement = $scope.$parent.CustomerDetailData.CustomerEntitlements;
        GetCustomerEntitlement($scope.customerEntitlement);
    });
    function ChangeCustomerEntitlement(postalcode) {
        var getData = EntitlementService.GetCustomerEntitlement(ShareData.cmisid, ShareData.csid, postalcode);
        getData.then(function (emp) {
            $scope.customerEntitlement = emp.data;
            GetCustomerEntitlement($scope.customerEntitlement);
        }, function (errorPl) {
            myApp.ErrorNotification("Failed to load data:" + errorPl.data, errorPl.status);
        });
    }
    function GetCustomerEntitlement(customerEntitlement) {
        $scope.EntitlementArray = [];
        ProvinceService.context.entitlement = [];
        angular.forEach(customerEntitlement, function (value, key) {
            if (value.EntitlementQty == null) {
                if (value.Directory.BookType == "WP")
                    $scope.Quantity = 0;
                else
                    $scope.Quantity = 1;

                $scope.OptIn = false;
                $scope.OptOut = false;
            }
            else {
                $scope.Quantity = value.EntitlementQty.Quantity;
                $scope.ConfirmationDate = value.EntitlementQty.WhenCreated;
                if ($scope.Quantity == 0) {
                    $scope.OptIn = false;
                    $scope.OptOut = true;
                }
                else {
                    $scope.OptIn = true;
                    $scope.OptOut = false;
                }
            }

            $scope.EntitlementArray.push({
                //EntitlementMappingID: value.EntitlementMappingID,
                SKU: value.SKU,
                Title: value.Directory.Title,
                DeliveryStart: value.Directory.DeliveryStart,
                DeliveryEnd: value.Directory.DeliveryEnd,
                ActualEndDate: value.Directory.ActualEndDate,
                BookType: value.Directory.BookType,
                InventoryStatus: value.Directory.InventoryInfo.InventoryStatus,
                CMISCallCentreQty: value.Directory.InventoryInfo.CMISCallCentreQty,
                EntitlementQtyID: 0,
                Quantity: $scope.Quantity,//(value.entitlementQty == null) ? 1 : value.entitlementQty.Quantity,
                OptIn: $scope.OptIn,
                OptOut: $scope.OptOut,
                ConfirmationDate: $scope.ConfirmationDate,
                FSEmail: (value.UserDirectoryMapping == null) ? '' : value.UserDirectoryMapping.email,
                Directory: value.Directory,
                eSend: false,
                RouteNumber: (value.CustomerDelivery == null) ? 'N/A' : value.CustomerDelivery.RouteNumber,
                DeliveryStatus: (value.CustomerDelivery == null) ? 'N/A' : value.CustomerDelivery.DeliveryStatus,
                LastDeliveredDate: (value.CustomerDelivery == null) ? 'N/A' : value.CustomerDelivery.LastDeliveredDate,
                DeliveryType: (value.CustomerDelivery == null) ? 'N/A' : value.CustomerDelivery.DeliveryType
            })
            // need when you place entitlement order
            ProvinceService.context.entitlement.push({ entitlement: value.SKU });

        });
        $scope.original = angular.copy($scope.EntitlementArray);
        $scope.initialComparison = angular.equals($scope.EntitlementArray, $scope.original);
        $scope.dataHasChanged = angular.copy($scope.initialComparison);
    };



    //function GetEntitlementMappingByPostalCode(postalcode) {
    //    debugger;
    //    $scope.EntitlementArray = [];
    //    ProvinceService.context.entitlement = [];
    //    var getData = EntitlementService.GetEntitlementMappingByPostalCode(postalcode);
    //    getData.then(function (emp) {
    //        debugger;
    //        $scope.entitlementMapping = emp.data;
    //        angular.forEach($scope.entitlementMapping, function (value, key) {
    //            debugger;
    //            //GetEntitlementQty($scope.cmisid, value.Directory.SKU);
    //            $scope.FSEmail = "";
    //            if (value.UserDirectoryMapping != null)
    //                $scope.FSEmail = value.UserDirectoryMapping.email;
    //            $scope.EntitlementArray.push({
    //                EntitlementMappingID: value.EntitlementMappingID, SKU: value.Directory.SKU, Title: value.Directory.Title,
    //                DeliveryStart: value.Directory.DeliveryStart, DeliveryEnd: value.Directory.DeliveryEnd, ActualEndDate: value.Directory.ActualEndDate,
    //                BookType: value.Directory.BookType,
    //                InventoryStatus: value.Directory.InventoryInfo.InventoryStatus, CMISCallCentreQty: value.Directory.InventoryInfo.CMISCallCentreQty,
    //                EntitlementQtyID:0, Quantity: $scope.Quantity, OptIn: $scope.OptIn, OptOut: $scope.OptOut, ConfirmationDate:'',
    //                FSEmail: $scope.FSEmail,
    //                Directory: value.Directory,
    //                eSend: false
    //            })
    //            ProvinceService.context.entitlement.push({entitlement: value.Directory.SKU});

    //        });

    //    }, function (emp) {
    //        alert("Records gathering failed!");
    //    });
    //}

    $scope.addEntitlementOrder = function (directory) {
        $rootScope.$broadcast("item:added", directory);  // calling order function in order controller
    };


    //$scope.GetEntitlementQty = function  (entitlement) {
    //    var getData = EntitlementService.GetEntitlementQtyByCMISID(ShareData.cmisid,entitlement.SKU)//(cmisid, sku);
    //    getData.then(function (emp) {
    //        $scope.entitlementQty = emp.data;

    //        if ($scope.entitlementQty.length == 0) {
    //            entitlement.EntitlementQtyID = 0;
    //            if(entitlement.BookType == "YP")
    //                entitlement.Quantity = 1;
    //            else
    //                entitlement.Quantity = 0;

    //            entitlement.OptIn = false;
    //            entitlement.OptOut = false;
    //        }
    //        else {
    //            angular.forEach($scope.entitlementQty, function (value, key) {
    //                entitlement.EntitlementQtyID = value.EntitlementQtyID;
    //                entitlement.Quantity = value.Quantity;
    //                if (value.Quantity == 0) {
    //                    entitlement.ConfirmationDate = value.WhenUpdated;
    //                    entitlement.OptIn = false;
    //                    entitlement.OptOut = true;
    //                }
    //                else {
    //                    entitlement.ConfirmationDate = value.WhenUpdated;
    //                    entitlement.OptIn = true;
    //                    entitlement.OptOut = false;

    //                }
    //            });
    //        }
    //        $scope.original = angular.copy($scope.EntitlementArray);
    //        $scope.initialComparison = angular.equals($scope.EntitlementArray, $scope.original);
    //        $scope.dataHasChanged = angular.copy($scope.initialComparison);
    //    });

    //};

    $scope.checkOptIn = function (entitlement, selected) {
        if (selected) {
            entitlement.Quantity = 1;
            entitlement.OptOut = false;
        }
        else if (entitlement.BookType == "YP")
            entitlement.Quantity = 1;
        else
            entitlement.Quantity = 0;


        $scope.dataHasChanged = angular.equals($scope.EntitlementArray, $scope.original);
    }
    $scope.checkOptOut = function (entitlement, selected) {

        if (selected) {
            entitlement.Quantity = 0;
            entitlement.OptIn = false;
        }
        else if (entitlement.BookType == "YP")
            entitlement.Quantity = 1;
        else
            entitlement.Quantity = 0;

        $scope.dataHasChanged = angular.equals($scope.EntitlementArray, $scope.original);
    }
    $scope.changeQuantity = function (entitlement, qty) {
        entitlement.Quantity = parseInt(qty,10);
        if (qty == 0) {
            entitlement.OptIn = false;
            entitlement.OptOut = true;
            
        }
        else {
            entitlement.OptIn = true
            entitlement.OptOut= false;
        }
        $scope.dataHasChanged = angular.equals($scope.EntitlementArray, $scope.original);
    }


    //broadcast
    $scope.NewEntitlementList=[];
    $scope.$on('AddCallEntitlementQty', function (e) {
        var currentDate = new Date();
        for (var i = 0, len = $scope.EntitlementArray.length; i < len; i++) {
            if ($scope.EntitlementArray[i].eSend)
            {
                $scope.vm.new_edirectory.push($scope.EntitlementArray[i].Directory.ID);
                ShareData.EmailSent = 1;
                $scope.EntitlementArray[i].eSend = false;
            }
            if (!angular.equals($scope.EntitlementArray[i], $scope.original[i])) {
                var entitlement = ({
                    EntitlementQtyID: $scope.EntitlementArray[i]["EntitlementQtyID"], //value.EntitlementQtyID,
                    CS_CustomerID: ShareData.csid,
                    CMIS_CustomerID: ShareData.cmisid,
                    SKU: $scope.EntitlementArray[i]["SKU"], //value.SKU,
                    Quantity: $scope.EntitlementArray[i]["Quantity"], //value.Quantity,
                    Loaded: 0,
                    Note: ""
                });

                $scope.NewEntitlementList.push(entitlement)
            }
        }
        $scope.vm.new_entitlement = $scope.NewEntitlementList;
        $scope.NewEntitlementList = [];
    });

    // entitlement update in separate tab. not being used for now
    //$scope.AddCallEntitlementQty = function () {
    //    var currentDate = new Date();
    //    for (var i = 0, len = $scope.EntitlementArray.length; i < len; i++) {
    //        if (!angular.equals($scope.EntitlementArray[i], $scope.original[i])) {
    //            debugger;
    //            var entitlement = ({
    //                EntitlementQtyID: $scope.EntitlementArray[i]["EntitlementQtyID"], //value.EntitlementQtyID,
    //                CS_CustomerID: 0,
    //                CMIS_CustomerID: $scope.cmisid,
    //                SKU: $scope.EntitlementArray[i]["SKU"], //value.SKU,
    //                Quantity: $scope.EntitlementArray[i]["Quantity"], //value.Quantity,
    //                WhenCreated: currentDate,
    //                WhoCreated: "test",
    //                Loaded: 0,
    //                Note: "test"
    //            });
            
    //            var getData = EntitlementService.AddCallEntitlementQty(entitlement);
    //            getData.then(function (emp) {
    //                debugger;
    //                alert("success");
    //            }, function (emp) {
    //                close
    //                alert("failed!");
    //            });
    //        }
    //    }
    //};
}]);