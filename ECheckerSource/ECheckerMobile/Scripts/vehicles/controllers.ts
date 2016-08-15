module app.vehicles {
    'use strict';

    declare var Ionic;

    class VehicleListController {

        private modal: any;
        private user: any;
        private loadingModal: any;
        private data: VehicleInformation[]

        static $inject = [
            '$ionicLoading',
            '$timeout',
            '$state',
            'app.shared.VehicleService',
            'app.vehicles.VehiclesService']
        constructor(
            private $ionicLoading,
            private $timeout,
            private $state,
            private vehicle: app.shared.VehicleService,
            private svc: app.vehicles.VehiclesService) {

            this.user = Ionic.User.current();

            if (this.user.id) {
                console.log('Connecting to server.')
                this.$ionicLoading.show({ template: 'Loading data... <ion-spinner></ion-spinner>' });

                svc.GetVehicles().then((it) => {
                    console.log('Connection completed.');
                    this.$ionicLoading.hide();

                    var minimumDataLength = 1;
                    var IsDataEmpty = it.length < minimumDataLength;
                    if (IsDataEmpty) {
                        alert('คุณยังไม่มีรถอยู่ในระบบ กรุณาเพิ่มรถลงในระบบก่อนใช้งาน');
                        console.log('User is not has any vehicle.');
                        console.log('Go to manage vehicle page for add vehicle.');
                        $state.go('app.manvehicles')
                    }
                    else {
                        this.data = it;
                        this.NotifyAllVehicle();
                    }
                });
            }
        }

        //Get vehicle is not ready to analysis (ตรวจยังไม่เสร็จ)
        private DisplayVehichleNotReady(): VehicleInformation[] {
            if (this.data == null) return null;
            return this.data.filter(it => it.StatusCode == 0);
        }

        //Get vehicle is ready to analysis (รอส่งวิเคราะห์)
        private DisplayVehichleReady(): VehicleInformation[] {
            if (this.data == null) return null;
            return this.data.filter(it => it.StatusCode == 1);
        }

        //Get vehicle analysis comepleted (วิเคราะห์แล้ว)
        private DisplayVehichleCompleted(): VehicleInformation[] {
            if (this.data == null) return null;
            return this.data.filter(it => it.StatusCode == 2);
        }

        //Set select vehicle to service
        private SelectVehicle(vehicleSelected: VehicleInformation) {
            this.vehicle.VehicleSelected = vehicleSelected;

            //Delay 5 seconds before go to vehicle status
            var secondDelay = 5;
            var millisecondDelay = secondDelay * 1000;
            this.$ionicLoading.show({
                template: 'Loading... <ion-spinner></ion-spinner>'
            });

            this.$timeout(() => {
                this.$ionicLoading.hide();
                this.$state.go('app.vehicle.status');
            }, millisecondDelay);
        }

        //Notify all vehicle on list
        private NotifyAllVehicle() {
            var IsInvalid = this.data == null;
            if (IsInvalid) return;

            var alertData = this.data.filter(it =>
                (it.IsPBRActive && this.IsNotify(it.PBRDate)) ||
                (it.IsDrivingLicenseActive && this.IsNotify(it.DrivingLicenseDate)) ||
                (it.IsCheckActive && this.IsNotify(it.CheckDate)) ||
                (it.IsTaxActive && this.IsNotify(it.TaxDate)) ||
                (it.IsPayActive && this.IsNotify(it.PayDate)));

            var notifyMessage: Array<string> = [];
            for (var item of alertData) {
                notifyMessage.push('ทะเบียน ' + item.PlateNumber + '\n');
                if (item.IsPBRActive && this.IsNotify(item.PBRDate)) notifyMessage.push('> ครบกำหนดพรบ\n');
                if (item.IsDrivingLicenseActive && this.IsNotify(item.DrivingLicenseDate)) notifyMessage.push('> ครบกำหนดใบขับขี่\n');
                if (item.IsCheckActive && this.IsNotify(item.CheckDate)) notifyMessage.push('> ครบกำหนดตรวจสภาพรถ\n');
                if (item.IsTaxActive && this.IsNotify(item.TaxDate)) notifyMessage.push('> ครบกำหนดต่อภาษี\n');
                if (item.IsPayActive && this.IsNotify(item.PayDate)) notifyMessage.push('> ครบกำหนดผ่อนงวด\n');
                notifyMessage.push('\n');
            }

            var minimumLetter = 1;
            var IsNotifyMessageEmpty = notifyMessage.length < minimumLetter;
            if (!IsNotifyMessageEmpty) alert(notifyMessage.join(''));
        }

        //Get should notify result
        private IsNotify(notiDate: any): boolean {
            var limitDateToNotify = 3;

            var dateLimit = new Date();
            dateLimit.setDate(dateLimit.getDate() + limitDateToNotify);
            dateLimit.setHours(0, 0, 0, 0);

            var currentDate = new Date();
            currentDate.setHours(0, 0, 0, 0);

            var IsStillRunningOnDate = dateLimit.toISOString().substr(0, 10) >= notiDate.substr(0, 10);
            var IsDateInRange = currentDate.toISOString().substr(0, 10) <= notiDate.substr(0, 10);

            var result = IsStillRunningOnDate && IsDateInRange;
            //console.log(
            //    'DateLimit: ' + dateLimit.toISOString() +
            //    '\nNotiDate: ' + notiDate +
            //    '\nCurrentDate: ' + currentDate.toISOString() +
            //    '\nResult: ' + result);
            return result;
        }
    }

    class VehicleAddController {

        private newVehicle: VehicleInformation;
        private VehicleTypeId: number;

        static $inject = [
            '$ionicLoading',
            '$timeout',
            '$state',
            'app.vehicles.VehiclesService']
        constructor(
            private $ionicLoading,
            private $timeout,
            private $state,
            private svc: app.vehicles.VehiclesService) {
        }

        //Send vehicle information to server
        private Submit(): void {
            var user = Ionic.User.current();
            this.newVehicle.Email = user.id;
            this.newVehicle.VehicleTypeId = this.VehicleTypeId;
            this.svc.AddVehicle(this.newVehicle);

            //Delay 3 seconds before go to vehicle status
            var secondDelay = 3;
            var millisecondDelay = secondDelay * 1000;
            this.$ionicLoading.show({
                template: 'Loading... <ion-spinner></ion-spinner>'
            });

            this.$timeout(() => {
                this.$ionicLoading.hide();
                this.$state.go('app.manvehicles');
            }, millisecondDelay);
        }
    }

    class VehicleEditController {
        static $inject = [
            '$ionicLoading',
            '$timeout',
            '$state',
            'app.vehicles.VehiclesService',
            'app.shared.VehicleService']
        constructor(
            private $ionicLoading,
            private $timeout,
            private $state,
            private svc: app.vehicles.VehiclesService,
            private vehicle: app.shared.VehicleService) {
        }

        //Send vehicle information to server
        private Submit(): void {
            this.svc.UpdateVehicle(this.vehicle.VehicleSelected);

            //Delay 3 seconds before go to vehicle status
            var secondDelay = 3;
            var millisecondDelay = secondDelay * 1000;
            this.$ionicLoading.show({
                template: 'Loading... <ion-spinner></ion-spinner>'
            });

            this.$timeout(() => {
                this.$ionicLoading.hide();
                this.$state.go('app.manvehicles');
            }, millisecondDelay);
        }

        //Display vehicle type to html
        private DisplayVehicleType(): string {
            var VehicleType = this.vehicle.VehicleSelected.VehicleTypeId == 11 ? 'รถยนต์' : 'รถจักรยานยนต์';
            return VehicleType;
        }
    }

    class ManageVehicleController {

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicle: app.shared.VehicleService) {
        }

        //Set select vehicle to service
        private SelectVehicle(vehicleSelected: VehicleInformation) {
            this.vehicle.VehicleSelected = vehicleSelected;
        }
    }

    angular
        .module('app.vehicles')
        .controller('app.vehicles.VehicleListController', VehicleListController)
        .controller('app.vehicles.VehicleEditController', VehicleEditController)
        .controller('app.vehicles.VehicleAddController', VehicleAddController)
        .controller('app.vehicles.ManageVehicleController', ManageVehicleController);
}