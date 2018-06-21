app.service("EntitlementService",['$http', function ($http) {

    var uri = "/ypcsr/api";


    // Entitlement Mapping
    //
    this.GetEntitlementMappingByPostalCode = function (postalCode) {
        var response = $http({
            method: "get",
            url: uri + "/entitlement/",
            params: {
                postalCode: postalCode,
                token:SecurityManager.generate()
            }
        });
        return response;
    }

    // Entitlement Mapping
    this.GetEntitlementQtyByCMISID = function (cmisid,sku) {
        var response = $http({
            method: "get",
            url: uri + "/entitlement/",
            params: {
                cmisid: cmisid,
                sku: sku,
                token: SecurityManager.generate()
            }
        });
        return response;
    }

    this.AddCallEntitlementQty = function (entitlement) {
        debugger;
        var response = $http({
            method: "post",
            url: uri + "/entitlement/",
            data: entitlement
        });
        return response;
    }
    this.GetCustomerEntitlement = function (cmisid, csid, postalcode) {
        debugger;
        var response = $http({
            method: "get",
            url: uri + "/customerdetail/",
            params: {
                cmisid: cmisid,
                csid: csid,
                postalcode: postalcode,
                token: SecurityManager.generate()
            }
        });
        return response;
    }
}]);