module app.vehicles {
    'use strict';

    class VehicleListController {

        static $inject = ['data']
        constructor(private data: VehicleInformation[]) {
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
    
    class VehicleStatusController {

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicleSvc: app.shared.VehicleService) {
            vehicleSvc.VehicleSelected = data;
        }
    }

    class ManageVehicleController {

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicle: app.shared.VehicleService) {
        }

        private SelectVehicle(vehicleSelected: VehicleInformation) {
            this.vehicle.VehicleSelected = vehicleSelected;
        }
    }

    angular
        .module('app.vehicles')
        .controller('app.vehicles.VehicleListController', VehicleListController)
        .controller('app.vehicles.VehicleEditController', VehicleEditController)
        .controller('app.vehicles.VehicleAddController', VehicleAddController)
        .controller('app.vehicles.VehicleStatusController', VehicleStatusController)
        .controller('app.vehicles.ManageVehicleController', ManageVehicleController);
}