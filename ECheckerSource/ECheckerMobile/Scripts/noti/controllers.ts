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
            this.PlateNumber = vehicleSvc.VehiclesData.PlateNumber;
            this.PBRActive = vehicleSvc.VehiclesData.PBRActive;
            this.PBRDate = vehicleSvc.VehiclesData.PBRDate;
            this.DrivingLicenseActive = vehicleSvc.VehiclesData.DrivingLicenseActive;
            this.DrivingLicenseDate = vehicleSvc.VehiclesData.DrivingLicenseDate;
            this.CheckActive = vehicleSvc.VehiclesData.CheckActive;
            this.CheckDate = vehicleSvc.VehiclesData.CheckDate;
            this.TaxActive = vehicleSvc.VehiclesData.TaxActive;
            this.TaxDate = vehicleSvc.VehiclesData.TaxDate;
            this.PayActive = vehicleSvc.VehiclesData.PayActive;
            this.PayDate = vehicleSvc.VehiclesData.PayDate;
        }

    }

    angular
        .module('app.noti')
        .controller('app.noti.NotificationController', NotificationController);
}