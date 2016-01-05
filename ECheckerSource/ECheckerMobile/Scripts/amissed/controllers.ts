module app.amissed {
	'use strict';

    class AmissedDetailController {

        private PlateNumber: any;
        
        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehiclesData.PlateNumber;
            console.log(data);
		}
	}

	angular
		.module('app.amissed')
        .controller('app.amissed.AmissedDetailController', AmissedDetailController);
}