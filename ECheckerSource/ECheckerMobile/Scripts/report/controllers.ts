module app.report {
    'use strict';

    class ReportController {
        static $inject = ['data', 'app.shared.VehicleService', 'app.shared.AmissDetailService']
        constructor(private data, private vehicle: app.shared.VehicleService, private amissed: app.shared.AmissDetailService) {
        }
        
        private SelectAmissedDeatil(amissed: AmissedInformation) {
            this.amissed.AmissedInfo = amissed;
        }

        private IsCar(): boolean {
            return this.vehicle.VehicleSelected.VehicleTypeId == 11;
        }
    }

    angular
        .module('app.report')
        .controller('app.report.ReportController', ReportController);
}