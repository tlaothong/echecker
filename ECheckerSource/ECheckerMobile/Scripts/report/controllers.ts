module app.report {
    'use strict';

    class ReportController {
        static $inject = ['data', 'app.shared.VehicleService']
        constructor(private data, private vehicle: app.shared.VehicleService) {
        }
    }

    angular
        .module('app.report')
        .controller('app.report.ReportController', ReportController);
}