module app.noti {
    'use strict';

    class NotificationController {

        private notiVehicle: VehicleInformation

        static $inject = ['$state', 'app.noti.NotificationService', 'app.shared.VehicleService'];
        constructor(private $state, private svc: app.noti.NotificationService, private vehicle: app.shared.VehicleService) {
            this.notiVehicle = vehicle.VehicleSelected;
        }

        private Submit(): void {
            //var x = this.vehicle.VehicleSelected.IsPBRActive;
            //this.svc.UpdateNotification(this.vehicle.VehicleSelected);

         
            //this.notiVehicle.id = this.vehicle.VehicleSelected.id;
            //this.notiVehicle.PBRDate = this.vehicle.VehicleSelected.PBRDate; 
            //this.notiVehicle.IsPBRActive = this.vehicle.VehicleSelected.IsPBRActive;
            //this.notiVehicle.CheckDate = this.vehicle.VehicleSelected.CheckDate;
            //this.notiVehicle.IsCheckActive = this.vehicle.VehicleSelected.IsCheckActive;
            //this.notiVehicle.DrivingLicenseDate = this.vehicle.VehicleSelected.DrivingLicenseDate;
            //this.notiVehicle.IsDrivingLicenseActive = this.vehicle.VehicleSelected.IsDrivingLicenseActive;
            //this.notiVehicle.PayDate = this.vehicle.VehicleSelected.PayDate;
            //this.notiVehicle.IsPayActive = this.vehicle.VehicleSelected.IsPayActive;
            //this.notiVehicle.TaxDate = this.vehicle.VehicleSelected.TaxDate;
            //this.notiVehicle.IsTaxActive = this.vehicle.VehicleSelected.IsTaxActive;          

            this.svc.UpdateNotification(this.notiVehicle);


            this.$state.go('app.vehicle.status');
        }
    }

    angular
        .module('app.noti')
        .controller('app.noti.NotificationController', NotificationController);
}