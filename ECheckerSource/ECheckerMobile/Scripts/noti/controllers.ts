module app.noti {
    'use strict';

    class NotificationController {

        private PlateNumber: any;
        private PBRActive: boolean;
        private PBRDate: Date;
        private DrivingLicenseActive: boolean;
        private DrivingLicenseDate: Date;
        private CheckActive: boolean;
        private CheckDate: Date;
        private TaxActive: boolean;
        private TaxDate: Date;
        private PayActive: boolean;
        private PayDate: Date;

        static $inject = ['app.shared.VehicleService'];
        constructor(private vehicleSvc: app.shared.VehicleService) {
            this.PlateNumber = vehicleSvc.VehicleSelected.PlateNumber;
            this.PBRActive = vehicleSvc.VehicleSelected.IsPBRActive;
            this.PBRDate = vehicleSvc.VehicleSelected.PBRDate;
            this.DrivingLicenseActive = vehicleSvc.VehicleSelected.IsDrivingLicenseActive;
            this.DrivingLicenseDate = vehicleSvc.VehicleSelected.DrivingLicenseDate;
            this.CheckActive = vehicleSvc.VehicleSelected.IsCheckActive;
            this.CheckDate = vehicleSvc.VehicleSelected.CheckDate;
            this.TaxActive = vehicleSvc.VehicleSelected.IsTaxActive;
            this.TaxDate = vehicleSvc.VehicleSelected.TaxDate;
            this.PayActive = vehicleSvc.VehicleSelected.IsPayActive;
            this.PayDate = vehicleSvc.VehicleSelected.PayDate;
        }

    }

    angular
        .module('app.noti')
        .controller('app.noti.NotificationController', NotificationController);
}