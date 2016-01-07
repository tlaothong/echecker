module app.amissed {
    'use strict';

    class AmissedDetailController {

        private PlateNumber: any;

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehiclesData.PlateNumber;
        }

    }

    class ReportController {

        private PlateNumber: any;
        
        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehiclesData.PlateNumber;
        }
        
        public IsPass(topic: any): boolean {
            return topic.IsPass == "TRUE";
        }
        public IsFalse(topic: any): boolean {
            return topic.IsPass == "FALSE";
        }
    }

    angular
        .module('app.amissed')
        .controller('app.amissed.AmissedDetailController', AmissedDetailController)
        .controller('app.amissed.ReportController', ReportController);
}