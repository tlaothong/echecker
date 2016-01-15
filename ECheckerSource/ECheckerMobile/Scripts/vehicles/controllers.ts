module app.vehicles {
    'use strict';

    class VehicleListController {

        private modal: any;

        static $inject = [
            '$state',
            '$scope',
            '$ionicModal',
            '$timeout',
            'data',
            'app.shared.VehicleService',
            'app.shared.UserService']
        constructor(
            private $state,
            private $scope,
            private $ionicModal,
            private $timeout,
            private data: VehicleInformation[],
            private vehicle: app.shared.VehicleService,
            private user: app.shared.UserService) {

            var IsDataEmpty = data.length < 1;
            if (IsDataEmpty) {
                console.log('User is not has any vehicle.');
                console.log('Go to manage vehicle page for add vehicle.');
                $state.go('app.manvehicles')
            }

            $ionicModal.fromTemplateUrl('templates/login.html', {
                backdropClickToClose: false,
                scope: $scope
            }).then((modal) => {
                this.modal = modal;
                $scope.modal = modal;
                
                if (!user.IsLogin) this.modal.show();
                });
            if (user.IsLogin) this.NotifyAllVehicle();
        }
        
        //Get vehicle is not ready to analysis (ตรวจยังไม่เสร็จ)
        private DisplayVehichleNotReady(): VehicleInformation[] {
            return this.data.filter(it=> it.StatusCode == 0);
        }

        //Get vehicle is ready to analysis (รอส่งวิเคราะห์)
        private DisplayVehichleReady(): VehicleInformation[] {
            return this.data.filter(it=> it.StatusCode == 1);
        }

        //Get vehicle analysis comepleted (วิเคราะห์แล้ว)
        private DisplayVehichleCompleted(): VehicleInformation[] {
            return this.data.filter(it=> it.StatusCode == 2);
        }
        
        //Set select vehicle to service
        private SelectVehicle(vehicleSelected: VehicleInformation) {
            this.vehicle.VehicleSelected = vehicleSelected;
        }

        //Login successed
        public Logined() {
            //Delay 1 sec to hide modal
            this.user.IsLogin = true;
            console.log('Login succeed.');
            this.$timeout(() => { this.modal.hide(); }, 1000);
        }

        //Notify all vehicle on list
        private NotifyAllVehicle() {
            if (this.data == null) return;
            this.data.filter(it=> (it.IsPBRActive && this.IsNotify(it.PBRDate)));

            var alertData = this.data.filter(it=>
                (it.IsPBRActive && this.IsNotify(it.PBRDate)) ||
                (it.IsDrivingLicenseActive && this.IsNotify(it.DrivingLicenseDate)) ||
                (it.IsCheckActive && this.IsNotify(it.CheckDate)) ||
                (it.IsTaxActive && this.IsNotify(it.TaxDate)) ||
                (it.IsPayActive && this.IsNotify(it.PayDate)));

            var message: Array<string> = [];
            for (var item of alertData) {
                message.push('ทะเบียน ' + item.PlateNumber + '\n');
                if (item.IsPBRActive) message.push('ครบกำหนดพรบ\n');
                if (item.IsDrivingLicenseActive) message.push('ครบกำหนดใบขับขี่\n');
                if (item.IsCheckActive) message.push('ครบกำหนดตรวจสภาพรถ\n');
                if (item.IsTaxActive) message.push('ครบกำหนดต่อภาษี\n');
                if (item.PayDate) message.push('ครบกำหนดผ่อนงวด\n');
            }
            alert(message.join(''));
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

        static $inject = ['$state', 'app.vehicles.VehiclesService', 'app.shared.UserService']
        constructor(private $state, private svc: app.vehicles.VehiclesService, private user: app.shared.UserService) {
        }

        //Send vehicle information to server
        private Submit(): void {
            this.newVehicle.Email = this.user.UserData.Email;
            this.svc.AddVehicle(this.newVehicle);
            this.$state.go('app.manvehicles');
        }
    }

    class VehicleEditController {
        static $inject = ['$state', 'app.vehicles.VehiclesService', 'app.shared.VehicleService']
        constructor(private $state, private svc: app.vehicles.VehiclesService, private vehicle: app.shared.VehicleService) {
        }

        //Send vehicle information to server
        private Submit(): void {
            this.svc.UpdateVehicle(this.vehicle.VehicleSelected);
            this.$state.go('app.manvehicles');
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