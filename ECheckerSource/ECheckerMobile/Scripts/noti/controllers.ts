module app.noti {
    'use strict';

    class NotificationController {
    
        static $inject = ['$state', 'app.noti.NotificationService', 'app.shared.VehicleService'];
        constructor(private $state, private svc: app.noti.NotificationService, private vehicle: app.shared.VehicleService) {
        }
        
        private Submit(): void {
            var x = this.vehicle.VehicleSelected.IsPBRActive;
            //this.svc.UpdateNotification(this.vehicle.VehicleSelected);
            this.$state.go('app.vehicle.status');
        }
    }

    angular
        .module('app.noti')
        .controller('app.noti.NotificationController', NotificationController);
}