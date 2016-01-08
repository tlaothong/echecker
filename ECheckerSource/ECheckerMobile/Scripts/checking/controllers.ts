module app.checking {
    'use strict';
    
    class TopicsController {

        private PlateNumber: any;
        private VehicleStatus: any;

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehicleSelected.PlateNumber;
            this.VehicleStatus = vehicleSvc.VehicleSelected.StatusCode;
        }

        private IsPass(topic: any): boolean {
            return topic.IsPass == "TRUE";
        }
        private IsFalse(topic: any): boolean {
            return topic.IsPass == "FALSE";
        }

    }

    class CheckAmissController {
        static $inject = ['data'];
        constructor(private data) {
        }

    }

    angular
        .module('app.checking')
        .controller('app.checking.TopicsController', TopicsController)
        .controller('app.checking.CheckAmissController', CheckAmissController);
}