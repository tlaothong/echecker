module app.noti {
    'use strict';

    class NotificationController {

        private notiVehicle: VehicleInformation

        static $inject = ['$state', 'app.noti.NotificationService', 'app.shared.VehicleService'];
        constructor(private $state, private svc: app.noti.NotificationService, private vehicle: app.shared.VehicleService) {
            this.notiVehicle = this.GetNotificationValue(vehicle.VehicleSelected);
        }

        private Submit(): void {
            
            //Update vehicle checked
            this.vehicle.VehicleSelected.IsPBRActive = this.notiVehicle.IsPBRActive;
            this.vehicle.VehicleSelected.IsDrivingLicenseActive = this.notiVehicle.IsDrivingLicenseActive;
            this.vehicle.VehicleSelected.IsCheckActive = this.notiVehicle.IsCheckActive;
            this.vehicle.VehicleSelected.IsTaxActive = this.notiVehicle.IsTaxActive;
            this.vehicle.VehicleSelected.IsPayActive = this.notiVehicle.IsPayActive;

            //Update vehicle date
            var date: number = this.notiVehicle.PBRDate.getDate();
            this.vehicle.VehicleSelected.PBRDate = this.notiVehicle.PBRDate;
            this.vehicle.VehicleSelected.DrivingLicenseDate = this.notiVehicle.DrivingLicenseDate;
            this.vehicle.VehicleSelected.CheckDate = this.notiVehicle.CheckDate;
            this.vehicle.VehicleSelected.TaxDate = this.notiVehicle.TaxDate;
            this.vehicle.VehicleSelected.PayDate = this.notiVehicle.PayDate;

            console.log('PBRDate: ' + this.vehicle.VehicleSelected.PBRDate);
            console.log('DrivingLicenseDate: ' + this.vehicle.VehicleSelected.DrivingLicenseDate);
            console.log('CheckDate: ' + this.vehicle.VehicleSelected.CheckDate);
            console.log('TaxDate: ' + this.vehicle.VehicleSelected.TaxDate);
            console.log('PayDate: ' + this.vehicle.VehicleSelected.PayDate);

            this.svc.UpdateNotification(this.vehicle.VehicleSelected);
            this.$state.go('app.vehicle.status');
        }

        //Get only notification value from vehicle (such IsPBRActive, PBRDate etc.)
        private GetNotificationValue(vehicle: VehicleInformation): VehicleInformation {
            var newVehicle = new VehicleInformation();
            newVehicle.IsPBRActive = vehicle.IsPBRActive;
            newVehicle.IsDrivingLicenseActive = vehicle.IsDrivingLicenseActive;
            newVehicle.IsCheckActive = vehicle.IsCheckActive;
            newVehicle.IsTaxActive = vehicle.IsTaxActive;
            newVehicle.IsPayActive = vehicle.IsPayActive;

            newVehicle.PBRDate = vehicle.PBRDate;
            newVehicle.DrivingLicenseDate = vehicle.DrivingLicenseDate;
            newVehicle.CheckDate = vehicle.CheckDate;
            newVehicle.TaxDate = vehicle.TaxDate;
            newVehicle.PayDate = vehicle.PayDate;
            return newVehicle;
        }
    }

    angular
        .module('app.noti')
        .controller('app.noti.NotificationController', NotificationController);
}
