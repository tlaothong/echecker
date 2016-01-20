module app.vehicles {
    'use strict';

    declare var Ionic;

    class VehicleListController {

        private modal: any;
        private user: any;
        private data: VehicleInformation[]

        static $inject = [
            '$state',
            'app.shared.VehicleService',
            'app.vehicles.VehiclesService']
        constructor(
            private $state,
            private vehicle: app.shared.VehicleService,
            private svc: app.vehicles.VehiclesService) {

            this.user = Ionic.User.current();

            if (this.user.id) {
                console.log('Connecting to server.')
                svc.GetVehicles().then((it) => {
                    console.log('Connection completed.');
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
            return this.data.filter(it=> it.StatusCode == 0);
        }

        //Get vehicle is ready to analysis (รอส่งวิเคราะห์)
        private DisplayVehichleReady(): VehicleInformation[] {
            if (this.data == null) return null;
            return this.data.filter(it=> it.StatusCode == 1);
        }

        //Get vehicle analysis comepleted (วิเคราะห์แล้ว)
        private DisplayVehichleCompleted(): VehicleInformation[] {
            if (this.data == null) return null;
            return this.data.filter(it=> it.StatusCode == 2);
        }
        
        //Set select vehicle to service
        private SelectVehicle(vehicleSelected: VehicleInformation) {
            this.vehicle.VehicleSelected = vehicleSelected;
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

            var notifyMessage: Array<string> = [];
            for (var item of alertData) {
                notifyMessage.push('ทะเบียน ' + item.PlateNumber + '\n');
                if (item.IsPBRActive) notifyMessage.push('> ครบกำหนดพรบ\n');
                if (item.IsDrivingLicenseActive) notifyMessage.push('> ครบกำหนดใบขับขี่\n');
                if (item.IsCheckActive) notifyMessage.push('> ครบกำหนดตรวจสภาพรถ\n');
                if (item.IsTaxActive) notifyMessage.push('> ครบกำหนดต่อภาษี\n');
                if (item.IsPayActive) notifyMessage.push('> ครบกำหนดผ่อนงวด\n');
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

        static $inject = ['$state', 'app.vehicles.VehiclesService']
        constructor(private $state, private svc: app.vehicles.VehiclesService) {
        }

        //Send vehicle information to server
        private Submit(): void {
            var user = Ionic.User.current();
            this.newVehicle.Email = user.id;
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