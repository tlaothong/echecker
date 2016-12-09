module app.noti {
    'use strict';

    class NotificationController {

        private notiVehicle: VehicleInformation
        private _localNotificationId: number = 1;

        static $inject = [
            '$state',
            '$cordovaLocalNotification',
            '$cordovaDatePicker',
            'app.noti.NotificationService',
            'app.shared.VehicleService'];
        constructor(
            private $state,
            private $cordovaLocalNotification,
            private $cordovaDatePicker,
            private svc: app.noti.NotificationService,
            private vehicle: app.shared.VehicleService) {
            this.notiVehicle = this.GetNotificationValue(vehicle.VehicleSelected);
        }

        private Click(newDateFor: any) {

            var noti = this.notiVehicle;
            var currentDate;
            if (newDateFor == 'PBRDate') currentDate = noti.PBRDate;
            else if (newDateFor == 'DrivingLicenseDate') currentDate = noti.DrivingLicenseDate;
            else if (newDateFor == 'CheckDate') currentDate = noti.CheckDate;
            else if (newDateFor == 'TaxDate') currentDate = noti.TaxDate;
            else if (newDateFor == 'PayDate') currentDate = noti.PayDate;

            var options = {
                date: new Date(currentDate.toString()),
                mode: 'date'
            };

            this.$cordovaDatePicker.show(options)
                .then((date: Date) => {
                    if (newDateFor == 'PBRDate') { noti.PBRDate = date; }
                    else if (newDateFor == 'DrivingLicenseDate') { noti.DrivingLicenseDate = date; }
                    else if (newDateFor == 'CheckDate') { noti.CheckDate = date; }
                    else if (newDateFor == 'TaxDate') { noti.TaxDate = date; }
                    else if (newDateFor == 'PayDate') { noti.PayDate = date; }
                });
        }

        private Submit(): void {

            //Update local-vehicle checked
            this.vehicle.VehicleSelected.IsPBRActive = this.notiVehicle.IsPBRActive;
            this.vehicle.VehicleSelected.IsDrivingLicenseActive = this.notiVehicle.IsDrivingLicenseActive;
            this.vehicle.VehicleSelected.IsCheckActive = this.notiVehicle.IsCheckActive; 52
            this.vehicle.VehicleSelected.IsTaxActive = this.notiVehicle.IsTaxActive;
            this.vehicle.VehicleSelected.IsPayActive = this.notiVehicle.IsPayActive;

            //Update local-vehicle date
            this.vehicle.VehicleSelected.PBRDate = this.notiVehicle.PBRDate;
            this.vehicle.VehicleSelected.DrivingLicenseDate = this.notiVehicle.DrivingLicenseDate;
            this.vehicle.VehicleSelected.CheckDate = this.notiVehicle.CheckDate;
            this.vehicle.VehicleSelected.TaxDate = this.notiVehicle.TaxDate;
            this.vehicle.VehicleSelected.PayDate = this.notiVehicle.PayDate;

            //Update notification to server
            this.svc.UpdateNotification(this.vehicle.VehicleSelected);
            this.$state.go('app.vehicle.status');


            //Local Notifications
            this.$cordovaLocalNotification.cancelAll().then(function (result) {
                // ...
                console.log("Cancel all local notifications!");
            });
            if (this.vehicle.VehicleSelected.IsPBRActive) this.LocalNotification(this.vehicle.VehicleSelected.PBRDate, this.vehicle.VehicleSelected.PlateNumber + " พรบ หมดอายุ");
            if (this.vehicle.VehicleSelected.IsDrivingLicenseActive) this.LocalNotification(this.vehicle.VehicleSelected.DrivingLicenseDate, this.vehicle.VehicleSelected.PlateNumber + " ใบขับขี่หมดอายุ");
            if (this.vehicle.VehicleSelected.IsCheckActive) this.LocalNotification(this.vehicle.VehicleSelected.CheckDate, this.vehicle.VehicleSelected.PlateNumber + " ตรวจสภาพ");
            if (this.vehicle.VehicleSelected.IsTaxActive) this.LocalNotification(this.vehicle.VehicleSelected.TaxDate, this.vehicle.VehicleSelected.PlateNumber + " ต่อภาษีคู่มือรถ");
            if (this.vehicle.VehicleSelected.IsPayActive) this.LocalNotification(this.vehicle.VehicleSelected.PayDate, this.vehicle.VehicleSelected.PlateNumber + " จ่ายงวดรถ");
        }

        //Alert local notification
        private LocalNotification(notiDate: Date, message: string): void {

            var currentNotiDate = new Date(notiDate)
            currentNotiDate.setHours(8, 30, 0, 0);
            var beforeNotiDateOneDay = new Date();
            beforeNotiDateOneDay.setDate(currentNotiDate.getDate() - 1);
            beforeNotiDateOneDay.setHours(8, 30, 0, 0);
            var beforeNotiDateTwoeDay = new Date();
            beforeNotiDateTwoeDay.setDate(currentNotiDate.getDate() - 2);
            beforeNotiDateTwoeDay.setHours(8, 30, 0, 0);

            var textLocalNoti = message + "วันที่ " + currentNotiDate.toLocaleDateString();

            this.$cordovaLocalNotification.schedule([
                {
                    id: this._localNotificationId++,
                    title: 'DLTEChecker',
                    text: textLocalNoti,
                    at: beforeNotiDateTwoeDay
                },
                {
                    id: this._localNotificationId++,
                    title: 'DLTEChecker',
                    text: textLocalNoti,
                    at: beforeNotiDateOneDay
                },
                {
                    id: this._localNotificationId++,
                    title: 'DLTEChecker',
                    text: textLocalNoti,
                    at: currentNotiDate
                }
            ]).then(function (result) {
                // ...
                console.log("Set schedule for local notifications!");
            });


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
