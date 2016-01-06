module app.checking {
    'use strict';
    
    class TopicsController {

        private PlateNumber: any;
        private VehicleStatus: any;

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(public data, private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehiclesData.PlateNumber;
            this.VehicleStatus = vehicleSvc.VehiclesData.VehicleStatus;
        }

        public IsPass(topic: any): boolean {
            return topic.IsPass == "TRUE";
        }
        public IsFalse(topic: any): boolean {
            return topic.IsPass == "FALSE";
        }

    }

    class CheckAmissController {
        static $inject = ['data'];
        constructor(public data) {
        }

    }

    angular
        .module('app.checking')
        .controller('app.checking.TopicsController', TopicsController)
        .controller('app.checking.CheckAmissController', CheckAmissController);
}