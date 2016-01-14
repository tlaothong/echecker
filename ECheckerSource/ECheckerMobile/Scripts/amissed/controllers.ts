module app.amissed {
    'use strict';

    class AmissedDetailController {
    
        static $inject = ['app.shared.VehicleService', 'app.shared.AmissDetailService'];
        constructor(private vehicle: app.shared.VehicleService, private amissed: app.shared.AmissDetailService) {
        }
        
    }
    
    angular
        .module('app.amissed')
        .controller('app.amissed.AmissedDetailController', AmissedDetailController);
}