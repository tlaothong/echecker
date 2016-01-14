module app.amissed {
    'use strict';

    class AmissedDetailController {

        //private PlateNumber: any;

        static $inject = ['app.shared.VehicleService', 'app.shared.AmissDetailService'];
        constructor(private vehicle: app.shared.VehicleService, private amissed: app.shared.AmissDetailService) {
            //this.PlateNumber = vehicleSvc.VehicleSelected.PlateNumber;
        }


    }

    //class ReportController {

    //    private PlateNumber: any;
        
    //    static $inject = ['data', 'app.shared.VehicleService'];
    //    constructor(private data, private vehicleSvc: app.shared.VehicleService) {
    //        this.PlateNumber = vehicleSvc.VehicleSelected.PlateNumber;
    //    }

    //    private IsPass(topic: any): boolean {
    //        return topic.IsPass == "TRUE";
    //    }
    //    private IsFalse(topic: any): boolean {
    //        return topic.IsPass == "FALSE";
    //    }
    //}


    angular
        .module('app.amissed')
        .controller('app.amissed.AmissedDetailController', AmissedDetailController);
}