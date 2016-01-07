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

    class VehicleEditController {
        static $inject = ['data', '$state']
        constructor(private data, private $state) {
        }

        private Submit(): void {
        
            //TODO: Process something...
            this.$state.go('app.manvehicles');
        }
    }

    class VehicleAddController {
        static $inject = ['$state']
        constructor(private $state) {
        }

        private Submit(): void {
        
            //TODO: Process something...
            this.$state.go('app.manvehicles');
        }
    }

    class VehicleStatusController {

        static $inject = ['data', 'app.shared.VehicleService'];
        constructor(private data, private vehicleSvc: app.shared.VehicleService) {
            vehicleSvc.VehiclesData = data;
        }
    }

    class ManageVehicleController {

        static $inject = ['data'];
        constructor(private data) {
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